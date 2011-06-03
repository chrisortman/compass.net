using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Compass {
	public class CompassRuntime {
		
		
		public IEnumerable<string> ExecuteCommandLine(string workingDirectory,string commandline) {
			var locationOfDll = Assembly.GetExecutingAssembly().Location;
			var loader = new RubyPathLoader();
			var searchPaths =  loader.DiscoverGemPaths(Path.GetDirectoryName(locationOfDll));

			var arguments = new List<string>(commandline.Trim().Split(' '));
			arguments.Insert(0, workingDirectory);

			var setup = IronRuby.Ruby.CreateRubySetup();

			setup.Options.Add("SearchPaths", searchPaths);
			setup.Options.Add("Arguments", arguments.ToArray());

			var sr = new Microsoft.Scripting.Hosting.ScriptRuntimeSetup();
			sr.LanguageSetups.Add(setup);

			var runtime = IronRuby.Ruby.CreateRuntime(sr);

			var memoryStream = new MemoryStream();
			runtime.IO.SetOutput(memoryStream, Encoding.UTF8);

			var engine = runtime.GetEngineByFileExtension(".rb");

			var rubyProgramPath = Path.Combine(Path.GetDirectoryName(locationOfDll), "Program.rb");
			engine.ExecuteFile(rubyProgramPath);

			memoryStream.Seek(0, SeekOrigin.Begin);
			var reader = new StreamReader(runtime.IO.OutputStream);
			var text = reader.ReadToEnd().Split(new[] {Environment.NewLine}, StringSplitOptions.None);
			return text;

		}
	}
}
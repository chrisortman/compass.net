using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Scripting.Hosting;

namespace TestProject {
	class Program {
		static void Main(string[] args) {
			//Module1.execute_compass();
			ExecuteCompassCsharp();

			Console.WriteLine("DONE");
			Console.ReadLine();

		}

		private static void ExecuteCompassCsharp()
		{
			var ir_path =  "e:\\code\\nugetpackages\\ironruby\\lib";
			var compass_net_path = "e:\\Code\\compass_net\\src\\compass_net\\compass_net\\Program.rb";

			var setup = IronRuby.Ruby.CreateRubySetup();

			setup.Options.Add("SearchPaths", "E:\\Code\\compass_net\\tools\\compass-0.11.1\\lib");
			var sr = new Microsoft.Scripting.Hosting.ScriptRuntimeSetup();
			sr.LanguageSetups.Add(setup);

			var runtime = IronRuby.Ruby.CreateRuntime(sr);
			var memoryStream = new MemoryStream();
			runtime.IO.SetOutput(memoryStream, Encoding.UTF8);

			var engine = runtime.GetEngineByFileExtension(".rb");
			engine.ExecuteFile(compass_net_path);

			memoryStream.Seek(0, SeekOrigin.Begin);
			var reader = new StreamReader(runtime.IO.OutputStream);
			var text = reader.ReadToEnd();
			Console.WriteLine(text);
		}
	}
}

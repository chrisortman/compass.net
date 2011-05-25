using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Text;

namespace Compass.Commands {
	[Cmdlet(verbName:"Invoke",nounName:"Compass",DefaultParameterSetName = "Command")]
	public class InvokeCompassCommand : BaseCompassCommand {

		[Parameter(ParameterSetName = "Command",Mandatory = true,Position = 1,ValueFromPipeline = true,ValueFromPipelineByPropertyName = true)]
		public string Command { get; set; }

		protected override void ProcessRecord() {
			var ir_path =  "e:\\code\\nugetpackages\\ironruby\\lib";
			var compass_net_path = "e:\\Code\\compass_net\\src\\compass_net\\Program.rb";

			var arguments = new List<string>(Command.Trim().Split(' '));
			arguments.Insert(0, AppDomain.CurrentDomain.BaseDirectory);

			var setup = IronRuby.Ruby.CreateRubySetup();
			
			setup.Options.Add("SearchPaths", "E:\\Code\\compass_net\\tools\\compass-0.11.1\\lib");
			setup.Options.Add("Arguments", arguments.ToArray());

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
			WriteObject(text);
		}
	}
}
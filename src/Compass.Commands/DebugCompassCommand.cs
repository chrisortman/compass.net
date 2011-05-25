using System;
using System.Management.Automation;
using System.Reflection;

namespace Compass.Commands {
	[Cmdlet(verbName:"Debug",nounName:"Compass")]
	public class DebugCompassCommand : BaseCompassCommand{

		protected override void ProcessRecord() {
			WriteObject("Base Directory: " + AppDomain.CurrentDomain.BaseDirectory);
			WriteObject("Current Directory:" + System.IO.Directory.GetCurrentDirectory());
			WriteObject("Assembly Location:" + Assembly.GetExecutingAssembly().Location);
		}
	}
}
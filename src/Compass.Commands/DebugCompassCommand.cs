using System;
using System.Management.Automation;
using System.Reflection;
using NuGet.VisualStudio;

namespace Compass.Commands {
	[Cmdlet(verbName:"Debug",nounName:"Compass")]
	public class DebugCompassCommand : BaseCompassCommand{
			private ISolutionManager _solutionManager;
			public DebugCompassCommand() 
			: this(ServiceLocator.GetInstance<ISolutionManager>()) {}

		public DebugCompassCommand(ISolutionManager solutionManager) {
			_solutionManager = solutionManager;
		}
		protected override void ProcessRecord() {
			WriteObject("Base Directory: " + AppDomain.CurrentDomain.BaseDirectory);
			WriteObject("Current Directory:" + System.IO.Directory.GetCurrentDirectory());
			WriteObject("Assembly Location:" + Assembly.GetExecutingAssembly().Location);
			WriteObject("Project FullPath: " + _solutionManager.DefaultProject.GetFullPath());
		}
	}
}
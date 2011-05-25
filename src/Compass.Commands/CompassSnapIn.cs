using System.ComponentModel;
using System.Management.Automation;

namespace Compass.Commands
{
	[RunInstaller(true)]
	public class CompassSnapIn : PSSnapIn
	{
		public override string Description {
			get { return "Compass SnapIn"; }
		}

		public override string Name {
			get { return "Compass"; }
		}

		public override string Vendor {
			get { return "Chris Ortman"; }
		}
	}
}
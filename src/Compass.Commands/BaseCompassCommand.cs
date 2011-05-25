using System.Management.Automation;

namespace Compass.Commands
{
	public class BaseCompassCommand : Cmdlet
	{
		public void Execute()
		{
			BeginProcessing();
			ProcessRecord();
			EndProcessing();
		}
	}
}
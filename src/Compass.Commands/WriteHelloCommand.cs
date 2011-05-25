using System.Management.Automation;

namespace Compass.Commands
{
	[Cmdlet(verbName:"Write",nounName:"Hello",DefaultParameterSetName = "Name")]
	public class WriteHelloCommand : BaseCompassCommand
	{
		[Parameter(ParameterSetName = "Name",Mandatory = true,Position = 1,ValueFromPipeline = true,ValueFromPipelineByPropertyName = true)]
		public string Name { get; set; }

		protected override void ProcessRecord()
		{
			WriteObject("Hello " + Name);
		}
	}
}
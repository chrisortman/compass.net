using System.Linq;
using Compass.Commands;
using Xunit;
using CmdletTests.Helpers;
using Shouldly;

namespace CmdletTests
{
	public class SanityChecks
	{
		[Fact]
		public void CanExecuteWriteHello()
		{
			var cmdlet = new WriteHelloCommand() { Name = "Chris"};
			var results = cmdlet.GetResults<string>();

			results.First().ShouldBe("Hello Chris");
		}

		[Fact]
		public void CanGetCompassVersion() {
			var cmdlet = new InvokeCompassCommand() {Command = "version"};
			var results = cmdlet.GetResults<string>();

			results.First().ShouldContain("Compass 0.11.1 (Antares)");
		}
	}

	public class When_running_in_real_powershell : RunsInPowershellTest {
		
		[Fact]
		public void CanSayHello() {

			var psOutput = ExecuteCommands("Write-Hello -Name Chris");
			psOutput.ShouldBe("Hello Chris");
		}

		[Fact]
		public void Can_get_compass_version() {
			ExecuteCommands("Invoke-Compass -Command version").ShouldContain("Compass 0.11.1 (Antares)");
		}


	}
}
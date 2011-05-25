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
}
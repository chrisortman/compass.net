using System;
using System.IO;
using Compass;
using Shouldly;
using Xunit;

namespace CmdletTests {
	public class PathLoadingTests : IDisposable {
		private string _baseDirectory;

		public PathLoadingTests() {
			_baseDirectory = Path.Combine(Path.GetTempPath(), "PathLoadingTest");
			var di = new DirectoryInfo(_baseDirectory);
			di.CreateSubdirectory("compass-1.1\\lib");
			di.CreateSubdirectory("sass-1.2\\lib");
		}

		[Fact]
		public void DiscoversLibPaths() {
			var loader = new RubyPathLoader();
			var paths = loader.DiscoverGemPaths(_baseDirectory);
			paths.ShouldContain(Path.Combine(_baseDirectory,"compass-1.1\\lib"));
			paths.ShouldContain(Path.Combine(_baseDirectory,"sass-1.2\\lib"));
		}

		public void Dispose() {
			Directory.Delete(_baseDirectory, true);
		}
	}
}
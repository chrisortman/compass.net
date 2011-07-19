using System.Collections.Generic;
using System.IO;

namespace Compass {
	public class RubyPathLoader {
		
		public IEnumerable<string> DiscoverGemPaths(string toolsDirectory) {
			var di = new DirectoryInfo(toolsDirectory);
			foreach(var subdir in di.GetDirectories()) {
				var libDir = Path.Combine(subdir.FullName, "lib");
				if(Directory.Exists(libDir)) {
					yield return libDir;
				}
			}
		}

		public IEnumerable<string> DiscoverSassPaths(string toolsDirectory) {
			yield return Path.Combine(toolsDirectory, "compass-0.11.1", "frameworks", "blueprint", "stylesheets");
			yield return Path.Combine(toolsDirectory, "compass-0.11.1", "frameworks", "compass", "stylesheets");
		}
	}
}
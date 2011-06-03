using System.Collections.Generic;
using System.IO;

namespace Compass {
	public class RubyPathLoader {
		
		public IEnumerable<string> DiscoverGemPaths(string baseDirectory) {
			var di = new DirectoryInfo(baseDirectory);
			foreach(var subdir in di.GetDirectories()) {
				var libDir = Path.Combine(subdir.FullName, "lib");
				if(Directory.Exists(libDir)) {
					yield return libDir;
				}
			}
		}

		public IEnumerable<string> DiscoverSassPaths(string baseDirectory) {
			
		}
	}
}
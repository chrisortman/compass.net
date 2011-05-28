using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using EnvDTE;
using NuGet.VisualStudio;

namespace Compass.Commands {
	/// <summary>
	/// Simpler interface over the VS DTE project system
	/// </summary>
	public class VisualStudioProjectFacade {
		private readonly Project _project;

		public VisualStudioProjectFacade(Project project) {
			_project = project;
		}

		/// <summary>
		/// Tests the project to see if the item is
		/// included.
		/// </summary>
		/// <param name="itemPath">Accepts a path/to/item (file or directory)
		/// Path is assumed to be relative starting at the project leve.</param>
		/// <returns></returns>
		[Pure]
		public bool Includes(string itemPath) {

			itemPath = NormalizeItemPath(itemPath);
			
			//assume directory first
			var item = _project.GetProjectItems(itemPath);

			if(item == null) {
				//might be a file.
				var singleItem = _project.GetProjectItem(itemPath);
				return singleItem != null;
			}
			return true;
			
		}

		public void IncludeDirectory(string itemPath) {
			itemPath = NormalizeItemPath(itemPath);

			var projectDirectory = Path.GetDirectoryName(_project.FullName);

			if (projectDirectory == null) {
				throw new InvalidOperationException("Could not find project directory");
			}

			var absolutePathToItem = Path.Combine(projectDirectory, itemPath);

			if(!Directory.Exists(absolutePathToItem)) {
				throw new InvalidOperationException("Can not add a directory that does not exist.");
			}
			//support only the most naive of cases right now
			var parts = SplitPath(NormalizeItemPath(itemPath));
			var contentFolder = _project.GetProjectItems(parts[0]);
			contentFolder.AddFromDirectory(absolutePathToItem);
		}

		public void IncludeFile(string itemPath) {
			itemPath = NormalizeItemPath(itemPath);
			var projectDirectory = Path.GetDirectoryName(_project.FullName);

			if (projectDirectory == null) {
				throw new InvalidOperationException("Could not find project directory");
			}

			var absolutePathToItem = Path.Combine(projectDirectory, itemPath);

			if(!File.Exists(absolutePathToItem)) {
				throw new InvalidOperationException("Can not add a directory that does not exist.");
			}
			//support only the most naive of cases right now
			var parts = SplitPath(NormalizeItemPath(itemPath));
			var contentFolder = _project.GetProjectItems(parts[0]);
			contentFolder.AddFromFile(absolutePathToItem);
		}

		[Pure]
		private static string NormalizeItemPath(string itemPath) {
			Contract.Ensures(itemPath.Contains('/') == false);

			return itemPath.Replace('/', '\\');
		}

		[Pure]
		private static string[] SplitPath(string itemPath) {
			Contract.Requires(itemPath.Contains('/') == false);

			return itemPath.Split('\\');
		}
	}
}
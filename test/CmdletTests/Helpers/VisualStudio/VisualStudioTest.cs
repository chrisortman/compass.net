using System;
using System.Diagnostics.Contracts;
using EnvDTE;

namespace CmdletTests.Helpers.VisualStudio {
	public abstract class VisualStudioTest : IDisposable {
		protected DTE Ide { get; private set; }
		protected Project SampleProject { get; private set; }

		protected VisualStudioTest() {

			// http://stackoverflow.com/questions/5259712/how-to-unit-test-visual-studio-addin-interacting-with-vs-dom

			Type type = System.Type.GetTypeFromProgID("VisualStudio.DTE.10.0");
			object inst = System.Activator.CreateInstance(type, true);
			Ide = (DTE) inst;

			MessageFilter.Register();

			Ide.Solution.Open(@"E:\Code\compass_net\test\VisualStudioProjectForTesting\CompassTestWebApp.sln");

			Project webProject = null;
			foreach (var project in Ide.Solution.Projects) {
				webProject = (Project) project;
				break;
			}

			Contract.Assert(webProject != null);
			SampleProject = webProject;
		
		}

		public void Dispose() {
			if (Ide != null) {
				Ide.Quit();
			}
			MessageFilter.Revoke();
		}
	}
}
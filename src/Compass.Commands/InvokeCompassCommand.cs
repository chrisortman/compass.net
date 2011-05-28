using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using EnvDTE;
using NuGet.VisualStudio;

namespace Compass.Commands {
	public class NullSolutionManager : ISolutionManager {
		public Project GetProject(string projectSafeName) {
			throw new NotImplementedException();
		}

		public IEnumerable<Project> GetProjects() {
			throw new NotImplementedException();
		}

		public string GetProjectSafeName(Project project) {
			throw new NotImplementedException();
		}

		public IEnumerable<Project> GetDependentProjects(Project project) {
			throw new NotImplementedException();
		}

		public string SolutionDirectory {
			get { throw new NotImplementedException(); }
		}

		public string DefaultProjectName {
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public Project DefaultProject {
			get { return new NullProject(); }
		}

		public bool IsSolutionOpen {
			get { throw new NotImplementedException(); }
		}

		public event EventHandler SolutionOpened;
		public event EventHandler SolutionClosing;
		public event EventHandler SolutionClosed;
	}

	public class NullProject : Project {
		public void SaveAs(string NewFileName) {
			throw new NotImplementedException();
		}

		public void Save(string FileName) {
			throw new NotImplementedException();
		}

		public void Delete() {
			throw new NotImplementedException();
		}

		public string Name {
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public string FileName {
			get { throw new NotImplementedException(); }
		}

		public bool IsDirty {
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public Projects Collection {
			get { throw new NotImplementedException(); }
		}

		public DTE DTE {
			get { throw new NotImplementedException(); }
		}

		public string Kind {
			get { throw new NotImplementedException(); }
		}

		public ProjectItems ProjectItems {
			get { throw new NotImplementedException(); }
		}

		public Properties Properties {
			get { throw new NotImplementedException(); }
		}

		public string UniqueName {
			get { throw new NotImplementedException(); }
		}

		public object Object {
			get { throw new NotImplementedException(); }
		}

		public object get_Extender(string ExtenderName) {
			throw new NotImplementedException();
		}

		public object ExtenderNames {
			get { throw new NotImplementedException(); }
		}

		public string ExtenderCATID {
			get { throw new NotImplementedException(); }
		}

		public string FullName {
			get { return @"E:\Code\compass_net\test\VisualStudioProjectForTesting\CompassTestWebApp\CompassTestWebApp.csproj"; }
		}

		public bool Saved {
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public ConfigurationManager ConfigurationManager {
			get { throw new NotImplementedException(); }
		}

		public Globals Globals {
			get { throw new NotImplementedException(); }
		}

		public ProjectItem ParentProjectItem {
			get { throw new NotImplementedException(); }
		}

		public CodeModel CodeModel {
			get { throw new NotImplementedException(); }
		}
	}

	[Cmdlet(verbName: "Invoke", nounName: "Compass", DefaultParameterSetName = "Command")]
	public class InvokeCompassCommand : BaseCompassCommand {
		private ISolutionManager _solutionManager;

		[Parameter(ParameterSetName = "Command", Mandatory = true, Position = 1, ValueFromPipeline = true,
			ValueFromPipelineByPropertyName = true)]
		public string Command { get; set; }

		public InvokeCompassCommand() {
			try {
				_solutionManager = ServiceLocator.GetInstance<ISolutionManager>();
			} catch(NullReferenceException) {
				_solutionManager = new NullSolutionManager();
			}
			
		}

		protected override void ProcessRecord() {
			var locationOfDll = Assembly.GetExecutingAssembly().Location;

			var rubyProgramPath = Path.Combine(Path.GetDirectoryName(locationOfDll), "Program.rb");
			var searchPaths = Path.Combine(Path.GetDirectoryName(locationOfDll), "compass-0.11.1\\lib");

			var arguments = new List<string>(Command.Trim().Split(' '));
			var currentProject = _solutionManager.DefaultProject;
			arguments.Insert(0, Path.Combine(Path.GetDirectoryName(currentProject.FullName), "Content"));

			var setup = IronRuby.Ruby.CreateRubySetup();

			setup.Options.Add("SearchPaths", searchPaths);
			setup.Options.Add("Arguments", arguments.ToArray());

			var sr = new Microsoft.Scripting.Hosting.ScriptRuntimeSetup();
			sr.LanguageSetups.Add(setup);

			var runtime = IronRuby.Ruby.CreateRuntime(sr);

			var memoryStream = new MemoryStream();
			runtime.IO.SetOutput(memoryStream, Encoding.UTF8);

			var engine = runtime.GetEngineByFileExtension(".rb");
			engine.ExecuteFile(rubyProgramPath);

			memoryStream.Seek(0, SeekOrigin.Begin);
			var reader = new StreamReader(runtime.IO.OutputStream);
			var text = reader.ReadToEnd().Split(new[] {Environment.NewLine}, StringSplitOptions.None);

			var project = new VisualStudioProjectFacade(currentProject);

			foreach (var line in text) {
				if (line.Trim().StartsWith("directory")) {
					var directory = "Content/" + line.Split(' ')[1];
					if (!project.Includes(directory)) {
						project.IncludeDirectory( directory);
					}

				} else if (line.Trim().StartsWith("create")) {
					var filePath = "Content/" + line.Trim().Split(' ')[1];
					if(!project.Includes(filePath)) {
						project.IncludeFile( filePath);
					}
				} else if (line.Trim().StartsWith("identical")) {
					var filePath = "Content/" + line.Trim().Split(' ')[1];
					if (Path.HasExtension(filePath)) {
						if (!project.Includes(filePath)) {
							project.IncludeFile( filePath);
						}
					} else {
						if (!project.Includes(filePath)) {
							project.IncludeDirectory(filePath);
						}
					}
				}
			}
			WriteObject(text);
		}
	}
}
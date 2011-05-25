using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace CmdletTests.Helpers {
	public class RunsInPowershellTest {
		
		protected string ExecuteCommands(params string[] commands) {
			Runspace runspace = RunspaceFactory.CreateRunspace();

			// open it

			runspace.Open();
			PSSnapInException snapInWarnings;
			//	runspace.RunspaceConfiguration.AddPSSnapIn("Compass", out snapInWarnings);
			var dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Compass.Commands.dll");


			// create a pipeline and feed it the script text

			Pipeline pipeline = runspace.CreatePipeline();
			pipeline.Commands.AddScript("Import-module '" + dllPath + "'");
			foreach(var command in commands) {
				pipeline.Commands.AddScript(command);
			}

			// add an extra command to transform the script
			// output objects into nicely formatted strings

			// remove this line to get the actual objects
			// that the script returns. For example, the script

			// "Get-Process" returns a collection
			// of System.Diagnostics.Process instances.

			pipeline.Commands.Add("Out-String");

			// execute the script

			Collection<PSObject> results = pipeline.Invoke();

			// close the runspace

			runspace.Close();

			// convert the script result into a single string

			StringBuilder stringBuilder = new StringBuilder();
			foreach (PSObject obj in results)
			{
				stringBuilder.AppendLine(obj.ToString());
			}

			var psOutput = stringBuilder.ToString().Trim();
			return psOutput;
		}
	}
}
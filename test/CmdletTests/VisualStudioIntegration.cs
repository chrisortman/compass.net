using System;
using System.Collections.Generic;
using CmdletTests.Helpers.VisualStudio;
using Compass.Commands;
using Xunit;
using Shouldly;

namespace CmdletTests {
	public class VisualStudioIntegration : VisualStudioTest, IDisposable {
	
		[Fact]
		public void Can_include_a_folder_into_existing_project() {
		
			var project = new VisualStudioProjectFacade(SampleProject);
			project.Includes("Content/Images").ShouldBe(false);
			project.IncludeDirectory("Content/Images");
			project.Includes("Content/Images").ShouldBe(true);
					
		}

		[Fact]
		public void Can_check_for_path_that_does_not_exist() {
			var project = new VisualStudioProjectFacade(SampleProject);
			project.Includes("Content/IDontExist").ShouldBe(false);
		}

		[Fact]
		public void Including_a_folder_that_does_not_exist_should_throw() {
			var project = new VisualStudioProjectFacade(SampleProject);
			
			Should.Throw<InvalidOperationException>(() =>
			                                        project.IncludeDirectory("Content/IDontExist")
				);
			

		}

		[Fact]
		public void Can_check_if_a_file_exists_in_project() {
			var project = new VisualStudioProjectFacade(SampleProject);
			project.Includes("Content/Site.css").ShouldBe(true);
			project.Includes("Global.asax").ShouldBe(true);
			project.Includes("BLAH_BLAH.txt").ShouldBe(false);
			project.Includes("Malware").ShouldBe(false);
		}

		[Fact]
		public void Can_add_existing_file() {
			var project = new VisualStudioProjectFacade(SampleProject);
			project.Includes("Content/config.rb").ShouldBe(false);
			project.IncludeFile("Content/config.rb");
			project.Includes("Content/config.rb").ShouldBe(true);
		}


	}
}
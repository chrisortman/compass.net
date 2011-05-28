using Xunit;

namespace CmdletTests {
	public class CompassOutput {
		[Fact]
		public void Detects_file_change_on_clean_run() {
			string compassOutput = @"
directory images/
directory sass/
directory sass/partials/
directory stylesheets/
   create config.rb 
   create sass/screen.scss 
   create sass/partials/_base.scss 
   create sass/partials/_form.scss 
   create sass/partials/_page.scss 
   create sass/partials/_two_col.scss 
   create sass/print.scss 
   create sass/ie.scss 
   create images/grid.png 
   create stylesheets/ie.css 
   create stylesheets/print.css 
   create stylesheets/screen.css 

*********************************************************************
Congratulations! Your compass project has been created.

You may now add and edit sass stylesheets in the sass subdirectory of your project.

Sass files beginning with an underscore are called partials and won't be
compiled to CSS, but they can be imported into other sass stylesheets.

You can configure your project by editing the config.rb configuration file.

You must compile your sass stylesheets into CSS when they change.
This can be done in one of the following ways:
  1. To compile on demand:
     compass compile [path/to/project]
  2. To monitor your project for changes and automatically recompile:
     compass watch [path/to/project]

More Resources:
  * Website: http://compass-style.org/
  * Sass: http://sass-lang.com
  * Community: http://groups.google.com/group/compass-users/


Please see the blueprint website for documentation on how blueprint works:

    http://blueprintcss.org/

Docs on the compass port of blueprint can be found on the wiki:

    http://wiki.github.com/chriseppstein/compass/blueprint-documentation

To get started, edit the screen.sass file and read the comments and code there.


";
		}

		[Fact]
		public void Detects_file_changes_when_all_directories_exist_but_missing_root_file() {
			string compassOutput =
				@"   create config.rb 
identical sass/screen.scss 
identical sass/partials/_base.scss 
identical sass/partials/_form.scss 
identical sass/partials/_page.scss 
identical sass/partials/_two_col.scss 
identical sass/print.scss 
identical sass/ie.scss 
identical images/grid.png 
identical stylesheets/ie.css 
identical stylesheets/print.css 
identical stylesheets/screen.css 

*********************************************************************
Congratulations! Your compass project has been created.

You may now add and edit sass stylesheets in the sass subdirectory of your project.

Sass files beginning with an underscore are called partials and won't be
compiled to CSS, but they can be imported into other sass stylesheets.

You can configure your project by editing the config.rb configuration file.

You must compile your sass stylesheets into CSS when they change.
This can be done in one of the following ways:
  1. To compile on demand:
     compass compile [path/to/project]
  2. To monitor your project for changes and automatically recompile:
     compass watch [path/to/project]

More Resources:
  * Website: http://compass-style.org/
  * Sass: http://sass-lang.com
  * Community: http://groups.google.com/group/compass-users/


Please see the blueprint website for documentation on how blueprint works:

    http://blueprintcss.org/

Docs on the compass port of blueprint can be found on the wiki:

    http://wiki.github.com/chriseppstein/compass/blueprint-documentation

To get started, edit the screen.sass file and read the comments and code there.
";
		}

		[Fact]
		public void Detects_file_changes_when_missing_sass_folder() {
			string compassOutput = @"
directory sass/
directory sass/partials/
   create sass/screen.scss 
   create sass/partials/_base.scss 
   create sass/partials/_form.scss 
   create sass/partials/_page.scss 
   create sass/partials/_two_col.scss 
   create sass/print.scss 
   create sass/ie.scss 
identical images/grid.png 
identical stylesheets/ie.css 
identical stylesheets/print.css 
identical stylesheets/screen.css 

*********************************************************************
Congratulations! Your compass project has been created.

You may now add and edit sass stylesheets in the sass subdirectory of your project.

Sass files beginning with an underscore are called partials and won't be
compiled to CSS, but they can be imported into other sass stylesheets.

You can configure your project by editing the config.rb configuration file.

You must compile your sass stylesheets into CSS when they change.
This can be done in one of the following ways:
  1. To compile on demand:
     compass compile [path/to/project]
  2. To monitor your project for changes and automatically recompile:
     compass watch [path/to/project]

More Resources:
  * Website: http://compass-style.org/
  * Sass: http://sass-lang.com
  * Community: http://groups.google.com/group/compass-users/


Please see the blueprint website for documentation on how blueprint works:

    http://blueprintcss.org/

Docs on the compass port of blueprint can be found on the wiki:

    http://wiki.github.com/chriseppstein/compass/blueprint-documentation

To get started, edit the screen.sass file and read the comments and code there.
";
		}
	}
}
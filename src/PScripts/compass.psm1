	#loads the iron ruby assemblies
	# .NET 4.0
# http://stackoverflow.com/questions/2094694/launch-powershell-under-net-4
#

function Invoke-Compass 
{
	param($command = 'version')
		


    $irPath = $Variable:compass_ironruby_path
    $compass_net_path = $Variable:compass_program_path
    $project = (Get-Project)
    $compass_install_path = (Split-Path -Parent $project.Fullname | Join-Path -ChildPath "Content")
	$rubyLanguage = [IronRuby.Ruby]::CreateRubySetup();
    $searchPaths = (Split-Path -Parent $compass_net_path | Join-Path -Childpath "compass-0.11.1" | Join-Path -ChildPath  "lib")
	$rubyLanguage.Options.Add("SearchPaths",$searchPaths)
	$rubyLanguage.Options.Add("Arguments", ($compass_install_path + ' ' + $command).Split(' '))
	
	$scriptRuntimeSetup = new-object -type Microsoft.Scripting.Hosting.ScriptRuntimeSetup
	$scriptRuntimeSetup.LanguageSetups.Add($rubyLanguage)
	

	$rubyRuntime = [IronRuby.Ruby]::CreateRuntime($scriptRuntimeSetup);
	$ms = New-Object System.IO.MemoryStream
	$rubyRuntime.IO.SetOutput($ms,[System.Text.Encoding]::UTF8)
	
	$rubyEngine = $rubyRuntime.GetEngineByFileExtension("rb");

	$rubyEngine.ExecuteFile($compass_net_path)

	$ms.Seek(0,[System.IO.SeekOrigin]::Begin)
	$streamReader = New-Object -TypeName System.IO.StreamReader -ArgumentList $ms
	$text = $streamReader.ReadToEnd()

	Write-Host $text
}

function Initialize-Compass 
{
 	param($Template = 'blueprint/semantic', $SassDir = 'sass', $CssDir = 'css', $JavascriptsDir = 'javascripts', $ImagesDir = 'images', $ExtraArgs = '' )
    $compass_args = "init --using $Template --sass-dir $SassDir --css-dir $CssDir --javascripts-dir $JavascriptsDir --images-dir $ImagesDir $ExtraArgs"
    Write-Host $compass_args
    Invoke-Compass -Command $compass_args
}
 
Export-ModuleMember Invoke-Compass
Export-ModuleMember Initialize-Compass

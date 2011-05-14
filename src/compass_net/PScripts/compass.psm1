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
	$rubyLanguage.Options.Add("SearchPaths","E:\Code\compass_net\tools\compass-0.11.1\lib")
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
 	param($template = 'blueprint/semantic')
 
 	Invoke-Compass -Command "init --using blueprint/semantic --sass-dir 'sass' --css-dir 'css' --javascripts-dir 'javascripts' --images-dir 'images'"
}
 
Export-ModuleMember Invoke-Compass
Export-ModuleMember Initialize-Compass
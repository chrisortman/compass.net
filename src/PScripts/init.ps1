param($installPath = "e:\Code\compass_net\src\compass_net\packages\compass.net.0.0.1", $toolsPath = "e:\Code\compass_net\src\compass_net\packages\compass.net.0.0.1\tools", $package = '')

#compass needs your home environment variable set
$env:HOME = (Join-Path -Path $env:HOMEDRIVE $env:HOMEPATH)

#
#load up the iron ruby assemblies
#http://stackoverflow.com/questions/2094694/launch-powershell-under-net-4
#
$packagePath = (Split-Path -Parent $installPath)
$rubyDir = Get-ChildItem -Path "$packagePath\IronRuby*" -Name
$irPath =  Join-Path -Path $packagePath -ChildPath $rubyDir | Join-Path -ChildPath "Lib"
Get-ChildItem -Path $irPath -Include *.dll -recurse | %{ Add-Type -Path $_; Write-Host "Added types from $_" }
 $compass_net_path = (Join-Path -Path $toolsPath -ChildPath "Program.rb")
 
function global:Print-Stuff 
{
	Write-Host $irPath
}

function global:Invoke-Compass 
{
	param($command = 'version')
			
    $project = (Get-Project)
    $compass_install_path = (Split-Path -Parent $project.Fullname | Join-Path -ChildPath "Content")
	
	$rubyLanguage = [IronRuby.Ruby]::CreateRubySetup();
    $searchPaths = (Split-Path -Parent $compass_net_path | Join-Path -Childpath "compass-0.11.1" | Join-Path -ChildPath  "lib")
	$rubyLanguage.Options.Add("SearchPaths",$searchPaths)
	$rubyLanguage.Options.Add("Arguments", ((get-location).Path + ' ' + $command).Trim().Split(' '))
	
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

function global:Initialize-Compass 
{
 	param($Template = 'blueprint/semantic', $SassDir = 'sass', $CssDir = 'css', $JavascriptsDir = 'javascripts', $ImagesDir = 'images', $ExtraArgs = '' )
    $compass_args = "init --using $Template --sass-dir $SassDir --css-dir $CssDir --javascripts-dir $JavascriptsDir --images-dir $ImagesDir $ExtraArgs"
 	#Invoke-Compass -Command "init --using blueprint/semantic --sass-dir 'sass' --css-dir 'css' --javascripts-dir 'javascripts' --images-dir 'images'"
    Write-Host $compass_args
    Invoke-Compass -Command $compass_args
}
 

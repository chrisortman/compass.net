	#loads the iron ruby assemblies
	# .NET 4.0
# http://stackoverflow.com/questions/2094694/launch-powershell-under-net-4
#

function Invoke-Compass 
{
	param($command = 'version')
	
	$ir_path = "e:\code\nugetpackages\ironruby\lib"
	$compass_net_path = "e:\Code\compass_net\src\compass_net\compass_net\Program.rb";
	$args = [String[]] @( $command )
	Get-ChildItem -Path $ir_path -Include *.dll -recurse | %{ Add-Type -Path $_ }

	$rubyLanguage = [IronRuby.Ruby]::CreateRubySetup();
	$rubyLanguage.Options.Add("SearchPaths","E:\Code\compass_net\tools\compass-0.11.1\lib")
	$rubyLanguage.Options.Add("Arguments",$args)
	
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

Invoke-Compass -command 'help'

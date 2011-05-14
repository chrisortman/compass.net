param($installPath, $toolsPath, $package, $project)

$packagePath = (Split-Path -Parent $installPath)
$rubyDir = Get-ChildItem -Path "$packagePath\IronRuby*" -Name
$irPath = Join-Path -Path $packagePath -ChildPath $rubyDir | Join-Path -ChildPath "Lib"
	
$compass_net_path = (Join-Path -Path $toolsPath -ChildPath "Program.rb")



Set-Variable -Scope Global -Name 'compass_ironruby_path' -Value $irPath
Set-Variable -Scope Global -Name 'compass_program_path' -Value $compass_net_path
Set-Variable -Scope Global -Name 'compass_install_path' -Value $compass_install_path
$env:HOME = (Join-Path -Path $env:HOMEDRIVE $env:HOMEPATH)
Import-Module (Join-Path $toolsPath compass.psm1)
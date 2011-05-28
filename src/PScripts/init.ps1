param($installPath = "e:\Code\compass_net\src\compass_net\packages\compass.net.0.0.1", $toolsPath = "e:\Code\compass_net\src\compass_net\packages\compass.net.0.0.1\tools", $package = '')

#compass needs your home environment variable set
$env:HOME = (Join-Path -Path $env:HOMEDRIVE $env:HOMEPATH)

 Import-Module -Name (Join-Path -Path $toolsPath -ChildPath "Compass.Commands.dll")
 Import-Module (Join-Path $toolsPath compass.psm1)
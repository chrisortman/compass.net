	#loads the iron ruby assemblies
	# .NET 4.0
# http://stackoverflow.com/questions/2094694/launch-powershell-under-net-4
#

function Initialize-Compass 
{
 	param($Template = 'blueprint/semantic', $SassDir = 'sass', $CssDir = 'css', $JavascriptsDir = 'javascripts', $ImagesDir = 'images', $ExtraArgs = '' )
    $compass_args = "init --using $Template --sass-dir $SassDir --css-dir $CssDir --javascripts-dir $JavascriptsDir --images-dir $ImagesDir $ExtraArgs"
    Write-Host $compass_args
    Invoke-Compass -Command $compass_args
}
 
Export-ModuleMember Initialize-Compass

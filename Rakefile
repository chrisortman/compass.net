require 'rake'

task :unpack_compass do
  Dir.chdir 'tools'
  %w( compass chunky_png fssm sass ).each do |lib|
    sh "igem unpack #{lib}"
  end
  Dir.chdir '..'
end

task :local_nuget do
  rm 'compass.net.0.0.1.nupkg'
  sh 'nuget pack compass.net.nuspec'
  cp 'compass.net.0.0.1.nupkg', 'e:\\mynugetpackages'
end


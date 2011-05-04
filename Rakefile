require 'rake'

task :unpack_compass do
  Dir.chdir 'tools'
  %w( compass chunky_png fssm sass ).each do |lib|
    sh "igem unpack #{lib}"
  end
  Dir.chdir '..'
end

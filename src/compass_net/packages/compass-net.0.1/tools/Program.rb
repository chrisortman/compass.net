require 'compass'
require 'compass/exec'

runner = Proc.new do
    Dir.chdir ARGV.shift
    Compass::Exec::SubCommandUI.new(ARGV).run!
end

runner.call

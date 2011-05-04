require 'compass'
require 'compass/exec'

runner = Proc.new do
    Compass::Exec::SubCommandUI.new(ARGV).run!
end

runner.call

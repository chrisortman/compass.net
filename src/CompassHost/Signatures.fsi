module Cmdlets

open System.Management.Automation

type WriteHelloCommand = class
    inherit Cmdlet
    new: unit -> WriteHelloCommand
    member Name : System.String with set
end

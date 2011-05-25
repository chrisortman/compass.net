module Cmdlets

open System
open System.Management.Automation


[<Cmdlet("Write","Hello",DefaultParameterSetName="Name")>]
type WriteHelloCommand = class
    inherit Cmdlet

    val mutable name : String

    new() = {name = "Chris";}

    [<Parameter(ParameterSetName = "Name",Mandatory = true, Position = 1, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)>]
    member x.Name
        with get () = x.name
        and set p = x.name <- p

    override x.ProcessRecord() =
      x.WriteObject("Hello " + x.name)
end


let GetCommand =
    new WriteHelloCommand()



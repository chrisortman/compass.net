// Learn more about F# at http://fsharp.net
module CompassHost

let ir_path =  "e:\\code\\nugetpackages\\ironruby\\lib";
let compass_net_path = "e:\\Code\\compass_net\\src\\compass_net\\compass_net\\Program.rb";

let parse_command_line(commandLine:string) : string[] = 
    commandLine.Split(' ')

let execute_compass(commandLine:string) =    
    
    let setup = IronRuby.Ruby.CreateRubySetup()
    setup.Options.Add("SearchPaths","E:\\Code\\compass_net\\tools\\compass-0.11.1\\lib");
    setup.Options.Add("Arguments", parse_command_line(commandLine));
    
    let scriptRuntimeSetup = new Microsoft.Scripting.Hosting.ScriptRuntimeSetup()
    scriptRuntimeSetup.LanguageSetups.Add(setup)
   
    let rubyRuntime = 
        IronRuby.Ruby.CreateRuntime(scriptRuntimeSetup)

    let rubyEngine = rubyRuntime.GetEngineByFileExtension("rb")
    rubyEngine.ExecuteFile(compass_net_path) |> ignore
    


    
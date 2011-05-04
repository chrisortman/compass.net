// Learn more about F# at http://fsharp.net
module CompassHost

let ir_path =  "e:\\code\\nugetpackages\\ironruby\\lib";
let compass_net_path = "e:\\Code\\compass_net\\src\\compass_net\\compass_net\\Program.rb";

let rubyLanguage = 
    let setup = IronRuby.Ruby.CreateRubySetup();
    setup.Options.Add("SearchPaths","E:\\Code\\compass_net\\tools\\compass-0.11.1\\lib");
    setup

let scriptRuntimeSetup = 
    let sr = new Microsoft.Scripting.Hosting.ScriptRuntimeSetup()
    sr.LanguageSetups.Add(rubyLanguage)
    sr

let rubyRuntime = 
    IronRuby.Ruby.CreateRuntime(scriptRuntimeSetup)

let execute_compass() =     
    let rubyEngine = rubyRuntime.GetEngineByFileExtension("rb")
    rubyEngine.ExecuteFile(compass_net_path) |> ignore
    


    
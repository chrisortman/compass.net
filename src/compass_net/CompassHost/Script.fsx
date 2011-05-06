// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.

let ensure_array commandLine =     
   match commandLine with
   | :? string as s -> printfn "string"


ensure_array [|"Hello";"World"|]
ensure_array "Hello"


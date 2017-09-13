// Learn more about F# at http://fsharp.org

open System
open System.IO
open System.Diagnostics

let (|DriveLetter|) (input:string) =
    if (input.Length > 2 && input.[1] = ':') then
        Some(input)
    else
        None

let (|StartsWith|_|) (start:char) (input:string) =
    if (input.Length > 1 && input.[1] = start) then
        Some(input)
    else
        None

let isRelative (path:string) =
    match path with
    | StartsWith '.' path -> true
    | StartsWith '/' path -> false
    | DriveLetter path -> false
    | _ -> false 

let fullPath (path:string) = 
    match (isRelative path) with
    | false -> path
    | true -> Path.Combine(Directory.GetCurrentDirectory(), path)

let getVersion (path:string) =
    let path' = fullPath path
    let info = FileVersionInfo.GetVersionInfo(path)
    printfn "%A" info
    
[<EntryPoint>]
let main argv =
    getVersion (argv.[0])
    0 // return an integer exit code

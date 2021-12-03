#load "./Source.fsx"

open Source
open System.IO

let readInput filePath =
    File.ReadAllLines filePath |> Array.toList

//part one answer
let result =
    readInput "./input.txt" |> getReportNumbers

printfn "Day 3 - Part 1 Answer is %i" result

let result2 =
    readInput "./input.txt" |> getLifeSupportNumbers

printfn "Day 3 - Part 2 Answer is %i" result2

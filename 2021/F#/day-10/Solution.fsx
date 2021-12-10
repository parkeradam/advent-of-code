#load "./Source.fsx"

open Source
open System.IO

let input =
    File.ReadLines("./input.txt") |> Seq.toList


let result = calculateInvalidScore input
printfn "Day 10 - Part 1 : Result is %i" result

let result2 = calculateAutoCompleteScore input

printfn "Day 10 - Part 2 : Result is %i" result2

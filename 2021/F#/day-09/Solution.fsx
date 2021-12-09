#load "./Source.fsx"

open Source
open System.IO

let input =
    File.ReadAllLines("./input.txt") |> Seq.toList

let result = getRiskLevel input

printfn "Day 9 - Part 1 : Result is %i" result

let result2 = getBasinSizes input
printfn "Day 9 - Part 2 : Result is %i" result2

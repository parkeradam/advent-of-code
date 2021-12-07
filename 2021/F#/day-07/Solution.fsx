#load "./Source.fsx"

open Source
open System.IO

let input =
    File.ReadAllText("./input.txt")
    |> (fun x -> x.Split(','))
    |> Seq.map int
    |> Seq.toList

let result = calculateFuelSimple input

printfn "Day 7 - Part 1 : Result is %i" result

let result2 = calculateFuelComplex input
printfn "Day 7 - Part 2 : Result is %i" result2

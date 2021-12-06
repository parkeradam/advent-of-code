#load "./Source.fsx"

open Source
open System.IO

let input =
    File.ReadAllText("./input.txt")
    |> (fun x -> x.Split(','))
    |> Seq.toList
    |> List.map int

let result1 = countFish input 80
printfn "Day 6 - Part 1 : Result is %i" result1

let result2 = countFish2 input 256
printfn "Day 6 - Part 2 : Result is %i" result2

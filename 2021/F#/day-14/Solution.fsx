#load "./Source.fsx"

open Source
open System.IO

let input =
    File.ReadAllLines("./input.txt") |> List.ofArray

let result = expandAndCount input 10
printfn "Day 14 - Part 1 : Result = %i" result


let result2 = expandAndCount input 40
printfn "Day 14 - Part 2 : Result = %i" result2

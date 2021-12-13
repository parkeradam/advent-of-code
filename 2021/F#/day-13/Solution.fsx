#load "Source.fsx"

open Source
open System.IO

let input =
    File.ReadAllLines("./input.txt") |> Array.toList

let result = countNumberHoles input
printfn "Day 13 - Part 1 : Result is %i" result

let result2 = findCode input
File.WriteAllLines("./Part2.txt", result2)

#load "Source.fsx"

open Source
open System.IO

let readInput filePath =
    File.ReadAllLines filePath |> Array.toList


let result =
    readInput "./input.txt" |> getWinnersNumber

printfn "Day 4 - Part 1 Answer is %i" result


let result2 =
    readInput "./input.txt" |> getLastWinnersNumbers

printfn "Day 4 - Part 2 Answer is %i" result2

#load "./Source.fsx"

open Source
open System.IO

let input =
    File.ReadAllLines("./input.txt") |> Array.toList

let result = chekUniqueDigits input

printfn "Day 8 - Part 1 : Answer is %i" result

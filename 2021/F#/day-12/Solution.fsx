#load "Source.fsx"

open Source
open System.IO

let input = File.ReadAllLines("./input.txt")

let result = calculatePaths input false
printfn "Day 12 - Part 1 : Result is %i" result

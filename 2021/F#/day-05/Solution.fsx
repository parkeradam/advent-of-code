#load "./Source.fsx"

open Source
open System.IO

let readInput filePath = File.ReadLines(filePath) |> Seq.toList

let result1 =
    readInput "./input.txt"
    |> getOverlappingLinesNoDiag

printfn "Day5 - Part 1 result = %i" result1


let result2 =
    readInput "./input.txt" |> getOverlappingLinesAll


printfn "Day5 - Part 1 result = %i" result2

#load "./Source.fsx"

open Source
open System.IO

let readInput filePath =
    File.ReadAllLines filePath |> Array.toList

//part one answer
let result =
    readInput "./input.txt" |> getPositionPartOne

printfn "Day 2 - Part 1 Answer is %i" (result.Depth * result.Horizontal)


//part 2 answer
let result2 =
    readInput "./input.txt" |> getPositionPartTwo

printfn "Day 2 - Part 2 Answer is %i" (result2.Depth * result2.Horizontal)

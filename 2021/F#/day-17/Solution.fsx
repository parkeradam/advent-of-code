#load "Source.fsx"
open Source

let input = "target area: x=179..201, y=-109..-63"

let result = calculateHeight input
printfn "Day 17 - Part 1 : Result is %A" result

let result2 = calculateNumAllPaths input
printfn "Day 17 - Part 2 : Result is %A" result2

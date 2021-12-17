#load "./Source.fsx"
open Source

let sampleInput = "target area: x=20..30, y=-10..-5"

let testResult1 = calculateHeight sampleInput
printfn "Test 1 : Given sample input, should find height of 45 : %i" testResult1

let testResult2 = calculateNumAllPaths sampleInput
printfn "Test 2 : Given sample input, should find total of 112 paths : %i" testResult2

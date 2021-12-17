#load "./Source.fsx"
open Source

let sampleInput = "target area: x=20..30, y=-10..-5"

let testResult1 = calculateHeight sampleInput
printfn "Test 1 : Given sampel input, should find height of 45 : %i" testResult1

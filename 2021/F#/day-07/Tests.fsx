#load "./Source.fsx"
open Source

let sampleInput = [ 16; 1; 2; 0; 4; 2; 7; 1; 2; 14 ]

let testResult1 = calculateFuelSimple sampleInput

printfn "Test 1 : Given smaple input, fuel cost should be 37 : %i" testResult1

let testResult2 = calculateFuelComplex sampleInput
printfn "Test 2 : Given sample input, fuel cost should be 168 : %i" testResult2

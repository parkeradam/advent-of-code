#load "./Source.fsx"
open Source

let sampleState = [ 3; 4; 3; 1; 2 ]

let testResult1 = countFish sampleState 80
printfn "Test1 : Given sampleInput for 80 days, should have 5934 : %i" testResult1

let testResult2 = countFish2 sampleState 256
printfn "Test2 : Given sampleInput for 256 days, should have 26984457539 : %i" testResult2

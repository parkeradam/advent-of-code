#load "./Source.fsx"
open Source

let sampleInput =
    [ "2199943210"
      "3987894921"
      "9856789892"
      "8767896789"
      "9899965678" ]

//let testResult = getRiskLevel sampleInput

//printfn "Test 1 : Given sample input, risk level should be 15 : %i" testResult

let testResult2 = getBasinSizes sampleInput

printfn "Test 2 : Given sample input, basin sizes should be 1134 : %i" testResult2

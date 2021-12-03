#load "./Source.fsx"
open Source

let sampleInput =
    [ "00100"
      "11110"
      "10110"
      "10111"
      "10101"
      "01111"
      "00111"
      "11100"
      "10000"
      "11001"
      "00010"
      "01010" ]

let sampleResult1 = getReportNumbers sampleInput

printfn "Test 1: Given sample input Diagnostic Report output should be 198 : %i" sampleResult1

let sampleResult2 = getLifeSupportNumbers sampleInput

//printfn "Test 2: Given sample input Life Support output should be 230 : %i" sampleResult2

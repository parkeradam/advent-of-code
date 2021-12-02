#load "./Source.fsx"

open Source

let sampleInput =
    [ 199
      200
      208
      210
      200
      207
      240
      269
      260
      263 ]

let result = numberIncreased sampleInput

printfn "Test 1 : given sample input, result = 7 : %b" (result = 7)


let slidingScaleNums =
    [ 607
      618
      618
      617
      647
      716
      769
      792 ]

let scaledNums = sampleInput |> slidingScale

printfn "Test 2 : Sliding scale based on example = %O" scaledNums

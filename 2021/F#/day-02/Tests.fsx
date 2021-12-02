#load "./Source.fsx"
open Source

let sampleInput =
    [ "forward 5"
      "down 5"
      "forward 8"
      "up 3"
      "down 8"
      "forward 2" ]

let result = getPositionPartOne sampleInput

printfn "Test 1 : result from sample is as expected with no aim: %b" (result = { Horizontal = 15; Depth = 10; Aim = 0 })

let result2 = getPositionPartTwo sampleInput
printfn "Test 2 : result from sample is as expected with aim %b" ((result2.Depth * result2.Horizontal) = 900)

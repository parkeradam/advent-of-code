#load "./Source.fsx"
open Source

let sampleInput =
    """0,9 -> 5,9
    8,0 -> 0,8
    9,4 -> 3,4
    2,2 -> 2,1
    7,0 -> 7,4
    6,4 -> 2,0
    0,9 -> 2,9
    3,4 -> 1,4
    0,0 -> 8,8
    5,5 -> 8,2"""

let testResult =
    getOverlappingLinesNoDiag (sampleInput.Split('\n') |> Seq.toList)

printfn "Test 1 : Result of number of overlapping points should be 5 : %A" testResult

let testResult2 =
    getOverlappingLinesAll (sampleInput.Split('\n') |> Seq.toList)

printfn "Test 2 : Result of number of overlapping points should be 12 : %A" testResult2

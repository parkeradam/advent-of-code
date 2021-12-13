#load "./Source.fsx"

open Source
open System

let sampleInput1 =
    """start-A
    start-b
    A-c
    A-b
    b-d
    A-end
    b-end"""
    |> (fun x -> x.Split Environment.NewLine)


let sampleInput2 =
    """dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc"""
    |> (fun x -> x.Split Environment.NewLine)

let testResult1 = calculatePaths sampleInput1 false
printfn "Test 1 : Given small sample input, should find 10 routes : %i" testResult1

let testResult2 = calculatePaths sampleInput2 false
printfn "Test 2 : Given larger sample input, should find 19 routes : %i" testResult2


let testResult3 = calculatePaths sampleInput1 true
printfn "Test 3: Given small sample input and able to visit small nodes twice, should find 36 paths : %i" testResult3

let testResult4 = calculatePaths sampleInput2 true
printfn "Test 4: Given larger sample input and able to visit small nodes twice, should find 103 paths : %i" testResult4

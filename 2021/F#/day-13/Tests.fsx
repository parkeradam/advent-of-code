#load "./Source.fsx"

open Source
open System
open System.IO

let sampleInput =
    """6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5"""
        .Split(Environment.NewLine)
    |> Array.toList

let testResult1 = countNumberHoles sampleInput
printfn "Test 1 : Given sample input, after 1 fold, should show 17 dots :%i" testResult1

let testResult2 = findCode sampleInput
File.WriteAllLines("./Test2.txt", testResult2)

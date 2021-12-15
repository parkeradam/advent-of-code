#load "./Source.fsx"

open Source
open System

let sampleInput =
    """NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C"""
        .Split(Environment.NewLine)
    |> Array.toList

let testResult1 = expandAndCount sampleInput 10
printfn "Test 1 : Given sample input, expanding 10 times, answer should be 1588 :  %i" testResult1

let testResult2 = expandAndCount sampleInput 40
printfn "Test 2 : Given sample input, expanding 40 times, answer should be 2188189693529 :  %i" testResult2

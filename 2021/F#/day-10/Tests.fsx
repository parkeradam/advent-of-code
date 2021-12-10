#load "./Source.fsx"
open Source

let sampleInput =
    """[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]
"""
    |> (fun x -> x.Split())
    |> Array.toList

let testResult1 = calculateInvalidScore sampleInput
printfn "Test 1 : Given sample input, score should be 26397 : %i" testResult1

let testResult2 = calculateAutoCompleteScore sampleInput
printfn "Test 2 : Given sample input, score should be 288957 : %i" testResult2

#load "./Source.fsx"

open Source

open System.IO

let readLines (filePath: string) =
    seq {
        use sr = new StreamReader(filePath)

        while not sr.EndOfStream do
            yield sr.ReadLine()
    }

let input =
    readLines "./input.txt"
    |> Seq.map int
    |> Seq.toList


//Part 1
let partOneAnswer = input |> numberIncreased

printfn "%i" partOneAnswer

//Part 2
let partTwoAnswer = input |> slidingScale |> numberIncreased

printfn "%i" partTwoAnswer

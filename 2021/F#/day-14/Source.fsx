open System
open System.Text

let private charArrToStr (arr: char array) =
    arr
    |> Array.map (fun x -> x.ToString())
    |> Array.reduce (+)

let private parsePairings (input: string list) =
    input
    |> List.map (fun x ->
        let split = x.Split(" -> ")
        (split.[0].Trim(), split.[1].Trim()))
    |> Map.ofList


let private printAndPass x =
    printfn "%A" x
    x

let expandAndCount (input: string list) (numExpansions: int) =
    let initialString = input.[0].Trim()
    let pairings = parsePairings (input |> List.skip 2)

    let rec innerloop (str: string) =
        if (str.Length < 2) then
            match String.IsNullOrEmpty(str) with
            | true -> ""
            | false -> str
        else
            let pair = str.Substring(0, 2)
            let toAdd = pairings.[pair]

            let remainingString = str.Substring(1)

            StringBuilder()
                .Append(pair.[0].ToString())
                .Append(toAdd)
                .Append((innerloop remainingString))
                .ToString()

    let rec loop iterations (str: string) =
        printfn "Iteration %i" (iterations + 1)

        if (iterations = numExpansions) then
            str
        else
            str |> innerloop |> loop (iterations + 1)

    let characterCounts =
        loop 0 initialString
        |> Seq.groupBy id
        |> Seq.map (fun x -> (fst x, snd x |> Seq.length))

    let minChar = characterCounts |> Seq.minBy snd
    let maxChar = characterCounts |> Seq.maxBy snd

    (snd maxChar) - (snd minChar)

open System.Collections.Generic
open System

let countFish (input: int list) numDays =
    let timeLeft fish =
        if (fish = 0) then
            [ 6; 8 ]
        else
            [ fish - 1 ]

    let rec loop index lst =
        if index >= numDays then
            lst
        else
            let updatedList = lst |> List.map timeLeft |> List.concat
            printfn "%i" index
            loop (index + 1) updatedList

    loop 0 input |> List.length

let countFish2 (input: int list) numDays =
    let fishCounts = new Dictionary<int, UInt64>()

    [ 0 .. 8 ]
    |> List.iter (fun x -> fishCounts.Add(x, 0UL)) // Setup the dictionary

    input
    |> List.groupBy id
    |> List.map (fun x -> ((fst x), (snd x).Length))
    |> List.iter (fun x -> fishCounts.[fst x] <- uint64 (snd x))

    let mapIndex index =
        let total = fishCounts.[index]

        if (index = 0) then
            [ (6, total); (8, total) ]
        else
            [ (index - 1, total) ]

    let sumAndInsert (counts: (int * (int * UInt64) list)) =
        let daysLeft = fst counts

        let count =
            snd counts |> List.map snd |> List.reduce (+)

        fishCounts.[daysLeft] <- count


    let rec loop index =
        if index >= numDays then
            ()
        else
            [ 8 .. -1 .. 0 ]
            |> List.map (mapIndex)
            |> List.concat
            |> List.groupBy fst
            |> List.iter sumAndInsert


            printfn "Start of dict for index %i" index

            for value in fishCounts do
                printfn "Key = %i : Value = %i" value.Key value.Value

            printfn "End of dict"

            loop (index + 1)

    loop 0

    seq { for value in fishCounts.Values -> value }
    |> Seq.reduce (+)

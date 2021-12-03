open System

let binaryToInt str = Convert.ToInt32(str, 2)

let toString (input: char list) =
    System.String.Concat(Array.ofList (input))

let getReportNumbers (input: string list) =

    let length = input.Head.Length

    let rec loop acc index predicate =

        if index >= length then
            acc |> List.rev |> toString |> binaryToInt
        else
            let mapped = input |> List.map (fun x -> x.[index])

            let numZeroes =
                mapped
                |> List.filter (fun c -> c = '0')
                |> List.length

            let numOnes =
                mapped
                |> List.filter (fun c -> c = '1')
                |> List.length

            let bitToAdd = predicate numZeroes numOnes
            loop (bitToAdd :: acc) (index + 1) predicate


    let leastRelevent numZeroes numOnes =
        match numZeroes = numOnes with
        | true -> '0'
        | false ->
            match numZeroes < numOnes with
            | true -> '0'
            | false -> '1'

    let mostRelevent numZeroes numOnes =
        match numZeroes = numOnes with
        | true -> '1'
        | false ->
            match numZeroes < numOnes with
            | true -> '1'
            | false -> '0'

    let gamma = loop [] 0 mostRelevent

    let epsilon = loop [] 0 leastRelevent

    gamma * epsilon



let getLifeSupportNumbers (input: string list) =
    let rec loop (remainingList: string list) index predicate =
        match remainingList with
        | [ _ ] -> remainingList
        | _ ->
            let numberZeroes =
                remainingList
                |> List.filter (fun x -> x.[index] = '0')
                |> List.length

            let numberOnes =
                remainingList
                |> List.filter (fun x -> x.[index] = '1')
                |> List.length

            let bitToFilterBy =
                match numberZeroes = numberOnes with
                | true -> '1'
                | false ->
                    match numberZeroes < numberOnes with
                    | true -> '1'
                    | false -> '0'

            loop
                (remainingList
                 |> List.filter (predicate bitToFilterBy index))
                (index + 1)
                predicate

    let oxygenPredicate bit index (value: string) = value.[index] = bit
    let co2Predicate bit index (value: string) = value.[index] <> bit

    let oxygen =
        (loop input 0 oxygenPredicate)
        |> List.head
        |> binaryToInt

    let co2 =
        (loop input 0 co2Predicate)
        |> List.head
        |> binaryToInt

    oxygen * co2

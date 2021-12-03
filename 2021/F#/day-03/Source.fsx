open System

let binaryToInt str = Convert.ToInt32(str, 2)

let toString (input: char list) =
    System.String.Concat(Array.ofList (input))

let pivotList (lists: char list list) =
    lists
    |> List.map (fun item ->
        item
        |> List.mapi (fun index _ -> lists |> List.map (fun x -> x.[index])))
    |> List.head


type DiagnosticReportBit =
    { GammaRateBit: char
      EpisilonRateBit: char }

let toDiagnosticReportBit (input: char list) =

    let numZeroes =
        input
        |> List.filter (fun x -> x = '0')
        |> List.length

    let numOnes =
        input
        |> List.filter (fun x -> x = '1')
        |> List.length

    match numZeroes < numOnes with
    | true ->
        { GammaRateBit = '1'
          EpisilonRateBit = '0' }
    | false ->
        { GammaRateBit = '0'
          EpisilonRateBit = '1' }

let getReportNumbers (input: string list) =
    let diagnosticBits =
        input
        |> List.map (fun x -> x.ToCharArray())
        |> List.map Array.toList
        |> pivotList
        |> List.map toDiagnosticReportBit

    let episilonBits =
        diagnosticBits
        |> List.fold (fun state item -> item.EpisilonRateBit :: state) []
        |> List.rev

    let gammaBits =
        diagnosticBits
        |> List.fold (fun state item -> item.GammaRateBit :: state) []
        |> List.rev

    let epsilonNumber =
        Convert.ToInt32(episilonBits |> toString, 2)

    let gammaNumber =
        Convert.ToInt32(gammaBits |> toString, 2)

    epsilonNumber * gammaNumber



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

let print = printfn "%s"

let printAndPass prefix x =
    printfn "%s = %A" prefix x
    x

let chekUniqueDigits (input: string list) =

    let countByLength number (input: string array) =
        input
        |> Array.filter (fun x -> x.Length = number)
        |> Array.length

    let countOnes (input: string array) = countByLength 2 input
    let countFours (input: string array) = countByLength 4 input
    let countSevens (input: string array) = countByLength 3 input
    let countEights (input: string array) = countByLength 7 input

    let countAll input =
        [ countOnes input
          countFours input
          countSevens input
          countEights input ]
        |> List.reduce (+)

    input
    |> List.map (fun x -> x.Split('|').[1].Trim())
    |> List.map (fun x -> x.Split(" "))
    |> List.map countAll
    |> List.reduce (+)

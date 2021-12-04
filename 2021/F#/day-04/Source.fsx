open System

type CardNumber =
    | Called
    | Uncalled of int

type BingoCard = { Rows: CardNumber list list }

let bingoCardFromList (arr: string list) =
    let rowFromString (str: string) =
        str.Split(" ")
        |> Array.filter (fun x -> x.Length > 0)
        |> Array.map Int32.Parse
        |> Array.map Uncalled
        |> Array.toList

    { Rows = arr |> List.map (fun x -> x |> rowFromString) }

let updateBingoCard num (card: BingoCard) =
    let rows =
        card.Rows
        |> List.map (
            List.map (fun c ->
                match c with
                | Called -> c
                | Uncalled (value) ->
                    match value = num with
                    | true -> Called
                    | false -> c)
        )

    { Rows = rows }

let countUncalled =
    List.map (function
        | Uncalled (_) -> 1
        | Called -> 0)
    >> List.reduce (+)

let checkCollumnWin (card: BingoCard) =
    let maxLength = card.Rows.Head.Length

    let rec loop index =
        if index >= maxLength then
            false
        else
            let numUncalled =
                card.Rows
                |> List.map (fun x -> x.[index])
                |> countUncalled

            if numUncalled = 0 then
                true
            else
                loop (index + 1)

    loop 0

let checkRowWin (card: BingoCard) =
    let count =
        card.Rows
        |> List.map (fun x -> x |> countUncalled)
        |> List.filter (fun x -> x = 0)
        |> List.length

    count > 0

let winnerWinner card =
    checkRowWin card || checkCollumnWin card

let loserLoser card = not (winnerWinner card)

let totalUncalled =
    List.concat
    >> List.map (function
        | Uncalled (value) -> value
        | Called -> 0)
    >> List.reduce (+)

let getOrderedWinners (input: string list) =
    let bingoNumbers =
        input.Head
        |> (fun x -> x.Split(','))
        |> Array.map Int32.Parse
        |> Array.toList

    let rec makeCards acc remainingList =
        match (remainingList |> List.length < 5) with
        | true -> acc
        | false ->
            match remainingList with
            | [] -> acc
            | _ ->
                let card =
                    remainingList |> List.take 5 |> bingoCardFromList

                makeCards (acc @ [ card ]) (remainingList |> List.skip (6))

    let cards =
        input.Tail |> List.skip 1 |> makeCards []

    let rec getWinners acc numbers cards =
        match numbers with
        | head :: tail ->
            let newCards = cards |> List.map (updateBingoCard head)
            let winners = newCards |> List.filter winnerWinner
            let newWinners = winners |> List.map (fun x -> (head, x))
            getWinners (acc @ newWinners) tail (newCards |> List.filter loserLoser)
        | [] -> acc

    getWinners [] bingoNumbers cards


let getWinnersNumber (input: string list) =
    let firstWinner = (getOrderedWinners input) |> List.head
    let winner = snd firstWinner
    (fst firstWinner) * (totalUncalled winner.Rows)

let getLastWinnersNumbers (input: string list) =
    let lastWinner =
        (getOrderedWinners input) |> List.rev |> List.head

    let winner = snd lastWinner
    (fst lastWinner) * (totalUncalled winner.Rows)

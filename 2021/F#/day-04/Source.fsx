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
        |> List.map (fun x ->
            x
            |> List.map (fun c ->
                match c with
                | Called -> Called
                | Uncalled (value) ->
                    match value = num with
                    | true -> Called
                    | false -> c))

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

let totalUncalled (list: CardNumber list list) =
    list
    |> List.concat
    |> List.map (function
        | Uncalled (value) -> value
        | Called -> 0)
    |> List.reduce (+)

let getWinnersNumber (input: string list) =

    let bingoNumbers =
        input.Head
        |> (fun x -> x.Split(','))
        |> Array.map Int32.Parse
        |> Array.toList

    let rec makeCards acc remainingList =
        match (remainingList |> List.length < 5) with
        | true -> acc |> List.rev
        | false ->
            match remainingList with
            | [] -> acc |> List.rev
            | _ ->
                let card =
                    remainingList |> List.take 5 |> bingoCardFromList

                makeCards (card :: acc) (remainingList |> List.skip (6))

    let cards =
        input.Tail |> List.skip 1 |> makeCards []

    let rec getWinners numbers cards =
        match numbers with
        | head :: tail ->
            let newCards = cards |> List.map (updateBingoCard head)
            let winners = newCards |> List.filter winnerWinner

            if winners.Length > 0 then
                (head, winners.Head)
            else
                getWinners tail newCards
        | _ -> raise (new Exception("NO WINNERS!!!!"))

    let winners = getWinners bingoNumbers cards
    let winner = snd winners
    (fst winners) * (totalUncalled winner.Rows)


let getLastWinnersNumbers (input: string list) =
    let bingoNumbers =
        input.Head
        |> (fun x -> x.Split(','))
        |> Array.map Int32.Parse
        |> Array.toList

    let rec makeCards acc remainingList =
        match (remainingList |> List.length < 5) with
        | true -> acc |> List.rev
        | false ->
            match remainingList with
            | [] -> acc |> List.rev
            | _ ->
                let card =
                    remainingList |> List.take 5 |> bingoCardFromList

                makeCards (card :: acc) (remainingList |> List.skip (6))

    let cards =
        input.Tail |> List.skip 1 |> makeCards []

    let rec getLastWinner numbers cards =
        match numbers with
        | head :: tail ->
            let newCards =
                cards
                |> List.map (updateBingoCard head)
                |> List.filter loserLoser

            if newCards.Length = 0 then
                let lastWinner = updateBingoCard head cards.Head
                (head, lastWinner)
            else
                getLastWinner tail newCards
        | _ -> raise (new Exception("NO WINNERS!!!!"))

    let winners = getLastWinner bingoNumbers cards
    let winner = snd winners
    (fst winners) * (totalUncalled winner.Rows)

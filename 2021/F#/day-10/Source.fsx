#load "./Either.fsx"

open Either
open System

let processLine (input: string) =
    let rec loop (acc: char list) index =
        if (index >= input.Length) then
            Left(acc)
        else
            let char = input.[index]

            match char with
            | ']' ->
                if acc.Head = '[' then
                    loop acc.Tail (index + 1)
                else
                    Right(']')
            | '}' ->
                if acc.Head = '{' then
                    loop acc.Tail (index + 1)
                else
                    Right('}')
            | ')' ->
                if acc.Head = '(' then
                    loop acc.Tail (index + 1)
                else
                    Right(')')
            | '>' ->
                if acc.Head = '<' then
                    loop acc.Tail (index + 1)
                else
                    Right('>')
            | _ -> loop (char :: acc) (index + 1)

    loop [] 0


let calculateInvalidScore (input: string list) =
    let charToScore x =
        match x with
        | ')' -> 3
        | ']' -> 57
        | '}' -> 1197
        | '>' -> 25137
        | _ -> 0

    input
    |> List.map processLine
    |> List.filter isRight
    |> List.map unwrapRight
    |> List.map charToScore
    |> List.reduce (+)

let calculateAutoCompleteScore (input: string list) =

    let toPairing =
        function
        | '(' -> ')'
        | '[' -> ']'
        | '{' -> '}'
        | '<' -> '>'
        | _ -> raise (new Exception("No Pair Found"))

    let charToScore x =
        match x with
        | ')' -> 1
        | ']' -> 2
        | '}' -> 3
        | '>' -> 4
        | _ -> 0

    let foldCharacters (charList: char list) =
        charList
        |> List.map charToScore
        |> List.map uint64
        |> List.fold (fun acc elem -> (acc * uint64 5) + elem) (uint64 0)

    let allScores =
        input
        |> List.map processLine
        |> List.filter isLeft
        |> List.map unwrapLeft
        |> List.map (List.map toPairing)
        |> List.map foldCharacters
        |> List.sort

    let middleIndex =
        if allScores.Length % 2 = 0 then
            ((allScores.Length) / 2)
        else
            ((allScores.Length - 1) / 2)

    allScores.[middleIndex] // -1 because off by 0 error

open System

type Position =
    { Depth: int
      Horizontal: int
      Aim: int }

type Direction =
    | Forward of int
    | Down of int
    | Up of int

let tupleToDirection (tuple: (string * string)) =
    match tuple with
    | ("forward", value) -> Forward(int value)
    | ("down", value) -> Down(int value)
    | ("up", value) -> Up(int value)
    | _ -> raise (new Exception("Not a valid command"))


let getPosition (folder: Position -> Direction -> Position) (input: string list) : Position =
    let instructions =
        input
        |> List.map (fun (x) -> x.Split(' '))
        |> List.map (fun (x) -> (x.[0], x.[1]))
        |> List.map tupleToDirection

    instructions
    |> List.fold folder { Depth = 0; Horizontal = 0; Aim = 0 }

let getPositionPartOne (input: string list) =
    let folder (acc: Position) dir =
        match dir with
        | Forward (value) -> { acc with Horizontal = acc.Horizontal + value }
        | Down (value) -> { acc with Depth = acc.Depth + value }
        | Up (value) -> { acc with Depth = acc.Depth - value }

    getPosition folder input


let getPositionPartTwo (input: string list) =
    let folder (acc: Position) dir =
        match dir with
        | Forward (value) ->
            { acc with
                Horizontal = acc.Horizontal + value
                Depth = acc.Depth + (value * acc.Aim) }
        | Down (value) -> { acc with Aim = acc.Aim + value }
        | Up (value) -> { acc with Aim = acc.Aim - value }

    getPosition folder input

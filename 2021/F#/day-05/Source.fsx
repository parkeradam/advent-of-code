open System

type Point = { X: int; Y: int }

type Line = { Start: Point; End: Point }

let listToLine (lst: string list) =
    { Start = { X = int lst.[0]; Y = int lst.[1] }
      End = { X = int lst.[2]; Y = int lst.[3] } }

let isHorizontal line = line.Start.Y = line.End.Y
let isVertical line = line.Start.X = line.End.X

let isHorizontalOrVertical line =
    (isHorizontal line) || (isVertical line)

let genList first second =
    match (first < second) with
    | true -> [ first .. 1 .. second ]
    | false -> [ first .. -1 .. second ]

let generatePoints line =
    let firstPoint = line.Start
    let secondPoint = line.End

    if isVertical line then
        genList firstPoint.Y secondPoint.Y
        |> List.map (fun y -> { X = firstPoint.X; Y = y })
    else if isHorizontal line then
        genList firstPoint.X secondPoint.X
        |> List.map (fun x -> { X = x; Y = firstPoint.Y })
    else
        let xs = genList firstPoint.X secondPoint.X
        let ys = genList firstPoint.Y secondPoint.Y

        List.zip xs ys
        |> List.map (fun pair -> { X = fst pair; Y = snd pair })

let getOverlappigLines lineFilter (input: string list) =
    let seperators = [| " -> "; "," |]

    input
    |> List.map (fun x ->
        x.Split(seperators, StringSplitOptions.None)
        |> Seq.toList)
    |> List.map listToLine
    |> List.filter lineFilter
    |> List.map generatePoints
    |> List.concat
    |> List.groupBy (fun x -> x)
    |> List.filter (fun lst -> (snd lst).Length > 1)
    |> List.length

let getOverlappingLinesNoDiag =
    getOverlappigLines isHorizontalOrVertical

let getOverlappingLinesAll = getOverlappigLines (fun _ -> true)

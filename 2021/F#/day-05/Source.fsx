open System

type Point =
    { X: int
      Y: int }
    static member create a b = { X = int a; Y = int b }

let isHorizontal (pair: Point * Point) = (fst pair).Y = (snd pair).Y
let isVertical (pair: Point * Point) = (fst pair).X = (snd pair).X

let isHorizontalOrVertical (pair: Point * Point) =
    (isHorizontal pair) || (isVertical pair)

let genList first second =
    match (first < second) with
    | true -> [ first .. 1 .. second ]
    | false -> [ first .. -1 .. second ]

let lowestXandY a b =
    let lowestX = if a.X < b.X then a.X else b.X
    let lowestY = if a.Y < b.Y then a.Y else b.Y
    (lowestX, lowestY)

let generatePoints (points: Point * Point) =
    let firstPoint = fst points
    let secondPoint = snd points

    if isVertical points then
        genList firstPoint.Y secondPoint.Y
        |> List.map (fun y -> { X = firstPoint.X; Y = y })
    else if isHorizontal points then
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
    |> List.map (fun x -> (Point.create x.[0] x.[1], Point.create x.[2] x.[3]))
    |> List.filter lineFilter
    |> List.map generatePoints
    |> List.concat
    |> List.groupBy (fun x -> x)
    |> List.filter (fun lst -> (snd lst).Length > 1)
    |> List.length

let getOverlappingLinesNoDiag =
    getOverlappigLines isHorizontalOrVertical

let getOverlappingLinesAll = getOverlappigLines (fun _ -> true)

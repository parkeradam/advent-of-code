open System

type Fold =
    | Horizonal of int
    | Vertical of int
    static member fromString(str: string) =
        let splitString = str.Split "="

        match splitString.[0] with
        | "y" -> Horizonal(Int32.Parse(splitString.[1]))
        | "x" -> Vertical(Int32.Parse(splitString.[1]))
        | _ -> raise (Exception("Not a X or Y fold"))

type Point =
    { X: int
      Y: int }
    static member create (posX: string) (posY: string) =
        { X = Int32.Parse(posX)
          Y = Int32.Parse(posY) }

let mirrorPointX (mirrorVal: int) (point: Point) =
    let diff = point.X - mirrorVal |> Math.Abs

    if (point.X < mirrorVal) then
        { point with X = mirrorVal + diff }
    else
        { point with X = mirrorVal - diff }

let mirrorPointY (mirrorVal: int) (point: Point) =
    let diff = point.Y - mirrorVal |> Math.Abs

    if (point.Y < mirrorVal) then
        { point with Y = mirrorVal + diff }
    else
        { point with Y = mirrorVal - diff }

let maxBy (func: Point -> int) lst = lst |> List.maxBy func |> func
let minBy (func: Point -> int) lst = lst |> List.minBy func |> func

let getHolesAsString (points: Point list) =
    let maxX = maxBy (fun point -> point.X) points
    let minX = minBy (fun point -> point.X) points

    let maxY = maxBy (fun point -> point.Y) points
    let minY = minBy (fun point -> point.Y) points

    let results =
        [ minY .. maxY ]
        |> List.map (fun y ->
            [ minX .. maxX ]
            |> List.map (fun x ->
                if (points |> List.contains { X = x; Y = y }) then
                    "#"
                else
                    ".")
            |> List.reduce (+))

    results

let private printHoles (points: Point list) =
    getHolesAsString points |> List.map (printfn "%s")

let getRemainingHoles (input: string list) (numFolds: int option) =
    let cutoffIndex =
        input |> List.findIndex (fun x -> x = "")

    let split = input |> List.splitAt cutoffIndex

    let dots =
        fst split
        |> List.map (fun x ->
            let s = x.Split(",")
            Point.create s.[0] s.[1])
        |> Set.ofList

    let folds =
        snd split
        |> List.filter (String.IsNullOrEmpty >> not)
        |> List.map (fun x -> x.Replace("fold along ", ""))

    let foldsToUse =
        (numFolds
         |> Option.defaultValue folds.Length
         |> List.take)
            folds
        |> List.map Fold.fromString

    let folder (state: Set<Point>) (value: Fold) =
        let maxX =
            state
            |> Set.toList
            |> List.maxBy (fun x -> x.X)
            |> (fun x -> x.X)

        let maxY =
            state
            |> Set.toList
            |> List.maxBy (fun x -> x.Y)
            |> (fun x -> x.Y)

        match value with
        | Horizonal (pos) ->
            let toMirror =
                state
                |> Set.filter (fun x ->
                    if pos < maxY / 2 then
                        x.Y < pos
                    else
                        x.Y > pos)

            let mirroredPoints = toMirror |> Set.map (mirrorPointY pos)
            let leftoverPoints = Set.difference state toMirror
            Set.union leftoverPoints mirroredPoints
        | Vertical (pos) ->
            let toMirror =
                state
                |> Set.filter (fun x ->
                    if pos < maxX / 2 then
                        x.X < pos
                    else
                        x.X > pos)

            let mirroredPoints = toMirror |> Set.map (mirrorPointX pos)
            let leftoverPoints = Set.difference state toMirror
            Set.union leftoverPoints mirroredPoints


    foldsToUse |> List.fold folder dots |> Set.toList

let countNumberHoles (input: string list) =
    getRemainingHoles input (Some 1) |> List.length

let findCode (input: string list) =
    getRemainingHoles input None |> getHolesAsString

open System

let stringToCharArray (x: string) = x.ToCharArray()

type DepthPos = { PosX: int; PosY: int; Number: int }

let isLower (depthPos1: DepthPos) (depthPos2: DepthPos) = depthPos1.Number < depthPos2.Number

let lowerWithDefault toCheck =
    Option.map (isLower toCheck)
    >> Option.defaultValue true


let checkWithOffsets (toCheckAgainst: DepthPos list) toCheck xOffset yOffset =
    toCheckAgainst
    |> List.tryFind (fun x ->
        x.PosY = toCheck.PosY + yOffset
        && x.PosX = toCheck.PosX + xOffset)

let isLowPoint (toCheckAgainst: DepthPos list) (toCheck: DepthPos) =

    let checkWithOffsetsDefaulted = checkWithOffsets toCheckAgainst toCheck

    let above = checkWithOffsetsDefaulted 0 (-1)
    let below = checkWithOffsetsDefaulted 0 1
    let left = checkWithOffsetsDefaulted (-1) 0
    let right = checkWithOffsetsDefaulted 1 0

    let lowerWithDefaultCheck = lowerWithDefault toCheck

    let lowerThanAbove = above |> lowerWithDefaultCheck
    let lowerThanBelow = below |> lowerWithDefaultCheck
    let lowerThanLeft = left |> lowerWithDefaultCheck
    let lowerThanRight = right |> lowerWithDefaultCheck

    lowerThanAbove
    && lowerThanBelow
    && lowerThanLeft
    && lowerThanRight

let printAndPass x =
    printfn "%A" x
    x

let getDepthPositions (input: string list) =
    input
    |> List.map stringToCharArray
    |> List.mapi (fun yPos item ->
        item
        |> Array.mapi (fun xPos character ->
            { PosX = xPos
              PosY = yPos
              Number = Int32.Parse(character.ToString()) })
        |> Array.toList)
    |> List.concat

let getLowPoints (input: string list) =
    let depthPositions = getDepthPositions input
    let isLow = isLowPoint depthPositions
    depthPositions |> List.filter isLow

let getRiskLevel (input: string list) =
    getLowPoints input
    |> List.map (fun x -> x.Number + 1)
    |> List.reduce (+)


let doesNotEqual9 =
    Option.map (fun x -> (x.Number <> 9))
    >> Option.defaultValue false

let getBasinSizes (input: string list) =
    let depthPositions = getDepthPositions input
    let lowPoints = getLowPoints input

    let rec traverseBasin acc points =
        if (points |> List.length = 0) then
            acc
        else
            points
            |> List.map (fun x ->
                let checkWithOffsetsDefaulted = checkWithOffsets depthPositions x

                let above = checkWithOffsetsDefaulted 0 (-1)
                let below = checkWithOffsetsDefaulted 0 1
                let left = checkWithOffsetsDefaulted (-1) 0
                let right = checkWithOffsetsDefaulted 1 0

                let aboveInBasin = above |> doesNotEqual9
                let belowInBasin = below |> doesNotEqual9
                let leftInBasin = left |> doesNotEqual9
                let rightInBasin = right |> doesNotEqual9

                [ if aboveInBasin then above else None
                  if belowInBasin then below else None
                  if leftInBasin then left else None
                  if rightInBasin then right else None ]
                |> List.choose id
                |> List.filter (fun x -> not (List.contains x acc)))
            |> List.concat
            |> List.distinct
            //|> printAndPass
            |> traverseBasin (acc @ points)

    lowPoints
    |> List.map (fun x -> traverseBasin [] [ x ])
    |> List.map (fun x -> x.Length)
    |> List.sortByDescending id
    |> List.take 3
    |> List.reduce (*)

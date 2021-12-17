open System

type private Target =
    { minX: int
      maxX: int
      minY: int
      maxY: int }

let private targetFromInput (input: string) =
    let cleanedInput =
        input
            .Replace("target area: x=", "")
            .Replace(" y=", "")

    cleanedInput.Split(",")
    |> (fun inp ->
        let xVals = inp.[0].Split("..")
        let yVals = inp.[1].Split("..")

        { minX = Int32.Parse xVals.[0]
          maxX = Int32.Parse xVals.[1]
          minY = Int32.Parse yVals.[0]
          maxY = Int32.Parse yVals.[1] })

let private isInTarget (target: Target) (positionX: int) (positionY: int) =
    let inX =
        positionX <= target.maxX
        && target.minX <= positionX

    let inY =
        positionY <= target.maxY
        && target.minY <= positionY

    inX && inY

let private pathCrossesTarget (target: Target) (path: (int * int) list) =
    path
    |> List.map (fun node -> isInTarget target (fst node) (snd node))
    |> List.contains true

let private isPastTarget (target: Target) posX posY =
    posX > target.maxX || posY < target.minY

let valOrZero value = if (value < 0) then 0 else value

let private calculatePaths (target: Target) =
    let position = (0, 0)

    let getPath target velocity position =
        let rec loop posX posY velocityX velocityY =
            let newPos = (posX + velocityX, posY + velocityY)

            if isPastTarget target (fst newPos) (snd newPos) then
                [ (posX, posY) ]
            else
                (posX, posY)
                :: loop (fst newPos) (snd newPos) (valOrZero (velocityX - 1)) (velocityY - 1)

        loop (fst position) (snd position) (fst velocity) (snd velocity)

    [ 0 .. target.maxX ]
    |> List.collect (fun x ->
        [ target.minY .. (-(target.minY * 2)) ]
        |> List.map (fun y -> getPath target (x, y) position))

let calculateHeight (input: string) =
    let target = targetFromInput input
    let allPaths = calculatePaths target

    allPaths
    |> List.filter (fun path -> pathCrossesTarget target path)
    |> List.concat
    |> List.maxBy snd
    |> snd

let calculateNumAllPaths (input: string) =
    let target = targetFromInput input
    let allPaths = calculatePaths target

    allPaths
    |> List.filter (fun path -> pathCrossesTarget target path)
    |> List.length

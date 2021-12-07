open System

let fuelPerStep number = [ 0 .. number ] |> List.reduce (+)

let calculateFuelCost calcFunc (input: int list) =

    let maximum = input |> List.max
    let minimum = input |> List.min

    [ minimum .. maximum ]
    |> List.map calcFunc
    |> List.minBy (fun x -> snd x)
    |> snd

let calculateFuelSimple (input: int list) =
    let calculateSingleFulePerStep x =
        let distances =
            input |> List.map (fun a -> Math.Abs(a - x))

        (x, distances |> List.sum)

    calculateFuelCost calculateSingleFulePerStep input

let calculateFuelComplex (input: int list) =
    let calculateExtraFuelPerStep x =
        let distances =
            input
            |> List.map (fun a -> Math.Abs(a - x))
            |> List.map fuelPerStep

        (x, distances |> List.sum)

    calculateFuelCost calculateExtraFuelPerStep input

open System

let right = 1
let left = -1
let above = -10
let aboveLeft = above - 1
let aboveRight = above + 1
let below = 10
let belowLeft = below - 1
let belowRight = below + 1


let arraySafeSet (arr: int array) incrementAmount index =
    arr
    |> Array.tryItem index
    |> Option.map (fun x ->
        arr.[index] <- (arr.[index] + incrementAmount)
        ())
    |> Option.defaultValue ()

let getFlashCount (input: string list) (numSteps: int) =
    let mutable hasSteppedNowBreak = true
    let octNums = input |> List.reduce (+)

    let mutable numArray =
        octNums.ToCharArray()
        |> Array.map (fun x -> Int32.Parse(x.ToString()))

    let mutable flashCount = 0

    let rec step () =
        let mutable hasFlashed = false

        numArray
        |> Array.iteri (fun index item ->
            let num = Int32.Parse(item.ToString())
            let updatedNum = num + 1

            if (updatedNum = 10) then
                printfn "%A" index
                flashCount <- (flashCount + 1)
                hasFlashed <- true

                let numsSafeIncrement = arraySafeSet numArray 1

                numsSafeIncrement (index + right)
                numsSafeIncrement (index + left)
                numsSafeIncrement (index + above)
                numsSafeIncrement (index + aboveLeft)
                numsSafeIncrement (index + aboveRight)
                numsSafeIncrement (index + below)
                numsSafeIncrement (index + belowLeft)
                numsSafeIncrement (index + belowRight)

            else
                numArray.[index] <- (numArray.[index] + 1))

        if hasFlashed then
            hasFlashed <- false

            if hasSteppedNowBreak then
                printfn "Before recheck %A" numArray
                hasSteppedNowBreak <- false
                step ()
            else
                flashCount
        else
            flashCount


    let resetFlash () =
        printfn "Flashing NumArray"

        numArray <-
            numArray
            |> Array.map (fun x -> if x >= 10 then 0 else x)

    let mutable stepTotal = 0

    for stepNum in [ 1 .. numSteps ] do
        printfn "beforeStep %A" numArray
        stepTotal <- stepTotal + step ()
        //printfn "Flash Toal %i after step %i" stepTotal stepNum
        resetFlash () |> ignore
        printfn "afterStep %A" numArray

    stepTotal

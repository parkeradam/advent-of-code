type CaveType =
    | Big of string
    | Small of string

type Line = { From: CaveType; To: CaveType }

let printAndPass x =
    printfn "%A" x
    x

let isUpper (str: string) = str.ToUpper() = str

let getStringFromCaveType =
    function
    | Big (str) -> str
    | Small (str) -> str

let getLines (input: string array) =
    input
    |> Array.map (fun x -> x.Split '-' |> Array.map (fun x -> x.Trim()))
    |> Array.map (fun pointsPath ->
        let cavePoints =
            pointsPath
            |> Array.map (fun point ->
                match isUpper point with
                | true -> Big point
                | false -> Small point)

        [| { From = cavePoints.[0]
             To = cavePoints.[1] }
           { From = cavePoints.[1]
             To = cavePoints.[0] } |])
    |> Array.concat
//|> printAndPass

let calculatePaths (input: string array) =
    let lines = getLines input

    let rec loop (current: CaveType) (visited: Set<CaveType>) : string array =
        // printfn "visiting %A" current
        // printfn "visited %A" visited

        let possibleRoutes =
            lines
            |> Array.filter (fun x -> x.From = current) // Get possible to routes
            |> Array.map (fun x -> x.To) // map to the to routes
            |> Array.filter (fun x -> visited.Contains(x) |> not) // Filter if it hasnt been visited

        if getStringFromCaveType current = "end" then
            [| "end" |]
        else
            match current with
            | Big (str) ->
                possibleRoutes
                |> Array.map (fun x -> loop x visited)
                |> Array.collect (fun y -> y |> Array.map (fun z -> str + z))
            | Small (str) ->
                let newSet = visited.Add current

                possibleRoutes
                |> Array.map (fun x -> loop x newSet)
                |> Array.collect (fun y -> y |> Array.map (fun z -> str + z))

    loop (Small "start") Set.empty
    |> Array.filter (fun x -> x.StartsWith("start") && x.EndsWith("end"))
    |> Array.length

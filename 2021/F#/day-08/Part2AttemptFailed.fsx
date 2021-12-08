#load "./NumberConfigs.fsx"
open NumberConfigs

let print = printfn "%s"

let printAndPass prefix x =
    printfn "%s = %A" prefix x
    x

let getConfiguration (possibilities: (char * Segment) list list) (input: string) =

    printfn "%A" input

    let segmentListToStringSegments input =
        let string =
            input
            |> List.map fst
            |> List.toArray
            |> (fun x -> new string (x))

        (string, input)

    let parsedInput = input.Split(" ")

    let eightString =
        parsedInput
        |> Array.filter (fun x -> x.Length = 7)
        |> Array.head

    print eightString

    let oneFilter (input: (char * Segment) list) =
        let oneString =
            parsedInput
            |> Array.filter (fun x -> x.Length = 2)
            |> Array.head

        print oneString

        printfn
            "%A"
            (input
             |> List.filter (fun x -> oneString.Contains(fst x)))

        let possible =
            input
            |> List.filter (fun x -> oneString.Contains(fst x))
            |> List.filter (fun x -> one |> List.contains (snd x))
            |> List.length

        if possible > 0 then true else false

    let fourFilter (input: (char * Segment) list) =
        let fourString =
            parsedInput
            |> Array.filter (fun x -> x.Length = 4)
            |> Array.head

        print fourString

        let possible =
            input
            |> List.filter (fun x -> fourString.Contains(fst x))
            |> List.filter (fun x -> four |> List.contains (snd x))
            |> List.length

        if possible > 0 then true else false

    let sevenFilter (input: (char * Segment) list) =
        let sevenString =
            parsedInput
            |> Array.filter (fun x -> x.Length = 3)
            |> Array.head

        let possible =
            input
            |> List.filter (fun x -> sevenString.Contains(fst x))
            |> List.filter (fun x -> seven |> List.contains (snd x))
            |> List.length

        if possible > 0 then true else false


    possibilities
    |> List.map segmentListToStringSegments
    |> printAndPass "Segments: "
    |> List.filter (fun x -> fst x = eightString)
    |> printAndPass "Eight Strings: "
    |> List.map snd
    |> printAndPass "Segemnts 2: "
    |> List.filter oneFilter
    |> printAndPass "One Filtered: "
    |> List.filter fourFilter
    |> printAndPass "Four Filtered: "
    |> List.filter sevenFilter
    |> printAndPass "Seven Filtered: "
    |> List.head



let calculateActualNumbers (input: string list) =

    let letterToSegment letterList =
        [ Top
          TopLeft
          TopRight
          Middle
          BottomLeft
          BottomRight
          Bottom ]
        |> List.zip letterList


    let segmentListToNumber segmentList =
        match segmentList with
        | item when item = zero -> Some(0)
        | item when item = one -> Some(1)
        | item when item = two -> Some(2)
        | item when item = three -> Some(3)
        | item when item = four -> Some(4)
        | item when item = five -> Some(5)
        | item when item = six -> Some(6)
        | item when item = seven -> Some(7)
        | item when item = eight -> Some(8)
        | item when item = nine -> Some(9)
        | _ -> None

    let permute list =
        let rec inserts e =
            function
            | [] -> [ [ e ] ]
            | x :: xs as list ->
                (e :: list)
                :: [ for xs' in inserts e xs -> x :: xs' ]

        List.fold (fun accum x -> List.collect (inserts x) accum) [ [] ] list


    let listToNumber (input: (char * Segment) list) =
        let string =
            input
            |> List.map fst
            |> List.toArray
            |> (fun x -> new string (x))

        let number =
            input |> List.map snd |> segmentListToNumber

        (string, number)



    let chars = [ 'a'; 'b'; 'c'; 'd'; 'e'; 'f'; 'g' ]

    let segmentPossibilities =
        chars |> permute |> List.map letterToSegment

    let configs =
        input
        |> List.map (fun x -> x.Split("|"))
        |> List.map (fun x -> getConfiguration segmentPossibilities (x.[0].Trim()))


    let getSegmentsFromString (input: ((char * Segment) list * string)) =
        let charSegmentlist = fst input

        (snd input).ToCharArray()
        |> Array.map (fun x ->
            charSegmentlist
            |> List.where (fun cs -> fst cs = x)
            |> List.head
            |> snd)
        |> Array.toList

    let stringTrim (str: string) = str.Trim()

    let results =
        input
        |> List.map (fun x -> x.Split("|").[1] |> stringTrim)
        |> List.zip configs
        |> List.map getSegmentsFromString
        |> List.map segmentListToNumber
        |> List.choose id
        |> List.map (fun x -> x.ToString())


    //TODO Get segemnts from string (string -> char -> Segments)
    // Segments to numbers
    // Combine Numbers As String
    // Add All Numbers

    printfn "possibilities = %A" (results)

    1

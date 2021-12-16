open System
open System.Collections.Generic

type Position = { X: int; Y: int; Risk: int }

let private parseInput =
    Array.mapi (fun xIndex x ->
        x
        |> Seq.mapi (fun yIndex y ->
            { X = xIndex
              Y = yIndex
              Risk = Int32.Parse(y.ToString()) })
        |> Seq.toArray)
    >> Array.concat

let private getNeighbours (lst: Position list) (pos: Position) =

    let tryFind posX posY =
        lst
        |> List.tryFind (fun x -> x.X = posX && x.Y = posY)

    [ tryFind (pos.X - 1) pos.Y //left
      tryFind (pos.X + 1) pos.Y //right
      tryFind pos.X (pos.Y - 1) //up
      tryFind pos.X (pos.Y + 1) ] // down
    |> List.choose id


let calculateRisk (input: string array) =

    let shortest = new Dictionary<Position, int>()
    let positions = parseInput input |> Array.toList

    positions
    |> List.iter (fun x -> shortest.Add(x, Int32.MaxValue))

    let topLeft =
        positions
        |> List.find (fun x -> x.X = 0 && x.Y = 0)

    shortest.[topLeft] <- 0

    let mutable visited = List.empty<Position>
    let mutable toVisit = List.singleton topLeft
    let findNeighbours = getNeighbours positions

    while visited.Length <> positions.Length do
        let current =
            toVisit
            |> List.filter (fun x -> (List.contains x visited) |> not)
            |> List.sortByDescending (fun x -> shortest.[x])
            |> List.head

        printfn "Shortest = %A" shortest

        printfn "Using Node  %A" current
        let neighbours = findNeighbours current

        for neighbour in neighbours do
            let neighbourDist = shortest.[current] + neighbour.Risk

            if (shortest.[neighbour] > neighbourDist) then
                shortest.[neighbour] <- neighbourDist

        toVisit <-
            (toVisit @ neighbours)
            |> List.filter (fun x -> x <> current)

        visited <- current :: visited

    let maxX =
        positions
        |> List.sortByDescending (fun x -> x.X)
        |> List.head
        |> (fun x -> x.X)

    let maxY =
        positions
        |> List.sortByDescending (fun x -> x.Y)
        |> List.head
        |> (fun x -> x.Y)

    let bottomRight =
        positions
        |> List.find (fun x -> x.X = maxX && x.Y = maxY)

    shortest.[bottomRight]
//    1

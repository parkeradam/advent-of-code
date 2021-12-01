let numberIncreased (list: int list) : int =
    let rec loop acc listPart =
        match listPart with
        | [] -> acc
        | [ _ ] -> acc
        | head :: tail ->
            if (head < tail.Head) then
                loop (acc + 1) tail
            else
                loop acc tail

    loop 0 list

let slidingScale (list: int list) : int list =
    let rec loop acc (listPart: int list) =
        if listPart.Length >= 3 then
            let sum = listPart |> List.take 3 |> List.sum
            loop (sum :: acc) (listPart.Tail)
        else
            acc |> List.rev

    loop [] list

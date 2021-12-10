open System

type Either<'a, 'b> =
    | Left of 'a
    | Right of 'b

let isLeft =
    function
    | Left _ -> true
    | _ -> false

let isRight =
    function
    | Right _ -> true
    | _ -> false

let unwrapLeft =
    function
    | Left a -> a
    | _ -> raise (new Exception("Not a left"))

let unwrapRight =
    function
    | Right a -> a
    | _ -> raise (new Exception("Not a right"))

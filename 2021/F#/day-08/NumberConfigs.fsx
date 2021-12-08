type Segment =
    | Top
    | TopLeft
    | TopRight
    | Middle
    | BottomLeft
    | BottomRight
    | Bottom


let zero =
    [ Top
      TopLeft
      TopRight
      BottomLeft
      BottomRight
      Bottom ]

let one = [ TopRight; BottomRight ]

let two =
    [ Top
      TopRight
      Middle
      BottomLeft
      Bottom ]

let three =
    [ Top
      TopRight
      Middle
      BottomRight
      Bottom ]

let four =
    [ TopLeft
      TopRight
      Middle
      BottomRight ]

let five =
    [ Top
      TopLeft
      Middle
      BottomRight
      Bottom ]

let six =
    [ Top
      TopLeft
      Middle
      BottomLeft
      BottomRight
      Bottom ]

let seven = [ Top; TopRight; BottomLeft ]

let eight =
    [ Top
      TopLeft
      TopRight
      Middle
      BottomLeft
      BottomRight
      Bottom ]

let nine =
    [ Top
      TopLeft
      TopRight
      Middle
      BottomLeft
      Bottom ]

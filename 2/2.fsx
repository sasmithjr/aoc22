open System.IO

let rounds =
    File.ReadAllText($"{__SOURCE_DIRECTORY__}/input.txt").Trim().Split('\n')
    |> Array.map (fun cs ->
        let values = cs.Split(' ')
        char values[0], char values[1])

let getMoveScore (move: char) =
    match move with
    | 'A'
    | 'X' -> 1
    | 'B'
    | 'Y' -> 2
    | 'C'
    | 'Z' -> 3

type Outcome =
    | Lose
    | Draw
    | Win

    static member FromChar(c: char) =
        match c with
        | 'X' -> Lose
        | 'Y' -> Draw
        | 'Z' -> Win

let getOutcomePoints o =
    match o with
    | Lose -> 0
    | Draw -> 3
    | Win -> 6

let getWinnerScore (m1: char) (m2: char) =
    let m2 = int m2 - 23
    let result = m2 - int m1

    match result with
    | 0 -> Draw
    | 1
    | -2 -> Win
    | 2
    | -1 -> Lose
    |> getOutcomePoints

let getRoundScore (m1, m2) =
    (getWinnerScore m1 m2) + (getMoveScore m2)

let score1 = rounds |> Array.map getRoundScore |> Array.sum

printfn $"{score1}"

let getScoreGivenOutcome (m: char, o) =
    let outcome = Outcome.FromChar(o)
    let outcomeScore = outcome |> getOutcomePoints

    let moveScore =
        match outcome with
        | Win -> 1
        | Draw -> 0
        | Lose -> 2
        |> fun diff -> ((int m) - 65 + diff) % 3 + 65 |> char
        |> getMoveScore

    outcomeScore + moveScore

let score2 = rounds |> Array.map getScoreGivenOutcome |> Array.sum

printfn $"{score2}"

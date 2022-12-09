open System.IO

let calories =
    File.ReadAllText($"{__SOURCE_DIRECTORY__}/input.txt").Trim().Split("\n\n")
    |> Array.map (fun cs -> cs.Split("\n") |> Array.map int)

let topCals = calories |> Array.map Array.sum |> Array.max

printfn $"{topCals}"

let topThreeCals =
    calories
    |> Array.map Array.sum
    |> Array.sortDescending
    |> Array.take 3
    |> Array.sum

printfn $"{topThreeCals}"

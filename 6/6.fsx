open System.IO

let content = File.ReadAllText($"{__SOURCE_DIRECTORY__}/input.txt")

let findWindowedDistinctIndex size =
    content
    |> Seq.windowed size
    |> Seq.findIndex (fun cs -> cs = Array.distinct cs)
    |> (+) size

let part1 = findWindowedDistinctIndex 4
printfn $"{part1}"

let part2 = findWindowedDistinctIndex 14
printfn $"{part2}"

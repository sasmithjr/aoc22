open System.IO

let asRangeSet (range: string) =
    let split = range.Split("-")
    Set.ofArray [| (int split[0]) .. (int split[1]) |]

let ranges =
    File.ReadAllText($"{__SOURCE_DIRECTORY__}/input.txt").Split('\n')
    |> Array.map (fun pairLine ->
        let pairs = pairLine.Split(",")
        asRangeSet pairs[0], asRangeSet pairs[1])

let boolAsNum b = if b then 1 else 0

let isSubset (s1, s2) =
    (s1 |> Set.isSubset s2) || (s2 |> Set.isSubset s1)

let subsets = ranges |> Array.map isSubset |> Array.sumBy boolAsNum
printfn $"{subsets}"

let hasOverlap (s1, s2) =
    Set.intersect s1 s2 |> Set.isEmpty |> not

let overlaps = ranges |> Array.map hasOverlap |> Array.sumBy boolAsNum
printfn $"{overlaps}"

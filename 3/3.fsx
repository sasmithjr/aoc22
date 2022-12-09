let rucksacks =
    System
        .IO
        .File
        .ReadAllText($"{__SOURCE_DIRECTORY__}/input.txt")
        .Trim()
        .Split('\n')

let compartments =
    rucksacks
    |> Array.map (fun rucksack ->
        let compartments = Seq.splitInto 2 rucksack
        let first = compartments |> Seq.item 0 |> Set.ofSeq
        let second = compartments |> Seq.item 1 |> Set.ofSeq
        first, second)

let getPriority (c: char) =
    if c >= 'a' && c <= 'z' then 96 else 38
    |> fun offset -> int c - offset

let findSharedItem (s1, s2) = (Set.intersect s1 s2).MinimumElement

let prioritySum =
    compartments |> Array.map (findSharedItem >> getPriority) |> Array.sum

printfn $"{prioritySum}"

let groups =
    rucksacks
    |> Array.chunkBySize 3
    |> Array.map (fun group -> group |> Array.map Set.ofSeq)

let groupSum =
    groups
    |> Array.map (Set.intersectMany >> Set.minElement >> getPriority)
    |> Array.sum

printfn $"{groupSum}"

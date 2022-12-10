open System.IO

let buildStacks (stackChunk: string) =
    let lines = stackChunk.Split("\n")

    let chunkedLines =
        lines
        |> Array.map (Seq.chunkBySize 4 >> Array.ofSeq)
        |> fun lines -> Array.removeAt (lines.Length - 1) lines // Drop stack id row
        |> Array.rev //Start at bottom row so top of stack is lead of list

    let stacks = Array.init chunkedLines[0].Length (fun _ -> [])

    chunkedLines
    |> Array.iter (fun chunkedLine ->
        for column = 0 to chunkedLine.Length - 1 do
            let container = chunkedLine[column][1]

            if container <> ' ' then
                stacks[column] <- container :: stacks[column])

    stacks

let parseMoves (movesChunk: string) =
    movesChunk.Split("\n")
    |> Array.map (fun line ->
        let pieces = line.Split(' ')
        int pieces[1], int pieces[3], int pieces[5])

let printTopOfStacks (stacks: array<List<char>>) =
    stacks |> Array.map (fun s -> s.Head) |> System.String.Concat |> printfn "%s"

let stacks, moves =
    File.ReadAllText($"{__SOURCE_DIRECTORY__}/input.txt").Split("\n\n")
    |> fun splits -> buildStacks splits[0], parseMoves splits[1]

let part1 =
    let rec loop (stacks: array<List<char>>) remainingMoves (count, src, dest) =
        let src' = src - 1
        let dest' = dest - 1
        stacks[dest'] <- stacks[src'].Head :: stacks[dest']
        stacks[src'] <- stacks[src'].Tail

        match count, remainingMoves with
        | n, _ when n >= 2 -> loop stacks remainingMoves (n - 1, src, dest)
        | _, [||] -> stacks
        | _, _ -> loop stacks (Array.tail remainingMoves) remainingMoves[0]

    loop (Array.copy stacks) (Array.tail moves) moves[0]

printTopOfStacks part1

let part2 =
    (Array.copy stacks, moves)
    ||> Array.fold (fun acc (count, src, dest) ->
        let src' = src - 1
        let dest' = dest - 1
        let toMove, remaining = acc[src'] |> List.splitAt count
        acc[src'] <- remaining
        acc[dest'] <- toMove @ acc[dest']
        acc)

printTopOfStacks part2

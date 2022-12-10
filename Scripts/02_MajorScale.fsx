open System

module String =
    let joinWith (separator: string) (strings: string list) =
        String.Join(separator, strings)

// Note
type Note =
    | C | CSharp
    | D | DSharp
    | E
    | F | FSharp
    | G | GSharp
    | A | ASharp
    | B

module Note =
    let all = [
        C; CSharp
        D; DSharp
        E
        F; FSharp
        G; GSharp
        A; ASharp
        B
    ]

    let name note =
        match note with
        | CSharp -> "C#"
        | DSharp -> "D#"
        | FSharp -> "F#"
        | GSharp -> "G#"
        | ASharp -> "A#"
        | n -> string n

    let addSemitones note semitones =
        let index =
            all
            |> List.findIndex ((=) note)
        
        let newIndex = (index + semitones) % all.Length
        all[newIndex]

let (+) note semitones = Note.addSemitones note semitones

// Major scale
type MajorScale = MajorScale of Note
module MajorScale =
    let private intervals = [ 2; 2; 1; 2; 2; 2; 1 ]

    let notes (MajorScale root) =
        intervals
        |> List.take (intervals.Length - 1)
        |> List.scan Note.addSemitones root



// Get a random note
let random = Random()
let randomIndex = random.Next(0, Note.all.Length)
let randomNote = Note.all.[randomIndex]

// Show the note
randomNote
|> Note.name
|> printfn "Chosen note: %s"

// Show the notes of the scale
MajorScale randomNote
|> MajorScale.notes
|> List.map Note.name
|> String.joinWith ", "
|> printfn "Notes of the major scale: %s"


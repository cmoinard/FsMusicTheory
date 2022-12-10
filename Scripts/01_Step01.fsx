open System
    
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

let random = Random()
let randomIndex = random.Next(0, Note.all.Length)
let randomNote = Note.all.[randomIndex]

// Show the note
randomNote
|> Note.name
|> printfn "Chosen note: %s"
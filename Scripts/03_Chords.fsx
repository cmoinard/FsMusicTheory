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



// Chord
type ChordType =
    | Major
    | Minor
    | Diminished
module ChordType =
    let fromIntervals intervals =
        match intervals with
        | [4; 7] -> Major
        | [3; 7] -> Minor
        | [3; 6] -> Diminished
        | _ -> invalidOp "Unknown interval"

type Chord = Chord of Note * ChordType
module Chord =
    let name (Chord (root, chordType)) =
        let note = root |> Note.name
        match chordType with
        | Major -> note
        | Minor -> $"{note}m"
        | Diminished -> $"{note}°"



// Degree
type DegreeName = I | II | III | IV | V | VI | VII
module DegreeName =
    let all = [ I; II; III; IV; V; VI; VII ]

type Degree = Degree of DegreeName * ChordType
module Degree =
    let name (Degree (degreeName, chordType)) =
        let romanName = string degreeName
        match chordType with
        | Major -> romanName
        | Minor -> romanName.ToLower()
        | Diminished -> $"{romanName.ToLower()}°"



// Major scale
type MajorScale = MajorScale of Note
module MajorScale =
    let private intervals = [ 2; 2; 1; 2; 2; 2; 1 ]
    let private degrees = [
        Degree (I,   Major)
        Degree (II,  Minor)
        Degree (III, Minor)
        Degree (IV,  Major)
        Degree (V,   Major)
        Degree (VI,  Minor)
        Degree (VII, Diminished)
    ]

    let notes (MajorScale root) =
        intervals
        |> List.take (intervals.Length - 1)
        |> List.scan Note.addSemitones root
    
    let chords (scale: MajorScale): (Degree * Chord) list =
        let degreeChord degree note =
            let (Degree (_, chordType)) = degree
            degree, Chord (note, chordType)

        List.map2
            degreeChord
            degrees
            (notes scale)



// Get a random note
let random = Random()
let randomIndex = random.Next(0, Note.all.Length)
let randomNote = Note.all.[randomIndex]

// Show the note
randomNote
|> Note.name
|> printfn "Chosen note: %s"

let randomScale = MajorScale randomNote

// Show the notes of the scale
randomScale
|> MajorScale.notes
|> List.map Note.name
|> String.joinWith ", "
|> printfn "Notes of the major scale: %s"

// Show the chords of the scale
randomScale
|> MajorScale.chords
|> List.iter (fun (degree, chord) ->
    let degreeName = Degree.name degree
    let chordName = Chord.name chord
    printfn "%-4s: %s" degreeName chordName
)
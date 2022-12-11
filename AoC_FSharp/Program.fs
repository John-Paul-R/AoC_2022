// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

let printGreeting name =
    printfn $"Hello {name}!"

printGreeting "Alice"

let names = [ "Sam", "Steve", "Alice" ]

let names2 = [ "Sam"; "Steve"; "Alice" ]

printGreeting names

printGreeting names2

let pow (a, b) = a ** b

let myVal = pow 3 4
printfn "%d" myVal

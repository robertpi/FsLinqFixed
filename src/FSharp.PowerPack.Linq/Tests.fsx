#I @"bin\Debug"
#r "FSharp.PowerPack.Linq.Fixed.dll"
#r "System.Core.dll"
open System.Linq
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Linq.Query
 

let makeQueryable (items: seq<_>) = 
    let temp = new ResizeArray<_>(items)
    temp.AsQueryable()

let items1 = makeQueryable [ 1 .. 100 ]
let test1 = query <@  items1 |> Seq.skip 90  @>

let items2 = makeQueryable [ for x in 1 .. 100 do yield x / 10, x]
let test1 = query <@  items1 |> Seq.skip 90  @>

namespace FSharp.PowerPack.LinqTests.Tests
open NUnit.Framework
open FsUnit

open System.Linq
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Linq.Query


[<AutoOpen>]
module Helper =
    let makeQueryable (items: seq<_>) = 
        let temp = new ResizeArray<_>(items)
        temp.AsQueryable()


[<TestFixture>] 
type SkipTests ()=
    [<Test>]
    member test.``Skipping 90 in a list of 100 should produce list length 10`` ()=
            let items1 = makeQueryable [ 1 .. 100 ]
            let test1 = query <@  items1 |> Seq.skip 90  @> |> List.ofSeq
            test1.Length |> should equal 10


[<TestFixture>] 
type EqualsTests ()=
    [<Test>]
    member test.``Filter 90 integer`` ()=
            let items1 = makeQueryable [ 1 .. 100 ]
            let test1 = query <@  items1 |> Seq.filter (fun x -> x = 90)  @> |> Seq.toList
            test1.Length |> should equal 1
            test1.Head |> should equal 90

    [<Test>]
    member test.``Filter 90 string`` ()=
            let items1 = makeQueryable ([ 1 .. 100 ] |> Seq.map string)
            let test1 = query <@  items1 |> Seq.filter (fun x -> x = "90")  @> |> Seq.toList
            test1.Length |> should equal 1
            test1.Head |> should equal "90"

type Item<'a> = 
    {Item: 'a}

[<TestFixture>] 
type InequalsTests ()=
    [<Test>]
    member test.``Filter > 90`` ()=
            let items1 = makeQueryable ([ 1 .. 100 ] |> Seq.map (fun x -> {Item = x}))
            let test1 = query <@  items1 |> Seq.filter (fun x -> x.Item > 90)  @> |> Seq.toList
            test1.Length |> should equal 10
            test1.Head.Item |> should equal 91

    [<Test>]
    member test.``Filter >= 90`` ()=
            let items1 = makeQueryable ([ 1 .. 100 ] |> Seq.map (fun x -> {Item = x}))
            let test1 = query <@  items1 |> Seq.filter (fun x -> x.Item >= 90)  @> |> Seq.toList
            test1.Length |> should equal 11
            test1.Head.Item |> should equal 90
    [<Test>]
    member test.``Filter < 90`` ()=
            let items1 = makeQueryable ([ 1 .. 100 ] |> Seq.map (fun x -> {Item = x}))
            let test1 = query <@  items1 |> Seq.filter (fun x -> x.Item < 90)  @> |> Seq.toList
            test1.Length |> should equal 89
            test1.Head.Item |> should equal 1

    [<Test>]
    member test.``Filter <= 90`` ()=
            let items1 = makeQueryable ([ 1 .. 100 ] |> Seq.map (fun x -> {Item = x}))
            let test1 = query <@  items1 |> Seq.filter (fun x -> x.Item <= 90)  @> |> Seq.toList
            test1.Length |> should equal 90
            test1.Head.Item |> should equal 1

    [<Test>]
    member test.``Filter <> 90`` ()=
            let items1 = makeQueryable ([ 1 .. 100 ] |> Seq.map (fun x -> {Item = x}))
            let test1 = query <@  items1 |> Seq.filter (fun x -> x.Item <> 90)  @> |> Seq.toList
            test1.Length |> should equal 99
            test1.Head.Item |> should equal 1

    [<Test>]
    member test.``Filter <> 90 string`` ()=
            let items1 = makeQueryable ([ 1 .. 100 ] |> Seq.map string |> Seq.map (fun x -> {Item = x}))
            let test1 = query <@  items1 |> Seq.filter (fun x -> x.Item <> "90")  @> |> Seq.toList
            test1.Length |> should equal 99
            test1.Head.Item |> should equal "1"

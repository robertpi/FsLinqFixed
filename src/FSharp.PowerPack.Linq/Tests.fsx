#I @"bin\Debug"
#r "FSharp.PowerPack.Linq.Fixed.dll"
#r "System.Core.dll"
open System.Linq
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Linq.Query
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Linq.QuotationEvaluation
open System.Linq.Expressions
open System

System.Diagnostics.Debugger.Launch()

let makeQueryable (items: seq<_>) = 
    let temp = new ResizeArray<_>(items)
    temp.AsQueryable()

let items1 = makeQueryable [ 1 .. 100 ]
let test1 = query <@  items1 |> Seq.skip 90  @>

let items2 = makeQueryable [ for x in 1 .. 100 do yield x / 10, x]
//let test1 = query <@  items1 |> Seq.skip 90  @>

let items3 = makeQueryable ["sam"; "tom"; "john"]

let test3 = 
  let ToLinq (exp : Expr<'a -> 'b>) =
    let linq = exp.ToLinqExpression()
    let call = linq :?> MethodCallExpression
    let lambda = call.Arguments.[0] :?> LambdaExpression
    Expression.Lambda<Func<'a, 'b>>(lambda.Body, lambda.Parameters)
  let convert = ToLinq (<@ fun u -> u = "sam" @>)
  convert.Body


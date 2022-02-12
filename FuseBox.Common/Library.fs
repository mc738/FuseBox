namespace FuseBox.Common

open System.Net.Http

module Transactions =

    type TaskResultBuilder() =

        member _.Bind(x, f) =
            x
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> Result.bind f
        
        member _.Return(x) = x
            
        member _.ReturnFrom(x) =
            x
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> fun r ->
                match r with
                | Ok v -> Ok v
                | Error e -> Error $"Transaction failed. Error: {e}"
            
    let taskResult = TaskResultBuilder()


    






module Say =
    let hello name = printfn "Hello %s" name

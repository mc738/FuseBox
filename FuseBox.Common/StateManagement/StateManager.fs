namespace FuseBox.StateManagement

open Freql.Sqlite
open FuseBox.StateManagement.Persistence

module Internal =
    ()

type FetchResult<'T> =
    | Exists of 'T
    | NotFound
    | Failure of string

type StateManager(ctx: SqliteContext) =

    member _.Context = ctx
    
    member _.GetUserMetadataValue(userId: string, key: string) =
        Operations.selectProjectMetadataItemRecord ctx [ "WHERE user_id = @0 AND data_key = @1" ] [ userId; key ]
        |> Option.bind (fun md -> Some md.DataValue)

    member _.GetProjectMetadata(projectId: string, key: string) =
        Operations.selectProjectMetadataItemRecord ctx [ "WHERE project_id = @0 AND data_key = @1" ] [ projectId; key ]
        |> Option.bind (fun md -> Some md.DataValue)

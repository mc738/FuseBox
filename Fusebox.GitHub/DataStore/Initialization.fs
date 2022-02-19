namespace FuseBox.GitHub.DataStore

open System
open Freql.Sqlite

module Initialization =


    open FuseBox.Common.DataStore

    let registerHandlers (ctx: SqliteContext) =
        [ "github-issue", 1L ]
        |> List.map (fun (name, version) -> RegisteredHandlers.add ctx name version)
        |> ignore

    let registerTypes (ctx: SqliteContext) =
        [ "github-issue", "github-issue" ]
        |> List.map (fun (name, handlerName) -> RegisteredTypes.add ctx name handlerName)
        |> ignore

    let addActionTypes (ctx: SqliteContext) =
        [ "github-issue_a-create", "create", "github-issue", [||]
          "github-issue_a-update", "update", "github-issue", [||]
          "github-issue_a-close", "close", "github-issue", [||] ]
        |> List.map
            (fun (id, name, parentTypeId, serializedTypeBlob) ->
                ActionTypes.add ctx id name parentTypeId serializedTypeBlob)
        |> ignore

    let addConditionTypes (ctx: SqliteContext) =
        [ "github-issue_c-is_closed", "is_closed", "github-issue", [||]
          "github-issue_c-is_open", "is_open", "github-issue", [||]
          "github-issue_c-is_assigned", "is_assigned", "github-issue", [||]
          "github-issue_c-has_tag", "has_tag", "github-issue", [||] ]
        |> List.map
            (fun (id, name, parentTypeId, serializedTypeBlob) ->
                ConditionTypes.add ctx id name parentTypeId serializedTypeBlob)
        |> ignore


    let addEventTypes (ctx: SqliteContext) =
        [ "github-issue_e-created", "created", "github-issue", [||]
          "github-issue_e-assigned", "assigned", "github-issue", [||]
          "github-issue_e-updated", "updated", "github-issue", [||]
          "github-issue_e-closed", "closed", "github-issue", [||] ]
        |> List.map
            (fun (id, name, parentTypeId, serializedTypeBlob) ->
                EventTypes.add ctx id name parentTypeId serializedTypeBlob)
        |> ignore

    let seed =
        [ registerHandlers
          registerTypes
          addActionTypes
          addConditionTypes
          addEventTypes ]

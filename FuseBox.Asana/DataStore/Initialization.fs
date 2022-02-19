namespace FuseBox.Asana.DataStore

open System
open Freql.Sqlite

module Initialization =

    open FuseBox.Common.DataStore

    let registerHandlers (ctx: SqliteContext) =
        [ "asana-task", 1L ]
        |> List.map (fun (name, version) -> RegisteredHandlers.add ctx name version)
        |> ignore

    let registerTypes (ctx: SqliteContext) =
        [ "asana-task", "asana-task" ]
        |> List.map (fun (name, handlerName) -> RegisteredTypes.add ctx name handlerName)
        |> ignore

    let addActionTypes (ctx: SqliteContext) =
        [ "asana-task_a-create", "create", "asana-task", [||]
          "asana-task_a-update", "update", "asana-task", [||]
          "asana-task_a-close", "close", "asana-task", [||] ]
        |> List.map
            (fun (id, name, parentTypeId, serializedTypeBlob) ->
                ActionTypes.add ctx id name parentTypeId serializedTypeBlob)
        |> ignore

    let addConditionTypes (ctx: SqliteContext) =
        [ "asana-task_c-is_closed", "is_closed", "asana-task", [||]
          "asana-task_c-is_open", "is_open", "asana-task", [||]
          "asana-task_c-is_assigned", "is_assigned", "asana-task", [||]
          "asana-task_c-has_tag", "has_tag", "asana-task", [||] ]
        |> List.map
            (fun (id, name, parentTypeId, serializedTypeBlob) ->
                ConditionTypes.add ctx id name parentTypeId serializedTypeBlob)
        |> ignore


    let addEventTypes (ctx: SqliteContext) =
        [ "asana-task_e-created", "created", "asana-task", [||]
          "asana-task_e-assigned", "assigned", "asana-task", [||]
          "asana-task_e-updated", "updated", "asana-task", [||]
          "asana-task_e-closed", "closed", "asana-task", [||] ]
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

namespace FuseBox.Common.DataStore

open System
open System.IO
open Freql.Core.Common.Types
open Freql.Sqlite
open FuseBox.Common.DataStore.Persistence
open Microsoft.FSharp.Core


[<RequireQualifiedAccess>]
module Initialization =

    let run (ctx: SqliteContext) (seedFns: (SqliteContext -> unit) list) =
        //ctx.ExecuteInTransaction
        //    (fun t ->
        // Create the tables.
        [ Records.Project.CreateTableSql()
          Records.ProjectMetadataItem.CreateTableSql()
          Records.User.CreateTableSql()
          Records.UserMetadataItem.CreateTableSql()
          Records.RegisteredHandler.CreateTableSql()
          Records.RegisteredType.CreateTableSql()
          Records.Item.CreateTableSql()
          Records.ItemState.CreateTableSql()
          Records.ItemMetadataItem.CreateTableSql()
          Records.Resource.CreateTableSql()
          Records.ResourceVersion.CreateTableSql()
          Records.ResourceMetadataItem.CreateTableSql()
          Records.Link.CreateTableSql()
          Records.LinkMetadataItem.CreateTableSql()
          Records.ProjectItemLink.CreateTableSql()
          Records.ProjectItemLinkMetadataItem.CreateTableSql()
          Records.ProjectResourceLink.CreateTableSql()
          Records.ProjectResourceLinkMetadataItem.CreateTableSql()
          Records.ItemResourceLink.CreateTableSql()
          Records.ItemResourceLinkMetadataItem.CreateTableSql()
          Records.ActionType.CreateTableSql()
          Records.ConditionType.CreateTableSql()
          Records.EventType.CreateTableSql()
          Records.Workflow.CreateTableSql()
          Records.WorkflowActionType.CreateTableSql()
          Records.WorkflowEventType.CreateTableSql()
          Records.WorkflowConditionType.CreateTableSql()
          Records.WorkflowEventAction.CreateTableSql()
          Records.WorkflowEvent.CreateTableSql()
          Records.WorkflowAction.CreateTableSql() ]
        |> List.map ctx.ExecuteSqlNonQuery
        |> ignore
        // Seed the database.
        seedFns |> List.map (fun fn -> fn ctx) |> ignore //)
        Ok ()

[<RequireQualifiedAccess>]
module RegisteredHandlers =

    let add ctx name version =
        ({ Name = name; Version = version }: Parameters.NewRegisteredHandler)
        |> Operations.insertRegisteredHandler ctx

    let getByName (ctx: SqliteContext) (name: string) =
        Operations.selectRegisteredHandlerRecord ctx [ "WHERE name = @0" ] [ name ]

    let getAll (ctx: SqliteContext) =
        Operations.selectRegisteredHandlerRecords ctx [] []


[<RequireQualifiedAccess>]
module RegisteredTypes =

    let add ctx name handlerName =
        ({ Name = name
           HandlerName = handlerName }: Parameters.NewRegisteredType)
        |> Operations.insertRegisteredType ctx

    let getByName (ctx: SqliteContext) (name: string) =
        Operations.selectRegisteredTypeRecord ctx [ "WHERE name = @0" ] [ name ]

    let getAll (ctx: SqliteContext) =
        Operations.selectRegisteredTypeRecords ctx [] []


[<RequireQualifiedAccess>]
module Projects =

    let add (ctx: SqliteContext) (id: string) (name: string) =
        ({ Id = id; Name = name }: Parameters.NewProject)
        |> Operations.insertProject ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectProjectRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectProjectRecords ctx [] []

    let addMetadata (ctx: SqliteContext) (projectId: string) (key: string) (value: string) =
        ({ ProjectId = projectId
           DataKey = key
           DataValue = value }: Parameters.NewProjectMetadataItem)
        |> Operations.insertProjectMetadataItem ctx

    let getMetadata (ctx: SqliteContext) (projectId: string) =
        Operations.selectProjectMetadataItemRecords ctx [ "WHERE project_id = @0" ] [ projectId ]
        |> List.map (fun mdi -> mdi.DataKey, mdi.DataValue)
        |> Map.ofList

    let getMetadataValue (ctx: SqliteContext) (projectId: string) (key: string) =
        Operations.selectProjectMetadataItemRecord ctx [ "WHERE project_id = @0 AND data_key = @1" ] [ projectId; key ]
        |> Option.bind (fun md -> Some md.DataValue)

[<RequireQualifiedAccess>]
module Users =

    let add ctx id username displayName =
        ({ Id = id
           Username = username
           DisplayName = displayName }: Parameters.NewUser)
        |> Operations.insertUser ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectUserRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) = Operations.selectUserRecords ctx [] []

    let addMetadata ctx userId key value =
        ({ UserId = userId
           DataKey = key
           DataValue = value }: Parameters.NewUserMetadataItem)
        |> Operations.insertUserMetadataItem ctx

    let getMetadata ctx userId =
        Operations.selectUserMetadataItemRecords ctx [ "WHERE user_id = @0" ] [ userId ]
        |> List.map (fun imd -> imd.DataKey, imd.DataValue)
        |> Map.ofList

    let getMetadataValue ctx userId key =
        Operations.selectProjectMetadataItemRecord ctx [ "WHERE user_id = @0 AND data_key = @1" ] [ userId; key ]
        |> Option.bind (fun md -> Some md.DataValue)

[<RequireQualifiedAccess>]
module Items =

    let add ctx id referenceId referenceType =
        ({ Id = id
           ReferenceId = referenceId
           ReferenceType = referenceType }: Parameters.NewItem)
        |> Operations.insertItem ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectItemRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) = Operations.selectItemRecords ctx [] []

    let addMetadata ctx itemId key value =
        ({ ItemId = itemId
           DataKey = key
           DataValue = value }: Parameters.NewItemMetadataItem)
        |> Operations.insertItemMetadataItem ctx

    let getMetadata ctx itemId =
        Operations.selectItemMetadataItemRecords ctx [ "WHERE item_id = @0" ] [ itemId ]
        |> List.map (fun imd -> imd.DataKey, imd.DataValue)
        |> Map.ofList

    let getMetaDataValue ctx itemId key =
        Operations.selectItemMetadataItemRecord ctx [ "WHERE item_id = @0 AND data_key = @1" ] [ itemId; key ]
        |> Option.bind (fun md -> Some md.DataValue)

[<RequireQualifiedAccess>]
module Resources =

    let add ctx id name =
        ({ Id = id; Name = name }: Parameters.NewResource)
        |> Operations.insertResource ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectResourceRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectResourceRecords ctx [] []

    let addMetadata ctx resourceId key value =
        ({ ResourceId = resourceId
           DataKey = key
           DataValue = value }: Parameters.NewResourceMetadataItem)
        |> Operations.insertResourceMetadataItem ctx

    let getMetadata ctx itemId =
        Operations.selectResourceMetadataItemRecords ctx [ "WHERE resource_id = @0" ] [ itemId ]
        |> List.map (fun imd -> imd.DataKey, imd.DataValue)
        |> Map.ofList

    let getMetaDataValue ctx itemId key =
        Operations.selectResourceMetadataItemRecord ctx [ "WHERE resource_id = @0 AND data_key = @1" ] [ itemId; key ]
        |> Option.bind (fun md -> Some md.DataValue)

[<RequireQualifiedAccess>]
module Links =

    let add ctx id fromId toId twoWay =
        ({ Id = id
           FromId = fromId
           ToId = toId
           TwoWay = twoWay }: Parameters.NewLink)
        |> Operations.insertLink ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectLinkRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) = Operations.selectLinkRecords ctx [] []

    let addMetadata ctx linkId key value =
        ({ LinkId = linkId
           DataKey = key
           DataValue = value }: Parameters.NewLinkMetadataItem)
        |> Operations.insertLinkMetadataItem ctx

    let getMetadata ctx itemId =
        Operations.selectLinkMetadataItemRecords ctx [ "WHERE link_id = @0" ] [ itemId ]
        |> List.map (fun imd -> imd.DataKey, imd.DataValue)
        |> Map.ofList

    let getMetaDataValue ctx itemId key =
        Operations.selectLinkMetadataItemRecord ctx [ "WHERE link_id = @0 AND data_key = @1" ] [ itemId; key ]
        |> Option.bind (fun md -> Some md.DataValue)

[<RequireQualifiedAccess>]
module ProjectItemLinks =

    let add ctx id projectId itemId twoWay =
        ({ Id = id
           ProjectId = projectId
           ItemId = itemId }: Parameters.NewProjectItemLink)
        |> Operations.insertProjectItemLink ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectProjectItemLinkRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectProjectItemLinkRecords ctx [] []

    let addMetadata ctx linkId key value =
        ({ LinkId = linkId
           DataKey = key
           DataValue = value }: Parameters.NewProjectItemLinkMetadataItem)
        |> Operations.insertProjectItemLinkMetadataItem ctx

    let getMetadata ctx itemId =
        Operations.selectProjectItemLinkMetadataItemRecords ctx [ "WHERE link_id = @0" ] [ itemId ]
        |> List.map (fun imd -> imd.DataKey, imd.DataValue)
        |> Map.ofList

    let getMetaDataValue ctx itemId key =
        Operations.selectProjectItemLinkMetadataItemRecord
            ctx
            [ "WHERE link_id = @0 AND data_key = @1" ]
            [ itemId; key ]
        |> Option.bind (fun md -> Some md.DataValue)

[<RequireQualifiedAccess>]
module ProjectResourceLinks =

    let add ctx id projectId resourceId =
        ({ Id = id
           ProjectId = projectId
           ResourceId = resourceId }: Parameters.NewProjectResourceLink)
        |> Operations.insertProjectResourceLink ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectProjectResourceLinkRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectProjectResourceLinkRecords ctx [] []

    let addMetadata ctx linkId key value =
        ({ LinkId = linkId
           DataKey = key
           DataValue = value }: Parameters.NewProjectResourceLinkMetadataItem)
        |> Operations.insertProjectResourceLinkMetadataItem ctx

    let getMetadata ctx itemId =
        Operations.selectProjectResourceLinkMetadataItemRecords ctx [ "WHERE link_id = @0" ] [ itemId ]
        |> List.map (fun imd -> imd.DataKey, imd.DataValue)
        |> Map.ofList

    let getMetaDataValue ctx itemId key =
        Operations.selectProjectResourceLinkMetadataItemRecord
            ctx
            [ "WHERE link_id = @0 AND data_key = @1" ]
            [ itemId; key ]
        |> Option.bind (fun md -> Some md.DataValue)

[<RequireQualifiedAccess>]
module ItemResourceLinks =

    let add ctx id itemId resourceId =
        ({ Id = id
           ItemId = itemId
           ResourceId = resourceId }: Parameters.NewItemResourceLink)
        |> Operations.insertItemResourceLink ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectItemResourceLinkRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectItemResourceLinkRecords ctx [] []

    let addMetadata ctx linkId key value =
        ({ LinkId = linkId
           DataKey = key
           DataValue = value }: Parameters.NewItemResourceLinkMetadataItem)
        |> Operations.insertItemResourceLinkMetadataItem ctx

    let getMetadata ctx itemId =
        Operations.selectItemResourceLinkMetadataItemRecords ctx [ "WHERE link_id = @0" ] [ itemId ]
        |> List.map (fun imd -> imd.DataKey, imd.DataValue)
        |> Map.ofList

    let getMetaDataValue ctx itemId key =
        Operations.selectItemResourceLinkMetadataItemRecord
            ctx
            [ "WHERE link_id = @0 AND data_key = @1" ]
            [ itemId; key ]
        |> Option.bind (fun md -> Some md.DataValue)

[<RequireQualifiedAccess>]
module ActionTypes =

    let add ctx id name parentTypeId (serializedTypeBlob: byte []) =
        use ms = new MemoryStream(serializedTypeBlob)

        ({ Id = id
           Name = name
           ParentType = parentTypeId
           TypeBlob = BlobField.FromStream ms }: Parameters.NewActionType)
        |> Operations.insertActionType ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectActionTypeRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectActionTypeRecords ctx [] []

[<RequireQualifiedAccess>]
module EventTypes =

    let add ctx id name parentTypeId (serializedTypeBlob: byte []) =
        use ms = new MemoryStream(serializedTypeBlob)

        ({ Id = id
           Name = name
           ParentType = parentTypeId
           TypeBlob = BlobField.FromStream ms }: Parameters.NewEventType)
        |> Operations.insertEventType ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectEventTypeRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectEventTypeRecords ctx [] []

[<RequireQualifiedAccess>]
module ConditionTypes =

    let add ctx id name parentTypeId (serializedTypeBlob: byte []) =
        use ms = new MemoryStream(serializedTypeBlob)

        ({ Id = id
           Name = name
           ParentType = parentTypeId
           TypeBlob = BlobField.FromStream ms }: Parameters.NewConditionType)
        |> Operations.insertConditionType ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectConditionTypeRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectConditionTypeRecords ctx [] []

[<RequireQualifiedAccess>]
module Workflows =

    let add ctx id name =
        ({ Id = id; Name = name }: Parameters.NewWorkflow)
        |> Operations.insertWorkflow ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectWorkflowRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectWorkflowRecords ctx [] []


[<RequireQualifiedAccess>]
module WorkflowActions =

    let add ctx id eventId typeId status complete =
        ({ EventId = eventId
           TypeId = typeId
           Status = status
           Complete = complete
           ActionTimestamp = Some DateTime.UtcNow }: Parameters.NewWorkflowAction)
        |> Operations.insertWorkflowAction ctx

    let getByEvent (ctx: SqliteContext) (eventId: string) =
        Operations.selectWorkflowRecords ctx [ "WHERE event_id = @0" ] [ eventId ]

    let getByType (ctx: SqliteContext) (typeId: string) =
        Operations.selectWorkflowRecords ctx [ "WHERE type_id = @0" ] [ typeId ]

    let getByEventAndType (ctx: SqliteContext) (eventId: string) (typeId: string) =
        Operations.selectWorkflowRecord ctx [ "WHERE event_id = @0 AND type_id = @1" ] [ eventId; typeId ]

    let getAll (ctx: SqliteContext) =
        Operations.selectWorkflowRecords ctx [] []


[<RequireQualifiedAccess>]
module WorkflowActionTypes =

    let add ctx id workflowId actionTypeId name =
        ({ Id = id
           WorkflowId = workflowId
           ActionTypeId = actionTypeId
           Name = name }: Parameters.NewWorkflowActionType)
        |> Operations.insertWorkflowActionType ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectWorkflowActionTypeRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectWorkflowActionTypeRecords ctx [] []


[<RequireQualifiedAccess>]
module WorkflowConditionsTypes =

    let add ctx id workflowId conditionTypeId name =
        ({ Id = id
           WorkflowId = workflowId
           ConditionTypeId = conditionTypeId
           Name = name }: Parameters.NewWorkflowConditionType)
        |> Operations.insertWorkflowConditionType ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectWorkflowConditionTypeRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectWorkflowConditionTypeRecords ctx [] []

[<RequireQualifiedAccess>]
module WorkflowEvents =

    let add ctx id typeId itemId (serializedOpsBlob: byte array) =
        use ms = new MemoryStream(serializedOpsBlob)

        ({ Id = id
           TypeId = typeId
           ItemId = itemId
           OpsBlob = BlobField.FromStream ms
           EventTimestamp = DateTime.UtcNow }: Parameters.NewWorkflowEvent)
        |> Operations.insertWorkflowEvent ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectWorkflowEventRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectWorkflowEventRecords ctx [] []

[<RequireQualifiedAccess>]
module WorkflowEventTypes =

    let add ctx id workflowId eventTypeId name =
        ({ Id = id
           WorkflowId = workflowId
           EventTypeId = eventTypeId
           Name = name }: Parameters.NewWorkflowEventType)
        |> Operations.insertWorkflowEventType ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectWorkflowEventTypeRecord ctx [ "WHERE id = @0" ] [ id ]

    let getAll (ctx: SqliteContext) =
        Operations.selectWorkflowEventTypeRecords ctx [] []

[<RequireQualifiedAccess>]
module WorkflowEventActions =

    let add ctx id eventId actionId conditionIdOption =
        ({ Id = id
           EventId = eventId
           ActionId = actionId
           ConditionId = conditionIdOption
           Active = true }: Parameters.NewWorkflowEventAction)
        |> Operations.insertWorkflowEventAction ctx

    let getById (ctx: SqliteContext) (id: string) =
        Operations.selectWorkflowEventActionRecord ctx [ "WHERE id = @0" ] [ id ]

    let getByEventId (ctx: SqliteContext) (eventId: string) =
        Operations.selectWorkflowEventActionRecords ctx [ "WHERE event_id = @0" ] [ eventId ]

    let getAll (ctx: SqliteContext) =
        Operations.selectWorkflowEventActionRecords ctx [] []

type FetchResult<'T> =
    | Exists of 'T
    | NotFound
    | Failure of string

type FuseBoxDataStore(ctx: SqliteContext) =

    static member Initialize(path: string) =

        match File.Exists path with
        | true -> SqliteContext.Open path
        | false ->
            let ctx = SqliteContext.Create(path)

            [ Records.Project.CreateTableSql()
              Records.ProjectMetadataItem.CreateTableSql()
              Records.User.CreateTableSql()
              Records.UserMetadataItem.CreateTableSql()
              Records.RegisteredHandler.CreateTableSql()
              Records.RegisteredType.CreateTableSql()
              Records.Item.CreateTableSql()
              Records.ItemState.CreateTableSql()
              Records.ItemMetadataItem.CreateTableSql()
              Records.Link.CreateTableSql()
              Records.LinkMetadataItem.CreateTableSql() ]
            |> List.map ctx.ExecuteSqlNonQuery
            |> ignore

            ctx
        |> FuseBoxDataStore

    member _.Context = ctx

    // Projects
    member _.AddProject(id, name) = Projects.add ctx id name

    member _.AddProjectMetadata(projectId: string, key: string, value: string) =
        Projects.addMetadata ctx projectId key value

    member _.GetProjectMetadataValue(projectId: string, key: string) =
        Projects.getMetadataValue ctx projectId key

    // Users
    member _.AddUser(id, username, displayName) = Users.add ctx id username displayName

    member _.AddUserMetadata(userId: string, key: string, value: string) = Users.addMetadata ctx userId key value

    member _.GetUserMetadata(userId: string) = Users.getMetadata ctx userId

    member _.GetUserMetadataValue(userId: string, key: string) = Users.getMetadataValue ctx userId key

    // Items
    member _.AddItem(id: string, referenceId: string, referenceType: string) =
        Items.add ctx id referenceId referenceType

    member _.AddItemMetadata(itemId: string, key: string, value: string) = Items.addMetadata ctx itemId key value

    member _.GetItemMetadata(itemId: string) = Items.getMetadata ctx itemId

    member _.GetItemMetadataValue(itemId: string, key: string) = Items.getMetaDataValue ctx itemId key

    // Links
    member _.AddLink(id: string, fromId: string, toId: string, twoWay: bool) =
        ({ Id = id
           FromId = fromId
           ToId = toId
           TwoWay = twoWay }: Parameters.NewLink)
        |> Operations.insertLink ctx

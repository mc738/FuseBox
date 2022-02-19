namespace FuseBox.Asana

open System
open System.IO
open System.Text
open System.Text.Json
open Freql.Core.Common.Types
open Microsoft.FSharp.Core

module V1 =
    
    open System
    open System.Net.Http
    open System.Text.Json.Serialization
    open System.Threading.Tasks
    open ToolBox.Core
    
    module Domain =

        [<CLIMutable>]
        type ResourceReference =
            { [<JsonPropertyName("gid")>]
              Gid: string
              [<JsonPropertyName("name")>]
              Name: string
              [<JsonPropertyName("resource_type")>]
              ResourceType: string }

        [<CLIMutable>]
        type WorkspaceOverviewResponse =
            { [<JsonPropertyName("data")>]
              Data: ResourceReference seq }

        [<CLIMutable>]
        type ProjectOverviewResponse =
            { [<JsonPropertyName("data")>]
              Data: ResourceReference seq }

        [<CLIMutable>]
        type TaskOverviewResponse =
            { [<JsonPropertyName("data")>]
              Data: ResourceReference seq }

        [<CLIMutable>]
        type TaskDetailsResponse =
            { [<JsonPropertyName("data")>]
              Data: TaskDetails }

        and [<CLIMutable>] TaskDetails =
            { [<JsonPropertyName("gid")>]
              Gid: string
              [<JsonPropertyName("assignee")>]
              Assignee: ResourceReference
              [<JsonPropertyName("assignee_status")>]
              AssigneeStatus: string
              [<JsonPropertyName("complete")>]
              Complete: bool
              [<JsonPropertyName("completed_at")>]
              CompletedAt: Nullable<DateTime>
              [<JsonPropertyName("created_at")>]
              CreatedAt: DateTime
              [<JsonPropertyName("due_at")>]
              DueAt: Nullable<DateTime>
              [<JsonPropertyName("due_on")>]
              DueOn: Nullable<DateTime>
              [<JsonPropertyName("followers")>]
              Followers: ResourceReference seq
              [<JsonPropertyName("hearted")>]
              Hearted: bool
              [<JsonPropertyName("hearts")>]
              Hearts: UserReference seq
              [<JsonPropertyName("liked")>]
              Liked: bool
              [<JsonPropertyName("likes")>]
              Likes: UserReference seq
              [<JsonPropertyName("memberships")>]
              Memberships: Memberships seq
              [<JsonPropertyName("modified_at")>]
              ModifiedAt: DateTime
              [<JsonPropertyName("name")>]
              Name: string
              [<JsonPropertyName("notes")>]
              Notes: string
              [<JsonPropertyName("num_hearts")>]
              NumHearts: int
              [<JsonPropertyName("num_likes")>]
              NumLikes: int
              [<JsonPropertyName("parent")>]
              Parent: ResourceReference
              [<JsonPropertyName("permalink_url")>]
              PermalinkUrl: string
              [<JsonPropertyName("projects")>]
              Projects: ResourceReference seq
              [<JsonPropertyName("resource_type")>]
              ResourceType: string
              [<JsonPropertyName("started_at")>]
              StartedAt: Nullable<DateTime>
              [<JsonPropertyName("started_on")>]
              StartedOn: Nullable<DateTime>
              [<JsonPropertyName("tags")>]
              Tags: ResourceReference seq
              [<JsonPropertyName("resource_subtype")>]
              ResourceSubtype: string
              [<JsonPropertyName("workspace")>]
              Workspace: ResourceReference }


        and [<CLIMutable>] UserReference =
            { [<JsonPropertyName("gid")>]
              Gid: string
              [<JsonPropertyName("user")>]
              User: ResourceReference }

        and [<CLIMutable>] Memberships =
            { [<JsonPropertyName("project")>]
              Project: ResourceReference
              [<JsonPropertyName("section")>]
              Section: ResourceReference }

        [<CLIMutable>]
        type AddBasicAssignedTaskRequest =
            { [<JsonPropertyName("data")>]
              Data: AddBasicAssignedTask }

        and [<CLIMutable>] AddBasicAssignedTask =
            { [<JsonPropertyName("approval_status")>]
              ApprovalStatus: string
              [<JsonPropertyName("assignee")>]
              Assignee: string
              [<JsonPropertyName("assignee_status")>]
              AssigneeStatus: string
              [<JsonPropertyName("completed")>]
              Completed: bool
              [<JsonPropertyName("followers")>]
              Followers: string seq
              [<JsonPropertyName("html_notes")>]
              HtmlNotes: string
              [<JsonPropertyName("liked")>]
              Liked: bool
              [<JsonPropertyName("name")>]
              Name: string
              [<JsonPropertyName("notes")>]
              Notes: string
              [<JsonPropertyName("projects")>]
              Projects: string seq }

            static member Create(project: string, name: string, htmlNotes: string,  assignee: string) =
                { ApprovalStatus = "pending"
                  Assignee = assignee
                  AssigneeStatus = "upcoming"
                  Completed = false
                  Followers = [ assignee ]
                  HtmlNotes = htmlNotes
                  Liked = false
                  Name = name
                  Notes = ""
                  Projects = [ project ] }

    open Domain
    
    module Core =

        open Domain

        let handleResult<'T> (result: Http.RequestResult<'T>) =
            match result with
            | Http.RequestResult.Success v -> Ok v
            | Http.RequestResult.Failure f -> Error $"STATUS: {f.StatusCode} - {f.Message}"
            | Http.RequestResult.DeserializationError failure -> Error $"Deserializer error - {failure.Message}"
            | Http.RequestResult.Exception exn -> Error $"Unhandled exception - {exn.Message}"


        let handleTaskResult<'T> (result: Task<Http.RequestResult<'T>>) =
            result
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> handleResult

        let setToken (token: string) (client: HttpClient) = Http.setBearerToken token client

        let getWorkspaces (client: HttpClient) =
            Http.getAsync<WorkspaceOverviewResponse> "https://app.asana.com/api/1.0/workspaces" client
            |> handleTaskResult

        let getProjects (client: HttpClient) =
            Http.getAsync<ProjectOverviewResponse> "https://app.asana.com/api/1.0/projects" client
            |> handleTaskResult

        let getProjectTasks (projectId: string) (client: HttpClient) =
            Http.getAsync<TaskOverviewResponse> $"https://app.asana.com/api/1.0/tasks?project={projectId}" client
            |> handleTaskResult

        let getTaskDetails (taskId: string) (client: HttpClient) =
            Http.getAsync<TaskDetailsResponse> $"https://app.asana.com/api/1.0/tasks/{taskId}" client
            |> handleTaskResult

        let addBasicAssignedTask (assignedTask: AddBasicAssignedTaskRequest) (client: HttpClient) =
            Http.postAndReplyJsonAsync<AddBasicAssignedTaskRequest, TaskDetailsResponse>
                "https://app.asana.com/api/1.0/tasks/"
                client
                assignedTask

    (*
    [<AutoOpen>]
    module Extensions =
        open Core
        open FuseBox.StateManagement
        open FuseBox.Asana.StateManagement.Persistence
        open Domain
        
        type StateManager with

            member sm.FetchAsanaTask(taskId: string) =
                Operations.selectAsanaTaskRecord sm.Context [ "WHERE gid = @0" ] [ taskId ]

            member sm.InsertAsanaTask(taskDetails: TaskDetails) =
                sm.Context.ExecuteInTransaction(fun t ->
                    use taskBlob = new MemoryStream(JsonSerializer.Serialize taskDetails |> Encoding.UTF8.GetBytes)
                    
                    // Insert the task.
                    ({
                        Gid = taskDetails.Gid
                        OriginalName = taskDetails.Name
                        CurrentName = taskDetails.Name
                        CreatedOn = taskDetails.CreatedAt
                        LastModified = taskDetails.ModifiedAt
                        Active = true
                        CompletedOn = None
                        Serial = 1L
                    }: Parameters.NewAsanaTask)
                    |> Operations.insertAsanaTask t
                    
                    ({
                        TaskId = taskDetails.Gid
                        ExtractedOn = DateTime.UtcNow
                        Serial = 1L
                        TaskBlob = BlobField.FromStream taskBlob
                    }: Parameters.NewAsanaTaskBlob)
                    |> Operations.insertAsanaTaskBlob t)
                
            member sm.UpdateAsanaTask(taskDetails: TaskDetails) =
                
                ()
            
            member sm.FetchAsanaProject(projectId: string) =
                Operations.selectAsanaProjectRecord sm.Context [ "WHERE gid = @0" ] [ projectId ]
        
        
        
        let i = ()
    *)  

open System.Net.Http
open Freql.Sqlite
open FuseBox.Asana.V1
open FuseBox.Asana.V1.Core
open FuseBox.Asana.V1.Domain
//open FuseBox.StateManagement
open FuseBox.GitHub
open Octokit
open FuseBox.Common.Transactions

let user = "[name]"
let repo = "[repo]"
let pat = "[asana-token]"
let githubToken = "[github-token]"



(*
let workflowTest _ =

    let githubClient = GitHubClient(ProductHeaderValue(user, repo))
    githubClient.Credentials <- Credentials(githubToken)

    use asanaClient = new HttpClient()
    setToken pat asanaClient |> ignore

    //let r = Issues.get client |> Async.AwaitTask |> Async.RunSynchronously
    let sm = StateManager(SqliteContext.Open("C:\\ProjectData\\Asana\\asana_test.db"))

    let fetchIssues (client: GitHubClient) =
        Issues.getForRepo repo client
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> List.ofSeq
        |> List.fold (fun (acc: Issue list) issue ->
            match sm.FetchGitHubIssue(issue.Id.ToString()) with
            | Some _ ->
                printfn $"Issue already exists, skipping. {issue.Id}"
                acc
            | None ->
                match sm.InsertGitHubIssue(issue, "test-repo") with
                | Ok _ -> acc @ [ issue ]
                | Error e ->
                    printfn $"Failed to add GitHub issue, skipping. Error: {e}"
                    acc) []

    let createTask (issue: Issue) =
        let headerText = $"<strong>This task was generated from GitHub via Fusebox</strong>"
        let details = $"<ul><li>Issue id: {issue.Id}</li><li>Number: {issue.Number}</li><li>Url: <a href='{issue.Url}'>{issue.Url}</a></li><li>Created on: {issue.CreatedAt}</li></ul>"
        //let body = FuseBox.Formatting.MarkDown.toHtml issue.Body

        let notes = $"<body>{headerText}{details}{issue.Body}</body>"
        let newTask = ({
            // Bug tracking project id - "1201808691470438"
            // Test repo id - "1201810353532819"
            // Assingee - 1201824511681397
            Data = AddBasicAssignedTask.Create("1201810353532819", issue.Title, notes, "1201824511681397")
        }: AddBasicAssignedTaskRequest)

        match addBasicAssignedTask newTask asanaClient |> handleTaskResult with
        | Ok newTask ->
            printfn "Added new Asana task."
            sm.InsertAsanaTask newTask.Data
        | Error e ->
            failwith e

    let r =
        fetchIssues githubClient
        |> List.map createTask

    ()
*)

let transactionTest _ =

    let failedAttempt _ =
        task {
            printfn "Failure"
            return Error "Failed test"
        }

    let testTask _ =
        task {
            printfn "Successful"
            return Ok ""
        }

    taskResult {

        //let! f = failedAttempt ()
        let! s = testTask ()
        let! s = testTask ()
        let! s = testTask ()

        return! (testTask ())
    }


let initialize path =
    let ctx = SqliteContext.Create path

    let seed =
        [ FuseBox.Asana.DataStore.Initialization.seed
          FuseBox.GitHub.DataStore.Initialization.seed ]
        |> List.concat

    FuseBox.Common.DataStore.Initialization.run ctx seed
    |> Result.bind (fun _ -> Ok ctx)

module IssueHandlerWorkflow =

    open FuseBox.Common.DataStore

    let workflowId = "issue-handler"

    let add ctx =

        // Add workflow
        Workflows.add ctx workflowId "Issue handler"

        // Add actions
        [ "github-issue_a-create", "Create GitHub issue"
          "github-issue_a-update", "Update GitHub issue"
          "github-issue_a-close", "Close GitHub issue"
          "asana-task_a-create", "Create Asana task"
          "asana-task_a-update", "Update Asana task"
          "asana-task_a-close", "Close Asana task" ]
        |> List.map
            (fun (actionId, displayName) ->
                WorkflowActionTypes.add ctx $"{workflowId}#{actionId}" workflowId actionId displayName)
        |> ignore

        // Add events
        [ "asana-task_e-created", "Asana task created"
          "asana-task_e-assigned", "Asana task assigned"
          "asana-task_e-updated", "Asana task updated"
          "asana-task_e-closed", "Asana task closed"
          "github-issue_e-created", "GitHub issue created"
          "github-issue_e-assigned", "GitHub issue assigned"
          "github-issue_e-updated", "GitHub issue updated"
          "github-issue_e-closed", "Asana task closed" ]
        |> List.map
            (fun (eventId, displayName) ->
                WorkflowEventTypes.add ctx $"{workflowId}#{eventId}" workflowId eventId displayName)
        |> ignore

        //asana-task_c-is_assigned
        // Add conditions
        [ "github-issue_c-is_closed", "GitHub issue is closed"
          "github-issue_c-is_open", "GitHub issue is open"
          "github-issue_c-is_assigned", "GitHub issue is assigned"
          "github-issue_c-has_tag", "GitHub issue has tag"
          "asana-task_c-is_closed", "Asana task is closed"
          "asana-task_c-is_open", "Asana task is open"
          "asana-task_c-is_assigned", "Asana task is assigned"
          "asana-task_c-has_tag", "Asana task has tag" ]
        |> List.map
            (fun (conditionId, displayName) ->
                WorkflowConditionsTypes.add ctx $"{workflowId}#{conditionId}" workflowId conditionId displayName)
        |> ignore

        // Add action events.
        [ $"{workflowId}-create-task-from-issue", "github-issue_e-created", "asana-task_a-create", None
          $"{workflowId}-update-issue-from-task", "asana-task_e-updated", "github-issue_a-update", Some "github-issue_c-is_open"
          $"{workflowId}-close-issue-from-task", "asana-task_e-closed", "github-issue_a-close", Some "github-issue_c-is_open" ]
        |> List.map
            (fun (id, eventId, actionId, conditionIdOption) ->
                WorkflowEventActions.add
                    ctx
                    id
                    $"{workflowId}#{eventId}"
                    $"{workflowId}#{actionId}"
                    (conditionIdOption
                     |> Option.bind (fun v -> Some $"{workflowId}#{v}")))
        |> ignore

    let queryEvent ctx eventId =
        WorkflowEventActions.getByEventId ctx $"{workflowId}#{eventId}"
        



    let run _ =


        ()



//let ctx = StateManager.Initialize("C:\\ProjectData\\Fusebox\\dev\\fusebox.db")


//asanaTest ()
//let r = githubTest ()

//workflowTest ()

(*
match transactionTest () with
| Ok r ->
    printfn $"Success"
| Error e -> printfn $"Error: {e}"
*)

match initialize "C:\\ProjectData\\Fusebox\\dev\\fusebox.db" with
| Ok ctx ->
    printfn "Initialized."
    
    IssueHandlerWorkflow.add ctx
    
    let test1 = IssueHandlerWorkflow.queryEvent ctx "github-issue_e-created"
    let test2 = IssueHandlerWorkflow.queryEvent ctx "asana-task_e-updated"
    let test3 = IssueHandlerWorkflow.queryEvent ctx "asana-task_e-closed"
    
    ()
| Error e -> printfn $"Error: {e}"

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

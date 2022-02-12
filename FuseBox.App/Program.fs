
open System.Net.Http
open Freql.Sqlite
open FuseBox.Asana.V1
open FuseBox.Asana.V1.Core
open FuseBox.Asana.V1.Domain
open FuseBox.StateManagement
open Fusebox.GitHub
open Octokit
open FuseBox.Common.Transactions

let user = "[name]"
let repo = "[repo]"
let pat = "[asana-token]"
let githubToken = "[github-token]"
    
    

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


let transactionTest _ =
    
    let failedAttempt _ = task {
        printfn "Failure"
        return Error "Failed test"
    }
    
    let testTask _ = task {
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
    
    
    

//asanaTest ()
//let r = githubTest ()
  
workflowTest ()
        
match transactionTest () with
| Ok r ->
    printfn $"Success"
| Error e -> printfn $"Error: {e}"

        
// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
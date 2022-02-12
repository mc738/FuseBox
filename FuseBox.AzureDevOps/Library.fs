namespace FuseBox.AzureDevOps

open System
open System.Threading.Tasks
open Microsoft.TeamFoundation.Core.WebApi
open Microsoft.TeamFoundation.SourceControl.WebApi
open Microsoft.VisualStudio.Services.Common

[<AutoOpen>]
module Common =

    type Configuration =
        { Url: string
          AccessToken: string
          Project: string
          RepositoryId: string }

[<RequireQualifiedAccess>]
module PullRequests =

    // ***FETCH***
    // [user code - creates a list of update operations]
    // ***UPDATE***

    /// Fetch operations are run first.
    type FetchOperation =
        | ById of int
        | Changes of int
        | All

    type UpdateOperation =
        | AddTag of string
        | CreatePullRequest


    type PullRequest =
        { Title: string
          Description: string
          Commits: Commit list }

    and Commit = { Id: string; Changes: Change list }

    and Change =
        { Path: string
          Type: VersionControlChangeType }

    let runAsync<'T> (cfg: Configuration) (fn: GitHttpClient -> Task<Result<'T, string>>) =
        let credentials =
            VssBasicCredential(String.Empty, cfg.AccessToken)

        use client =
            new GitHttpClient(Uri(cfg.Url), VssCredentials.op_Implicit credentials)

        fn client |> Async.AwaitTask

    let run<'T> (cfg: Configuration) (fn: GitHttpClient -> Result<'T, string>) =
        let credentials =
            VssBasicCredential(String.Empty, cfg.AccessToken)

        use client =
            new GitHttpClient(Uri(cfg.Url), VssCredentials.op_Implicit credentials)

        fn client
    //runAsync<'T>  |> Async.RunSynchronously

    let runTask<'T> (t: Task<'T>) =
        t |> Async.AwaitTask |> Async.RunSynchronously

    let bind<'T1, 'T2>
        (fnB: 'T1 -> GitHttpClient -> Result<'T2, string>)
        (fnA: GitHttpClient -> Result<'T1, string>)
        (client: GitHttpClient)
        =
        match fnA client with
        | Ok r1 -> fnB r1 client
        | Error e -> Error e


    let getById (cfg: Configuration) (id: int) (client: GitHttpClient) =
        task {
            let! pr =
                client.GetPullRequestAsync(
                    cfg.Project,
                    cfg.RepositoryId,
                    id,
                    includeCommits = true,
                    includeWorkItemRefs = true
                )

            return Ok pr
        }
        |> runTask

    let getChanges (cfg: Configuration) (pullRequest: GitPullRequest) (client: GitHttpClient) =
        let commits =
            pullRequest.Commits
            |> List.ofSeq
            |> List.map
                (fun commit ->
                    task {
                        let! changes = client.GetChangesAsync(cfg.Project, commit.CommitId, cfg.RepositoryId)

                        return
                            { Id = commit.CommitId
                              Changes =
                                  changes.Changes
                                  |> List.ofSeq
                                  |> List.map
                                      (fun c ->
                                          { Path = c.Item.Path
                                            Type = c.ChangeType }) }
                    }
                    |> runTask)

        commits |> Ok

    let addLabel (cfg: Configuration) (labelText: string) (pullRequestId: int) (client: GitHttpClient) =
        task {
            let label = WebApiCreateTagRequestData()
            label.Name <- labelText
            return! client.CreatePullRequestLabelAsync(label, cfg.Project, cfg.RepositoryId, pullRequestId)
        }
        |> runTask




module Say =
    let hello name = printfn "Hello %s" name

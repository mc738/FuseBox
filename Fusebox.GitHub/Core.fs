namespace FuseBox.GitHub

open System
open Octokit

module Issues =


    let get (client: GitHubClient) =
        task {
            let! issues = client.Issue.GetAllForCurrent()


            return ""
        }

    let getForRepo (repoName: string) (client: GitHubClient) =
        task { return! client.Issue.GetAllForRepository("mc738", repoName) }


(*
[<AutoOpen>]
module Extensions =
    open FuseBox.StateManagement
    open FuseBox.GitHub.StateManagement.Persistence

    type StateManager with

        member sm.FetchGitHubIssue(issueId: string) =
            Operations.selectGithubIssueRecord sm.Context [ "WHERE issue_id = @0" ] [ issueId ]


        member sm.InsertGitHubIssue(issue: Issue, repoId: string) =
            sm.Context.ExecuteInTransaction
                (fun t ->
                    ({ IssueId = issue.Id.ToString()
                       RepositoryId = repoId
                       Title = issue.Title
                       CreatedOn = issue.CreatedAt.DateTime
                       LastUpdatedOn =
                           issue.UpdatedAt
                           |> Option.ofNullable
                           |> Option.bind (fun dos -> Some dos.DateTime)
                           |> Option.defaultWith (fun _ -> issue.CreatedAt.DateTime)
                       Serial = 1L
                       IsOpen = true }: Parameters.NewGithubIssue)
                    |> Operations.insertGithubIssue t)
*)
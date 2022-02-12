open System.Text.RegularExpressions
open FuseBox.AzureDevOps

let test _ =

   let cfg = ({
      Url = "[url]"
      Project = "[name]"
      RepositoryId = "[repo_id]"
      AccessToken = "[token]"
   }: Configuration)

   let createLabels (path: string) =
      [
         if (Regex.IsMatch(path, "^/CommunityBridges.Core/")) then "core"
         if (Regex.IsMatch(path, "^/CommunityBridges.WebApi/")) then "web-api"
         if (Regex.IsMatch(path, "^/CommunityBridges.AdminApp/")) then "admin-app"
         if (Regex.IsMatch(path, "^/OpenReferralUk")) then "oruk"
      ]
      

   let pipeline =
      PullRequests.getById cfg 5
      |> PullRequests.bind (PullRequests.getChanges cfg) 
      |> PullRequests.run cfg

   match pipeline with
   | Ok commits ->
      let labels =
         commits
         |> List.map (fun commit ->
            commit.Changes
            |> List.map (fun change -> createLabels change.Path))
            |> List.concat
         |> List.concat
         |> List.distinct
         |> List.map (fun l -> PullRequests.addLabel cfg)
         
         
      
      ()
   | Error s -> printfn $"Error: {s}"




// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
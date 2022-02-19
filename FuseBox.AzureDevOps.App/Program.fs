open System.IO
open System.Text.Json
open System.Text.RegularExpressions
open FuseBox.AzureDevOps

let test _ =

   let cfg = 
      File.ReadAllText "C:\\ProjectData\\Fiket\\.fusebox\\azure-devops-config.json"
      |> JsonSerializer.Deserialize<Configuration>

   let createLabels (path: string) =
      [
         if (Regex.IsMatch(path, "/Fiket.Common/|/Fiket.Base.WebApi/|Fiket.Utils")) then [ "core" ]
         if (Regex.IsMatch(path, "/Fiket.Auth/")) then [ "auth"; "auth-core" ]
         if (Regex.IsMatch(path, "/Fiket.Auth.WebApi/")) then [ "auth"; "auth-web-api" ]
         if (Regex.IsMatch(path, "/Fiket.BlobStore/")) then [ "blob-store"; "blob-store-core" ]
         if (Regex.IsMatch(path, "/Fiket.BlobStore.WebApi/")) then [ "blob-store"; "blob-sore-web-api" ]
         if (Regex.IsMatch(path, "/Fiket.Comms/")) then [ "comms"; "comms-core" ]
         if (Regex.IsMatch(path, "/Fiket.Comms.Shared/")) then [ "comms"; "comms-share" ]
         if (Regex.IsMatch(path, "/Fiket.Comms.WebApi/")) then [ "comms"; "comms-api" ]
         if (Regex.IsMatch(path, "/Fiket.Events/")) then [ "events"; "events-core" ]
         if (Regex.IsMatch(path, "/Fiket.Events.WebApi/")) then [ "events"; "events-web-api" ]
         if (Regex.IsMatch(path, "/Fiket.Routing/")) then [ "routing"; "routing-core" ]
         if (Regex.IsMatch(path, "/Fiket.Routing.WebApi/")) then [ "routing"; "routing-web-api" ]
         if (Regex.IsMatch(path, "/Fiket.Tools")) then [ "tooling" ]
         if (Regex.IsMatch(path, "/Fiket.WorkFlows/")) then [ "workflows"; "workflows-core" ]
         if (Regex.IsMatch(path, "/Fiket.Workspaces/")) then [ "workspaces"; "workspaces-core" ]
         if (Regex.IsMatch(path, "/Fiket.Workspaces.Shared/")) then [ "workspaces"; "workspaces-share" ]
         if (Regex.IsMatch(path, "/Fiket.Workspaces.WebApi/")) then [ "workspaces"; "workspaces-web-api" ]
         if (Regex.IsMatch(path, "/Fiket.Workspaces.WebApp/")) then [ "workspaces"; "workspaces-web-app" ]
      ]
      |> List.concat
      

   let pipeline =
      PullRequests.getById cfg 10
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
         |> List.map (fun l -> PullRequests.addLabel cfg l 10)
         
      let r = PullRequests.run cfg (fun c -> labels |> List.map (fun l -> l c) |> Ok)
         
      
      ()
   | Error s -> printfn $"Error: {s}"



test ()

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
namespace FuseBox.Asana.StateManagement.Persistence

open System
open System.Text.Json.Serialization
open Freql.Core.Common
open Freql.Sqlite

/// Module generated on 18/02/2022 22:38:59 (utc) via Freql.Sqlite.Tools.
[<RequireQualifiedAccess>]
module Records =
    /// A record representing a row in the table `asana_project_task_links`.
    type AsanaProjectTaskLink =
        { [<JsonPropertyName("projectId")>] ProjectId: string
          [<JsonPropertyName("taskId")>] TaskId: string }
    
        static member Blank() =
            { ProjectId = String.Empty
              TaskId = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE asana_project_task_links (
	project_id TEXT NOT NULL,
	task_id TEXT NOT NULL,
	CONSTRAINT asana_project_task_links_PK PRIMARY KEY (project_id,task_id),
	CONSTRAINT asana_project_task_links_FK FOREIGN KEY (project_id) REFERENCES asana_projects(gid),
	CONSTRAINT asana_project_task_links_FK_1 FOREIGN KEY (task_id) REFERENCES asana_tasks(gid)
)
        """
    
        static member SelectSql() = """
        SELECT
              project_id,
              task_id
        FROM asana_project_task_links
        """
    
        static member TableName() = "asana_project_task_links"
    
    /// A record representing a row in the table `asana_projects`.
    type AsanaProject =
        { [<JsonPropertyName("gid")>] Gid: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Gid = String.Empty
              Name = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE asana_projects (
	gid TEXT NOT NULL,
	name TEXT NOT NULL,
	CONSTRAINT asana_projects_PK PRIMARY KEY (gid)
)
        """
    
        static member SelectSql() = """
        SELECT
              gid,
              name
        FROM asana_projects
        """
    
        static member TableName() = "asana_projects"
    
    /// A record representing a row in the table `asana_task_blobs`.
    type AsanaTaskBlob =
        { [<JsonPropertyName("taskId")>] TaskId: string
          [<JsonPropertyName("extractedOn")>] ExtractedOn: DateTime
          [<JsonPropertyName("serial")>] Serial: int64
          [<JsonPropertyName("taskBlob")>] TaskBlob: BlobField }
    
        static member Blank() =
            { TaskId = String.Empty
              ExtractedOn = DateTime.UtcNow
              Serial = 0L
              TaskBlob = BlobField.Empty() }
    
        static member CreateTableSql() = """
        CREATE TABLE asana_task_blobs (
	task_id TEXT NOT NULL,
	extracted_on TEXT NOT NULL,
	serial INTEGER NOT NULL,
	task_blob BLOB NOT NULL,
	CONSTRAINT asana_task_blobs_PK PRIMARY KEY (task_id,serial),
	CONSTRAINT asana_task_blobs_FK FOREIGN KEY (task_id) REFERENCES asana_tasks(gid)
)
        """
    
        static member SelectSql() = """
        SELECT
              task_id,
              extracted_on,
              serial,
              task_blob
        FROM asana_task_blobs
        """
    
        static member TableName() = "asana_task_blobs"
    
    /// A record representing a row in the table `asana_tasks`.
    type AsanaTask =
        { [<JsonPropertyName("gid")>] Gid: string
          [<JsonPropertyName("originalName")>] OriginalName: string
          [<JsonPropertyName("currentName")>] CurrentName: string
          [<JsonPropertyName("createdOn")>] CreatedOn: DateTime
          [<JsonPropertyName("lastModified")>] LastModified: DateTime
          [<JsonPropertyName("active")>] Active: bool
          [<JsonPropertyName("completedOn")>] CompletedOn: string option
          [<JsonPropertyName("serial")>] Serial: int64 }
    
        static member Blank() =
            { Gid = String.Empty
              OriginalName = String.Empty
              CurrentName = String.Empty
              CreatedOn = DateTime.UtcNow
              LastModified = DateTime.UtcNow
              Active = true
              CompletedOn = None
              Serial = 0L }
    
        static member CreateTableSql() = """
        CREATE TABLE asana_tasks (
	gid TEXT NOT NULL,
	original_name TEXT NOT NULL,
	current_name TEXT NOT NULL,
	created_on TEXT NOT NULL,
	last_modified TEXT NOT NULL,
	active INTEGER NOT NULL,
	completed_on TEXT,
	serial INTEGER NOT NULL,
	CONSTRAINT asana_tasks_PK PRIMARY KEY (gid)
)
        """
    
        static member SelectSql() = """
        SELECT
              gid,
              original_name,
              current_name,
              created_on,
              last_modified,
              active,
              completed_on,
              serial
        FROM asana_tasks
        """
    
        static member TableName() = "asana_tasks"
    

/// Module generated on 18/02/2022 22:38:59 (utc) via Freql.Tools.
[<RequireQualifiedAccess>]
module Parameters =
    /// A record representing a new row in the table `asana_project_task_links`.
    type NewAsanaProjectTaskLink =
        { [<JsonPropertyName("projectId")>] ProjectId: string
          [<JsonPropertyName("taskId")>] TaskId: string }
    
        static member Blank() =
            { ProjectId = String.Empty
              TaskId = String.Empty }
    
    
    /// A record representing a new row in the table `asana_projects`.
    type NewAsanaProject =
        { [<JsonPropertyName("gid")>] Gid: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Gid = String.Empty
              Name = String.Empty }
    
    
    /// A record representing a new row in the table `asana_task_blobs`.
    type NewAsanaTaskBlob =
        { [<JsonPropertyName("taskId")>] TaskId: string
          [<JsonPropertyName("extractedOn")>] ExtractedOn: DateTime
          [<JsonPropertyName("serial")>] Serial: int64
          [<JsonPropertyName("taskBlob")>] TaskBlob: BlobField }
    
        static member Blank() =
            { TaskId = String.Empty
              ExtractedOn = DateTime.UtcNow
              Serial = 0L
              TaskBlob = BlobField.Empty() }
    
    
    /// A record representing a new row in the table `asana_tasks`.
    type NewAsanaTask =
        { [<JsonPropertyName("gid")>] Gid: string
          [<JsonPropertyName("originalName")>] OriginalName: string
          [<JsonPropertyName("currentName")>] CurrentName: string
          [<JsonPropertyName("createdOn")>] CreatedOn: DateTime
          [<JsonPropertyName("lastModified")>] LastModified: DateTime
          [<JsonPropertyName("active")>] Active: bool
          [<JsonPropertyName("completedOn")>] CompletedOn: string option
          [<JsonPropertyName("serial")>] Serial: int64 }
    
        static member Blank() =
            { Gid = String.Empty
              OriginalName = String.Empty
              CurrentName = String.Empty
              CreatedOn = DateTime.UtcNow
              LastModified = DateTime.UtcNow
              Active = true
              CompletedOn = None
              Serial = 0L }
    
    
/// Module generated on 18/02/2022 22:38:59 (utc) via Freql.Tools.
[<RequireQualifiedAccess>]
module Operations =

    let buildSql (lines: string list) = lines |> String.concat Environment.NewLine

    /// Select a `Records.AsanaProjectTaskLink` from the table `asana_project_task_links`.
    /// Internally this calls `context.SelectSingleAnon<Records.AsanaProjectTaskLink>` and uses Records.AsanaProjectTaskLink.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectAsanaProjectTaskLinkRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectAsanaProjectTaskLinkRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.AsanaProjectTaskLink.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.AsanaProjectTaskLink>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.AsanaProjectTaskLink>` and uses Records.AsanaProjectTaskLink.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectAsanaProjectTaskLinkRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectAsanaProjectTaskLinkRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.AsanaProjectTaskLink.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.AsanaProjectTaskLink>(sql, parameters)
    
    let insertAsanaProjectTaskLink (context: SqliteContext) (parameters: Parameters.NewAsanaProjectTaskLink) =
        context.Insert("asana_project_task_links", parameters)
    
    /// Select a `Records.AsanaProject` from the table `asana_projects`.
    /// Internally this calls `context.SelectSingleAnon<Records.AsanaProject>` and uses Records.AsanaProject.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectAsanaProjectRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectAsanaProjectRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.AsanaProject.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.AsanaProject>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.AsanaProject>` and uses Records.AsanaProject.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectAsanaProjectRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectAsanaProjectRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.AsanaProject.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.AsanaProject>(sql, parameters)
    
    let insertAsanaProject (context: SqliteContext) (parameters: Parameters.NewAsanaProject) =
        context.Insert("asana_projects", parameters)
    
    /// Select a `Records.AsanaTaskBlob` from the table `asana_task_blobs`.
    /// Internally this calls `context.SelectSingleAnon<Records.AsanaTaskBlob>` and uses Records.AsanaTaskBlob.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectAsanaTaskBlobRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectAsanaTaskBlobRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.AsanaTaskBlob.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.AsanaTaskBlob>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.AsanaTaskBlob>` and uses Records.AsanaTaskBlob.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectAsanaTaskBlobRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectAsanaTaskBlobRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.AsanaTaskBlob.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.AsanaTaskBlob>(sql, parameters)
    
    let insertAsanaTaskBlob (context: SqliteContext) (parameters: Parameters.NewAsanaTaskBlob) =
        context.Insert("asana_task_blobs", parameters)
    
    /// Select a `Records.AsanaTask` from the table `asana_tasks`.
    /// Internally this calls `context.SelectSingleAnon<Records.AsanaTask>` and uses Records.AsanaTask.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectAsanaTaskRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectAsanaTaskRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.AsanaTask.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.AsanaTask>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.AsanaTask>` and uses Records.AsanaTask.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectAsanaTaskRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectAsanaTaskRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.AsanaTask.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.AsanaTask>(sql, parameters)
    
    let insertAsanaTask (context: SqliteContext) (parameters: Parameters.NewAsanaTask) =
        context.Insert("asana_tasks", parameters)
    
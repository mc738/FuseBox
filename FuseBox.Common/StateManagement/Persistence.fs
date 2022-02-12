namespace FuseBox.StateManagement.Persistence

open System
open System.Text.Json.Serialization
open Freql.Core.Common
open Freql.Sqlite

/// Module generated on 12/02/2022 16:12:00 (utc) via Freql.Sqlite.Tools.
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
    
    /// A record representing a row in the table `github_issue_blobs`.
    type GithubIssueBlob =
        { [<JsonPropertyName("issueId")>] IssueId: string
          [<JsonPropertyName("createdOn")>] CreatedOn: DateTime
          [<JsonPropertyName("issueBlob")>] IssueBlob: BlobField
          [<JsonPropertyName("serial")>] Serial: int64 }
    
        static member Blank() =
            { IssueId = String.Empty
              CreatedOn = DateTime.UtcNow
              IssueBlob = BlobField.Empty()
              Serial = 0L }
    
        static member CreateTableSql() = """
        CREATE TABLE github_issue_blobs (
	issue_id TEXT NOT NULL,
	created_on TEXT NOT NULL,
	issue_blob BLOB NOT NULL,
	serial INTEGER NOT NULL,
	CONSTRAINT github_issue_blobs_PK PRIMARY KEY (issue_id,serial),
	CONSTRAINT github_issue_blobs_FK FOREIGN KEY (issue_id) REFERENCES github_issues(issue_id)
)
        """
    
        static member SelectSql() = """
        SELECT
              issue_id,
              created_on,
              issue_blob,
              serial
        FROM github_issue_blobs
        """
    
        static member TableName() = "github_issue_blobs"
    
    /// A record representing a row in the table `github_issues`.
    type GithubIssue =
        { [<JsonPropertyName("issueId")>] IssueId: string
          [<JsonPropertyName("repositoryId")>] RepositoryId: string
          [<JsonPropertyName("title")>] Title: string
          [<JsonPropertyName("createdOn")>] CreatedOn: DateTime
          [<JsonPropertyName("lastUpdatedOn")>] LastUpdatedOn: DateTime
          [<JsonPropertyName("serial")>] Serial: int64
          [<JsonPropertyName("isOpen")>] IsOpen: bool }
    
        static member Blank() =
            { IssueId = String.Empty
              RepositoryId = String.Empty
              Title = String.Empty
              CreatedOn = DateTime.UtcNow
              LastUpdatedOn = DateTime.UtcNow
              Serial = 0L
              IsOpen = true }
    
        static member CreateTableSql() = """
        CREATE TABLE github_issues (
	issue_id TEXT NOT NULL,
	repository_id TEXT NOT NULL,
	title TEXT NOT NULL,
	created_on TEXT NOT NULL,
	last_updated_on TEXT NOT NULL,
	serial INTEGER NOT NULL,
	is_open INTEGER NOT NULL,
	CONSTRAINT github_issues_PK PRIMARY KEY (issue_id),
	CONSTRAINT github_issues_FK FOREIGN KEY (repository_id) REFERENCES github_repositories(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              issue_id,
              repository_id,
              title,
              created_on,
              last_updated_on,
              serial,
              is_open
        FROM github_issues
        """
    
        static member TableName() = "github_issues"
    
    /// A record representing a row in the table `github_repositories`.
    type GithubRepository =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string
          [<JsonPropertyName("url")>] Url: string
          [<JsonPropertyName("createdOn")>] CreatedOn: DateTime
          [<JsonPropertyName("lastModified")>] LastModified: DateTime
          [<JsonPropertyName("serial")>] Serial: int64 }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty
              Url = String.Empty
              CreatedOn = DateTime.UtcNow
              LastModified = DateTime.UtcNow
              Serial = 0L }
    
        static member CreateTableSql() = """
        CREATE TABLE github_repositories (
	id TEXT NOT NULL,
	name TEXT NOT NULL,
	url TEXT NOT NULL,
	created_on TEXT NOT NULL,
	last_modified TEXT NOT NULL,
	serial INTEGER NOT NULL,
	CONSTRAINT github_repositories_PK PRIMARY KEY (id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              name,
              url,
              created_on,
              last_modified,
              serial
        FROM github_repositories
        """
    
        static member TableName() = "github_repositories"
    
    /// A record representing a row in the table `item_states`.
    type ItemState =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("itemId")>] ItemId: string
          [<JsonPropertyName("itemType")>] ItemType: string
          [<JsonPropertyName("stateBlob")>] StateBlob: BlobField
          [<JsonPropertyName("serial")>] Serial: int64 }
    
        static member Blank() =
            { Id = String.Empty
              ItemId = String.Empty
              ItemType = String.Empty
              StateBlob = BlobField.Empty()
              Serial = 0L }
    
        static member CreateTableSql() = """
        CREATE TABLE item_states (
	id TEXT NOT NULL,
	item_id TEXT NOT NULL,
	item_type TEXT NOT NULL,
	state_blob BLOB NOT NULL,
	serial INTEGER NOT NULL
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              item_id,
              item_type,
              state_blob,
              serial
        FROM item_states
        """
    
        static member TableName() = "item_states"
    
    /// A record representing a row in the table `project_metadata`.
    type ProjectMetadataItem =
        { [<JsonPropertyName("projectId")>] ProjectId: int64
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { ProjectId = 0L
              DataKey = String.Empty
              DataValue = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE project_metadata (
	project_id INTEGER NOT NULL,
	data_key TEXT NOT NULL,
	data_value TEXT NOT NULL,
	CONSTRAINT project_metadata_PK PRIMARY KEY (project_id,data_key),
	CONSTRAINT project_metadata_FK FOREIGN KEY (project_id) REFERENCES projects(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              project_id,
              data_key,
              data_value
        FROM project_metadata
        """
    
        static member TableName() = "project_metadata"
    
    /// A record representing a row in the table `projects`.
    type Project =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE projects (
	id TEXT NOT NULL,
	name TEXT NOT NULL,
	CONSTRAINT projects_PK PRIMARY KEY (id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              name
        FROM projects
        """
    
        static member TableName() = "projects"
    
    /// A record representing a row in the table `user_metadata`.
    type UserMetadataItem =
        { [<JsonPropertyName("userId")>] UserId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { UserId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE user_metadata (
	user_id TEXT NOT NULL,
	data_key TEXT NOT NULL,
	data_value TEXT NOT NULL,
	CONSTRAINT user_metadata_PK PRIMARY KEY (user_id,data_key),
	CONSTRAINT user_metadata_FK FOREIGN KEY (user_id) REFERENCES users(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              user_id,
              data_key,
              data_value
        FROM user_metadata
        """
    
        static member TableName() = "user_metadata"
    
    /// A record representing a row in the table `users`.
    type User =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("username")>] Username: string
          [<JsonPropertyName("displayName")>] DisplayName: string }
    
        static member Blank() =
            { Id = String.Empty
              Username = String.Empty
              DisplayName = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE users (
	id TEXT NOT NULL,
	username TEXT NOT NULL,
	display_name TEXT NOT NULL,
	CONSTRAINT users_PK PRIMARY KEY (id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              username,
              display_name
        FROM users
        """
    
        static member TableName() = "users"
    

/// Module generated on 12/02/2022 16:12:00 (utc) via Freql.Tools.
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
    
    
    /// A record representing a new row in the table `github_issue_blobs`.
    type NewGithubIssueBlob =
        { [<JsonPropertyName("issueId")>] IssueId: string
          [<JsonPropertyName("createdOn")>] CreatedOn: DateTime
          [<JsonPropertyName("issueBlob")>] IssueBlob: BlobField
          [<JsonPropertyName("serial")>] Serial: int64 }
    
        static member Blank() =
            { IssueId = String.Empty
              CreatedOn = DateTime.UtcNow
              IssueBlob = BlobField.Empty()
              Serial = 0L }
    
    
    /// A record representing a new row in the table `github_issues`.
    type NewGithubIssue =
        { [<JsonPropertyName("issueId")>] IssueId: string
          [<JsonPropertyName("repositoryId")>] RepositoryId: string
          [<JsonPropertyName("title")>] Title: string
          [<JsonPropertyName("createdOn")>] CreatedOn: DateTime
          [<JsonPropertyName("lastUpdatedOn")>] LastUpdatedOn: DateTime
          [<JsonPropertyName("serial")>] Serial: int64
          [<JsonPropertyName("isOpen")>] IsOpen: bool }
    
        static member Blank() =
            { IssueId = String.Empty
              RepositoryId = String.Empty
              Title = String.Empty
              CreatedOn = DateTime.UtcNow
              LastUpdatedOn = DateTime.UtcNow
              Serial = 0L
              IsOpen = true }
    
    
    /// A record representing a new row in the table `github_repositories`.
    type NewGithubRepository =
        { [<JsonPropertyName("name")>] Name: string
          [<JsonPropertyName("url")>] Url: string
          [<JsonPropertyName("createdOn")>] CreatedOn: DateTime
          [<JsonPropertyName("lastModified")>] LastModified: DateTime
          [<JsonPropertyName("serial")>] Serial: int64 }
    
        static member Blank() =
            { Name = String.Empty
              Url = String.Empty
              CreatedOn = DateTime.UtcNow
              LastModified = DateTime.UtcNow
              Serial = 0L }
    
    
    /// A record representing a new row in the table `item_states`.
    type NewItemState =
        { [<JsonPropertyName("itemId")>] ItemId: string
          [<JsonPropertyName("itemType")>] ItemType: string
          [<JsonPropertyName("stateBlob")>] StateBlob: BlobField
          [<JsonPropertyName("serial")>] Serial: int64 }
    
        static member Blank() =
            { ItemId = String.Empty
              ItemType = String.Empty
              StateBlob = BlobField.Empty()
              Serial = 0L }
    
    
    /// A record representing a new row in the table `project_metadata`.
    type NewProjectMetadataItem =
        { [<JsonPropertyName("projectId")>] ProjectId: int64
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { ProjectId = 0L
              DataKey = String.Empty
              DataValue = String.Empty }
    
    
    /// A record representing a new row in the table `projects`.
    type NewProject =
        { [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Name = String.Empty }
    
    
    /// A record representing a new row in the table `user_metadata`.
    type NewUserMetadataItem =
        { [<JsonPropertyName("userId")>] UserId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { UserId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
    
    /// A record representing a new row in the table `users`.
    type NewUser =
        { [<JsonPropertyName("username")>] Username: string
          [<JsonPropertyName("displayName")>] DisplayName: string }
    
        static member Blank() =
            { Username = String.Empty
              DisplayName = String.Empty }
    
    
/// Module generated on 12/02/2022 16:12:00 (utc) via Freql.Tools.
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
    
    /// Select a `Records.GithubIssueBlob` from the table `github_issue_blobs`.
    /// Internally this calls `context.SelectSingleAnon<Records.GithubIssueBlob>` and uses Records.GithubIssueBlob.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectGithubIssueBlobRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectGithubIssueBlobRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.GithubIssueBlob.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.GithubIssueBlob>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.GithubIssueBlob>` and uses Records.GithubIssueBlob.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectGithubIssueBlobRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectGithubIssueBlobRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.GithubIssueBlob.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.GithubIssueBlob>(sql, parameters)
    
    let insertGithubIssueBlob (context: SqliteContext) (parameters: Parameters.NewGithubIssueBlob) =
        context.Insert("github_issue_blobs", parameters)
    
    /// Select a `Records.GithubIssue` from the table `github_issues`.
    /// Internally this calls `context.SelectSingleAnon<Records.GithubIssue>` and uses Records.GithubIssue.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectGithubIssueRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectGithubIssueRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.GithubIssue.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.GithubIssue>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.GithubIssue>` and uses Records.GithubIssue.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectGithubIssueRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectGithubIssueRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.GithubIssue.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.GithubIssue>(sql, parameters)
    
    let insertGithubIssue (context: SqliteContext) (parameters: Parameters.NewGithubIssue) =
        context.Insert("github_issues", parameters)
    
    /// Select a `Records.GithubRepository` from the table `github_repositories`.
    /// Internally this calls `context.SelectSingleAnon<Records.GithubRepository>` and uses Records.GithubRepository.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectGithubRepositoryRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectGithubRepositoryRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.GithubRepository.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.GithubRepository>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.GithubRepository>` and uses Records.GithubRepository.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectGithubRepositoryRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectGithubRepositoryRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.GithubRepository.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.GithubRepository>(sql, parameters)
    
    let insertGithubRepository (context: SqliteContext) (parameters: Parameters.NewGithubRepository) =
        context.Insert("github_repositories", parameters)
    
    /// Select a `Records.ItemState` from the table `item_states`.
    /// Internally this calls `context.SelectSingleAnon<Records.ItemState>` and uses Records.ItemState.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectItemStateRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectItemStateRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ItemState.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ItemState>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ItemState>` and uses Records.ItemState.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectItemStateRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectItemStateRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ItemState.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ItemState>(sql, parameters)
    
    let insertItemState (context: SqliteContext) (parameters: Parameters.NewItemState) =
        context.Insert("item_states", parameters)
    
    /// Select a `Records.ProjectMetadataItem` from the table `project_metadata`.
    /// Internally this calls `context.SelectSingleAnon<Records.ProjectMetadataItem>` and uses Records.ProjectMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectProjectMetadataItemRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectProjectMetadataItemRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ProjectMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ProjectMetadataItem>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ProjectMetadataItem>` and uses Records.ProjectMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectProjectMetadataItemRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectProjectMetadataItemRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ProjectMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ProjectMetadataItem>(sql, parameters)
    
    let insertProjectMetadataItem (context: SqliteContext) (parameters: Parameters.NewProjectMetadataItem) =
        context.Insert("project_metadata", parameters)
    
    /// Select a `Records.Project` from the table `projects`.
    /// Internally this calls `context.SelectSingleAnon<Records.Project>` and uses Records.Project.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectProjectRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectProjectRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.Project.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.Project>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.Project>` and uses Records.Project.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectProjectRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectProjectRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.Project.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.Project>(sql, parameters)
    
    let insertProject (context: SqliteContext) (parameters: Parameters.NewProject) =
        context.Insert("projects", parameters)
    
    /// Select a `Records.UserMetadataItem` from the table `user_metadata`.
    /// Internally this calls `context.SelectSingleAnon<Records.UserMetadataItem>` and uses Records.UserMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectUserMetadataItemRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectUserMetadataItemRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.UserMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.UserMetadataItem>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.UserMetadataItem>` and uses Records.UserMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectUserMetadataItemRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectUserMetadataItemRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.UserMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.UserMetadataItem>(sql, parameters)
    
    let insertUserMetadataItem (context: SqliteContext) (parameters: Parameters.NewUserMetadataItem) =
        context.Insert("user_metadata", parameters)
    
    /// Select a `Records.User` from the table `users`.
    /// Internally this calls `context.SelectSingleAnon<Records.User>` and uses Records.User.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectUserRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectUserRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.User.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.User>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.User>` and uses Records.User.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectUserRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectUserRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.User.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.User>(sql, parameters)
    
    let insertUser (context: SqliteContext) (parameters: Parameters.NewUser) =
        context.Insert("users", parameters)
    
namespace FuseBox.GitHub.DataStore.Persistence

open System
open System.Text.Json.Serialization
open Freql.Core.Common
open Freql.Sqlite

/// Module generated on 18/02/2022 22:38:42 (utc) via Freql.Sqlite.Tools.
[<RequireQualifiedAccess>]
module Records =
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
    

/// Module generated on 18/02/2022 22:38:42 (utc) via Freql.Tools.
[<RequireQualifiedAccess>]
module Parameters =
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
    
    
/// Module generated on 18/02/2022 22:38:42 (utc) via Freql.Tools.
[<RequireQualifiedAccess>]
module Operations =

    let buildSql (lines: string list) = lines |> String.concat Environment.NewLine

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
    
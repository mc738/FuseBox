namespace FuseBox.Common.DataStore.Persistence

open System
open System.Text.Json.Serialization
open Freql.Core.Common
open Freql.Sqlite

/// Module generated on 19/02/2022 14:41:16 (utc) via Freql.Sqlite.Tools.
[<RequireQualifiedAccess>]
module Records =
    /// A record representing a row in the table `action_types`.
    type ActionType =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string
          [<JsonPropertyName("parentType")>] ParentType: string
          [<JsonPropertyName("typeBlob")>] TypeBlob: BlobField }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty
              ParentType = String.Empty
              TypeBlob = BlobField.Empty() }
    
        static member CreateTableSql() = """
        CREATE TABLE action_types (
	id TEXT NOT NULL,
	name TEXT NOT NULL,
	parent_type TEXT NOT NULL,
	type_blob BLOB NOT NULL,
	CONSTRAINT action_types_PK PRIMARY KEY (id),
	CONSTRAINT action_types_FK FOREIGN KEY (parent_type) REFERENCES registered_types(name)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              name,
              parent_type,
              type_blob
        FROM action_types
        """
    
        static member TableName() = "action_types"
    
    /// A record representing a row in the table `condition_types`.
    type ConditionType =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string
          [<JsonPropertyName("parentType")>] ParentType: string
          [<JsonPropertyName("typeBlob")>] TypeBlob: BlobField }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty
              ParentType = String.Empty
              TypeBlob = BlobField.Empty() }
    
        static member CreateTableSql() = """
        CREATE TABLE condition_types (
	id TEXT NOT NULL,
	name TEXT NOT NULL,
	parent_type TEXT NOT NULL,
	type_blob BLOB NOT NULL,
	CONSTRAINT condition_types_PK PRIMARY KEY (id),
	CONSTRAINT condition_types_FK FOREIGN KEY (parent_type) REFERENCES registered_types(name)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              name,
              parent_type,
              type_blob
        FROM condition_types
        """
    
        static member TableName() = "condition_types"
    
    /// A record representing a row in the table `event_types`.
    type EventType =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string
          [<JsonPropertyName("parentType")>] ParentType: string
          [<JsonPropertyName("typeBlob")>] TypeBlob: BlobField }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty
              ParentType = String.Empty
              TypeBlob = BlobField.Empty() }
    
        static member CreateTableSql() = """
        CREATE TABLE event_types (
	id TEXT NOT NULL,
	name TEXT NOT NULL,
	parent_type TEXT NOT NULL,
	type_blob BLOB NOT NULL,
	CONSTRAINT event_types_PK PRIMARY KEY (id),
	CONSTRAINT event_types_FK FOREIGN KEY (parent_type) REFERENCES registered_types(name)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              name,
              parent_type,
              type_blob
        FROM event_types
        """
    
        static member TableName() = "event_types"
    
    /// A record representing a row in the table `item_metadata`.
    type ItemMetadataItem =
        { [<JsonPropertyName("itemId")>] ItemId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { ItemId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE item_metadata (
	item_id TEXT NOT NULL,
	data_key TEXT NOT NULL,
	data_value TEXT NOT NULL,
	CONSTRAINT item_metadata_PK PRIMARY KEY (item_id,data_key),
	CONSTRAINT item_metadata_FK FOREIGN KEY (item_id) REFERENCES items(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              item_id,
              data_key,
              data_value
        FROM item_metadata
        """
    
        static member TableName() = "item_metadata"
    
    /// A record representing a row in the table `item_resource_link_metadata`.
    type ItemResourceLinkMetadataItem =
        { [<JsonPropertyName("linkId")>] LinkId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { LinkId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE item_resource_link_metadata (
	link_id TEXT NOT NULL,
	data_key TEXT NOT NULL,
	data_value TEXT NOT NULL,
	CONSTRAINT item_resource_link_metadata_PK PRIMARY KEY (link_id,data_key),
	CONSTRAINT item_resource_link_metadata_FK FOREIGN KEY (link_id) REFERENCES item_resource_links(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              link_id,
              data_key,
              data_value
        FROM item_resource_link_metadata
        """
    
        static member TableName() = "item_resource_link_metadata"
    
    /// A record representing a row in the table `item_resource_links`.
    type ItemResourceLink =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("itemId")>] ItemId: string
          [<JsonPropertyName("resourceId")>] ResourceId: string }
    
        static member Blank() =
            { Id = String.Empty
              ItemId = String.Empty
              ResourceId = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE item_resource_links (
	id TEXT NOT NULL,
	item_id TEXT NOT NULL,
	resource_id TEXT NOT NULL,
	CONSTRAINT item_resource_links_PK PRIMARY KEY (id),
	CONSTRAINT item_resource_links_FK FOREIGN KEY (item_id) REFERENCES items(id),
	CONSTRAINT item_resource_links_FK_1 FOREIGN KEY (resource_id) REFERENCES resources(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              item_id,
              resource_id
        FROM item_resource_links
        """
    
        static member TableName() = "item_resource_links"
    
    /// A record representing a row in the table `item_states`.
    type ItemState =
        { [<JsonPropertyName("itemId")>] ItemId: string
          [<JsonPropertyName("serial")>] Serial: int64
          [<JsonPropertyName("stateBlob")>] StateBlob: BlobField
          [<JsonPropertyName("stateTimestamp")>] StateTimestamp: DateTime }
    
        static member Blank() =
            { ItemId = String.Empty
              Serial = 0L
              StateBlob = BlobField.Empty()
              StateTimestamp = DateTime.UtcNow }
    
        static member CreateTableSql() = """
        CREATE TABLE item_states (
	item_id TEXT NOT NULL,
	serial INTEGER NOT NULL,
	state_blob BLOB NOT NULL,
	state_timestamp TEXT NOT NULL,
	CONSTRAINT item_states_PK PRIMARY KEY (item_id,serial),
	CONSTRAINT item_states_FK FOREIGN KEY (item_id) REFERENCES items(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              item_id,
              serial,
              state_blob,
              state_timestamp
        FROM item_states
        """
    
        static member TableName() = "item_states"
    
    /// A record representing a row in the table `items`.
    type Item =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("referenceId")>] ReferenceId: string
          [<JsonPropertyName("referenceType")>] ReferenceType: string }
    
        static member Blank() =
            { Id = String.Empty
              ReferenceId = String.Empty
              ReferenceType = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE items (
	id TEXT NOT NULL,
	reference_id TEXT NOT NULL,
	reference_type TEXT NOT NULL,
	CONSTRAINT items_PK PRIMARY KEY (id),
	CONSTRAINT items_FK FOREIGN KEY (reference_type) REFERENCES registered_types(name)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              reference_id,
              reference_type
        FROM items
        """
    
        static member TableName() = "items"
    
    /// A record representing a row in the table `link_metadata`.
    type LinkMetadataItem =
        { [<JsonPropertyName("linkId")>] LinkId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { LinkId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE link_metadata (
	link_id TEXT NOT NULL,
	data_key TEXT NOT NULL,
	data_value TEXT NOT NULL,
	CONSTRAINT link_metadata_PK PRIMARY KEY (link_id,data_key),
	CONSTRAINT link_metadata_FK FOREIGN KEY (link_id) REFERENCES links(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              link_id,
              data_key,
              data_value
        FROM link_metadata
        """
    
        static member TableName() = "link_metadata"
    
    /// A record representing a row in the table `links`.
    type Link =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("fromId")>] FromId: string
          [<JsonPropertyName("toId")>] ToId: string
          [<JsonPropertyName("twoWay")>] TwoWay: bool }
    
        static member Blank() =
            { Id = String.Empty
              FromId = String.Empty
              ToId = String.Empty
              TwoWay = true }
    
        static member CreateTableSql() = """
        CREATE TABLE links (
	id TEXT NOT NULL,
	from_id TEXT NOT NULL,
	to_id TEXT NOT NULL,
	two_way INTEGER NOT NULL,
	CONSTRAINT links_PK PRIMARY KEY (id),
	CONSTRAINT links_FK FOREIGN KEY (from_id) REFERENCES items(id),
	CONSTRAINT links_FK_1 FOREIGN KEY (to_id) REFERENCES items(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              from_id,
              to_id,
              two_way
        FROM links
        """
    
        static member TableName() = "links"
    
    /// A record representing a row in the table `project_item_link_metadata`.
    type ProjectItemLinkMetadataItem =
        { [<JsonPropertyName("linkId")>] LinkId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { LinkId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE project_item_link_metadata (
	link_id TEXT NOT NULL,
	data_key TEXT NOT NULL,
	data_value TEXT NOT NULL,
	CONSTRAINT project_item_link_metadata_PK PRIMARY KEY (link_id,data_key),
	CONSTRAINT project_item_link_metadata_FK FOREIGN KEY (link_id) REFERENCES project_item_links(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              link_id,
              data_key,
              data_value
        FROM project_item_link_metadata
        """
    
        static member TableName() = "project_item_link_metadata"
    
    /// A record representing a row in the table `project_item_links`.
    type ProjectItemLink =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("projectId")>] ProjectId: string
          [<JsonPropertyName("itemId")>] ItemId: string }
    
        static member Blank() =
            { Id = String.Empty
              ProjectId = String.Empty
              ItemId = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE project_item_links (
	id TEXT NOT NULL,
	project_id TEXT NOT NULL,
	item_id TEXT NOT NULL,
	CONSTRAINT project_item_links_PK PRIMARY KEY (id),
	CONSTRAINT project_item_links_FK FOREIGN KEY (project_id) REFERENCES projects(id),
	CONSTRAINT project_item_links_FK_1 FOREIGN KEY (item_id) REFERENCES items(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              project_id,
              item_id
        FROM project_item_links
        """
    
        static member TableName() = "project_item_links"
    
    /// A record representing a row in the table `project_metadata`.
    type ProjectMetadataItem =
        { [<JsonPropertyName("projectId")>] ProjectId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { ProjectId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE project_metadata (
	project_id TEXT NOT NULL,
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
    
    /// A record representing a row in the table `project_resource_link_metadata`.
    type ProjectResourceLinkMetadataItem =
        { [<JsonPropertyName("linkId")>] LinkId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { LinkId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE project_resource_link_metadata (
	link_id TEXT NOT NULL,
	data_key TEXT NOT NULL,
	data_value TEXT NOT NULL,
	CONSTRAINT project_resource_link_metadata_PK PRIMARY KEY (link_id,data_key),
	CONSTRAINT project_resource_link_metadata_FK FOREIGN KEY (link_id) REFERENCES project_resource_links(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              link_id,
              data_key,
              data_value
        FROM project_resource_link_metadata
        """
    
        static member TableName() = "project_resource_link_metadata"
    
    /// A record representing a row in the table `project_resource_links`.
    type ProjectResourceLink =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("projectId")>] ProjectId: string
          [<JsonPropertyName("resourceId")>] ResourceId: string }
    
        static member Blank() =
            { Id = String.Empty
              ProjectId = String.Empty
              ResourceId = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE project_resource_links (
	id TEXT NOT NULL,
	project_id TEXT NOT NULL,
	resource_id TEXT NOT NULL,
	CONSTRAINT project_resource_links_PK PRIMARY KEY (id),
	CONSTRAINT project_resource_links_FK FOREIGN KEY (project_id) REFERENCES projects(id),
	CONSTRAINT project_resource_links_FK_1 FOREIGN KEY (resource_id) REFERENCES resources(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              project_id,
              resource_id
        FROM project_resource_links
        """
    
        static member TableName() = "project_resource_links"
    
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
    
    /// A record representing a row in the table `registered_handlers`.
    type RegisteredHandler =
        { [<JsonPropertyName("name")>] Name: string
          [<JsonPropertyName("version")>] Version: int64 }
    
        static member Blank() =
            { Name = String.Empty
              Version = 0L }
    
        static member CreateTableSql() = """
        CREATE TABLE registered_handlers (
	name TEXT NOT NULL,
	version INTEGER DEFAULT 1 NOT NULL,
	CONSTRAINT registered_handlers_PK PRIMARY KEY (name)
)
        """
    
        static member SelectSql() = """
        SELECT
              name,
              version
        FROM registered_handlers
        """
    
        static member TableName() = "registered_handlers"
    
    /// A record representing a row in the table `registered_types`.
    type RegisteredType =
        { [<JsonPropertyName("name")>] Name: string
          [<JsonPropertyName("handlerName")>] HandlerName: string }
    
        static member Blank() =
            { Name = String.Empty
              HandlerName = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE registered_types (
	name TEXT NOT NULL,
	handler_name TEXT NOT NULL,
	CONSTRAINT registered_types_PK PRIMARY KEY (name),
	CONSTRAINT registered_types_FK FOREIGN KEY (handler_name) REFERENCES registered_handlers(name)
)
        """
    
        static member SelectSql() = """
        SELECT
              name,
              handler_name
        FROM registered_types
        """
    
        static member TableName() = "registered_types"
    
    /// A record representing a row in the table `resource_metadata`.
    type ResourceMetadataItem =
        { [<JsonPropertyName("resourceId")>] ResourceId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { ResourceId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE resource_metadata (
	resource_id TEXT NOT NULL,
	data_key TEXT NOT NULL,
	data_value TEXT NOT NULL,
	CONSTRAINT resource_metadata_PK PRIMARY KEY (resource_id,data_key),
	CONSTRAINT resource_metadata_FK FOREIGN KEY (resource_id) REFERENCES resources(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              resource_id,
              data_key,
              data_value
        FROM resource_metadata
        """
    
        static member TableName() = "resource_metadata"
    
    /// A record representing a row in the table `resource_version`.
    type ResourceVersion =
        { [<JsonPropertyName("resourceId")>] ResourceId: string
          [<JsonPropertyName("version")>] Version: int64
          [<JsonPropertyName("versionName")>] VersionName: string option
          [<JsonPropertyName("resourceBlob")>] ResourceBlob: BlobField
          [<JsonPropertyName("blobHash")>] BlobHash: string
          [<JsonPropertyName("createdOn")>] CreatedOn: DateTime
          [<JsonPropertyName("contentType")>] ContentType: string }
    
        static member Blank() =
            { ResourceId = String.Empty
              Version = 0L
              VersionName = None
              ResourceBlob = BlobField.Empty()
              BlobHash = String.Empty
              CreatedOn = DateTime.UtcNow
              ContentType = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE resource_version (
	resource_id TEXT NOT NULL,
	version INTEGER NOT NULL,
	version_name TEXT,
	resource_blob BLOB NOT NULL,
	blob_hash TEXT NOT NULL,
	created_on TEXT NOT NULL,
	content_type TEXT NOT NULL,
	CONSTRAINT resource_version_PK PRIMARY KEY (resource_id,version),
	CONSTRAINT resource_version_FK FOREIGN KEY (resource_id) REFERENCES resources(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              resource_id,
              version,
              version_name,
              resource_blob,
              blob_hash,
              created_on,
              content_type
        FROM resource_version
        """
    
        static member TableName() = "resource_version"
    
    /// A record representing a row in the table `resources`.
    type Resource =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE resources (
	id TEXT NOT NULL,
	name TEXT NOT NULL,
	CONSTRAINT resources_PK PRIMARY KEY (id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              name
        FROM resources
        """
    
        static member TableName() = "resources"
    
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
    
    /// A record representing a row in the table `workflow_action_types`.
    type WorkflowActionType =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("workflowId")>] WorkflowId: string
          [<JsonPropertyName("actionTypeId")>] ActionTypeId: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Id = String.Empty
              WorkflowId = String.Empty
              ActionTypeId = String.Empty
              Name = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE workflow_action_types (
	id TEXT NOT NULL,
	workflow_id TEXT NOT NULL,
	action_type_id TEXT NOT NULL,
	name TEXT NOT NULL,
	CONSTRAINT workflow_action_types_PK PRIMARY KEY (id),
	CONSTRAINT workflow_action_types_FK FOREIGN KEY (workflow_id) REFERENCES workflows(id),
	CONSTRAINT workflow_action_types_FK_1 FOREIGN KEY (action_type_id) REFERENCES action_types(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              workflow_id,
              action_type_id,
              name
        FROM workflow_action_types
        """
    
        static member TableName() = "workflow_action_types"
    
    /// A record representing a row in the table `workflow_actions`.
    type WorkflowAction =
        { [<JsonPropertyName("eventId")>] EventId: string
          [<JsonPropertyName("typeId")>] TypeId: string
          [<JsonPropertyName("status")>] Status: string
          [<JsonPropertyName("complete")>] Complete: bool
          [<JsonPropertyName("actionTimestamp")>] ActionTimestamp: DateTime option }
    
        static member Blank() =
            { EventId = String.Empty
              TypeId = String.Empty
              Status = String.Empty
              Complete = true
              ActionTimestamp = None }
    
        static member CreateTableSql() = """
        CREATE TABLE workflow_actions (
	event_id TEXT NOT NULL,
	type_id TEXT NOT NULL,
	status TEXT NOT NULL,
	complete INTEGER NOT NULL,
	action_timestamp TEXT,
	CONSTRAINT workflow_actions_PK PRIMARY KEY (event_id,type_id),
	CONSTRAINT workflow_actions_FK FOREIGN KEY (event_id) REFERENCES workflow_events(id),
	CONSTRAINT workflow_actions_FK_1 FOREIGN KEY (type_id) REFERENCES workflow_action_types(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              event_id,
              type_id,
              status,
              complete,
              action_timestamp
        FROM workflow_actions
        """
    
        static member TableName() = "workflow_actions"
    
    /// A record representing a row in the table `workflow_condition_types`.
    type WorkflowConditionType =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("workflowId")>] WorkflowId: string
          [<JsonPropertyName("conditionTypeId")>] ConditionTypeId: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Id = String.Empty
              WorkflowId = String.Empty
              ConditionTypeId = String.Empty
              Name = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE workflow_condition_types (
	id TEXT NOT NULL,
	workflow_id TEXT NOT NULL,
	condition_type_id TEXT NOT NULL,
	name TEXT NOT NULL,
	CONSTRAINT workflow_condition_types_PK PRIMARY KEY (id),
	CONSTRAINT workflow_condition_types_FK FOREIGN KEY (workflow_id) REFERENCES workflows(id),
	CONSTRAINT workflow_condition_types_FK_1 FOREIGN KEY (condition_type_id) REFERENCES condition_types(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              workflow_id,
              condition_type_id,
              name
        FROM workflow_condition_types
        """
    
        static member TableName() = "workflow_condition_types"
    
    /// A record representing a row in the table `workflow_event_actions`.
    type WorkflowEventAction =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("eventId")>] EventId: string
          [<JsonPropertyName("actionId")>] ActionId: string
          [<JsonPropertyName("conditionId")>] ConditionId: string option
          [<JsonPropertyName("active")>] Active: bool }
    
        static member Blank() =
            { Id = String.Empty
              EventId = String.Empty
              ActionId = String.Empty
              ConditionId = None
              Active = true }
    
        static member CreateTableSql() = """
        CREATE TABLE workflow_event_actions (
	id TEXT NOT NULL,
	event_id TEXT NOT NULL,
	action_id TEXT NOT NULL,
	condition_id TEXT,
	active INTEGER NOT NULL,
	CONSTRAINT workflow_event_actions_PK PRIMARY KEY (id),
	CONSTRAINT workflow_event_actions_FK FOREIGN KEY (event_id) REFERENCES workflow_event_types(id),
	CONSTRAINT workflow_event_actions_FK_1 FOREIGN KEY (action_id) REFERENCES workflow_action_types(id),
	CONSTRAINT workflow_event_actions_FK_2 FOREIGN KEY (condition_id) REFERENCES workflow_condition_types(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              event_id,
              action_id,
              condition_id,
              active
        FROM workflow_event_actions
        """
    
        static member TableName() = "workflow_event_actions"
    
    /// A record representing a row in the table `workflow_event_types`.
    type WorkflowEventType =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("workflowId")>] WorkflowId: string
          [<JsonPropertyName("eventTypeId")>] EventTypeId: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Id = String.Empty
              WorkflowId = String.Empty
              EventTypeId = String.Empty
              Name = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE workflow_event_types (
	id TEXT NOT NULL,
	workflow_id TEXT NOT NULL,
	event_type_id TEXT NOT NULL,
	name TEXT NOT NULL,
	CONSTRAINT workflow_event_types_PK PRIMARY KEY (id),
	CONSTRAINT workflow_event_types_FK FOREIGN KEY (workflow_id) REFERENCES workflows(id),
	CONSTRAINT workflow_event_types_FK_1 FOREIGN KEY (event_type_id) REFERENCES event_types(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              workflow_id,
              event_type_id,
              name
        FROM workflow_event_types
        """
    
        static member TableName() = "workflow_event_types"
    
    /// A record representing a row in the table `workflow_events`.
    type WorkflowEvent =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("typeId")>] TypeId: string
          [<JsonPropertyName("itemId")>] ItemId: string
          [<JsonPropertyName("opsBlob")>] OpsBlob: BlobField
          [<JsonPropertyName("eventTimestamp")>] EventTimestamp: DateTime }
    
        static member Blank() =
            { Id = String.Empty
              TypeId = String.Empty
              ItemId = String.Empty
              OpsBlob = BlobField.Empty()
              EventTimestamp = DateTime.UtcNow }
    
        static member CreateTableSql() = """
        CREATE TABLE workflow_events (
	id TEXT NOT NULL,
	type_id TEXT NOT NULL,
	item_id TEXT NOT NULL,
	ops_blob BLOB NOT NULL,
	event_timestamp TEXT NOT NULL,
	CONSTRAINT workflow_events_PK PRIMARY KEY (id),
	CONSTRAINT workflow_events_FK FOREIGN KEY (type_id) REFERENCES workflow_event_types(id),
	CONSTRAINT workflow_events_FK_1 FOREIGN KEY (item_id) REFERENCES items(id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              type_id,
              item_id,
              ops_blob,
              event_timestamp
        FROM workflow_events
        """
    
        static member TableName() = "workflow_events"
    
    /// A record representing a row in the table `workflows`.
    type Workflow =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty }
    
        static member CreateTableSql() = """
        CREATE TABLE workflows (
	id TEXT NOT NULL,
	name TEXT NOT NULL,
	CONSTRAINT workflows_PK PRIMARY KEY (id)
)
        """
    
        static member SelectSql() = """
        SELECT
              id,
              name
        FROM workflows
        """
    
        static member TableName() = "workflows"
    

/// Module generated on 19/02/2022 14:41:16 (utc) via Freql.Tools.
[<RequireQualifiedAccess>]
module Parameters =
    /// A record representing a new row in the table `action_types`.
    type NewActionType =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string
          [<JsonPropertyName("parentType")>] ParentType: string
          [<JsonPropertyName("typeBlob")>] TypeBlob: BlobField }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty
              ParentType = String.Empty
              TypeBlob = BlobField.Empty() }
    
    
    /// A record representing a new row in the table `condition_types`.
    type NewConditionType =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string
          [<JsonPropertyName("parentType")>] ParentType: string
          [<JsonPropertyName("typeBlob")>] TypeBlob: BlobField }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty
              ParentType = String.Empty
              TypeBlob = BlobField.Empty() }
    
    
    /// A record representing a new row in the table `event_types`.
    type NewEventType =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string
          [<JsonPropertyName("parentType")>] ParentType: string
          [<JsonPropertyName("typeBlob")>] TypeBlob: BlobField }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty
              ParentType = String.Empty
              TypeBlob = BlobField.Empty() }
    
    
    /// A record representing a new row in the table `item_metadata`.
    type NewItemMetadataItem =
        { [<JsonPropertyName("itemId")>] ItemId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { ItemId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
    
    /// A record representing a new row in the table `item_resource_link_metadata`.
    type NewItemResourceLinkMetadataItem =
        { [<JsonPropertyName("linkId")>] LinkId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { LinkId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
    
    /// A record representing a new row in the table `item_resource_links`.
    type NewItemResourceLink =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("itemId")>] ItemId: string
          [<JsonPropertyName("resourceId")>] ResourceId: string }
    
        static member Blank() =
            { Id = String.Empty
              ItemId = String.Empty
              ResourceId = String.Empty }
    
    
    /// A record representing a new row in the table `item_states`.
    type NewItemState =
        { [<JsonPropertyName("itemId")>] ItemId: string
          [<JsonPropertyName("serial")>] Serial: int64
          [<JsonPropertyName("stateBlob")>] StateBlob: BlobField
          [<JsonPropertyName("stateTimestamp")>] StateTimestamp: DateTime }
    
        static member Blank() =
            { ItemId = String.Empty
              Serial = 0L
              StateBlob = BlobField.Empty()
              StateTimestamp = DateTime.UtcNow }
    
    
    /// A record representing a new row in the table `items`.
    type NewItem =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("referenceId")>] ReferenceId: string
          [<JsonPropertyName("referenceType")>] ReferenceType: string }
    
        static member Blank() =
            { Id = String.Empty
              ReferenceId = String.Empty
              ReferenceType = String.Empty }
    
    
    /// A record representing a new row in the table `link_metadata`.
    type NewLinkMetadataItem =
        { [<JsonPropertyName("linkId")>] LinkId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { LinkId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
    
    /// A record representing a new row in the table `links`.
    type NewLink =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("fromId")>] FromId: string
          [<JsonPropertyName("toId")>] ToId: string
          [<JsonPropertyName("twoWay")>] TwoWay: bool }
    
        static member Blank() =
            { Id = String.Empty
              FromId = String.Empty
              ToId = String.Empty
              TwoWay = true }
    
    
    /// A record representing a new row in the table `project_item_link_metadata`.
    type NewProjectItemLinkMetadataItem =
        { [<JsonPropertyName("linkId")>] LinkId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { LinkId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
    
    /// A record representing a new row in the table `project_item_links`.
    type NewProjectItemLink =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("projectId")>] ProjectId: string
          [<JsonPropertyName("itemId")>] ItemId: string }
    
        static member Blank() =
            { Id = String.Empty
              ProjectId = String.Empty
              ItemId = String.Empty }
    
    
    /// A record representing a new row in the table `project_metadata`.
    type NewProjectMetadataItem =
        { [<JsonPropertyName("projectId")>] ProjectId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { ProjectId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
    
    /// A record representing a new row in the table `project_resource_link_metadata`.
    type NewProjectResourceLinkMetadataItem =
        { [<JsonPropertyName("linkId")>] LinkId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { LinkId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
    
    /// A record representing a new row in the table `project_resource_links`.
    type NewProjectResourceLink =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("projectId")>] ProjectId: string
          [<JsonPropertyName("resourceId")>] ResourceId: string }
    
        static member Blank() =
            { Id = String.Empty
              ProjectId = String.Empty
              ResourceId = String.Empty }
    
    
    /// A record representing a new row in the table `projects`.
    type NewProject =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty }
    
    
    /// A record representing a new row in the table `registered_handlers`.
    type NewRegisteredHandler =
        { [<JsonPropertyName("name")>] Name: string
          [<JsonPropertyName("version")>] Version: int64 }
    
        static member Blank() =
            { Name = String.Empty
              Version = 0L }
    
    
    /// A record representing a new row in the table `registered_types`.
    type NewRegisteredType =
        { [<JsonPropertyName("name")>] Name: string
          [<JsonPropertyName("handlerName")>] HandlerName: string }
    
        static member Blank() =
            { Name = String.Empty
              HandlerName = String.Empty }
    
    
    /// A record representing a new row in the table `resource_metadata`.
    type NewResourceMetadataItem =
        { [<JsonPropertyName("resourceId")>] ResourceId: string
          [<JsonPropertyName("dataKey")>] DataKey: string
          [<JsonPropertyName("dataValue")>] DataValue: string }
    
        static member Blank() =
            { ResourceId = String.Empty
              DataKey = String.Empty
              DataValue = String.Empty }
    
    
    /// A record representing a new row in the table `resource_version`.
    type NewResourceVersion =
        { [<JsonPropertyName("resourceId")>] ResourceId: string
          [<JsonPropertyName("version")>] Version: int64
          [<JsonPropertyName("versionName")>] VersionName: string option
          [<JsonPropertyName("resourceBlob")>] ResourceBlob: BlobField
          [<JsonPropertyName("blobHash")>] BlobHash: string
          [<JsonPropertyName("createdOn")>] CreatedOn: DateTime
          [<JsonPropertyName("contentType")>] ContentType: string }
    
        static member Blank() =
            { ResourceId = String.Empty
              Version = 0L
              VersionName = None
              ResourceBlob = BlobField.Empty()
              BlobHash = String.Empty
              CreatedOn = DateTime.UtcNow
              ContentType = String.Empty }
    
    
    /// A record representing a new row in the table `resources`.
    type NewResource =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty }
    
    
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
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("username")>] Username: string
          [<JsonPropertyName("displayName")>] DisplayName: string }
    
        static member Blank() =
            { Id = String.Empty
              Username = String.Empty
              DisplayName = String.Empty }
    
    
    /// A record representing a new row in the table `workflow_action_types`.
    type NewWorkflowActionType =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("workflowId")>] WorkflowId: string
          [<JsonPropertyName("actionTypeId")>] ActionTypeId: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Id = String.Empty
              WorkflowId = String.Empty
              ActionTypeId = String.Empty
              Name = String.Empty }
    
    
    /// A record representing a new row in the table `workflow_actions`.
    type NewWorkflowAction =
        { [<JsonPropertyName("eventId")>] EventId: string
          [<JsonPropertyName("typeId")>] TypeId: string
          [<JsonPropertyName("status")>] Status: string
          [<JsonPropertyName("complete")>] Complete: bool
          [<JsonPropertyName("actionTimestamp")>] ActionTimestamp: DateTime option }
    
        static member Blank() =
            { EventId = String.Empty
              TypeId = String.Empty
              Status = String.Empty
              Complete = true
              ActionTimestamp = None }
    
    
    /// A record representing a new row in the table `workflow_condition_types`.
    type NewWorkflowConditionType =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("workflowId")>] WorkflowId: string
          [<JsonPropertyName("conditionTypeId")>] ConditionTypeId: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Id = String.Empty
              WorkflowId = String.Empty
              ConditionTypeId = String.Empty
              Name = String.Empty }
    
    
    /// A record representing a new row in the table `workflow_event_actions`.
    type NewWorkflowEventAction =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("eventId")>] EventId: string
          [<JsonPropertyName("actionId")>] ActionId: string
          [<JsonPropertyName("conditionId")>] ConditionId: string option
          [<JsonPropertyName("active")>] Active: bool }
    
        static member Blank() =
            { Id = String.Empty
              EventId = String.Empty
              ActionId = String.Empty
              ConditionId = None
              Active = true }
    
    
    /// A record representing a new row in the table `workflow_event_types`.
    type NewWorkflowEventType =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("workflowId")>] WorkflowId: string
          [<JsonPropertyName("eventTypeId")>] EventTypeId: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Id = String.Empty
              WorkflowId = String.Empty
              EventTypeId = String.Empty
              Name = String.Empty }
    
    
    /// A record representing a new row in the table `workflow_events`.
    type NewWorkflowEvent =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("typeId")>] TypeId: string
          [<JsonPropertyName("itemId")>] ItemId: string
          [<JsonPropertyName("opsBlob")>] OpsBlob: BlobField
          [<JsonPropertyName("eventTimestamp")>] EventTimestamp: DateTime }
    
        static member Blank() =
            { Id = String.Empty
              TypeId = String.Empty
              ItemId = String.Empty
              OpsBlob = BlobField.Empty()
              EventTimestamp = DateTime.UtcNow }
    
    
    /// A record representing a new row in the table `workflows`.
    type NewWorkflow =
        { [<JsonPropertyName("id")>] Id: string
          [<JsonPropertyName("name")>] Name: string }
    
        static member Blank() =
            { Id = String.Empty
              Name = String.Empty }
    
    
/// Module generated on 19/02/2022 14:41:16 (utc) via Freql.Tools.
[<RequireQualifiedAccess>]
module Operations =

    let buildSql (lines: string list) = lines |> String.concat Environment.NewLine

    /// Select a `Records.ActionType` from the table `action_types`.
    /// Internally this calls `context.SelectSingleAnon<Records.ActionType>` and uses Records.ActionType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectActionTypeRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectActionTypeRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ActionType.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ActionType>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ActionType>` and uses Records.ActionType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectActionTypeRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectActionTypeRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ActionType.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ActionType>(sql, parameters)
    
    let insertActionType (context: SqliteContext) (parameters: Parameters.NewActionType) =
        context.Insert("action_types", parameters)
    
    /// Select a `Records.ConditionType` from the table `condition_types`.
    /// Internally this calls `context.SelectSingleAnon<Records.ConditionType>` and uses Records.ConditionType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectConditionTypeRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectConditionTypeRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ConditionType.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ConditionType>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ConditionType>` and uses Records.ConditionType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectConditionTypeRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectConditionTypeRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ConditionType.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ConditionType>(sql, parameters)
    
    let insertConditionType (context: SqliteContext) (parameters: Parameters.NewConditionType) =
        context.Insert("condition_types", parameters)
    
    /// Select a `Records.EventType` from the table `event_types`.
    /// Internally this calls `context.SelectSingleAnon<Records.EventType>` and uses Records.EventType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectEventTypeRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectEventTypeRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.EventType.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.EventType>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.EventType>` and uses Records.EventType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectEventTypeRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectEventTypeRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.EventType.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.EventType>(sql, parameters)
    
    let insertEventType (context: SqliteContext) (parameters: Parameters.NewEventType) =
        context.Insert("event_types", parameters)
    
    /// Select a `Records.ItemMetadataItem` from the table `item_metadata`.
    /// Internally this calls `context.SelectSingleAnon<Records.ItemMetadataItem>` and uses Records.ItemMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectItemMetadataItemRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectItemMetadataItemRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ItemMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ItemMetadataItem>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ItemMetadataItem>` and uses Records.ItemMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectItemMetadataItemRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectItemMetadataItemRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ItemMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ItemMetadataItem>(sql, parameters)
    
    let insertItemMetadataItem (context: SqliteContext) (parameters: Parameters.NewItemMetadataItem) =
        context.Insert("item_metadata", parameters)
    
    /// Select a `Records.ItemResourceLinkMetadataItem` from the table `item_resource_link_metadata`.
    /// Internally this calls `context.SelectSingleAnon<Records.ItemResourceLinkMetadataItem>` and uses Records.ItemResourceLinkMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectItemResourceLinkMetadataItemRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectItemResourceLinkMetadataItemRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ItemResourceLinkMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ItemResourceLinkMetadataItem>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ItemResourceLinkMetadataItem>` and uses Records.ItemResourceLinkMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectItemResourceLinkMetadataItemRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectItemResourceLinkMetadataItemRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ItemResourceLinkMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ItemResourceLinkMetadataItem>(sql, parameters)
    
    let insertItemResourceLinkMetadataItem (context: SqliteContext) (parameters: Parameters.NewItemResourceLinkMetadataItem) =
        context.Insert("item_resource_link_metadata", parameters)
    
    /// Select a `Records.ItemResourceLink` from the table `item_resource_links`.
    /// Internally this calls `context.SelectSingleAnon<Records.ItemResourceLink>` and uses Records.ItemResourceLink.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectItemResourceLinkRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectItemResourceLinkRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ItemResourceLink.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ItemResourceLink>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ItemResourceLink>` and uses Records.ItemResourceLink.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectItemResourceLinkRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectItemResourceLinkRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ItemResourceLink.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ItemResourceLink>(sql, parameters)
    
    let insertItemResourceLink (context: SqliteContext) (parameters: Parameters.NewItemResourceLink) =
        context.Insert("item_resource_links", parameters)
    
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
    
    /// Select a `Records.Item` from the table `items`.
    /// Internally this calls `context.SelectSingleAnon<Records.Item>` and uses Records.Item.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectItemRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectItemRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.Item.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.Item>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.Item>` and uses Records.Item.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectItemRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectItemRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.Item.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.Item>(sql, parameters)
    
    let insertItem (context: SqliteContext) (parameters: Parameters.NewItem) =
        context.Insert("items", parameters)
    
    /// Select a `Records.LinkMetadataItem` from the table `link_metadata`.
    /// Internally this calls `context.SelectSingleAnon<Records.LinkMetadataItem>` and uses Records.LinkMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectLinkMetadataItemRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectLinkMetadataItemRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.LinkMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.LinkMetadataItem>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.LinkMetadataItem>` and uses Records.LinkMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectLinkMetadataItemRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectLinkMetadataItemRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.LinkMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.LinkMetadataItem>(sql, parameters)
    
    let insertLinkMetadataItem (context: SqliteContext) (parameters: Parameters.NewLinkMetadataItem) =
        context.Insert("link_metadata", parameters)
    
    /// Select a `Records.Link` from the table `links`.
    /// Internally this calls `context.SelectSingleAnon<Records.Link>` and uses Records.Link.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectLinkRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectLinkRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.Link.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.Link>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.Link>` and uses Records.Link.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectLinkRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectLinkRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.Link.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.Link>(sql, parameters)
    
    let insertLink (context: SqliteContext) (parameters: Parameters.NewLink) =
        context.Insert("links", parameters)
    
    /// Select a `Records.ProjectItemLinkMetadataItem` from the table `project_item_link_metadata`.
    /// Internally this calls `context.SelectSingleAnon<Records.ProjectItemLinkMetadataItem>` and uses Records.ProjectItemLinkMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectProjectItemLinkMetadataItemRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectProjectItemLinkMetadataItemRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ProjectItemLinkMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ProjectItemLinkMetadataItem>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ProjectItemLinkMetadataItem>` and uses Records.ProjectItemLinkMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectProjectItemLinkMetadataItemRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectProjectItemLinkMetadataItemRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ProjectItemLinkMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ProjectItemLinkMetadataItem>(sql, parameters)
    
    let insertProjectItemLinkMetadataItem (context: SqliteContext) (parameters: Parameters.NewProjectItemLinkMetadataItem) =
        context.Insert("project_item_link_metadata", parameters)
    
    /// Select a `Records.ProjectItemLink` from the table `project_item_links`.
    /// Internally this calls `context.SelectSingleAnon<Records.ProjectItemLink>` and uses Records.ProjectItemLink.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectProjectItemLinkRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectProjectItemLinkRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ProjectItemLink.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ProjectItemLink>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ProjectItemLink>` and uses Records.ProjectItemLink.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectProjectItemLinkRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectProjectItemLinkRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ProjectItemLink.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ProjectItemLink>(sql, parameters)
    
    let insertProjectItemLink (context: SqliteContext) (parameters: Parameters.NewProjectItemLink) =
        context.Insert("project_item_links", parameters)
    
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
    
    /// Select a `Records.ProjectResourceLinkMetadataItem` from the table `project_resource_link_metadata`.
    /// Internally this calls `context.SelectSingleAnon<Records.ProjectResourceLinkMetadataItem>` and uses Records.ProjectResourceLinkMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectProjectResourceLinkMetadataItemRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectProjectResourceLinkMetadataItemRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ProjectResourceLinkMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ProjectResourceLinkMetadataItem>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ProjectResourceLinkMetadataItem>` and uses Records.ProjectResourceLinkMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectProjectResourceLinkMetadataItemRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectProjectResourceLinkMetadataItemRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ProjectResourceLinkMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ProjectResourceLinkMetadataItem>(sql, parameters)
    
    let insertProjectResourceLinkMetadataItem (context: SqliteContext) (parameters: Parameters.NewProjectResourceLinkMetadataItem) =
        context.Insert("project_resource_link_metadata", parameters)
    
    /// Select a `Records.ProjectResourceLink` from the table `project_resource_links`.
    /// Internally this calls `context.SelectSingleAnon<Records.ProjectResourceLink>` and uses Records.ProjectResourceLink.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectProjectResourceLinkRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectProjectResourceLinkRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ProjectResourceLink.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ProjectResourceLink>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ProjectResourceLink>` and uses Records.ProjectResourceLink.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectProjectResourceLinkRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectProjectResourceLinkRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ProjectResourceLink.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ProjectResourceLink>(sql, parameters)
    
    let insertProjectResourceLink (context: SqliteContext) (parameters: Parameters.NewProjectResourceLink) =
        context.Insert("project_resource_links", parameters)
    
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
    
    /// Select a `Records.RegisteredHandler` from the table `registered_handlers`.
    /// Internally this calls `context.SelectSingleAnon<Records.RegisteredHandler>` and uses Records.RegisteredHandler.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectRegisteredHandlerRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectRegisteredHandlerRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.RegisteredHandler.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.RegisteredHandler>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.RegisteredHandler>` and uses Records.RegisteredHandler.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectRegisteredHandlerRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectRegisteredHandlerRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.RegisteredHandler.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.RegisteredHandler>(sql, parameters)
    
    let insertRegisteredHandler (context: SqliteContext) (parameters: Parameters.NewRegisteredHandler) =
        context.Insert("registered_handlers", parameters)
    
    /// Select a `Records.RegisteredType` from the table `registered_types`.
    /// Internally this calls `context.SelectSingleAnon<Records.RegisteredType>` and uses Records.RegisteredType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectRegisteredTypeRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectRegisteredTypeRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.RegisteredType.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.RegisteredType>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.RegisteredType>` and uses Records.RegisteredType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectRegisteredTypeRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectRegisteredTypeRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.RegisteredType.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.RegisteredType>(sql, parameters)
    
    let insertRegisteredType (context: SqliteContext) (parameters: Parameters.NewRegisteredType) =
        context.Insert("registered_types", parameters)
    
    /// Select a `Records.ResourceMetadataItem` from the table `resource_metadata`.
    /// Internally this calls `context.SelectSingleAnon<Records.ResourceMetadataItem>` and uses Records.ResourceMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectResourceMetadataItemRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectResourceMetadataItemRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ResourceMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ResourceMetadataItem>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ResourceMetadataItem>` and uses Records.ResourceMetadataItem.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectResourceMetadataItemRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectResourceMetadataItemRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ResourceMetadataItem.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ResourceMetadataItem>(sql, parameters)
    
    let insertResourceMetadataItem (context: SqliteContext) (parameters: Parameters.NewResourceMetadataItem) =
        context.Insert("resource_metadata", parameters)
    
    /// Select a `Records.ResourceVersion` from the table `resource_version`.
    /// Internally this calls `context.SelectSingleAnon<Records.ResourceVersion>` and uses Records.ResourceVersion.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectResourceVersionRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectResourceVersionRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ResourceVersion.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.ResourceVersion>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.ResourceVersion>` and uses Records.ResourceVersion.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectResourceVersionRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectResourceVersionRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.ResourceVersion.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.ResourceVersion>(sql, parameters)
    
    let insertResourceVersion (context: SqliteContext) (parameters: Parameters.NewResourceVersion) =
        context.Insert("resource_version", parameters)
    
    /// Select a `Records.Resource` from the table `resources`.
    /// Internally this calls `context.SelectSingleAnon<Records.Resource>` and uses Records.Resource.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectResourceRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectResourceRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.Resource.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.Resource>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.Resource>` and uses Records.Resource.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectResourceRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectResourceRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.Resource.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.Resource>(sql, parameters)
    
    let insertResource (context: SqliteContext) (parameters: Parameters.NewResource) =
        context.Insert("resources", parameters)
    
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
    
    /// Select a `Records.WorkflowActionType` from the table `workflow_action_types`.
    /// Internally this calls `context.SelectSingleAnon<Records.WorkflowActionType>` and uses Records.WorkflowActionType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowActionTypeRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowActionTypeRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.WorkflowActionType.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.WorkflowActionType>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.WorkflowActionType>` and uses Records.WorkflowActionType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowActionTypeRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowActionTypeRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.WorkflowActionType.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.WorkflowActionType>(sql, parameters)
    
    let insertWorkflowActionType (context: SqliteContext) (parameters: Parameters.NewWorkflowActionType) =
        context.Insert("workflow_action_types", parameters)
    
    /// Select a `Records.WorkflowAction` from the table `workflow_actions`.
    /// Internally this calls `context.SelectSingleAnon<Records.WorkflowAction>` and uses Records.WorkflowAction.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowActionRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowActionRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.WorkflowAction.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.WorkflowAction>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.WorkflowAction>` and uses Records.WorkflowAction.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowActionRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowActionRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.WorkflowAction.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.WorkflowAction>(sql, parameters)
    
    let insertWorkflowAction (context: SqliteContext) (parameters: Parameters.NewWorkflowAction) =
        context.Insert("workflow_actions", parameters)
    
    /// Select a `Records.WorkflowConditionType` from the table `workflow_condition_types`.
    /// Internally this calls `context.SelectSingleAnon<Records.WorkflowConditionType>` and uses Records.WorkflowConditionType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowConditionTypeRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowConditionTypeRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.WorkflowConditionType.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.WorkflowConditionType>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.WorkflowConditionType>` and uses Records.WorkflowConditionType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowConditionTypeRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowConditionTypeRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.WorkflowConditionType.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.WorkflowConditionType>(sql, parameters)
    
    let insertWorkflowConditionType (context: SqliteContext) (parameters: Parameters.NewWorkflowConditionType) =
        context.Insert("workflow_condition_types", parameters)
    
    /// Select a `Records.WorkflowEventAction` from the table `workflow_event_actions`.
    /// Internally this calls `context.SelectSingleAnon<Records.WorkflowEventAction>` and uses Records.WorkflowEventAction.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowEventActionRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowEventActionRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.WorkflowEventAction.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.WorkflowEventAction>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.WorkflowEventAction>` and uses Records.WorkflowEventAction.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowEventActionRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowEventActionRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.WorkflowEventAction.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.WorkflowEventAction>(sql, parameters)
    
    let insertWorkflowEventAction (context: SqliteContext) (parameters: Parameters.NewWorkflowEventAction) =
        context.Insert("workflow_event_actions", parameters)
    
    /// Select a `Records.WorkflowEventType` from the table `workflow_event_types`.
    /// Internally this calls `context.SelectSingleAnon<Records.WorkflowEventType>` and uses Records.WorkflowEventType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowEventTypeRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowEventTypeRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.WorkflowEventType.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.WorkflowEventType>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.WorkflowEventType>` and uses Records.WorkflowEventType.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowEventTypeRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowEventTypeRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.WorkflowEventType.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.WorkflowEventType>(sql, parameters)
    
    let insertWorkflowEventType (context: SqliteContext) (parameters: Parameters.NewWorkflowEventType) =
        context.Insert("workflow_event_types", parameters)
    
    /// Select a `Records.WorkflowEvent` from the table `workflow_events`.
    /// Internally this calls `context.SelectSingleAnon<Records.WorkflowEvent>` and uses Records.WorkflowEvent.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowEventRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowEventRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.WorkflowEvent.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.WorkflowEvent>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.WorkflowEvent>` and uses Records.WorkflowEvent.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowEventRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowEventRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.WorkflowEvent.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.WorkflowEvent>(sql, parameters)
    
    let insertWorkflowEvent (context: SqliteContext) (parameters: Parameters.NewWorkflowEvent) =
        context.Insert("workflow_events", parameters)
    
    /// Select a `Records.Workflow` from the table `workflows`.
    /// Internally this calls `context.SelectSingleAnon<Records.Workflow>` and uses Records.Workflow.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowRecord ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowRecord (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.Workflow.SelectSql() ] @ query |> buildSql
        context.SelectSingleAnon<Records.Workflow>(sql, parameters)
    
    /// Internally this calls `context.SelectAnon<Records.Workflow>` and uses Records.Workflow.SelectSql().
    /// The caller can provide extra string lines to create a query and boxed parameters.
    /// It is up to the caller to verify the sql and parameters are correct,
    /// this should be considered an internal function (not exposed in public APIs).
    /// Parameters are assigned names based on their order in 0 indexed array. For example: @0,@1,@2...
    /// Example: selectWorkflowRecords ctx "WHERE `field` = @0" [ box `value` ]
    let selectWorkflowRecords (context: SqliteContext) (query: string list) (parameters: obj list) =
        let sql = [ Records.Workflow.SelectSql() ] @ query |> buildSql
        context.SelectAnon<Records.Workflow>(sql, parameters)
    
    let insertWorkflow (context: SqliteContext) (parameters: Parameters.NewWorkflow) =
        context.Insert("workflows", parameters)
    
# WiqlBuilder

A fluent, strongly-typed WIQL query builder for .NET.

WiqlBuilder helps you compose Azure DevOps WIQL queries in C# using a readable builder API instead of hand-writing string-based queries.

## Why WiqlBuilder?

Writing WIQL by hand is easy to get wrong and hard to maintain as queries grow.

WiqlBuilder gives you:

- A fluent API for building queries step by step
- Strongly-typed access to common work item fields and values
- Cleaner, more maintainable query construction in application code
- Safer refactoring compared with raw string concatenation
- Predictable WIQL rendering for tests and automation

## Installation

```bash
dotnet add package WiqlBuilder
```

## Quick start

```csharp
using WiqlBuilder.Builders;
using WiqlBuilder.Fields;
using WiqlBuilder.Syntax;

var wiql = WiqlQueryBuilder.Create()
    .Select(
        SystemFields.Id,
        SystemFields.AssignedTo,
        SystemFields.State,
        SystemFields.Title)
    .From(WiqlQuerySource.WorkItems)
    .Where(w => w
        .AndWorkItemTypeIs(WorkItemType.Task)
        .AndEquals(SystemFields.IterationPath, "sprint 1")
        .AndStateIsOneOf(
            WorkItemState.Done,
            WorkItemState.Closed,
            WorkItemState.Resolved)
        .AndGreaterThanOrEqual(SystemFields.ChangedDate, new DateTime(2024, 1, 1))
        .AndLessThan(SystemFields.ChangedDate, new DateTime(2024, 1, 31)))
    .Build();
```

This renders to:

```sql
SELECT [System.Id], [System.AssignedTo], [System.State], [System.Title]
FROM WorkItems
WHERE ([System.WorkItemType] = 'Task'
AND [System.IterationPath] = 'sprint 1'
AND [System.State] IN ('Done', 'Closed', 'Resolved')
AND [System.ChangedDate] >= '2024-01-01T00:00:00.0000000'
AND [System.ChangedDate] < '2024-01-31T00:00:00.0000000')
```

## More examples

### Get tasks completed during a sprint

```csharp
var wiql = WiqlQueryBuilder.Create()
    .Select(
        SystemFields.Id,
        SystemFields.AssignedTo,
        SystemFields.State,
        SystemFields.Title)
    .From(WiqlQuerySource.WorkItems)
    .Where(w => w
        .AndWorkItemTypeIs(WorkItemType.Task)
        .AndEquals(SystemFields.IterationPath, "sprint 1")
        .AndStateIsOneOf(
            WorkItemState.Done,
            WorkItemState.Closed,
            WorkItemState.Resolved)
        .AndGreaterThanOrEqual(SystemFields.ChangedDate, new DateTime(2024, 1, 1))
        .AndLessThan(SystemFields.ChangedDate, new DateTime(2024, 1, 31)))
    .Build();
```

### Get bugs and user stories in a sprint

```csharp
var wiql = WiqlQueryBuilder.Create()
    .Select(SystemFields.Id)
    .From(WiqlQuerySource.WorkItems)
    .Where(w => w
        .AndEquals(SystemFields.IterationPath, "sprint 3")
        .AndWorkItemTypeIsOneOf(
            WorkItemType.Bug,
            WorkItemType.UserStory))
    .Build();
```

### Get bugs raised in a sprint

```csharp
var wiql = WiqlQueryBuilder.Create()
    .Select(SystemFields.Id)
    .From(WiqlQuerySource.WorkItems)
    .Where(w => w
        .AndWorkItemTypeIs(WorkItemType.Bug)
        .AndEquals(SystemFields.IterationPath, "sprint 2")
        .AndGreaterThanOrEqual(SystemFields.CreatedDate, new DateTime(2020, 1, 1))
        .AndLessThanOrEqual(SystemFields.CreatedDate, new DateTime(2020, 1, 31)))
    .Build();
```

### Get bugs closed in a sprint

```csharp
var wiql = WiqlQueryBuilder.Create()
    .Select(SystemFields.Id)
    .From(WiqlQuerySource.WorkItems)
    .Where(w => w
        .AndEquals(SystemFields.IterationPath, "sprint 5")
        .AndStateIs(WorkItemState.Closed)
        .AndGreaterThanOrEqual(SystemFields.ChangedDate, new DateTime(2020, 1, 1))
        .AndLessThanOrEqual(SystemFields.ChangedDate, new DateTime(2020, 1, 31)))
    .Build();
```

## Target framework

WiqlBuilder currently targets:

- `net10.0`

## Use cases

WiqlBuilder is a good fit when you want to:

- Generate WIQL dynamically from filters in your application
- Reuse query construction logic across services or jobs
- Keep Azure DevOps query definitions in C# instead of string literals
- Test query output deterministically

## Package metadata

Package ID: `WiqlBuilder`

## Roadmap ideas

Possible future enhancements:

- ORDER BY support
- Additional query sources and operators
- Custom field helpers
- Better support for area/iteration macros
- More examples and docs

## Contributing

Issues and pull requests are welcome.

If you find a bug, have an API improvement idea, or want additional WIQL operators supported, open an issue to discuss it.

## License

MIT

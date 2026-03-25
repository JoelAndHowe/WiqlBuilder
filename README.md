# WiqlBuilder

A fluent, strongly-typed WIQL query builder for .NET.

## Install

`powershell
dotnet add package WiqlBuilder
`

## Example

`csharp
var wiql = WiqlQueryBuilder.Create()
    .Select(SystemFields.Id)
    .FromWorkItems()
    .Where(w => w.AndWorkItemTypeIs(WorkItemType.Task))
    .Build();
`

## License

MIT

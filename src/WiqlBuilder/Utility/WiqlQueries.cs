using WiqlBuilder.Builders;
using WiqlBuilder.Fields;
using WiqlBuilder.Syntax;
using WiqlBuilder.Values;

namespace WiqlBuilder.Utility;

public static class WiqlQueries
{
    public static WiqlQueryBuilder ActiveBugs()
    {
        return WiqlQueryBuilder.Create()
            .Select(
                SystemFields.Id,
                SystemFields.Title,
                SystemFields.State,
                SystemFields.AssignedTo,
                SystemFields.ChangedDate)
            .From(WiqlQuerySource.WorkItems)
            .Where(w => w
                .AndEquals(SystemFields.WorkItemType, "Bug")
                .AndNotEquals(SystemFields.State, "Closed")
                .AndNotEquals(SystemFields.State, "Removed"));
    }

    public static WiqlQueryBuilder AssignedToMe()
    {
        return WiqlQueryBuilder.Create()
            .Select(
                SystemFields.Id,
                SystemFields.Title,
                SystemFields.State,
                SystemFields.AssignedTo)
            .From(WiqlQuerySource.WorkItems)
            .Where(w => w
                .AndEquals(SystemFields.AssignedTo, WiqlMacro.Me));
    }

    public static WiqlQueryBuilder ActiveWorkItemsAssignedToMe()
    {
        return WiqlQueryBuilder.Create()
            .Select(
                SystemFields.Id,
                SystemFields.Title,
                SystemFields.State,
                SystemFields.AssignedTo,
                SystemFields.WorkItemType)
            .From(WiqlQuerySource.WorkItems)
            .Where(w => w
                .AndEquals(SystemFields.AssignedTo, WiqlMacro.Me)
                .AndNotEquals(SystemFields.State, "Closed")
                .AndNotEquals(SystemFields.State, "Removed"));
    }

    public static WiqlQueryBuilder UnderAreaPath(string areaPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(areaPath);

        return WiqlQueryBuilder.Create()
            .Select(
                SystemFields.Id,
                SystemFields.Title,
                SystemFields.State)
            .From(WiqlQuerySource.WorkItems)
            .Where(w => w
                .AndUnder(SystemFields.AreaPath, areaPath));
    }

    public static WiqlQueryBuilder InIteration(string iterationPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(iterationPath);

        return WiqlQueryBuilder.Create()
            .Select(
                SystemFields.Id,
                SystemFields.Title,
                SystemFields.State)
            .From(WiqlQuerySource.WorkItems)
            .Where(w => w
                .AndEquals(SystemFields.IterationPath, iterationPath));
    }

    public static WiqlQueryBuilder ChangedSince(DateTime sinceUtc)
    {
        return WiqlQueryBuilder.Create()
            .Select(
                SystemFields.Id,
                SystemFields.Title,
                SystemFields.State,
                SystemFields.ChangedDate)
            .From(WiqlQuerySource.WorkItems)
            .Where(w => w
                .AndGreaterThanOrEqual(SystemFields.ChangedDate, sinceUtc));
    }

    public static WiqlQueryBuilder ByType(string workItemType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workItemType);

        return WiqlQueryBuilder.Create()
            .Select(
                SystemFields.Id,
                SystemFields.Title,
                SystemFields.State,
                SystemFields.WorkItemType)
            .From(WiqlQuerySource.WorkItems)
            .Where(w => w
                .AndEquals(SystemFields.WorkItemType, workItemType));
    }
}
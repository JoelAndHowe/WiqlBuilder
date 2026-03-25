using FluentAssertions;
using WiqlBuilder.Builders;
using WiqlBuilder.Fields;
using WiqlBuilder.Syntax;
using WiqlBuilder.Tests.TestHelpers;

namespace WiqlBuilder.Tests;

public sealed class WiqlRenderingTests
{
    [Fact]
    public void Build_GetTasksCompletedDuringSprint()
    {
        var builder = WiqlQueryBuilder.Create()
            .Select(SystemFields.Id, SystemFields.AssignedTo, SystemFields.State, SystemFields.Title)
            .From(WiqlQuerySource.WorkItems)
            .Where(w =>
                w.AndWorkItemTypeIs(WorkItemType.Task)
                .AndEquals(SystemFields.IterationPath, "sprint 1")
                .AndStateIsOneOf(WorkItemState.Done, WorkItemState.Closed, WorkItemState.Resolved)
                .AndGreaterThanOrEqual(SystemFields.ChangedDate, new DateTime(2024, 1, 1))
                .AndLessThan(SystemFields.ChangedDate, new DateTime(2024, 1, 31))
            );

        var expected =
        "SELECT [System.Id], [System.AssignedTo], [System.State], [System.Title]" + Environment.NewLine + "FROM WorkItems" + Environment.NewLine +
        "WHERE ([System.WorkItemType] = 'Task' " + 
        $"AND [System.IterationPath] = 'sprint 1' " +
        "AND [System.State] IN ('Done', 'Closed', 'Resolved') " +
        $"AND [System.ChangedDate] >= '2024-01-01T00:00:00.0000000' AND [System.ChangedDate] < '2024-01-31T00:00:00.0000000')";

        WiqlAssert.RendersTo(builder, expected);
    }

    [Fact]
    public void Build_GetBugsAndStoresInSprint()
    {
        var builder = WiqlQueryBuilder.Create()
            .Select(SystemFields.Id)
            .From(WiqlQuerySource.WorkItems)
            .Where(w =>
                w.AndEquals(SystemFields.IterationPath, "sprint 3")
                .AndWorkItemTypeIsOneOf(WorkItemType.Bug, WorkItemType.UserStory)
                
            );

        var expected =
        "SELECT [System.Id]" + Environment.NewLine + "FROM WorkItems" + Environment.NewLine +
        "WHERE ([System.IterationPath] = 'sprint 3' " +
        "AND [System.WorkItemType] IN ('Bug', 'User Story'))";

        WiqlAssert.RendersTo(builder, expected);
    }

    [Fact]
    public void Build_GetBugsRaisedInSprint()
    {
        var builder = WiqlQueryBuilder.Create()
            .Select(SystemFields.Id)
            .From(WiqlQuerySource.WorkItems)
            .Where(w =>
                w
                .AndWorkItemTypeIs(WorkItemType.Bug)
                .AndEquals(SystemFields.IterationPath, "sprint 2")
                .AndGreaterThanOrEqual(SystemFields.CreatedDate, new DateTime(2020, 01, 01))
                .AndLessThanOrEqual(SystemFields.CreatedDate, new DateTime(2020, 01, 31))
                

            );

        var expected =
        "SELECT [System.Id]" + Environment.NewLine + "FROM WorkItems" + Environment.NewLine +
        "WHERE (" +
        "[System.WorkItemType] = 'Bug' AND " +
        "[System.IterationPath] = 'sprint 2' AND " +
        "[System.CreatedDate] >= '2020-01-01T00:00:00.0000000' AND " +
        "[System.CreatedDate] <= '2020-01-31T00:00:00.0000000'" +
        ")";

        WiqlAssert.RendersTo(builder, expected);
    }

    [Fact]
    public void Build_GetBugsClosedInSprint()
    {
        var builder = WiqlQueryBuilder.Create()
            .Select(SystemFields.Id)
            .From(WiqlQuerySource.WorkItems)
            .Where(w =>
                w
                .AndEquals(SystemFields.IterationPath, "sprint 5")
                .AndStateIs(WorkItemState.Closed)
                .AndGreaterThanOrEqual(SystemFields.ChangedDate, new DateTime(2020, 01, 01))
                .AndLessThanOrEqual(SystemFields.ChangedDate, new DateTime(2020, 01, 31))


            );

        var expected =
        "SELECT [System.Id]" + Environment.NewLine + "FROM WorkItems" + Environment.NewLine +
        "WHERE (" +
        "[System.IterationPath] = 'sprint 5' AND " +
        "[System.State] = 'Closed' AND " +
        "[System.ChangedDate] >= '2020-01-01T00:00:00.0000000' AND " +
        "[System.ChangedDate] <= '2020-01-31T00:00:00.0000000'" +
        ")";

        WiqlAssert.RendersTo(builder, expected);
    }
}
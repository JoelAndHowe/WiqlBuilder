namespace WiqlBuilder.Syntax;

public enum WorkItemState
{
    New,
    Active,
    Approved,
    Committed,
    InProgress,
    Resolved,
    Done,
    Closed,
    Removed
}

internal static class WorkItemStateExtensions
{
    public static string ToWiql(this WorkItemState state) => state switch
    {
        WorkItemState.New => "New",
        WorkItemState.Active => "Active",
        WorkItemState.Approved => "Approved",
        WorkItemState.Committed => "Committed",
        WorkItemState.InProgress => "In Progress",
        WorkItemState.Resolved => "Resolved",
        WorkItemState.Done => "Done",
        WorkItemState.Closed => "Closed",
        WorkItemState.Removed => "Removed",
        _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
    };
}
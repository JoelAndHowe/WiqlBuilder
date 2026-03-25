namespace WiqlBuilder.Syntax;

public enum WorkItemType
{
    Bug,
    Task,
    UserStory,
    Feature,
    Epic,
    Issue,
    TestCase
}


internal static class WorkItemTypeExtensions
{
    public static string ToWiql(this WorkItemType type) => type switch
    {
        WorkItemType.Bug => "Bug",
        WorkItemType.Task => "Task",
        WorkItemType.UserStory => "User Story",
        WorkItemType.Feature => "Feature",
        WorkItemType.Epic => "Epic",
        WorkItemType.Issue => "Issue",
        WorkItemType.TestCase => "Test Case",
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };
}
namespace WiqlBuilder.Syntax;

public enum WiqlQuerySource
{
    WorkItems,
    WorkItemLinks
}

internal static class WiqlQuerySourceExtensions
{
    public static string ToWiql(this WiqlQuerySource source) => source switch
    {
        WiqlQuerySource.WorkItems => "WorkItems",
        WiqlQuerySource.WorkItemLinks => "WorkItemLinks",
        _ => throw new ArgumentOutOfRangeException(nameof(source), source, null)
    };
}
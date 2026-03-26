namespace WiqlBuilder.Fields;

public static class SystemFields
{
    public static readonly IntFieldReference Id = new("System.Id");
    public static readonly StringFieldReference Title = new("System.Title");
    public static readonly StringFieldReference State = new("System.State");
    public static readonly StringFieldReference AssignedTo = new("System.AssignedTo");
    public static readonly StringFieldReference WorkItemType = new("System.WorkItemType");
    public static readonly PathFieldReference AreaPath = new("System.AreaPath");
    public static readonly PathFieldReference IterationPath = new("System.IterationPath");
    public static readonly DateTimeFieldReference CreatedDate = new("System.CreatedDate");
    public static readonly DateTimeFieldReference ChangedDate = new("System.ChangedDate");
    public static readonly DateTimeFieldReference ClosedDate = new("Closed Date");
    public static readonly DateTimeFieldReference ResolvedDate = new("Resolved Date");
    public static readonly StringFieldReference Tags = new("System.Tags");
    public static readonly IntFieldReference StoryPoints = new("Story Points");
}


public static class CommonFields
{
    public static readonly IntFieldReference Priority = new("Microsoft.VSTS.Common.Priority");
}

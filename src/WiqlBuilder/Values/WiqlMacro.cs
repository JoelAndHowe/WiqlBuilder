namespace WiqlBuilder.Values;

public sealed record WiqlMacro(string Value)
{
    public static readonly WiqlMacro Me = new("@Me");
    public static readonly WiqlMacro Project = new("@Project");
    public static readonly WiqlMacro Today = new("@Today");

    public override string ToString() => Value;
}
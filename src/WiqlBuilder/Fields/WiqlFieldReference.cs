namespace WiqlBuilder.Fields;
public abstract record WiqlFieldReference(string Name)
{
    public string ToWiql()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new InvalidOperationException("Field name cannot be null or whitespace.");
        }

        return Name.StartsWith("[", StringComparison.Ordinal)
            ? Name
            : $"[{Name}]";
    }
}

public sealed record StringFieldReference(string Name) : WiqlFieldReference(Name);
public sealed record PathFieldReference(string Name) : WiqlFieldReference(Name);
public sealed record IntFieldReference(string Name) : WiqlFieldReference(Name);
public sealed record DateTimeFieldReference(string Name) : WiqlFieldReference(Name);
public sealed record BooleanFieldReference(string Name) : WiqlFieldReference(Name);
public sealed record DecimalFieldReference(string Name) : WiqlFieldReference(Name);
public sealed record CustomFieldReference(string Name) : WiqlFieldReference(Name);
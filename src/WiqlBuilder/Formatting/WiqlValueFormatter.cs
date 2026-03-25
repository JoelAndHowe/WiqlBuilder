using System.Collections;
using System.Globalization;
using WiqlBuilder.Syntax;
using WiqlBuilder.Values;

namespace WiqlBuilder.Formatting;

internal static class WiqlValueFormatter
{
    public static string Format(object? value, WiqlOperator op)
    {
        if (op is WiqlOperator.In or WiqlOperator.NotIn)
        {
            return FormatCollection(value);
        }

        return FormatSingle(value);
    }

    private static string FormatCollection(object? value)
    {
        if (value is null)
        {
            throw new InvalidOperationException("IN and NOT IN require a collection value.");
        }

        if (value is string)
        {
            throw new InvalidOperationException("IN and NOT IN require a collection, not a string.");
        }

        if (value is not IEnumerable enumerable)
        {
            throw new InvalidOperationException("IN and NOT IN require an enumerable value.");
        }

        var items = new List<string>();

        foreach (var item in enumerable)
        {
            items.Add(FormatSingle(item));
        }

        if (items.Count == 0)
        {
            throw new InvalidOperationException("IN and NOT IN require at least one item.");
        }

        return $"({string.Join(", ", items)})";
    }

    private static string FormatSingle(object? value) => value switch
    {
        null => "''",
        WiqlMacro macro => macro.Value,
        string s => Quote(s),
        DateTime dt => Quote(dt.ToString("o", CultureInfo.InvariantCulture)),
        DateTimeOffset dto => Quote(dto.ToString("o", CultureInfo.InvariantCulture)),
        bool b => b ? "true" : "false",
        byte or short or int or long or float or double or decimal
            => Convert.ToString(value, CultureInfo.InvariantCulture)
               ?? throw new InvalidOperationException("Numeric conversion failed."),
        _ => Quote(Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty)
    };

    private static string Quote(string value)
        => $"'{value.Replace("'", "''", StringComparison.Ordinal)}'";
}
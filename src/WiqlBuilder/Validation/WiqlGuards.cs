using System.Collections;
using WiqlBuilder.Fields;
using WiqlBuilder.Syntax;
using WiqlBuilder.Values;

namespace WiqlBuilder.Validation;

internal static class WiqlGuards
{
    public static void Validate(WiqlFieldReference field, WiqlOperator op, object? value)
    {
        ArgumentNullException.ThrowIfNull(field);

        ValidateOperatorForField(field, op);
        ValidateValueForOperator(op, value);
    }

    private static void ValidateOperatorForField(WiqlFieldReference field, WiqlOperator op)
    {
        if (field is IntFieldReference)
        {
            if (op is WiqlOperator.Contains or WiqlOperator.NotContains or WiqlOperator.Under)
            {
                throw new InvalidOperationException(
                    $"Operator '{op}' is not valid for integer field '{field.Name}'.");
            }
        }

        if (field is DateTimeFieldReference)
        {
            if (op is WiqlOperator.Contains or WiqlOperator.NotContains or WiqlOperator.Under)
            {
                throw new InvalidOperationException(
                    $"Operator '{op}' is not valid for date field '{field.Name}'.");
            }
        }

        if (field is BooleanFieldReference)
        {
            if (op is WiqlOperator.Contains or WiqlOperator.NotContains or WiqlOperator.Under
                or WiqlOperator.GreaterThan or WiqlOperator.GreaterThanOrEqual
                or WiqlOperator.LessThan or WiqlOperator.LessThanOrEqual)
            {
                throw new InvalidOperationException(
                    $"Operator '{op}' is not valid for boolean field '{field.Name}'.");
            }
        }

        if (op == WiqlOperator.Under && !IsTreePathField(field))
        {
            throw new InvalidOperationException(
                $"Operator 'Under' is only valid for path fields. Field '{field.Name}' is not supported.");
        }
    }

    private static void ValidateValueForOperator(WiqlOperator op, object? value)
    {
        if (op is WiqlOperator.In or WiqlOperator.NotIn)
        {
            if (value is null || value is string || value is not IEnumerable enumerable)
            {
                throw new InvalidOperationException($"{op} requires a non-string enumerable value.");
            }

            var hasAny = false;
            foreach (var _ in enumerable)
            {
                hasAny = true;
                break;
            }

            if (!hasAny)
            {
                throw new InvalidOperationException($"{op} requires at least one value.");
            }
        }

        if (value is WiqlMacro macro && string.IsNullOrWhiteSpace(macro.Value))
        {
            throw new InvalidOperationException("Macro value cannot be empty.");
        }
    }

    private static bool IsTreePathField(WiqlFieldReference field)
        => field.Name is "System.AreaPath" or "System.IterationPath";
}
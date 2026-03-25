namespace WiqlBuilder.Syntax;

public enum WiqlOperator
{
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Contains,
    NotContains,
    In,
    NotIn,
    Under,
    Ever
}

internal static class WiqlOperatorExtensions
{
    public static string ToWiql(this WiqlOperator value) => value switch
    {
        WiqlOperator.Equal => "=",
        WiqlOperator.NotEqual => "<>",
        WiqlOperator.GreaterThan => ">",
        WiqlOperator.GreaterThanOrEqual => ">=",
        WiqlOperator.LessThan => "<",
        WiqlOperator.LessThanOrEqual => "<=",
        WiqlOperator.Contains => "CONTAINS",
        WiqlOperator.NotContains => "NOT CONTAINS",
        WiqlOperator.In => "IN",
        WiqlOperator.NotIn => "NOT IN",
        WiqlOperator.Under => "UNDER",
        WiqlOperator.Ever => "EVER",
        _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
    };
}

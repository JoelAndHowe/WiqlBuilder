using WiqlBuilder.Syntax;
using System.Text;
using WiqlBuilder.Clauses;
using WiqlBuilder.Formatting;

namespace WiqlBuilder.Rendering;

internal static class WiqlRenderer
{
    public static string RenderClause(IWiqlClause clause)
        => clause switch
        {
            WiqlConditionClause condition => RenderCondition(condition),
            WiqlGroupClause group => RenderGroup(group),
            _ => throw new InvalidOperationException($"Unknown clause type: {clause.GetType().Name}")
        };

    private static string RenderCondition(WiqlConditionClause condition)
    {
        return $"{condition.Field.ToWiql()} {condition.Operator.ToWiql()} {WiqlValueFormatter.Format(condition.Value, condition.Operator)}";
    }

    private static string RenderGroup(WiqlGroupClause group)
    {
        if (!group.HasItems)
        {
            throw new InvalidOperationException("Cannot render an empty clause group.");
        }

        var sb = new StringBuilder();
        sb.Append('(');

        for (var i = 0; i < group.Items.Count; i++)
        {
            var (logicalOperator, clause) = group.Items[i];

            if (i > 0)
            {
                sb.Append(' ')
                  .Append(logicalOperator)
                  .Append(' ');
            }

            sb.Append(RenderClause(clause));
        }

        sb.Append(')');
        return sb.ToString();
    }
}
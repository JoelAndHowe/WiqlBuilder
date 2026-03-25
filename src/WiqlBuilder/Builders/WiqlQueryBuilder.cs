using WiqlBuilder.Syntax;
using System.Text;
using WiqlBuilder.Fields;
using WiqlBuilder.Rendering;

namespace WiqlBuilder.Builders;

public sealed class WiqlQueryBuilder
{
    private readonly List<WiqlFieldReference> _selectFields = new();
    private readonly List<(WiqlFieldReference Field, WiqlSortDirection Direction)> _orderBy = new();
    private readonly WiqlWhereBuilder _whereBuilder = new();

    private WiqlQuerySource _source = WiqlQuerySource.WorkItems;

    private WiqlQueryBuilder()
    {
    }

    public static WiqlQueryBuilder Create() => new();

    public WiqlQueryBuilder Select(params WiqlFieldReference[] fields)
    {
        ArgumentNullException.ThrowIfNull(fields);

        foreach (var field in fields)
        {
            ArgumentNullException.ThrowIfNull(field);
            _selectFields.Add(field);
        }

        return this;
    }

    public WiqlQueryBuilder From(WiqlQuerySource source)
    {
        _source = source;
        return this;
    }

    public WiqlQueryBuilder Where(Action<WiqlWhereBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        configure(_whereBuilder);
        return this;
    }

    public WiqlQueryBuilder OrderBy(WiqlFieldReference field, WiqlSortDirection direction = WiqlSortDirection.Asc)
    {
        ArgumentNullException.ThrowIfNull(field);

        _orderBy.Add((field, direction));
        return this;
    }

    public string Build()
    {
        if (_selectFields.Count == 0)
        {
            throw new InvalidOperationException("At least one field must be selected.");
        }

        var sb = new StringBuilder();

        sb.Append("SELECT ");
        sb.Append(string.Join(", ", _selectFields.Select(x => x.ToWiql())));
        sb.AppendLine();

        sb.Append("FROM ");
        sb.Append(_source.ToWiql());

        if (_whereBuilder.HasClauses)
        {
            sb.AppendLine();
            sb.Append("WHERE ");
            sb.Append(WiqlRenderer.RenderClause(_whereBuilder.BuildClause()));
        }

        if (_orderBy.Count > 0)
        {
            sb.AppendLine();
            sb.Append("ORDER BY ");
            sb.Append(string.Join(", ", _orderBy.Select(x => $"{x.Field.ToWiql()} {x.Direction}")));
        }

        return sb.ToString();
    }
}
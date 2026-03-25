namespace WiqlBuilder.Clauses;

internal sealed class WiqlGroupClause : IWiqlClause
{
    private readonly List<(string LogicalOperator, IWiqlClause Clause)> _items = new();

    public IReadOnlyList<(string LogicalOperator, IWiqlClause Clause)> Items => _items;

    public bool HasItems => _items.Count > 0;

    public void Add(string logicalOperator, IWiqlClause clause)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(logicalOperator);
        ArgumentNullException.ThrowIfNull(clause);

        _items.Add((logicalOperator, clause));
    }
}
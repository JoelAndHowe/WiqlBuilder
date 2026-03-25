using WiqlBuilder.Clauses;
using WiqlBuilder.Fields;
using WiqlBuilder.Syntax;
using WiqlBuilder.Validation;
using WiqlBuilder.Values;

namespace WiqlBuilder.Builders;

public sealed class WiqlWhereBuilder
{
    private readonly WiqlGroupClause _root = new();

    internal bool HasClauses => _root.HasItems;
    internal WiqlGroupClause BuildClause() => _root;

    public WiqlWhereBuilder And(WiqlFieldReference field, WiqlOperator op, object? value)
    {
        Add("AND", field, op, value);
        return this;
    }

    public WiqlWhereBuilder Or(WiqlFieldReference field, WiqlOperator op, object? value)
    {
        Add("OR", field, op, value);
        return this;
    }

    public WiqlWhereBuilder And(StringFieldReference field, WiqlOperator op, string value)
        => And((WiqlFieldReference)field, op, value);

    public WiqlWhereBuilder And(StringFieldReference field, WiqlOperator op, WiqlMacro value)
        => And((WiqlFieldReference)field, op, value);

    public WiqlWhereBuilder And(IntFieldReference field, WiqlOperator op, int value)
        => And((WiqlFieldReference)field, op, value);

    public WiqlWhereBuilder And(DateTimeFieldReference field, WiqlOperator op, DateTime value)
        => And((WiqlFieldReference)field, op, value);

    public WiqlWhereBuilder And(BooleanFieldReference field, WiqlOperator op, bool value)
        => And((WiqlFieldReference)field, op, value);

    public WiqlWhereBuilder And(PathFieldReference field, WiqlOperator op, string value)
        => And((WiqlFieldReference)field, op, value);

    public WiqlWhereBuilder Or(StringFieldReference field, WiqlOperator op, string value)
        => Or((WiqlFieldReference)field, op, value);

    public WiqlWhereBuilder Or(StringFieldReference field, WiqlOperator op, WiqlMacro value)
        => Or((WiqlFieldReference)field, op, value);

    public WiqlWhereBuilder Or(IntFieldReference field, WiqlOperator op, int value)
        => Or((WiqlFieldReference)field, op, value);

    public WiqlWhereBuilder Or(DateTimeFieldReference field, WiqlOperator op, DateTime value)
        => Or((WiqlFieldReference)field, op, value);

    public WiqlWhereBuilder Or(BooleanFieldReference field, WiqlOperator op, bool value)
        => Or((WiqlFieldReference)field, op, value);

    public WiqlWhereBuilder Or(PathFieldReference field, WiqlOperator op, string value)
        => Or((WiqlFieldReference)field, op, value);

    public WiqlWhereBuilder AndGroup(Action<WiqlWhereBuilder> configure)
        => AddGroup("AND", configure);

    public WiqlWhereBuilder OrGroup(Action<WiqlWhereBuilder> configure)
        => AddGroup("OR", configure);

    public WiqlWhereBuilder AndIn(StringFieldReference field, IEnumerable<string> values)
        => And(field, WiqlOperator.In, values.ToArray());

    public WiqlWhereBuilder AndIn(IntFieldReference field, IEnumerable<int> values)
        => And(field, WiqlOperator.In, values.ToArray());

    public WiqlWhereBuilder AndIn(PathFieldReference field, IEnumerable<string> values)
        => And(field, WiqlOperator.In, values.ToArray());

    public WiqlWhereBuilder OrIn(StringFieldReference field, IEnumerable<string> values)
        => Or(field, WiqlOperator.In, values.ToArray());

    public WiqlWhereBuilder OrIn(IntFieldReference field, IEnumerable<int> values)
        => Or(field, WiqlOperator.In, values.ToArray());

    public WiqlWhereBuilder OrIn(PathFieldReference field, IEnumerable<string> values)
        => Or(field, WiqlOperator.In, values.ToArray());

    public WiqlWhereBuilder AndUnder(PathFieldReference field, string value)
        => And(field, WiqlOperator.Under, value);

    public WiqlWhereBuilder OrUnder(PathFieldReference field, string value)
        => Or(field, WiqlOperator.Under, value);

    public WiqlWhereBuilder AndEquals(StringFieldReference field, string value)
        => And(field, WiqlOperator.Equal, value);

    public WiqlWhereBuilder AndEquals(StringFieldReference field, WiqlMacro value)
        => And(field, WiqlOperator.Equal, value);

    public WiqlWhereBuilder AndEquals(IntFieldReference field, int value)
        => And(field, WiqlOperator.Equal, value);

    public WiqlWhereBuilder AndEquals(BooleanFieldReference field, bool value)
        => And(field, WiqlOperator.Equal, value);

    public WiqlWhereBuilder AndEquals(DateTimeFieldReference field, DateTime value)
        => And(field, WiqlOperator.Equal, value);

    public WiqlWhereBuilder AndEquals(PathFieldReference field, string path)
        => And(field, WiqlOperator.Equal, path);

    public WiqlWhereBuilder AndNotEquals(StringFieldReference field, string value)
        => And(field, WiqlOperator.NotEqual, value);

    public WiqlWhereBuilder AndNotEquals(IntFieldReference field, int value)
        => And(field, WiqlOperator.NotEqual, value);

    public WiqlWhereBuilder AndNotEquals(BooleanFieldReference field, bool value)
        => And(field, WiqlOperator.NotEqual, value);

    public WiqlWhereBuilder AndNotEquals(DateTimeFieldReference field, DateTime value)
        => And(field, WiqlOperator.NotEqual, value);

    public WiqlWhereBuilder AndNotEquals(PathFieldReference field, string path)
        => And(field, WiqlOperator.NotEqual, path);

    public WiqlWhereBuilder OrEquals(StringFieldReference field, string value)
        => Or(field, WiqlOperator.Equal, value);

    public WiqlWhereBuilder OrEquals(StringFieldReference field, WiqlMacro value)
        => Or(field, WiqlOperator.Equal, value);

    public WiqlWhereBuilder OrEquals(IntFieldReference field, int value)
        => Or(field, WiqlOperator.Equal, value);

    public WiqlWhereBuilder OrEquals(BooleanFieldReference field, bool value)
        => Or(field, WiqlOperator.Equal, value);

    public WiqlWhereBuilder OrEquals(DateTimeFieldReference field, DateTime value)
        => Or(field, WiqlOperator.Equal, value);

    public WiqlWhereBuilder OrEquals(PathFieldReference field, string path)
        => Or(field, WiqlOperator.Equal, path);

    public WiqlWhereBuilder OrNotEquals(StringFieldReference field, string value)
        => Or(field, WiqlOperator.NotEqual, value);

    public WiqlWhereBuilder OrNotEquals(IntFieldReference field, int value)
        => Or(field, WiqlOperator.NotEqual, value);

    public WiqlWhereBuilder OrNotEquals(BooleanFieldReference field, bool value)
        => Or(field, WiqlOperator.NotEqual, value);

    public WiqlWhereBuilder OrNotEquals(DateTimeFieldReference field, DateTime value)
        => Or(field, WiqlOperator.NotEqual, value);

    public WiqlWhereBuilder OrNotEquals(PathFieldReference field, string path)
        => Or(field, WiqlOperator.NotEqual, path);

    public WiqlWhereBuilder AndGreaterThan(IntFieldReference field, int value)
        => And(field, WiqlOperator.GreaterThan, value);

    public WiqlWhereBuilder AndGreaterThan(DateTimeFieldReference field, DateTime value)
        => And(field, WiqlOperator.GreaterThan, value);

    public WiqlWhereBuilder OrGreaterThan(IntFieldReference field, int value)
        => Or(field, WiqlOperator.GreaterThan, value);

    public WiqlWhereBuilder OrGreaterThan(DateTimeFieldReference field, DateTime value)
        => Or(field, WiqlOperator.GreaterThan, value);

    public WiqlWhereBuilder AndGreaterThanOrEqual(IntFieldReference field, int value)
        => And(field, WiqlOperator.GreaterThanOrEqual, value);

    public WiqlWhereBuilder AndGreaterThanOrEqual(DateTimeFieldReference field, DateTime value)
        => And(field, WiqlOperator.GreaterThanOrEqual, value);

    public WiqlWhereBuilder OrGreaterThanOrEqual(IntFieldReference field, int value)
        => Or(field, WiqlOperator.GreaterThanOrEqual, value);

    public WiqlWhereBuilder OrGreaterThanOrEqual(DateTimeFieldReference field, DateTime value)
        => Or(field, WiqlOperator.GreaterThanOrEqual, value);

    public WiqlWhereBuilder AndLessThan(IntFieldReference field, int value)
        => And(field, WiqlOperator.LessThan, value);

    public WiqlWhereBuilder AndLessThan(DateTimeFieldReference field, DateTime value)
        => And(field, WiqlOperator.LessThan, value);

    public WiqlWhereBuilder OrLessThan(IntFieldReference field, int value)
        => Or(field, WiqlOperator.LessThan, value);

    public WiqlWhereBuilder OrLessThan(DateTimeFieldReference field, DateTime value)
        => Or(field, WiqlOperator.LessThan, value);

    public WiqlWhereBuilder AndLessThanOrEqual(IntFieldReference field, int value)
        => And(field, WiqlOperator.LessThanOrEqual, value);

    public WiqlWhereBuilder AndLessThanOrEqual(DateTimeFieldReference field, DateTime value)
        => And(field, WiqlOperator.LessThanOrEqual, value);

    public WiqlWhereBuilder OrLessThanOrEqual(IntFieldReference field, int value)
        => Or(field, WiqlOperator.LessThanOrEqual, value);

    public WiqlWhereBuilder OrLessThanOrEqual(DateTimeFieldReference field, DateTime value)
        => Or(field, WiqlOperator.LessThanOrEqual, value);

    public WiqlWhereBuilder AndEquals(WorkItemType value)
        => And(SystemFields.WorkItemType, WiqlOperator.Equal, value.ToWiql());

    public WiqlWhereBuilder AndNotEquals(WorkItemType value)
        => And(SystemFields.WorkItemType, WiqlOperator.NotEqual, value.ToWiql());

    public WiqlWhereBuilder OrEquals(WorkItemType value)
        => Or(SystemFields.WorkItemType, WiqlOperator.Equal, value.ToWiql());

    public WiqlWhereBuilder OrNotEquals(WorkItemType value)
        => Or(SystemFields.WorkItemType, WiqlOperator.NotEqual, value.ToWiql());

    public WiqlWhereBuilder AndEquals(WorkItemState value)
        => And(SystemFields.State, WiqlOperator.Equal, value.ToWiql());

    public WiqlWhereBuilder AndNotEquals(WorkItemState value)
        => And(SystemFields.State, WiqlOperator.NotEqual, value.ToWiql());

    public WiqlWhereBuilder OrEquals(WorkItemState value)
        => Or(SystemFields.State, WiqlOperator.Equal, value.ToWiql());

    public WiqlWhereBuilder OrNotEquals(WorkItemState value)
        => Or(SystemFields.State, WiqlOperator.NotEqual, value.ToWiql());

    public WiqlWhereBuilder AndIn(params WorkItemType[] workItemTypes)
    {
        ArgumentNullException.ThrowIfNull(workItemTypes);

        if (workItemTypes.Length == 0)
        {
            throw new ArgumentException("At least one work item type must be provided.", nameof(workItemTypes));
        }

        return AndIn(SystemFields.WorkItemType, workItemTypes.Select(x => x.ToWiql()).ToArray());
    }

    public WiqlWhereBuilder OrIn(params WorkItemType[] workItemTypes)
    {
        ArgumentNullException.ThrowIfNull(workItemTypes);

        if (workItemTypes.Length == 0)
        {
            throw new ArgumentException("At least one work item type must be provided.", nameof(workItemTypes));
        }

        return OrIn(SystemFields.WorkItemType, workItemTypes.Select(x => x.ToWiql()).ToArray());
    }

    public WiqlWhereBuilder AndIn(params WorkItemState[] states)
    {
        ArgumentNullException.ThrowIfNull(states);

        if (states.Length == 0)
        {
            throw new ArgumentException("At least one state must be provided.", nameof(states));
        }

        return AndIn(SystemFields.State, states.Select(x => x.ToWiql()).ToArray());
    }

    public WiqlWhereBuilder OrIn(params WorkItemState[] states)
    {
        ArgumentNullException.ThrowIfNull(states);

        if (states.Length == 0)
        {
            throw new ArgumentException("At least one state must be provided.", nameof(states));
        }

        return OrIn(SystemFields.State, states.Select(x => x.ToWiql()).ToArray());
    }

    private void Add(string logicalOperator, WiqlFieldReference field, WiqlOperator op, object? value)
    {
        ArgumentNullException.ThrowIfNull(field);
        WiqlGuards.Validate(field, op, value);

        _root.Add(logicalOperator, new WiqlConditionClause(field, op, value));
    }

    private WiqlWhereBuilder AddGroup(string logicalOperator, Action<WiqlWhereBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        var nested = new WiqlWhereBuilder();
        configure(nested);

        if (!nested.HasClauses)
        {
            throw new InvalidOperationException("Nested group cannot be empty.");
        }

        _root.Add(logicalOperator, nested.BuildClause());
        return this;
    }
}

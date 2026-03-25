using WiqlBuilder.Fields;
using WiqlBuilder.Syntax;

namespace WiqlBuilder.Builders;

public static class WiqlWhereBuilderExtensions
{
    public static WiqlWhereBuilder AndWorkItemTypeIs(
        this WiqlWhereBuilder builder,
        WorkItemType workItemType)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.AndEquals(SystemFields.WorkItemType, workItemType.ToWiql());
    }

    public static WiqlWhereBuilder OrWorkItemTypeIs(
        this WiqlWhereBuilder builder,
        WorkItemType workItemType)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.OrEquals(SystemFields.WorkItemType, workItemType.ToWiql());
    }

    public static WiqlWhereBuilder AndWorkItemTypeIsNot(
        this WiqlWhereBuilder builder,
        WorkItemType workItemType)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.AndNotEquals(SystemFields.WorkItemType, workItemType.ToWiql());
    }

    public static WiqlWhereBuilder OrWorkItemTypeIsNot(
        this WiqlWhereBuilder builder,
        WorkItemType workItemType)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.OrNotEquals(SystemFields.WorkItemType, workItemType.ToWiql());
    }

    public static WiqlWhereBuilder AndWorkItemTypeIsOneOf(
        this WiqlWhereBuilder builder,
        params WorkItemType[] workItemTypes)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(workItemTypes);

        if (workItemTypes.Length == 0)
        {
            throw new ArgumentException("At least one work item type must be provided.", nameof(workItemTypes));
        }

        return builder.AndIn(
            SystemFields.WorkItemType,
            workItemTypes.Select(x => x.ToWiql()).ToArray());
    }

    public static WiqlWhereBuilder OrWorkItemTypeIsOneOf(
        this WiqlWhereBuilder builder,
        params WorkItemType[] workItemTypes)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(workItemTypes);

        if (workItemTypes.Length == 0)
        {
            throw new ArgumentException("At least one work item type must be provided.", nameof(workItemTypes));
        }

        return builder.OrIn(
            SystemFields.WorkItemType,
            workItemTypes.Select(x => x.ToWiql()).ToArray());
    }

    public static WiqlWhereBuilder AndStateIs(
        this WiqlWhereBuilder builder,
        WorkItemState state)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.AndEquals(SystemFields.State, state.ToWiql());
    }

    public static WiqlWhereBuilder OrStateIs(
        this WiqlWhereBuilder builder,
        WorkItemState state)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.OrEquals(SystemFields.State, state.ToWiql());
    }

    public static WiqlWhereBuilder AndStateIsNot(
        this WiqlWhereBuilder builder,
        WorkItemState state)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.AndNotEquals(SystemFields.State, state.ToWiql());
    }

    public static WiqlWhereBuilder OrStateIsNot(
        this WiqlWhereBuilder builder,
        WorkItemState state)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.OrNotEquals(SystemFields.State, state.ToWiql());
    }

    public static WiqlWhereBuilder AndStateIsOneOf(
        this WiqlWhereBuilder builder,
        params WorkItemState[] states)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(states);

        if (states.Length == 0)
        {
            throw new ArgumentException("At least one state must be provided.", nameof(states));
        }

        return builder.AndIn(
            SystemFields.State,
            states.Select(x => x.ToWiql()).ToArray());
    }

    public static WiqlWhereBuilder OrStateIsOneOf(
        this WiqlWhereBuilder builder,
        params WorkItemState[] states)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(states);

        if (states.Length == 0)
        {
            throw new ArgumentException("At least one state must be provided.", nameof(states));
        }

        return builder.OrIn(
            SystemFields.State,
            states.Select(x => x.ToWiql()).ToArray());
    }
}

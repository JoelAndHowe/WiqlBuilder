using WiqlBuilder.Fields;
using WiqlBuilder.Syntax;

namespace WiqlBuilder.Clauses;

internal sealed record WiqlConditionClause(
    WiqlFieldReference Field,
    WiqlOperator Operator,
    object? Value) : IWiqlClause;
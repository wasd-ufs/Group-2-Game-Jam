public enum ExpressionType
{
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Plus,
    Minus,
    Multiply,
    Divide,
    Mod
}

public class Expression : IValue
{
    public IValue left;
    public IValue right;
    public ExpressionType comparisonType;

    public int Get() => comparisonType switch
    {
        ExpressionType.Equal => left.Get() == right.Get() ? 1 : 0,
        ExpressionType.NotEqual => left.Get() != right.Get() ? 1 : 0,
        ExpressionType.GreaterThan => left.Get() > right.Get() ? 1 : 0,
        ExpressionType.GreaterThanOrEqual => left.Get() >= right.Get() ? 1 : 0,
        ExpressionType.LessThan => left.Get() < right.Get() ? 1 : 0,
        ExpressionType.LessThanOrEqual => left.Get() <= right.Get() ? 1 : 0,
        ExpressionType.Plus => left.Get() + right.Get(),
        ExpressionType.Minus => left.Get() - right.Get(),
        ExpressionType.Multiply => left.Get() * right.Get(),
        ExpressionType.Divide => left.Get() / right.Get(),
        ExpressionType.Mod => left.Get() % right.Get(),
        _ => 0
    };
}
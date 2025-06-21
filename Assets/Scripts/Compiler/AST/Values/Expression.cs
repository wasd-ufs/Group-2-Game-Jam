public enum ExpressionType
{
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Sum,
    Subtract,
    Multiply,
    Divide,
    Mod
}

public class Expression : IValue
{
    public IValue left;
    public IValue right;
    public ExpressionType expressionType;

    public Expression(ExpressionType expressionType, IValue left, IValue right)
    {
        this.left = left;
        this.right = right;
        this.expressionType = expressionType;
    }

    public int Get() => expressionType switch
    {
        ExpressionType.Equal => left.Get() == right.Get() ? 1 : 0,
        ExpressionType.NotEqual => left.Get() != right.Get() ? 1 : 0,
        ExpressionType.GreaterThan => left.Get() > right.Get() ? 1 : 0,
        ExpressionType.GreaterThanOrEqual => left.Get() >= right.Get() ? 1 : 0,
        ExpressionType.LessThan => left.Get() < right.Get() ? 1 : 0,
        ExpressionType.LessThanOrEqual => left.Get() <= right.Get() ? 1 : 0,
        ExpressionType.Sum => left.Get() + right.Get(),
        ExpressionType.Subtract => left.Get() - right.Get(),
        ExpressionType.Multiply => left.Get() * right.Get(),
        ExpressionType.Divide => left.Get() / right.Get(),
        ExpressionType.Mod => left.Get() % right.Get(),
        _ => 0
    };
}
public class Conditional : AbstractSyntaxTreeNode
{
    public IValue condition;
    public AbstractSyntaxTreeNode next;

    public Conditional(IValue condition, AbstractSyntaxTreeNode next)
    {
        this.condition = condition;
        this.next = next;
    }
    
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitConditional(this);
    }
}
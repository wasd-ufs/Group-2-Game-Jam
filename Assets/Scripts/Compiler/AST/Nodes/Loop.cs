public class Loop : AbstractSyntaxTreeNode
{
    public IValue condition;
    public AbstractSyntaxTreeNode next;

    public Loop(IValue condition, AbstractSyntaxTreeNode next)
    {
        this.condition = condition;
        this.next = next;
    }
    
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitLoop(this);
    }
}
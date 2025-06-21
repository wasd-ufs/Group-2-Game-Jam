public class Assign : AbstractSyntaxTreeNode
{
    public Variable variable;
    public IValue value;

    public Assign(Variable variable, IValue value)
    {
        this.variable = variable;
        this.value = value;
    }
    
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitAssign(this);
    }
}
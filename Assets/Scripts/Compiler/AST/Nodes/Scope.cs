using System.Collections.Generic;
using NUnit.Framework;

public class Scope : AbstractSyntaxTreeNode
{
    public List<AbstractSyntaxTreeNode> content = new();
    public List<AbstractSyntaxTreeNode> bottom = new();

    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitScope(this);
    }
}
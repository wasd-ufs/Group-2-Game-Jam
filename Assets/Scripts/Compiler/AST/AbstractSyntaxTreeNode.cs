using UnityEngine;

public abstract class AbstractSyntaxTreeNode
{
    public abstract void Accept(AbstractSyntaxTreeVisitor visitor);
}

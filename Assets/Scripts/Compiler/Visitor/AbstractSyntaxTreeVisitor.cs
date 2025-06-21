using UnityEngine;

public abstract class AbstractSyntaxTreeVisitor
{
    public virtual void VisitScope(Scope scope) {}
    public virtual void VisitLoop(Loop loop) {}
    public virtual void VisitConditional(Conditional conditional) {}
    public virtual void VisitAssign(Assign assign) {}
}
using System;

public class Interpreter : AbstractSyntaxTreeVisitor
{
    public Action<AbstractSyntaxTreeNode> OnNodeEntered = node => {};
    public Action<AbstractSyntaxTreeNode> OnNodeExecutionFail = node => {};
    
    public void Interpret(AbstractSyntaxTreeNode node)
    {
        node.Accept(this);
    }
    
    public override void VisitAssign(Assign assign)
    {
        OnNodeEntered(assign);
        assign.variable.Set(assign.value.Get());
    }

    public override void VisitConditional(Conditional conditional)
    {
        OnNodeEntered(conditional);
        if (conditional.condition.Get() != 0)
            conditional.next.Accept(this);
    }

    public override void VisitLoop(Loop loop)
    {
        OnNodeEntered(loop);
        while(loop.condition.Get() != 0) 
            loop.next.Accept(this);
    }

    public override void VisitScope(Scope scope)
    {
        OnNodeEntered(scope);
        foreach (var content in scope.content)
            content.Accept(this);
        foreach (var content in scope.bottom)
            content.Accept(this);
    }
}
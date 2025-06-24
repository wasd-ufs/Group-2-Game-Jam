public class VariableFiller : TokenFiller
{
    public VariableType variableType;

    public override void FillVariable(ref Variable variable)
    {
        variable = new Variable(variableType);
    }

    public override void FillValue(ref IValue value)
    {
        value = new Variable(variableType);
    }
    
    public override void FillProgram(ref AbstractSyntaxTreeNode program)
    {
        program = null;
    }

    public override void FillScope(ref Scope scope)
    {
        scope = new Scope();
    }
}
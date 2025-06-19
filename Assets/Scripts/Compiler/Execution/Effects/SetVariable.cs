public class SetVariable : IExecutable
{
    public Variable variable;
    public IValue value;

    public SetVariable(Variable variable, IValue value)
    {
        this.variable = variable;
        this.value = value;
    }
    
    public bool Execute()
    {
        variable.Set(value.Get());
        return true;
    }
} 
public class SetVariable : IExecutable
{
    public Variable variable;
    public IValue value;
    
    public bool Execute()
    {
        variable.Set(value.Get());
        return true;
    }
} 
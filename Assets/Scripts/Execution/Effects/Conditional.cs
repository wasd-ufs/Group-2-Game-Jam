using System.Collections.Generic;

public class Conditional : IExecutable
{
    public IExecutable Executable;
    public IValue Condition;
    
    public bool Execute()
    {
        if (Condition.Get() == 0)
            return true;
        
        return Executable.Execute();
    }
}
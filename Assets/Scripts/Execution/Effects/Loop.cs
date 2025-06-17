using System.Collections.Generic;

public class Loop : IExecutable
{
    public IExecutable Executable;
    public IValue Condition;
    
    public bool Execute()
    {
        while (Condition.Get() != 0)
        {
            var sucess = Executable.Execute();
            
            if (!sucess)
                return false;
        }

        return true;
    }
}
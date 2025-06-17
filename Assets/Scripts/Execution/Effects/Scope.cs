using System.Collections.Generic;

public class Scope : IExecutable
{
    public List<IExecutable> executables;

    public void Insert(IExecutable executable)
    {
        executables.Add(executable);
    }
    
    public bool Execute()
    {
        foreach (IExecutable executable in executables)
        {
            var success = executable.Execute();
            if (!success)
                return false;
        }

        return true;
    }
}
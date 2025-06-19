using System.Collections.Generic;

public class Scope : IExecutable
{
    public List<IExecutable> executables = new();

    public Scope()
    {
        executables = new List<IExecutable>();
    }

    public void Clear()
    {
        executables.Clear();
    }

    public void Add(IExecutable executable)
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
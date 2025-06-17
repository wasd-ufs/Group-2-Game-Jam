using System.Collections.Generic;

public static class ExecutionLine
{
    public static void Execute(List<IExecutable> executables)
    {
        foreach (var executable in executables)
        {
            var sucess = executable.Execute();
            if (!sucess)
            {
                // Remove o espaço que Falhou
                continue;
            }
        }
    }
}
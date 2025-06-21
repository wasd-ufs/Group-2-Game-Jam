using System.Collections.Generic;

public class ProgramToken : Token
{
    public AbstractSyntaxTreeNode program;

    public ProgramToken(AbstractSyntaxTreeNode program)
    {
        this.program = program;
    }
    
    public override int VariablesRequired() => 0;

    public override int ValuesRequired() => 0;

    public override Scope Parse(Scope currentScope, ref Queue<Token> tokens)
    {
        if (program is Scope programScope)
        {
            currentScope.content.AddRange(programScope.content);
            currentScope.content.AddRange(programScope.bottom);
        }
        else
        {
            currentScope.content.Add(program);
        }
        
        return currentScope;
    }
}
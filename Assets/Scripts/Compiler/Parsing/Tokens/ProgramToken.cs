using System;
using System.Collections.Generic;

public class ProgramToken : Token
{
    public AbstractSyntaxTreeNode program = null;

    public void Awake()
    {
        List<TokenFiller> fillers = new(GetComponents<TokenFiller>());
        if (fillers.Count > 0) fillers[0].FillProgram(ref program);
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
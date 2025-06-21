using System.Collections.Generic;

public class VariableToken : Token
{
    public VariableToken(VariableType variableType) => this.variableType = variableType;
    
    public VariableType variableType;
    public override int VariablesRequired() => 0;

    public override int ValuesRequired() => 0;
    
    public override Scope Parse(Scope currentScope, ref Queue<Token> tokens)
    {
        return currentScope;
    }
}
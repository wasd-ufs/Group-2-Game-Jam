using System.Collections.Generic;

public class ValueToken : Token
{
    public ValueToken(int value) => this.value = value;
    
    public int value;
    public override int VariablesRequired() => 0;

    public override int ValuesRequired() => 0;
    
    public override Scope Parse(Scope currentScope, ref Queue<Token> tokens)
    {
        return currentScope;
    }
}
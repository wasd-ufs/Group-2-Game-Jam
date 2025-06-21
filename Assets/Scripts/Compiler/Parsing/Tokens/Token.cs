using System.Collections.Generic;

public abstract class Token
{
    public abstract int VariablesRequired();
    public abstract int ValuesRequired();
    public abstract Scope Parse(Scope currentScope, ref Queue<Token> tokens);
}
using System.Collections.Generic;

public class SwapToken : Token
{
    public Variable baseVariableA;
    public Variable baseVariableB;

    public SwapToken()
    {
        baseVariableA = null;
        baseVariableB = null;
    }

    public SwapToken(Variable baseVariableA, Variable baseVariableB)
    {
        this.baseVariableA = baseVariableA;
        this.baseVariableB = baseVariableB;
    }
    
    public override int VariablesRequired() => (baseVariableA == null ? 1 : 0) + (baseVariableB == null ? 1 : 0);

    public override int ValuesRequired() => 0;

    public override Scope Parse(Scope currentScope, ref Queue<Token> tokens)
    {
        var variableA = baseVariableA;
        var variableB = baseVariableB;

        if (variableA == null && tokens.TryDequeue(out var token1) && token1 is VariableToken variableToken1)
            variableA = new Variable(variableToken1.variableType);
        
        if (variableB == null && tokens.TryDequeue(out var token2) && token2 is VariableToken variableToken2)
            variableB = new Variable(variableToken2.variableType);

        currentScope.content.Add(new Assign(variableA, new Expression(ExpressionType.Sum, variableA, variableB)));
        currentScope.content.Add(new Assign(variableB, new Expression(ExpressionType.Subtract, variableA, variableB)));
        currentScope.content.Add(new Assign(variableA, new Expression(ExpressionType.Subtract, variableA, variableB)));
        return currentScope;
    }
}
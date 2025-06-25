using System.Collections.Generic;

public class SeParCollatzToken : Token
{
    public Variable variable;
    public Scope baseScope = new Scope();
    
    public void Awake()
    {
        List<TokenFiller> fillers = new(GetComponents<TokenFiller>());
        if (fillers.Count > 0) fillers[0].FillVariable(ref variable);
        if (fillers.Count > 1) fillers[1].FillScope(ref baseScope);
    }
    
    public override int VariablesRequired() => 1 - (variable == null ? 0 : 1);

    public override int ValuesRequired() => 0;
    
    public override Scope Parse(Scope currentScope, ref Queue<Token> tokens)
    {
        var variableCollatz = variable;
        if (variable == null && tokens.TryDequeue(out var token1))
        {
            if (token1 is VariableToken variableToken1)
                variableCollatz = new Variable(variableToken1.variableType);
        }

        Expression comparison = new Expression(ExpressionType.Mod, variableCollatz, new Constant(2));
        Assign assign = new Assign(variableCollatz, new Expression(ExpressionType.Divide, variableCollatz, new Constant(2)));
        var newScope = baseScope;
        var conditional = new Conditional(comparison, newScope);
        newScope.bottom.Add(assign);
        currentScope.content.Add(conditional);
        return newScope;
    }
}
using System.Collections.Generic;
using UnityEngine;

// Represents all of <Variable> = <Variable|Value>
public class AssignToken : Token
{
    private Variable baseVariable;
    private IValue baseValue;

    public AssignToken(Variable baseVariable, IValue baseValue)
    {
        this.baseVariable = baseVariable;
        this.baseValue = baseValue;
    }
    
    public override int VariablesRequired() => baseVariable != null ? 0 : 1;

    public override int ValuesRequired() => baseValue != null ? 0 : 1;
    
    public override Scope Parse(Scope currentScope, ref Queue<Token> tokens)
    {
        Variable variable = baseVariable;
        IValue value = baseValue;

        if (variable == null && tokens.TryDequeue(out var token1) && token1 is VariableToken variableToken1)
            variable = new Variable(variableToken1.variableType);

        if (value == null && tokens.TryDequeue(out var token2))
        {
            if (token2 is VariableToken variableToken2)
                value = new Variable(variableToken2.variableType);
            else if (token2 is ValueToken valueToken2)
                value = new Constant(valueToken2.value);
        }
        
        var assignment = new Assign(variable, value);
        currentScope.content.Add(assignment);
        return currentScope;
    }
}
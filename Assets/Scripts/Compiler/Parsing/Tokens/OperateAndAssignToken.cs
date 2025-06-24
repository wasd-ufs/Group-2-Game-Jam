using System.Collections.Generic;
using UnityEngine;

// Represents all of <Variable> = <Variable> <Operation> <Variable|Value>
public class OperateAndAssignToken : Token
{
    private ExpressionType expressionType;
    private Variable baseVariable = null;
    private IValue baseValue = null;

    public void Awake()
    {
        List<TokenFiller> fillers = new(GetComponents<TokenFiller>());
        if (fillers.Count > 0) fillers[0].FillVariable(ref baseVariable);
        if (fillers.Count > 1) fillers[1].FillValue(ref baseValue);
    }
    
    public override int VariablesRequired() => baseVariable != null ? 0 : 1;

    public override int ValuesRequired() => baseValue != null ? 0 : 1;
    
    public override Scope Parse(Scope currentScope, ref Queue<Token> tokens)
    {
        Variable variable = baseVariable;
        Expression value = new Expression(expressionType, baseVariable, baseValue);

        if (variable == null && tokens.TryDequeue(out var token1) && token1 is VariableToken variableToken1)
        {
            variable = new Variable(variableToken1.variableType);
            value.left = variable;
        }

        if (value.right == null && tokens.TryDequeue(out var token2))
        {
            if (token2 is VariableToken variableToken2)
                value.right = new Variable(variableToken2.variableType);
            else if (token2 is ValueToken valueToken2)
                value.right = new Constant(valueToken2.value);
        }
        
        var assignment = new Assign(variable, value);
        currentScope.content.Add(assignment);
        return currentScope;
    }
}
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

// Represents all of "while (<Variable|Value> <Operation> <Variable|Value>) {[...]}
public class WhileToken : Token
{
    public IValue baseLeftValue = null;
    public IValue baseRightValue = null;
    public ExpressionType expressionType;
    public Scope baseScope = new();
    
    public void Awake()
    {
        List<TokenFiller> fillers = new(GetComponents<TokenFiller>());
        if (fillers.Count > 0) fillers[0].FillValue(ref baseLeftValue);
        if (fillers.Count > 1) fillers[1].FillValue(ref baseRightValue);
        if (fillers.Count > 2) fillers[2].FillScope(ref baseScope);
    }
    
    public override int VariablesRequired() => 0;

    public override int ValuesRequired() => 2 - (baseLeftValue == null ? 0 : 1) - (baseRightValue == null ? 0 : 1);
    
    public override Scope Parse(Scope currentScope, ref Queue<Token> tokens)
    {
        Expression exp = new Expression(expressionType, baseLeftValue, baseRightValue);

        if (baseLeftValue == null && tokens.TryDequeue(out var token1))
        {
            if (token1 is VariableToken variableToken1)
                exp.left = new Variable(variableToken1.variableType);
            else if (token1 is ValueToken valueToken1)
                exp.left = new Constant(valueToken1.value);
        }

        if (baseRightValue == null && tokens.TryDequeue(out var token2))
        {
            if (token2 is VariableToken variableToken2)
                exp.right = new Variable(variableToken2.variableType);
            else if (token2 is ValueToken valueToken2)
                exp.right = new Constant(valueToken2.value);
        }

        var newScope = baseScope;
        var conditional = new Loop(exp, newScope);
        currentScope.content.Add(conditional);
        return newScope;
    }
}
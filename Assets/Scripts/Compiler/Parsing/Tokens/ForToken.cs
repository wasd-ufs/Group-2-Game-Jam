using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

// Represents all of "<Variable> = <StartValue> while (<Variable> <= <EndValue>) {[...] <Variable> += <IncrementValue>}
public class ForToken : Token
{
    public Variable baseIndex = null;
    public IValue baseStart = null;
    public IValue baseEnd = null;
    public IValue baseIncrement = null;
    public Scope baseScope = new();

    public void Awake()
    {
        List<TokenFiller> fillers = new(GetComponents<TokenFiller>());
        if (fillers.Count > 0) fillers[0].FillVariable(ref baseIndex);
        if (fillers.Count > 1) fillers[1].FillValue(ref baseStart);
        if (fillers.Count > 2) fillers[2].FillValue(ref baseEnd);
        if (fillers.Count > 3) fillers[3].FillValue(ref baseIncrement);
        if (fillers.Count > 4) fillers[4].FillScope(ref baseScope);
    }

    public override int VariablesRequired() => baseIndex != null ? 0 : 1;
    public override int ValuesRequired() => 3 - (baseStart != null ? 1 : 0) - (baseEnd != null ? 1 : 0) - (baseIncrement != null ? 1 : 0);

    public override Scope Parse(Scope currentScope, ref Queue<Token> tokens)
    {
        var index = baseIndex;
        var start = baseStart;
        var end = baseEnd;
        var increment = baseIncrement;
        var scope = baseScope;

        if (index == null && tokens.TryDequeue(out var token1) && token1 is VariableToken variableToken1)
            index = new Variable(variableToken1.variableType);

        if (start == null && tokens.TryDequeue(out var token2))
        {
            if (token2 is VariableToken variableToken2)
                start = new Variable(variableToken2.variableType);
            else if (token2 is ValueToken valueToken2)
                start = new Constant(valueToken2.value);
        }
        
        if (end == null && tokens.TryDequeue(out var token3))
        {
            if (token3 is VariableToken variableToken3)
                end = new Variable(variableToken3.variableType);
            else if (token3 is ValueToken valueToken3)
                end = new Constant(valueToken3.value);
        }
        
        if (increment == null && tokens.TryDequeue(out var token4))
        {
            if (token4 is VariableToken variableToken4)
                increment = new Variable(variableToken4.variableType);
            else if (token4 is ValueToken valueToken4)
                increment = new Constant(valueToken4.value);
        }

        var initializeCode = new Assign(index, start);
        currentScope.content.Add(initializeCode);
        
        var incrementCode = new Assign(index, new Expression(ExpressionType.Sum, index, increment));
        scope.bottom.Add(incrementCode);
        
        var conditionalCode = new Loop(new Expression(ExpressionType.LessThan, index, end), scope);
        currentScope.content.Add(conditionalCode);
        
        return scope;
    }
}
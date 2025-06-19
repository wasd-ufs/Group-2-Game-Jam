using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TokenStackAnalyzer
{
    private IExecutable program;
    private Scope currentScope;
    
    private List<IToken> incomplete;
    private int variablesNeeded;
    private int valuesNeeded;

    public TokenStackAnalyzer()
    {
        currentScope = new Scope();
        program = currentScope;
        
        incomplete = new List<IToken>();
        variablesNeeded = 0;
        valuesNeeded = 0;
    }

    public void Clear()
    {
        currentScope = new Scope();
        program = currentScope;
        
        incomplete.Clear();
        variablesNeeded = 0;
        valuesNeeded = 0;
    }

    public bool TryPush(IToken token)
    {
        if (!CanPush(token))
            return false;
        
        PushToIncomplete(token);
        
        if (IsCompletable())
            Complete();
        
        return true;
    }

    public bool CanPush(IToken token)
    {
        if (token is VariableToken)
            return variablesNeeded > 0 || valuesNeeded > 0;
        
        if (token is ValueToken)
            return valuesNeeded > 0;
        
        return incomplete.Count == 0;
    }

    private void PushToIncomplete(IToken token)
    {
        switch (token)
        {
            case VariableToken:
                if (variablesNeeded > 0)
                    variablesNeeded--;
                else if (valuesNeeded > 0)
                    valuesNeeded--;
                break;
            
            case ValueToken:
                valuesNeeded--;
                break;
            
            case PlusEqualsToken:
                variablesNeeded++;
                valuesNeeded++;
                break;
        }
        
        incomplete.Add(token);
    }

    private void Complete()
    {
        if (incomplete.Count == 0)
            return;

        var first = incomplete.First();
        switch (first)
        {
            case PlusEqualsToken:
                Variable variable = new Variable((incomplete[1] as VariableToken).variableType);
                IValue value;
                
                variable = new Variable((incomplete[1] as VariableToken).variableType);
                if (incomplete[2] is VariableToken)
                    value = new Variable((incomplete[2] as VariableToken).variableType);
                else
                    value = new Constant((incomplete[2] as ValueToken).value);
                
                var expression = new Expression(ExpressionType.Plus, variable, value);
                var set = new SetVariable(variable, expression);
                if (currentScope == null)
                    Debug.Log("how?");
                
                currentScope.Add(set);
                break;
        }
        
        incomplete.Clear();
    }
    private bool IsCompletable() => variablesNeeded == 0 && valuesNeeded == 0; 

    public bool IsProgramExecutable() => incomplete.Count == 0;
    public IExecutable GetProgram() => IsProgramExecutable() ? program : null;
}
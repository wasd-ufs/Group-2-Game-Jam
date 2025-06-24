using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Parser
{
    private readonly Stack<Token> tokens = new();
    private int variablesNeeded;
    private int valuesNeeded;

    public void Clear()
    {
        tokens.Clear();
        variablesNeeded = 0;
        valuesNeeded = 0;
    }

    public bool TryPush(Token token)
    {
        if (!CanPush(token))
            return false;
        
        Push(token);
        return true;
    }

    public Token Pop() => tokens.Pop();

    public bool CanPush(Token token)
    {
        if (token is VariableToken)
            return variablesNeeded > 0 || valuesNeeded > 0;
        
        if (token is ValueToken)
            return valuesNeeded > 0;
        
        return variablesNeeded == 0 && valuesNeeded == 0;
    }

    private void Push(Token token)
    {
        variablesNeeded += token.VariablesRequired();
        valuesNeeded += token.ValuesRequired();
        
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
        }
        
        tokens.Push(token);
    }
    
    public AbstractSyntaxTreeNode GetProgram()
    {
        var queue = new Queue<Token>(tokens.Reverse());
        Debug.Log(queue.Count);
        
        var currentScope = new Scope();
        var program = currentScope;

        while (queue.TryDequeue(out var token))
            currentScope = token.Parse(currentScope, ref queue);

        return program;
    }

    public bool IsProgramExecutable() => variablesNeeded == 0 && valuesNeeded == 0;
}
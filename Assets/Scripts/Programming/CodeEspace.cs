using System.Collections.Generic;
using System.Linq;

public class CodeEspace
{
    public Scope program = new Scope();
    public Scope top = null;
    public List<ICard> open = new();

    public void Clear()
    {
        program = new Scope();
        top = program;
        open.Clear();
    }

    public bool Push(ICard card)
    {
        top ??= program;
        
        if (CanPushToOpen(card))
            open.Add(card);
        else 
            return false;
        
        if (IsOpenComplete())
            PutOpenIntoProgram();
        
        return true;
    }

    public bool IsExecutable() => open.Count == 0;

    public bool IsOpenComplete()
    {
        var variablesNeeded = 0;
        var valuesNeeded = 0;
        foreach (var card in open)
        {
            if (variablesNeeded > 0 && card is not VariableCard)
                return false;

            if (valuesNeeded > 0 && card is not ValueCard)
                return false;
            
            variablesNeeded += card.VariablesRequired();
            valuesNeeded += card.ValuesRequired();
        }
        
        return variablesNeeded == 0 && valuesNeeded == 0;
    }

    public bool CanPushToOpen(ICard card)
    {
        var variablesNeeded = 0;
        var valuesNeeded = 0;
        foreach (var openCard in open)
        {
            variablesNeeded += openCard.VariablesRequired();
            valuesNeeded += openCard.ValuesRequired();
        }
        
        if (variablesNeeded > 0 && card is not VariableCard)
            return false;
        
        if (valuesNeeded > 0 && card is not ValueCard)
            return false;
        
        return true;
    }

    // Terminar sapoha
    public void PutOpenIntoProgram()
    {
        if (open.Count == 0)
            return;
        
        var valueStack = new Stack<IValue>();
        IExecutable executable = new Scope();

        open.Reverse();
        foreach (var card in open)
        {
            if (card is ValueCard value)
            {
                valueStack.Push(new Constant(value.value));
                continue;
            }

            if (card is VariableCard variable)
            {
                valueStack.Push(new Variable(variable.variableType));
                continue;
            }

            if (card is IncrementCard)
            {
                var set = new SetVariable();
                set.variable = valueStack.Pop() as Variable;

                var expression = new Expression();
                expression.comparisonType = ExpressionType.Plus;
                expression.left = set.variable;
                expression.right = valueStack.Pop();

                set.value = expression;
                executable = set;
                
                
                break;
            }
        }
    }
}
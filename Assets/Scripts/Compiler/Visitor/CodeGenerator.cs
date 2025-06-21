using System;
using System.Collections.Generic;
using System.Linq;

public class Code
{
    private List<String> lines;
    private Dictionary<AbstractSyntaxTreeNode, int> lineOfNode;
    
    public Code(List<String> lines, Dictionary<AbstractSyntaxTreeNode, int> lineOfNode)
    {
        this.lines = lines;
        this.lineOfNode = lineOfNode;
    }

    public string GetFullText() => string.Join(Environment.NewLine, lines);
    public string GetLine(int index) => lines[index];
    public string GetLineOfNode(AbstractSyntaxTreeNode node) => GetLine(lineOfNode[node]);
    public List<string> GetLines() => lines;
}

public class CodeGenerator : AbstractSyntaxTreeVisitor
{
    private int indentationLevel = 0;
    private List<String> lines = new();
    private readonly Dictionary<AbstractSyntaxTreeNode, int> nodesToLines = new();

    public string placeholderString = "__________";
    public string variable1Name = "a";
    public string variable2Name = "b";
    public string variable3Name = "c";
    public string variable4Name = "d";

    public Code Generate(AbstractSyntaxTreeNode tree)
    {
        indentationLevel = 0;
        lines.Clear();
        nodesToLines.Clear();
        
        tree.Accept(this);
        return new Code(new List<string>(lines), new Dictionary<AbstractSyntaxTreeNode, int>(nodesToLines));
    }
    
    public override void VisitAssign(Assign assign)
    {
        var variable = ValueAsString(assign.variable);
        var value = ValueAsString(assign.value);

        var statement = variable + " = " + value;
        var ending = ";";
        
        nodesToLines.Add(assign, lines.Count);
        lines.Add(Indentation() + statement + ending);
    }

    public override void VisitConditional(Conditional conditional)
    {
        var statement = "if (" + ValueAsString(conditional.condition) + ")";
        
        nodesToLines.Add(conditional, lines.Count);
        lines.Add(Indentation() + statement);
        conditional.next.Accept(this);
    }

    public override void VisitLoop(Loop loop)
    {
        var statement = "while (" + ValueAsString(loop.condition) + ")";
        
        nodesToLines.Add(loop, lines.Count);
        lines.Add(Indentation() + statement);
        loop.next.Accept(this);
    }

    public override void VisitScope(Scope scope)
    {
        var isInitialScope = lines.Count == 0;
        nodesToLines.Add(scope, lines.Count);
        
        if (!isInitialScope)
        {
            lines.Add(Indentation() + "{");
            indentationLevel++;
        }

        foreach (var next in scope.content)
            next.Accept(this);
        
        foreach (var next in scope.bottom)
            next.Accept(this);
        
        if (!isInitialScope)
        {
            indentationLevel--;
            lines.Add(Indentation() + "}");
        }
    }

    private string Indentation() => new string(' ', 4 * indentationLevel);

    private string ValueAsString(IValue value)
    {
        if (value == null)
            return placeholderString;
        
        if (value is Variable variable)
            return VariableAsString(variable);
        
        if (value is Expression expression)
            return ExpressionAsString(expression);

        return value.Get().ToString();
    }
    
    private String ExpressionAsString(Expression expression)
    {
        var left = expression.left switch
        {
            null => placeholderString,
            Variable leftVariable => VariableAsString(leftVariable),
            _ => expression.left.Get().ToString()
        };
        
        var right = expression.right switch
        {
            null => placeholderString,
            Variable rightVariable => VariableAsString(rightVariable),
            _ => expression.right.Get().ToString()
        };
        
        var operation = OperationAsString(expression.expressionType);
        return left + " " + operation + " " + right;
    }
    
    private string VariableAsString(Variable variable) => variable == null ? placeholderString : variable.variableType switch
    {
        VariableType.Player1Energy => "player1.energy",
        VariableType.Player2Energy => "player2.energy",
        VariableType.Player1CardCount => "player1.card_count",
        VariableType.Player2CardCount => "player2.card_count",
        VariableType.Variable1 => variable1Name,
        VariableType.Variable2 => variable2Name,
        VariableType.Variable3 => variable3Name,
        VariableType.Variable4 => variable4Name,
        _ => placeholderString
    };

    private string OperationAsString(ExpressionType expressionType) => expressionType switch
    {
        ExpressionType.Sum => "+",
        ExpressionType.Subtract => "-",
        ExpressionType.Multiply => "*",
        ExpressionType.Divide => "/",
        ExpressionType.Mod => "%",
        ExpressionType.Equal => "==",
        ExpressionType.NotEqual => "!=",
        ExpressionType.GreaterThan => ">",
        ExpressionType.GreaterThanOrEqual => ">=",
        ExpressionType.LessThan => "<",
        ExpressionType.LessThanOrEqual => "<=",
        _ => placeholderString
    };
}
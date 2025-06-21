using System.Collections.Generic;

public class DecoratedToken : Token
{
    public List<AbstractSyntaxTreeNode> prefix;
    public Token content;
    public List<AbstractSyntaxTreeNode> suffix;

    public DecoratedToken(Token content, List<AbstractSyntaxTreeNode> prefix, List<AbstractSyntaxTreeNode> suffix)
    {
        this.content = content;
        this.prefix = prefix;
        this.suffix = suffix;
    }
    
    public override int VariablesRequired() => content.VariablesRequired();

    public override int ValuesRequired() => content.ValuesRequired();

    public override Scope Parse(Scope currentScope, ref Queue<Token> tokens)
    {
        currentScope.content.AddRange(prefix);
        var scope = content.Parse(currentScope, ref tokens);
        currentScope.content.AddRange(suffix);
        return scope;
    }
}
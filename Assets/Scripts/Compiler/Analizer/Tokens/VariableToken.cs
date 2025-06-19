public class VariableToken : IToken
{
    public VariableToken(VariableType variableType) => this.variableType = variableType;
    
    public VariableType variableType;
}
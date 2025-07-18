public enum VariableType
{
    Player1Energy,
    Player2Energy,
    Player1CardCount,
    Player2CardCount,
    Variable1,
    Variable2,
    Variable3,
    Variable4,
}

public class Variable : IValue
{
    public VariableType variableType;
    
    public Variable(VariableType variableType) => this.variableType = variableType;

    public int Get() => variableType switch
    {
        VariableType.Player1Energy => PlayerVariables.Player1Energy,
        VariableType.Player2Energy => PlayerVariables.Player2Energy,
        VariableType.Player1CardCount => PlayerVariables.Player1CardCount,
        VariableType.Player2CardCount => PlayerVariables.Player2CardCount,
        VariableType.Variable1 => GlobalVariables.variable1,
        VariableType.Variable2 => GlobalVariables.variable2,
        VariableType.Variable3 => GlobalVariables.variable3,
        VariableType.Variable4 => GlobalVariables.variable4,
        _ => 0
    };

    public void Set(int value)
    {
        switch (variableType)
        {
            case VariableType.Player1Energy:
                PlayerVariables.Player1Energy = value;
                break;
            case VariableType.Player2Energy:
                PlayerVariables.Player2Energy = value;
                break;
            case VariableType.Player1CardCount:
                PlayerVariables.Player1CardCount = value;
                break;
            case VariableType.Player2CardCount:
                PlayerVariables.Player2CardCount = value;
                break;
            case VariableType.Variable1:
                GlobalVariables.variable1 = value;
                break;
            case VariableType.Variable2:
                GlobalVariables.variable2 = value;
                break;
            case VariableType.Variable3:
                GlobalVariables.variable3 = value;
                break;
            case VariableType.Variable4:
                GlobalVariables.variable4 = value;
                break;
        }
    }
}
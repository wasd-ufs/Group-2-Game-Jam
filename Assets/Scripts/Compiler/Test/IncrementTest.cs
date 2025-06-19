using UnityEngine;

public class IncrementTest : MonoBehaviour
{
    void Start()
    {
        TokenStackAnalyzer analyzer = new TokenStackAnalyzer();
        
        Debug.Log("Current Card Count is: " + PlayerVariables.Player1CardCount);
        analyzer.TryPush(new PlusEqualsToken());
        analyzer.TryPush(new VariableToken(VariableType.Player1CardCount));
        analyzer.TryPush(new ValueToken(5));
        
        analyzer.TryPush(new PlusEqualsToken());
        analyzer.TryPush(new VariableToken(VariableType.Player1CardCount));
        analyzer.TryPush(new VariableToken(VariableType.Player1CardCount));
        
        IExecutable program = analyzer.GetProgram();
        if (program == null)
        {
            Debug.LogError("Tu é burro pra krl ne");
            return;
        }

        program.Execute();
        Debug.Log("After running the program, Card Count is: " + PlayerVariables.Player1CardCount);
    }
}

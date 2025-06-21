using UnityEngine;

public class IncrementTest : MonoBehaviour
{
    void Start()
    {
        var analyzer = new Parser();

        analyzer.TryPush(new ForToken(new Constant(0), new Constant(3), new Constant(1)));
        analyzer.TryPush(new VariableToken(VariableType.Player1Energy));
        analyzer.TryPush(new ConditionalToken(ExpressionType.LessThan, new Variable(VariableType.Player1Energy), new Constant(2)));
        analyzer.TryPush(new AssignToken(new Variable(VariableType.Player1Energy), new Expression(ExpressionType.Multiply, new Variable(VariableType.Player1Energy), new Constant(2))));
        
        var program = analyzer.GetProgram();

        CodeGenerator generator = new CodeGenerator();
        Code code = generator.Generate(program);
        Debug.Log(code.GetFullText());
        
        Interpreter interpreter = new Interpreter();
        interpreter.OnNodeEntered = node => Debug.Log(code.GetLineOfNode(node));
        interpreter.Interpret(program);
    }
}

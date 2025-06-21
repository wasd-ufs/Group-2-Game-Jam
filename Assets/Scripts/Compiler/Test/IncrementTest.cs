using System.Collections.Generic;
using UnityEngine;

public class IncrementTest : MonoBehaviour
{
    void Start()
    {
        var analyzer = new Parser();
        analyzer.TryPush(new SwapToken(new Variable(VariableType.Variable1), new Variable(VariableType.Variable2)));
        
        var program = analyzer.GetProgram();

        CodeGenerator generator = new CodeGenerator();
        generator.variable1Name = "mario";
        generator.variable2Name = "luigi";
        Code code = generator.Generate(program);
        Debug.Log(code.GetFullText());
        
        Interpreter interpreter = new Interpreter();
        interpreter.OnNodeEntered = node => Debug.Log(code.GetLineOfNode(node));
        interpreter.Interpret(program);
    }
}

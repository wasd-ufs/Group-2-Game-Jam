using System.Collections.Generic;
using UnityEngine;

public class IncrementTest : MonoBehaviour
{
    void Start()
    {
        var analyzer = new Parser();
        var token = GetComponent<Token>();
        if (token == null)
            Debug.LogError($"{nameof(token)} is null");
        
        analyzer.TryPush(token);
        var program = analyzer.GetProgram();

        CodeGenerator generator = new CodeGenerator
        {
            variable1Name = "mario",
            variable2Name = "luigi"
        };
        Code code = generator.Generate(program);
        Debug.Log(code.GetFullText());
        
        if (analyzer.IsProgramExecutable())
        {
            Interpreter interpreter = new Interpreter
            {
                OnNodeEntered = node => Debug.Log(code.GetLineOfNode(node))
            };
            interpreter.Interpret(program);
        }
    }
}

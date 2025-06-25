using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExecutionController : MonoBehaviour
{
    public TMP_Text player1ExecutionText;
    public TMP_Text player2ExecutionText;
    
    private CodeGenerator codeGenerator = new();
    private Interpreter interpreter = new();

    public bool run = false;

    public void Start()
    {
        GameManager.onExecutionPhaseEntered.AddListener(() =>
        {
            run = true;
        });
    }

    public void LateUpdate()
    {
        if (run)
        {
            RunAllPrograms();
            CheckForWinner();
            run = false;
        }
    }

    public void RunAllPrograms()
    {
        for (int i = 0; i < ProgramSlots.PlayerSlots.Length; i++)
            Execute(i);
    }

    public void CheckForWinner()
    {
        var result = GameManager.CheckForWinner();
        
        Debug.Log(result);
        if (result == RoundResult.NoWinnerYet)
        {
            GameManager.GoToProgrammingState();
            Debug.Log("aaaaloooooooooo");
        }
    }

    public void Execute(int slot)
    {
        if (ProgramSlots.PlayerSlots[slot] != null)
        {
            var code = codeGenerator.Generate(ProgramSlots.PlayerSlots[slot]);
            if (slot % 2 == 0)
            {
                player1ExecutionText.text = code.GetFullText();
                player1ExecutionText.color = Color.white;
                player2ExecutionText.color = Color.darkGray;
            }
            else
            {
                player2ExecutionText.text = code.GetFullText();
                player2ExecutionText.color = Color.white;
                player1ExecutionText.color = Color.darkGray;
            }
        
            interpreter.OnNodeEntered = (node) =>
            {
                OnInterpreterNodeEntered(slot, code, node);
            };
            interpreter.OnNodeExecutionFail = _ =>
            {
                ProgramSlots.PlayerSlots[slot] = null;
            };
            interpreter.Interpret(ProgramSlots.PlayerSlots[slot]);
        }
    }

    public void OnInterpreterNodeEntered(int slot, Code code, AbstractSyntaxTreeNode node)
    {
        Debug.Log(code.GetLineOfNode(node));
        var lines = new List<string>(code.GetLines());
        var toHighlight = code.GetLineIndexOfNode(node);
        lines[toHighlight] = $"<mark=#ff0000aa>{lines[toHighlight]}</mark>";

        var text = string.Join('\n', lines);
        if (slot % 2 == 0)
        {
            player1ExecutionText.text = text;
        }
        else
        {
            player2ExecutionText.text = text;
        }
    }
}
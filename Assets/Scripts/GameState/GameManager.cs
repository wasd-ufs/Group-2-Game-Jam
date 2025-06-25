using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    NotStarted,
    Programming,
    Execution,
    RoundResults,
    GameResults
}

public enum RoundResult
{
    Player1Wins,
    Player2Wins,
    Draw,
    NoWinnerYet
}

public class GameManager : MonoBehaviour
{
    public static UnityEvent onProgrammingPhaseEntered = new();
    public static UnityEvent onExecutionPhaseEntered = new();
    public static UnityEvent<RoundResult> onRoundResultsEntered = new();
    public static UnityEvent<RoundResult> onGameResultsEntered = new();
    
    public static GameState state = GameState.NotStarted;
    
    public static void GoToProgrammingState()
    {
        if (IsProgramming())
            return;
        
        state = GameState.Programming;
        onProgrammingPhaseEntered.Invoke();
    }

    public static void GoToExecutionState()
    {
        if (IsExecuting())
            return;
        
        state = GameState.Execution;
        onExecutionPhaseEntered.Invoke();
    }

    public static void GoToRoundResultState()
    {
        if (IsChekingResult())
            return;

        var result = CheckForWinner();
        state = GameState.RoundResults;
        onRoundResultsEntered.Invoke(result);
    }

    public static void GoToGameResultsState()
    {
        if (IsGameResult())
            return;
        
        var result = CheckForWinner();
        state = GameState.GameResults;
        onGameResultsEntered.Invoke(result);
    }

    public static RoundResult CheckForWinner()
    {
        if (PlayerVariables.Player1Energy <= 0 && PlayerVariables.Player2Energy <= 0)
            return RoundResult.Draw;

        if (PlayerVariables.Player2Energy <= 0)
            return RoundResult.Player1Wins;

        if (PlayerVariables.Player1Energy <= 0)
            return RoundResult.Player2Wins;
        
        return RoundResult.NoWinnerYet;
    }
    
    public static bool IsProgramming() => state == GameState.Programming;
    public static bool IsExecuting() => state == GameState.Execution;
    public static bool IsChekingResult() => state == GameState.RoundResults;
    public static bool IsGameResult() => state == GameState.GameResults;
}
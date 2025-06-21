using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicWinsMatch : MonoBehaviour
{
    [SerializeField] private bool gameEnded = false;
    public GameObject endPanel;
    

    void Update()
    {
        if (gameEnded) return;

        CheckVictoryConditions();
    }

    private void CheckVictoryConditions()
    {
        if(PlayerVariables.Player1Energy <= 0)
        {
            EndGame("Player 2 venceu!! (Player 1 sem vida");
        }
        else if (PlayerVariables.Player2Energy <= 0)
        {
            EndGame("Player 1 venceu!! (Player 2 sem vida");
        }
        else if (PlayerVariables.Player1CardCount <= 0)
        {
            EndGame("Player 2 venceu!! (Player 1 sem cartas");
        }
        else if(PlayerVariables.Player2CardCount <= 0)
        {
            EndGame("Player 1 venceu!! (Player 2 sem cartas");
        }
    }

    private void EndGame(string message)
    {
        gameEnded = true;

        Time.timeScale = 0f;

        //Para ativar a tela de Menu de fim de partida
        endPanel.SetActive(true);
 
        Debug.Log(message);
        Debug.Log("Partida finalizada");
    }
    public void RestartMatch()  //Utilizar no botão de reiniciar partida
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()   //Utilizar no botão de voltar ao menu
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}



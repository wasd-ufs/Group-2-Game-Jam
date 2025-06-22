using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    [SerializeField] private int currentRound = 1;

    [SerializeField] private int Slots = 3;  //Só deixei como base, ja que Vitu vai puxar do codigo que ele fez

    public GameObject endPanel;

    private void Start()
    {
        StartProgrammimgPhase();
    }

    private void StartProgrammimgPhase()
    {
        Debug.Log("Fase de Programação " + currentRound);

        //Logica da programação das cartas..

        EndProgrammimgPhase();
    }

    private void EndProgrammimgPhase()
    {
        Debug.Log("Fase de Programação " + currentRound);

        //Logica de mostrar os codigos...

        StartCoroutine(ExecuteSlotsAlternately(Slots));
    }

    private IEnumerator ExecuteSlotsAlternately(int slots)
    {
        //So um esboço
        for (int i = 0; i< slots; i++)
        {
            ExecuteSlot(1, i); 
            yield return new WaitForSeconds(1f); 

            ExecuteSlot(2, i);
            yield return new WaitForSeconds(1f);
        }

        CheckVictoryConditions();
     
    }

    private void ExecuteSlot(int player, int slotIndex)
    {
        Debug.Log($"Executando Slot {slotIndex + 1} do Jogador {player}");

        bool hasCrash = false;

        //Aplicar a lógica de crash para remover as cartas

        if (hasCrash)
            BurnSlot(player, slotIndex);
    }

    private void BurnSlot(int player, int slotIndex)
    {
        Debug.Log($"?? Slot {slotIndex + 1} do Jogador {player} queimou!");
        //Adicionar a logica para remover as cartas do slot permanentemente
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
        else
        {
            currentRound++;
            StartProgrammimgPhase();
        }
    }

    private void EndGame(string message)
    {
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



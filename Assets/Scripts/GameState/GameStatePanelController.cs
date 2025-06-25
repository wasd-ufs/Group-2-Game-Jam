using UnityEngine;
using UnityEngine.UI;

public class GameStatePanelController : MonoBehaviour
{
    public GameObject programmingPanel;
    public GameObject executionPanel;
    public Button executionButton;
    public GameObject hand;

    public void Start()
    {
        GameManager.onProgrammingPhaseEntered.AddListener(() =>
        {
            programmingPanel.SetActive(true);
            executionPanel.SetActive(false);
            hand.SetActive(true);
        });   
        GameManager.onExecutionPhaseEntered.AddListener(() =>
        {
            programmingPanel.SetActive(false);
            executionPanel.SetActive(true);
            hand.SetActive(false);
        });
        executionButton.onClick.AddListener(GameManager.GoToExecutionState);
    }
}
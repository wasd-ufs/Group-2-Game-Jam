using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;


public class Slot : MonoBehaviour, IPointerClickHandler
{
    [Header("UI References")]
    public GameObject inputPanel;
    public TMP_InputField nameInputField;
    public TMP_Text functionNameText;
    public HandUIController originHand;

    [Header("Function Name")]
    private string functionName = "";

    [Header("Confirm Button (inside Input Panel)")]
    public Button confirmButton;


    private void Start()
    {
        if (inputPanel != null)
            inputPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inputPanel != null)
        {
            inputPanel.SetActive(true);
            nameInputField.text = "";
            nameInputField.ActivateInputField();

            // Clean previous listeners and add this slot's own ConfirmFusionName
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(ConfirmFunctionName);
        }
    }

    // This method should be called by the Confirm button in the Input Panel
    public void ConfirmFunctionName()
    {
        string typedName = nameInputField.text.Trim();

        // Store and display the function name
        functionName = typedName;
        functionNameText.text = functionName;

        // Move selected cards to the slot
        foreach (CardUI card in originHand.GetSelectedCards())
        {
            DisplayCode(card);
            GameObject.Destroy(card.gameObject); // Destroy the card GameObject
        }

        inputPanel.SetActive(false);
    }

    public string GetFunctionName()
    {
        return functionName;
    }

    private void DisplayCode(CardUI card)
    {
        Debug.Log($"Displaying code for card: {card.GetCardData().cardText}");
    }
}

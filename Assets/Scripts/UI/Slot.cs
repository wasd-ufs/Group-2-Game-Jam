using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;


public class Slot : MonoBehaviour, IPointerClickHandler
{
    private List<CardUI> cardsInSlot = new List<CardUI>();

    [Header("UI References")]
    public GameObject inputPanel;
    public TMP_InputField nameInputField;
    public TMP_Text functionNameText;
    public TMP_Text codeField;
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
        if (inputPanel == null)
        {
            Debug.LogWarning("Input Panel is not assigned in the Slot component.");
            return;
        }
        if (IsEmpty())
        {
            inputPanel.SetActive(true);
            nameInputField.text = "";
            nameInputField.ActivateInputField();

            // Clean previous listeners and add this slot's own ConfirmFusionName
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(ConfirmFunctionName);
        }
        else
        {
            ShowCardsInSlot();
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
        foreach (GameObject card in originHand.GetSelectedCards())
        {
            DisplayCode(card);
            cardsInSlot.Add(card.GetComponent<CardUI>());
            originHand.RemoveCard(card);
        }

        inputPanel.SetActive(false);
    }

    public string GetFunctionName()
    {
        return functionName;
    }

    private void DisplayCode(GameObject card)
    {
        codeField.text += $"{card.GetComponent<CardUI>().GetCardData().cardText}\n";
    }

    public bool IsEmpty()
    {
        return cardsInSlot.Count == 0;
    }

    private void ShowCardsInSlot()
    {
        string cardNames = "Cards in Slot:\n";
        foreach (CardUI card in cardsInSlot)
        {
            cardNames += $"{card.GetCardData().cardText}\n";
        }
        Debug.Log(cardNames);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;


public class Slot : MonoBehaviour, IPointerClickHandler
{
    private List<CardUI> cardsInSlot = new List<CardUI>();

    [Header("UI References")]
    private GameObject inputPanel;
    private TMP_InputField nameInputField;
    private TMP_Text codeField;
    private GameObject showPanel;
    private GameObject showPanelCardContainer;
    public HandUIController originHand;
    public GameObject cardPrefab; // Prefab for displaying cards in the slot

    private GameObject slotField;
    private CanvasGroup slotFieldCanvasGroup;



    [Header("Confirm Button (inside Input Panel)")]
    private Button confirmButton;


    private void Start()
    {
        // Find child elements relative to this Slot GameObject
        inputPanel = transform.Find("InputPanel")?.gameObject;
        nameInputField = inputPanel?.transform.Find("InputField")?.GetComponent<TMP_InputField>();
        confirmButton = inputPanel?.transform.Find("Confirm")?.GetComponent<Button>();

        codeField = transform.Find("CodeField")?.GetComponent<TMP_Text>();
        slotField = transform.Find("SlotField")?.gameObject;
        slotFieldCanvasGroup = slotField?.GetComponent<CanvasGroup>();

        showPanel = transform.Find("ShowPanel")?.gameObject;
        showPanelCardContainer = showPanel?.transform.Find("CardContainer")?.gameObject;




        if (inputPanel != null)
        {
            inputPanel.SetActive(false);
        }
        if (showPanel != null)
        {
            showPanel.SetActive(false);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void FieldWasClicked()
    {
        if (IsEmpty())
        {
            inputPanel.SetActive(true);
            nameInputField.text = "";
            nameInputField.ActivateInputField();

            // Clean previous listeners and add this slot's own ConfirmFusionName
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(ConfirmFunctionName);

            SetCardsInSlot(originHand.GetSelectedCards());
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
        slotField.GetComponent<SlotField>().SetFunctionName(typedName);

        inputPanel.SetActive(false);
    }

    public void SetCardsInSlot(List<GameObject> selectedCards)
    {
        cardsInSlot.Clear();
        codeField.text = ""; // Clear previous code

        foreach (GameObject card in selectedCards)
        {
            CardUI cardUI = card.GetComponent<CardUI>();
            if (cardUI != null)
            {
                cardsInSlot.Add(cardUI);
                DisplayCode(card);
                originHand.RemoveCard(card);
                card.transform.SetParent(showPanelCardContainer.transform, false);
            }
        }
    }

    public string GetFunctionName()
    {
        return slotField.GetComponent<SlotField>().GetFunctionName();
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
        slotFieldCanvasGroup.blocksRaycasts = false;
        showPanel.SetActive(true);

    }

    public void HideShowPanel()
    {
        slotFieldCanvasGroup.blocksRaycasts = true;
        showPanel.SetActive(false);
    }
}

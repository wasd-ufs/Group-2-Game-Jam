using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Serialization;


public class Slot : MonoBehaviour, IPointerClickHandler
{
    private List<CardUI> cardsInSlot = new List<CardUI>();
    public GameObject emptyCard;

    [Header("UI References")]
    public InputPanel inputPanel;
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
    
    [FormerlySerializedAs("tokenSlot")] [SerializeField] 
    private int programSlot = 0;
    private CodeGenerator codeGenerator = new();
    
    private void Start()
    {
        codeField = transform.Find("CodeField")?.GetComponent<TMP_Text>();
        slotField = transform.Find("SlotField")?.gameObject;
        slotFieldCanvasGroup = slotField?.GetComponent<CanvasGroup>();

        showPanel = transform.Find("ShowPanel")?.gameObject;
        showPanelCardContainer = showPanel?.transform.Find("CardContainer")?.gameObject;
        
        inputPanel.onShowPanel.AddListener(() =>
        {
            slotFieldCanvasGroup.blocksRaycasts = false;
            slotFieldCanvasGroup.interactable = false;
        });
        inputPanel.onHidePanel.AddListener(() =>
        {
            slotFieldCanvasGroup.blocksRaycasts = true;
            slotFieldCanvasGroup.interactable = true;
        });
        
        if (inputPanel != null)
        {
            inputPanel.Hide();
        }
        if (showPanel != null)
        {
            showPanel.SetActive(false);
        }
        
        GameManager.onProgrammingPhaseEntered.AddListener(() =>
        {
            if (ProgramSlots.PlayerSlots[programSlot] == null)
                ClearSlot();
        });
    }


    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void FieldWasClicked()
    {
        if (inputPanel.IsShowing())
            return;
        
        if (IsEmpty() && originHand.HasSelectedCards() && originHand.IsProgramExecutable())
        {
            // Clean previous listeners and add this slot's own ConfirmFusionName
            inputPanel.Show();
            inputPanel.onNameInputed.RemoveAllListeners();
            inputPanel.onNameInputed.AddListener(programName =>
            {
                slotField.GetComponent<SlotField>().SetFunctionName(programName);
                SetCardsInSlot(originHand.GetSelectedCards(), originHand.GetProgram());
                inputPanel.Hide();
            });
        }
        else if (ProgramSlots.PlayerSlots[programSlot] != null)
        {
            originHand.AddCard(GetFunctionCard());
            ClearSlot();
            //ShowCardsInSlot();
        }
    }

    // This method should be called by the Confirm button in the Input Panel
    public void ConfirmFunctionName()
    {
        string typedName = nameInputField.text.Trim();

        // Store and display the function name
        

        inputPanel.Hide();
    }

    public void SetCardsInSlot(List<GameObject> selectedCards, AbstractSyntaxTreeNode programNode)
    {
        cardsInSlot.Clear();
        codeField.text = ""; // Clear previous code

        foreach (GameObject card in selectedCards)
        {
            CardUI cardUI = card.GetComponent<CardUI>();
            if (cardUI != null)
            {
                cardsInSlot.Add(cardUI);
                originHand.RemoveCard(card);
                cardUI.OnUsed();
                card.transform.SetParent(showPanelCardContainer.transform, false);
            }
        }

        ProgramSlots.PlayerSlots[programSlot] = programNode;
        var code = codeGenerator.Generate(programNode);
        codeField.text = code.GetFullText();
    }

    public void OnFunctionNameInputed(string functionName)
    {
        
    }

    public void ClearSlot()
    {
        cardsInSlot.Clear();
        codeField.text = "";
        slotField.GetComponent<SlotField>().SetFunctionName("");
        ProgramSlots.PlayerSlots[programSlot] = null;
    }

    public string GetFunctionName()
    {
        return slotField.GetComponent<SlotField>().GetFunctionName();
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

    public GameObject GetFunctionCard()
    {
        if (ProgramSlots.PlayerSlots[programSlot] == null)
            return null;

        var card = Instantiate(emptyCard);
        
        var programToken = card.AddComponent<ProgramToken>();
        programToken.Setup(ProgramSlots.PlayerSlots[programSlot]);
        
        var cardUI = card.GetComponent<CardUI>();
        cardUI.cardText.text = slotField.GetComponent<SlotField>().GetFunctionName();
        return card;
    }
}

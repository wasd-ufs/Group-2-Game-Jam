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
    
    [FormerlySerializedAs("tokenSlot")] [SerializeField] 
    private int programSlot = 0;
    private CodeGenerator codeGenerator = new();
    
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
        if (IsEmpty() && originHand.HasSelectedCards() && originHand.IsProgramExecutable())
        {
            inputPanel.SetActive(true);
            nameInputField.text = "";
            nameInputField.ActivateInputField();

            // Clean previous listeners and add this slot's own ConfirmFusionName
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(ConfirmFunctionName);
            
            SetCardsInSlot(originHand.GetSelectedCards(), originHand.GetProgram());
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
        slotField.GetComponent<SlotField>().SetFunctionName(typedName);

        inputPanel.SetActive(false);
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

    public void ClearSlot()
    {
        cardsInSlot.Clear();
        codeField.text = "";
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

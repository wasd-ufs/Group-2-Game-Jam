using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;


public class SlotField : MonoBehaviour, IPointerClickHandler
{

    [Header("UI References")]
    private TMP_Text functionNameText;
    public GameObject cardPrefab; // Prefab for displaying cards in the slot

    private CanvasGroup slotCanvasGroup;
    private Slot originSlot;


    [Header("Function Name")]
    private string functionName = "";



    private void Start()
    {

        functionNameText = transform.Find("FunctionName")?.GetComponent<TMP_Text>();

        slotCanvasGroup = GetComponent<CanvasGroup>();
        originSlot = transform.parent.gameObject.GetComponent<Slot>();

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        originSlot.FieldWasClicked();
    }


    public string GetFunctionName()
    {
        return functionName;
    }

    public void SetFunctionName(string name)
    {
        functionName = name;
        if (functionNameText != null)
        {
            functionNameText.text = functionName;
        }
    }


}

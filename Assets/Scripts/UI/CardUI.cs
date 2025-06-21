using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image artImage;
    public TMP_Text cardText;
    private CardData cardData;

    private HandUIController originHand;

    private Vector3 originalScale;

    private Canvas canvas;
    private bool isSelected = false;

    private Color normalColor = Color.white;
    private Color selectedColor = Color.yellow;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        originalScale = transform.localScale;
    }

    public void Setup(CardData data, HandUIController handUIController)
    {
        cardData = data;
        cardText.text = data.cardText;
        artImage.sprite = data.cardSprite;
        originHand = handUIController;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * 1.2f;
        canvas.sortingOrder += 100;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            transform.localScale = originalScale;
        }
        canvas.sortingOrder -= 100;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        originHand.SelectCard(this);
        isSelected = !isSelected;
        UpdateVisualFeedback();
    }

    private void UpdateVisualFeedback()
    {
        GetComponent<Image>().color = isSelected ? selectedColor : normalColor;
        if (isSelected)
        {
            transform.localScale = originalScale * 1.2f;
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    public bool IsSelected() => isSelected;

    public void Deselect()
    {
        originHand.DeselectCard(this);
        isSelected = false;
        UpdateVisualFeedback();
    }

    public CardData GetCardData()
    {
        return cardData;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public enum CardState
{
    InUse,
    Selected,
    Unselected,
    Disabled
}

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image artImage;
    public TMP_Text cardText;
    private CardData cardData;

    private HandUIController originHand;

    private Vector3 originalScale;

    private Canvas canvas;
    private CardState state = CardState.Unselected;

    private Color normalColor = Color.white;
    private Color selectedColor = Color.yellow;
    private Color disabledColor = Color.darkGray;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        originalScale = transform.localScale;
    }

    public void Setup(HandUIController handUIController)
    {
        originHand = handUIController;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsSelected() || IsUnselected())
        {
            transform.localScale = originalScale * 1.2f;
        }
        canvas.sortingOrder += 100;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsUnselected() || IsDisabled())
        {
            transform.localScale = originalScale;
        }
        canvas.sortingOrder -= 100;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsSelected())
        {
            Deselect();
            return;
        }
        
        if (IsUnselected() && originHand.TrySelectCard(gameObject))
        {
            state = CardState.Selected;
            UpdateVisualFeedback();
            return;
        }
    }

    private void UpdateVisualFeedback()
    {
        GetComponent<Image>().color = IsSelected() ? selectedColor : IsDisabled() ? disabledColor : normalColor;
        if (IsSelected())
        {
            transform.localScale = originalScale * 1.2f;
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    public void OnParserUpdated(Parser parser)
    {
        if (state == CardState.Unselected && !parser.CanPush(GetComponent<Token>()))
        {
            state = CardState.Disabled;
            UpdateVisualFeedback();
        }

        if (state == CardState.Disabled && parser.CanPush(GetComponent<Token>()))
        {
            state = CardState.Unselected;
            UpdateVisualFeedback();
        }
    }

    public void ForceDeselect()
    {
        state = CardState.Unselected;
        UpdateVisualFeedback();
    }

    public void Deselect()
    {
        originHand.DeselectCard(gameObject);
        state = CardState.Unselected;
        UpdateVisualFeedback();
    }

    public void OnUsed()
    {
        state = CardState.InUse;
        UpdateVisualFeedback();
    }

    public CardData GetCardData()
    {
        return cardData;
    }
    
    public bool IsSelected() => state == CardState.Selected;
    public bool IsUnselected() => state == CardState.Unselected;
    public bool IsDisabled() => state == CardState.Disabled;
    public bool IsInUse() => state == CardState.InUse;
}

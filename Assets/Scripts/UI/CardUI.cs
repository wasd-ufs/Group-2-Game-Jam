using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image artImage;
    public TMP_Text cardText;

    private CardData cardData;
    private Vector3 originalScale;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Setup(CardData data)
    {
        cardData = data;
        cardText.text = data.cardText;
        artImage.sprite = data.cardSprite;
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Destaca a carta
        transform.localScale = originalScale * 1.2f;
        canvas.sortingOrder += 100; // traz pra frente
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Volta ao normal
        transform.localScale = originalScale;
        canvas.sortingOrder -= 100; // volta pra ordem padrão
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    public Image artImage;
    public TMP_Text cardText;
    private CardData cardData;

    public void Setup(CardData data)
    {
        cardData = data;
        cardText.text = data.cardText;
        artImage.sprite = data.cardSprite;
    }

    // Aqui você pode tratar hover ou clique
    public void OnClick()
    {
        Debug.Log("Clicou na carta: " + cardData.cardText);
    }
}

using UnityEngine;

public class Teste : MonoBehaviour
{
    public HandUIController hand;
    public Sprite[] spritesDeTeste;

    void Start()
    {
        for (int i = 0; i < spritesDeTeste.Length; i++)
        {
            CardData novaCarta = new CardData();
            novaCarta.cardText = "Carta " + (i + 1);
            novaCarta.cardSprite = spritesDeTeste[i];
            hand.AddCard(novaCarta);
        }
    }
}

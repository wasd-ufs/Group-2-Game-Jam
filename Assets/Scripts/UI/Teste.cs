using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    public HandUIController hand;
    public List<GameObject> handCards = new List<GameObject>();

    void Start()
    {
        foreach (GameObject card in handCards)
            hand.AddCard(card);
    }
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardRepository", menuName = "Scripts/GameState/CardRepository", order = 1)]
public class CardRepository : ScriptableObject
{
    public List<GameObject> valueCards = new();
    public List<GameObject> eventCards = new();
    
    public GameObject GetRandomValueCard() { return valueCards[Random.Range(0, valueCards.Count)]; }
    public GameObject GetRandomEventCard() { return eventCards[Random.Range(0, eventCards.Count)]; }

    public GameObject GetRandomCard()
    {
        var allCards = new List<GameObject>();
        allCards.AddRange(valueCards);
        allCards.AddRange(eventCards);
        
        return allCards[Random.Range(0, allCards.Count)];
    }
}
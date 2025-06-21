using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandUIController : MonoBehaviour
{


    [Header("Config")]
    public Transform handContainer; // Referência ao HandUI (pai das cartas)
    public GameObject cardPrefab;   // Prefab da carta

    private List<GameObject> handCards = new List<GameObject>();
    private List<GameObject> selectedCards = new List<GameObject>();

    // Adiciona uma nova carta à mão
    public void AddCard(CardData cardData)
    {
        GameObject newCard = Instantiate(cardPrefab, handContainer);
        CardUI cardUI = newCard.GetComponent<CardUI>();
        cardUI.Setup(cardData, this);
        handCards.Add(newCard);
    }

    // Remove uma carta da mão (por GameObject)
    public void RemoveCard(GameObject card)
    {
        if (handCards.Contains(card))
        {
            handCards.Remove(card);
        }
    }

    // Remove todas as cartas
    public void ClearHand()
    {

        handCards.Clear();
    }

    public void SelectCard(CardUI card)
    {
        selectedCards.Add(card.gameObject);
    }

    public void DeselectCard(CardUI card)
    {
        selectedCards.Remove(card.gameObject);
    }

    public List<GameObject> GetSelectedCards()
    {
        List<GameObject> selectedCardsRequested = new List<GameObject>();
        foreach (var card in selectedCards)
        {
            selectedCardsRequested.Add(card);
        }
        return selectedCardsRequested;
    }
}

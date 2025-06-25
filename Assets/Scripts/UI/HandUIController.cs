using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HandUIController : MonoBehaviour
{
    public CardRepository cardRepository;
    public int player = 0;
    
    [Header("Config")]
    public Transform handContainer; // Referência ao HandUI (pai das cartas)

    private List<GameObject> handCards = new List<GameObject>();
    private List<GameObject> selectedCards = new List<GameObject>();
    private Parser parser = new Parser();

    public void Awake()
    {
        if (player == 0)
        {
            GameManager.onProgrammingPhaseEntered.AddListener(() =>
            {
                var addCard = PlayerVariables.Player1CardCount >= handCards.Count;
                var count = Mathf.Abs(PlayerVariables.Player1CardCount - handCards.Count);
                for (int i = 0; i < count; i++)
                    if (addCard)
                        AddCard();
                    else
                        RemoveCard();
            });
        }
        else
        {
            GameManager.onProgrammingPhaseEntered.AddListener(() =>
            {
                var addCard = PlayerVariables.Player2CardCount >= handCards.Count;
                var count = Mathf.Abs(PlayerVariables.Player2CardCount - handCards.Count);
                for (int i = 0; i < count; i++)
                    if (addCard)
                        AddCard();
                    else
                        RemoveCard();
            });
        }
    }

    // Adiciona uma nova carta à mão
    public void AddCard(GameObject card = null)
    {
        if (player == 0)
        {
            if (handCards.Count + 1 > PlayerVariables.Player1CardCount)
                RemoveCard();
        }
        else
        {
            if (handCards.Count + 1 > PlayerVariables.Player2CardCount)
                RemoveCard();
        }
        
        
        var newCard = card ?? Instantiate(cardRepository.GetRandomCard());
        
        newCard.transform.SetParent(handContainer);
        CardUI cardUI = newCard.GetComponent<CardUI>();
        cardUI.Setup(this);
        cardUI.OnParserUpdated(parser);
        handCards.Add(newCard);
    }

    // Remove uma carta da mão (por GameObject)
    public void RemoveCard(GameObject toRemove = null)
    {
        if (handCards.Count == 0)
            return;
        
        var card = toRemove ?? handCards[Random.Range(0, handCards.Count)];
        
        if (handCards.Contains(card))
        {
            if (player == 0)
            {
                PlayerVariables.Player1CardCount--;
            }
            else
            {
                PlayerVariables.Player2CardCount--;
            }
            DeselectCard(card);
            handCards.Remove(card);
        }
    }

    // Remove todas as cartas
    public void ClearHand()
    {
        handCards.Clear();
        selectedCards.Clear();
        parser.Clear();
    }

    public bool TrySelectCard(GameObject card)
    {
        if (card.TryGetComponent<Token>(out var token) && parser.TryPush(token))
        {
            selectedCards.Add(card);
            handCards.ForEach(card => card.GetComponent<CardUI>().OnParserUpdated(parser));
            return true;
        }

        return false;
    }

    public void DeselectCard(GameObject card)
    {
        parser.Clear();
        selectedCards.Remove(card);
        var currentlySelectedCards = new List<GameObject>(selectedCards);

        var closed = false;
        foreach (var selected in currentlySelectedCards)
        {
            if (closed || !parser.TryPush(selected.GetComponent<Token>()))
            {
                closed = true;
                selected.GetComponent<CardUI>().ForceDeselect();
                selectedCards.Remove(selected);
            }
        }
        
        handCards.ForEach(card => card.GetComponent<CardUI>().OnParserUpdated(parser));
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

    public bool IsProgramExecutable() => parser.IsProgramExecutable();
    public AbstractSyntaxTreeNode GetProgram() => parser.GetProgram();
    public bool HasSelectedCards() => selectedCards.Count > 0;
}

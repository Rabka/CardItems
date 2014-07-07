using UnityEngine;
using System;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    private List<int> DeckList = new List<int>();
    private Stack<int> DeckStack = new Stack<int>();

    public void Start()
    {
    }

    /// <summary>
    /// Draws a card.
    /// </summary>
    /// <returns>
    /// The ID of the card ther got drawn.
    /// or 0000 if ther was no more cards.
    /// </returns>
    public int drawCard()
	{
        if(DeckStack.Count > 0)
            return DeckStack.Pop();
        return 0000;
	}

    /// <summary>
    /// Resets the deck, so all cards are in the stack.
    /// And shuffles it whit Shuffle().
    /// </summary>
	public void resetDeck()
	{
        DeckStack.Clear();
        foreach (int card in DeckList)
            DeckStack.Push(card);
        Shuffle();
	}

    /// <summary>
    /// Shuffles the deck stack.
    /// </summary>
    public void Shuffle()
    {
        int[] array = DeckStack.ToArray();

        var rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            int value = array[k];
            array[k] = array[n];
            array[n] = value;
        }

        DeckStack.Clear();
        for (int k = 0; k < array.Length; k++)
            DeckStack.Push(array[k]);
    }

    /// <summary>
    /// Adds a list of IDs of cards to the deck.
    /// </summary>
    /// <param name="_addList">The list of ints to add.</param>
    public void addCards(List<int> _addList)
	{
        DeckList.AddRange(_addList);
        resetDeck();
	}

    /// <summary>
    /// Removes a list of IDs of cards from the deck.
    /// </summary>
    /// <param name="_removeList">The list of ints to remove.</param>
    public void removeCards(List<int> _removeList)
	{
        foreach (int card in _removeList)
            DeckList.Remove(card);
        resetDeck();
	}
}


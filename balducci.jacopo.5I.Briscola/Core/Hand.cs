using System.Collections.Generic;

namespace balducci.jacopo._5I.Briscola.Core
{
    public class Hand
    {
        public Hand()
        {
            Cards = new List<Card>(3);
        }

        private List<Card> Cards { get; }

        public int Count => Cards.Count;

        public bool Contains(Card card)
        {
            return Cards.Contains(card);
        }

        public Card GetCard(int index)
        {
            return Cards[index];
        }

        public void ReplaceCard(Card oldCard, Card newCard)
        {
            Cards.Remove(oldCard);
            Cards.Add(newCard);
        }

        public void InitializeCards(Card[] cards)
        {
            int index = 0;
            foreach (Card card in cards)
            {
                card.Index = index++;
                Cards.Add(card);
            }
        }

        public void RemoveCard(Card card)
        {
            Cards.Remove(card);
        }

        public void AddCard(Card card)
        {
            card.Index = Cards.Count;
            Cards.Add(card);
        }
    }
}
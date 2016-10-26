using System;
using System.Collections.Generic;
using System.Linq;
using balducci.jacopo._5I.Briscola.Core.Enums;

namespace balducci.jacopo._5I.Briscola.Core
{
    internal class Deck : List<Card>
    {
        private static readonly Random Random = new Random();

        public Deck()
        {
            Dictionary<Faces, int> cardsPoints = new Dictionary<Faces, int>
            {
                {Faces.Asso, 11},
                {Faces.Cavallo, 3},
                {Faces.Cinque, 0},
                {Faces.Due, 0},
                {Faces.Fante, 2},
                {Faces.Quattro, 0},
                {Faces.Re, 4},
                {Faces.Sei, 0},
                {Faces.Sette, 0},
                {Faces.Tre, 10}
            };

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Add(new Card
                    {
                        Image =
                            @"\Sprites\" +
                            Enum.GetName(typeof(Faces), i) + "_" +
                            Enum.GetName(typeof(Seeds), j) + ".png",
                        Points = cardsPoints[(Faces) i],
                        Seed = (Seeds) j,
                        Face = (Faces) i
                    });
                }
            }
        }

        public Card[] DrawCards(int nCards, Player playerOwner)
        {
            Card[] returnCards = new Card[nCards];
            for (int i = 0; i < nCards; i++)
            {
                returnCards[i] = this[0];
                RemoveAt(0);
                returnCards[i].Owner = playerOwner;
            }
            return returnCards;
        }

        public Card GetBriscola()
        {
            return this.LastOrDefault();
        }

        public void Shuffle()
        {
            int n = Count;
            for (int i = 0; i < n; i++)
            {
                int r = i + (int) (Random.NextDouble()*(n - i));
                Card t = this[r];
                this[r] = this[i];
                this[i] = t;
            }
        }
    }
}
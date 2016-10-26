using balducci.jacopo._5I.Briscola.Core.Enums;

namespace balducci.jacopo._5I.Briscola.Core
{
    public class Card
    {
        public string Image { get; set; }
        public int Index { get; set; }

        public Player Owner { get; set; }

        public Seeds Seed { get; set; }
        public int Points { get; set; }
        public Faces Face { get; set; }
    }
}
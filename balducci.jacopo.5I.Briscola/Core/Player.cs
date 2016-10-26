namespace balducci.jacopo._5I.Briscola.Core
{
    public class Player
    {
        public Player()
        {
            Hand = new Hand();
        }

        public Hand Hand { get; }

        public int Points { get; set; }
    }
}
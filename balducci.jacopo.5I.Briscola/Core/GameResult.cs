using balducci.jacopo._5I.Briscola.Core.Enums;

namespace balducci.jacopo._5I.Briscola.Core
{
    public class GameResult
    {
        public string WinnerPlayer { get; set; }
        public GameResults Result { get; set; }

        public int PlayerPoints { get; set; }
        public int CpuPoints { get; set; }
    }
}
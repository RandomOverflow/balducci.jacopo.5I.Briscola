using System;
using System.Diagnostics;
using balducci.jacopo._5I.Briscola.Core.Enums;

namespace balducci.jacopo._5I.Briscola.Core
{
    internal class Briscola
    {
        public delegate void CpuPlayedHandler(Card card);

        public delegate void DeckFinishedHandler();

        public delegate void GameFinishredHandler(GameResult gameResult);

        public delegate void PlayerNewCardDrawnHandler(Card card, Card previousCard);

        public const string RetroImage = "\\Sprites\\retro.jpg";

        private bool _deckAlreadyFinished;

        private Player _playingPlayer;

        public Briscola(PlayerNewCardDrawnHandler playerNewCardDrawnHandler)
        {
            Deck = new Deck();
            Deck.Shuffle();
            WinningCard = Deck.GetBriscola();

            PlayerNewCardDrawn += playerNewCardDrawnHandler;
            Challenger = new Player();
            Cpu = new Cpu();

            Challenger.Hand.InitializeCards(Deck.DrawCards(3, Challenger));
            OnNewPlayerCardDrawn(Challenger.Hand.GetCard(0), null);
            OnNewPlayerCardDrawn(Challenger.Hand.GetCard(1), null);
            OnNewPlayerCardDrawn(Challenger.Hand.GetCard(2), null);

            Cpu.Hand.InitializeCards(Deck.DrawCards(3, Cpu));

            PlayingPlayer = Challenger;
        }

        private Player PlayingPlayer
        {
            get { return _playingPlayer; }
            set
            {
                _playingPlayer = value;
                if (PlayingPlayer == Cpu)
                {
                    Card card = Cpu.PlayCard(Deck, PreviousCard, WinningCard);
                    if (card == null)
                        return;
                    OnCpuPlayed(card);
                    PlayCard(ref card);
                }
            }
        }

        public int RemainingCards => Deck.Count;
        private Card PreviousCard { get; set; }
        private Deck Deck { get; }
        private Player Challenger { get; }
        private Cpu Cpu { get; }
        public Card WinningCard { get; }

        public bool IsGameFinished
            => RemainingCards == 0 && Challenger.Hand.Count == 0 && Cpu.Hand.Count == 0;

        public event CpuPlayedHandler CpuPlayed;

        public event PlayerNewCardDrawnHandler PlayerNewCardDrawn;

        public event GameFinishredHandler GameFinished;

        public event DeckFinishedHandler DeckFinished;

        private void OnNewPlayerCardDrawn(Card card, Card previousCard)
        {
            PlayerNewCardDrawn?.Invoke(card, previousCard);
        }

        private void OnCpuPlayed(Card card)
        {
            CpuPlayed?.Invoke(card);
        }

        public void PlayCard(ref Card card)
        {
            if (card == null) return;
            if (PlayingPlayer != card.Owner) return;
            if (PlayingPlayer != null && !PlayingPlayer.Hand.Contains(card)) return;
            if (PreviousCard == null)
            {
                //Si tratta della prima carta giocata

                PreviousCard = card;

                InvertTurn();
            }
            else
            {
                //Si tratta della seconda carta giocata

                Card fightCardResult = Fight(PreviousCard, card);
                fightCardResult.Owner.Points += PreviousCard.Points + card.Points;

                if (Deck.Count > 0)
                {
                    Card newCard1 = Deck.DrawCards(1, PlayingPlayer)[0];
                    Card newCard2 = Deck.DrawCards(1, PreviousCard.Owner)[0];

                    if (PlayingPlayer == Challenger)
                    {
                        OnNewPlayerCardDrawn(newCard1, card);
                        Challenger.Hand.ReplaceCard(card, newCard1);
                        Cpu.Hand.ReplaceCard(PreviousCard, newCard2);
                    }
                    else
                    {
                        OnNewPlayerCardDrawn(newCard2, PreviousCard);
                        Challenger.Hand.ReplaceCard(PreviousCard, newCard2);
                        Cpu.Hand.ReplaceCard(card, newCard1);
                    }
                }
                else
                {
                    if (PlayingPlayer == Challenger)
                    {
                        Challenger.Hand.RemoveCard(card);
                        Cpu.Hand.RemoveCard(PreviousCard);
                    }
                    else
                    {
                        Challenger.Hand.RemoveCard(PreviousCard);
                        Cpu.Hand.RemoveCard(card);
                    }
                }

                card = null;
                PreviousCard = null;

                if (IsGameFinished)
                {
                    OnGameFinished(CalculateGameResult());
                }
                else
                {
                    if (!_deckAlreadyFinished && Deck.Count == 0)
                    {
                        OnDeckFinished();
                        _deckAlreadyFinished = true;
                    }
                    ChangeTurn(fightCardResult.Owner); //Passo il turno a chi ha vinto lo scontro
                }
            }

            Debug.WriteLine("PUNTI CPU: " + Cpu.Points + Environment.NewLine + "PUNTI GIOCATORE: " + Challenger.Points);
        }

        private GameResult CalculateGameResult()
        {
            GameResult gameResult = new GameResult();
            if (Challenger.Points > Cpu.Points)
            {
                gameResult.Result = GameResults.Win;
            }
            else if (Challenger.Points < Cpu.Points)
            {
                gameResult.Result = GameResults.Lost;
            }
            else gameResult.Result = GameResults.Pareggio;
            gameResult.PlayerPoints = Challenger.Points;
            gameResult.CpuPoints = Cpu.Points;
            return gameResult;
        }

        private Card Fight(Card card1, Card card2) //Da controllare se funziona 100%
        {
            //card1 è la Carta giocata prima di card2.

            if (card2.Seed == WinningCard.Seed && card1.Seed != WinningCard.Seed) return card2;
            if (card1.Seed != card2.Seed) return card1;
            if (card1.Seed != WinningCard.Seed) return card1;
            //Entrambi seme comandante, vince la carta di valore maggiore

            return card1.Face >= card2.Face ? card1 : card2;
        }

        private void ChangeTurn(Player player)
        {
            PlayingPlayer = player;
        }

        private void InvertTurn()
        {
            PlayingPlayer = PlayingPlayer == Cpu ? Challenger : Cpu;
        }

        private void OnGameFinished(GameResult gameResult)
        {
            GameFinished?.Invoke(gameResult);
        }

        private void OnDeckFinished()
        {
            DeckFinished?.Invoke();
        }
    }
}
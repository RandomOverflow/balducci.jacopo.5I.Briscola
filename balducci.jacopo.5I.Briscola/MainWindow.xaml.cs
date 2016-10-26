using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using balducci.jacopo._5I.Briscola.Core;

namespace balducci.jacopo._5I.Briscola
{
    /// <summary>
    ///     Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly SoundPlayer _snd = new SoundPlayer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private Core.Briscola Briscola { get; set; }

        private void BriscolaOnDeckFinished()
        {
            ImageDeck.Source = null;
        }

        private void OnCpuPlayed(Card card)
        {
            if ((Card) ImageBriscola.Tag == card)
                ImageBriscola.Source = null;
            ImagePlayedPlayer2.Source = new BitmapImage(new Uri(card.Image, UriKind.Relative));
            ToolTipDeck.Content = "Remaining Cards: " + (Briscola.RemainingCards - 1);
            //DA FARE:Verificare l'index della carta e rimuovere l'immagine corrispondente
        }

        private static void GameFinished(GameResult gameResult)
        {
            GameResultWindow gameResultWindow = new GameResultWindow(gameResult);
            gameResultWindow.ShowDialog();
        }

        private void OnChallengerCardDrawn(Card card, Card previousCard)
        {
            Card card1Player1 = ImagePlayer1Card1.Tag as Card;
            Card card2Player1 = ImagePlayer1Card2.Tag as Card;
            Card card3Player1 = ImagePlayer1Card3.Tag as Card;

            if (previousCard == card1Player1)
            {
                ImagePlayer1Card1.Source = new BitmapImage(new Uri(card.Image, UriKind.Relative));
                ImagePlayer1Card1.Tag = card;
            }
            else if (previousCard == card2Player1)
            {
                ImagePlayer1Card2.Source = new BitmapImage(new Uri(card.Image, UriKind.Relative));
                ImagePlayer1Card2.Tag = card;
            }
            else if (previousCard == card3Player1)
            {
                ImagePlayer1Card3.Source = new BitmapImage(new Uri(card.Image, UriKind.Relative));
                ImagePlayer1Card3.Tag = card;
            }
            if (card == Briscola?.WinningCard) ImageBriscola.Source = null;

            if (Briscola != null) ToolTipDeck.Content = "Remaining Cards: " + (Briscola.RemainingCards - 1);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Briscola = new Core.Briscola(OnChallengerCardDrawn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            Briscola.CpuPlayed += OnCpuPlayed;
            Briscola.GameFinished += GameFinished;
            Briscola.DeckFinished += BriscolaOnDeckFinished;
            ImagePlayer2Card1.Source = new BitmapImage(new Uri(Core.Briscola.RetroImage, UriKind.Relative));

            ImagePlayer2Card2.Source = new BitmapImage(new Uri(Core.Briscola.RetroImage, UriKind.Relative));

            ImagePlayer2Card3.Source = new BitmapImage(new Uri(Core.Briscola.RetroImage, UriKind.Relative));

            ImageBriscola.Source = new BitmapImage(new Uri(Briscola.WinningCard.Image, UriKind.Relative));
            ImageBriscola.Tag = Briscola.WinningCard;

            ImageDeck.Source = new BitmapImage(new Uri(Core.Briscola.RetroImage, UriKind.Relative));
            _snd.SoundLocation = "Sounds\\click_one.wav";
        }

        private void ImagePlayer2Card1_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool) e.NewValue)
                BorderPlayer2Card1.BorderBrush = Brushes.Transparent;
            else
            {
                BorderPlayer2Card1.BorderBrush = Brushes.Black;

                _snd.Play();
            }
        }

        private void ImagePlayer2Card2_OnIsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool) e.NewValue)
                BorderPlayer2Card2.BorderBrush = Brushes.Transparent;
            else
            {
                BorderPlayer2Card2.BorderBrush = Brushes.Black;

                _snd.Play();
            }
        }

        private void ImagePlayer2Card3_OnIsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool) e.NewValue)
                BorderPlayer2Card3.BorderBrush = Brushes.Transparent;
            else
            {
                BorderPlayer2Card3.BorderBrush = Brushes.Black;

                _snd.Play();
            }
        }

        private void ImagePlayer1Card1_OnIsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool) e.NewValue)
                BorderPlayer1Card1.BorderBrush = Brushes.Transparent;
            else
            {
                BorderPlayer1Card1.BorderBrush = Brushes.Black;

                _snd.Play();
            }
        }

        private void ImagePlayer1Card2_OnIsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool) e.NewValue)
                BorderPlayer1Card2.BorderBrush = Brushes.Transparent;
            else
            {
                BorderPlayer1Card2.BorderBrush = Brushes.Black;

                _snd.Play();
            }
        }

        private void ImagePlayer1Card3_OnIsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool) e.NewValue)
                BorderPlayer1Card3.BorderBrush = Brushes.Transparent;
            else
            {
                BorderPlayer1Card3.BorderBrush = Brushes.Black;

                _snd.Play();
            }
        }

        private void ImagePlayerCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Card playedCard = (Card) ((Image) sender).Tag;
            if (playedCard?.Image != null)
            {
                ImagePlayedPlayer1.Source = new BitmapImage(new Uri(playedCard.Image, UriKind.Relative));
            }
            ((Image) sender).Source = null;
            Briscola.PlayCard(ref playedCard);
        }
    }
}
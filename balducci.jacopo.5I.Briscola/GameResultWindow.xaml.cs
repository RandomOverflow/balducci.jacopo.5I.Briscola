using System.Windows;
using balducci.jacopo._5I.Briscola.Core;

namespace balducci.jacopo._5I.Briscola
{
    /// <summary>
    ///     Logica di interazione per GameResultWindow.xaml
    /// </summary>
    public partial class GameResultWindow
    {
        public GameResultWindow(GameResult gameResult)
        {
            InitializeComponent();
            LabelWinner.Content += gameResult.Result.ToString();
            LabelPlayerPoints.Content += gameResult.PlayerPoints.ToString();
            LabelCpuPoints.Content += gameResult.CpuPoints.ToString();
        }

        private void GameResultWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
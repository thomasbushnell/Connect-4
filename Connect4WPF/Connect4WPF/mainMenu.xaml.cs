using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Connect4WPF
{
    /// <summary>
    /// Interaction logic for mainMenu.xaml
    /// </summary>
    public partial class mainMenu : Window
    {
        mainGame mainGame;
        string player1Name = "Yellow", player2Name = "Red";

        public mainMenu()
        {
            InitializeComponent();
        }

        private void Btn_Play_Click(object sender, RoutedEventArgs e)
        {
            if(mainGame == null)
            {
                mainGame = new mainGame(this);
            }
            mainGame.Top = this.Top;
            mainGame.Left = this.Left;
            mainGame.Show();
            this.Hide();
        }

        private void Btn_PlayerSettings_Click(object sender, RoutedEventArgs e)
        {
            playerSettingsGrid.Visibility = Visibility.Visible;
            playerSettingsGrid.Focus();
            GreyOverlay.Visibility = Visibility.Visible;
        }

        private void Btn_GameSettings_Click(object sender, RoutedEventArgs e)
        {
            GreyOverlay.Visibility = Visibility.Visible;
            
        }

        private void Tbx_player2Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            player2Name = tbx_player2Name.Text;
        }

        private void Tbx_player1Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            player1Name = tbx_player1Name.Text;
        }
    }
}

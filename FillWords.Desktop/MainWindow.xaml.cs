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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FillWords.Logic;

namespace FillWords.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!Files.SaveCheck())
                BTNContinueGame.Background = Brushes.Red;

        }
        private void StartNewGame(object sender, RoutedEventArgs e)
        {
            GridMainMenu.Visibility = Visibility.Hidden;
            GridGetPlayerName.Visibility = Visibility.Visible;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void EnterName(object sender, RoutedEventArgs e)
        {
            GridMainMenu.Visibility = Visibility.Visible;
            GridGetPlayerName.Visibility = Visibility.Hidden;
            this.Visibility = Visibility.Hidden;
            Level level = new Level();
            level.CreateLevel(1);
            var gameWindow = new PlayWindow(new KeybordMoveReader(), TBName.Text, level, this);
            gameWindow.Show();
        }

        private void ContinueGame(object sender, RoutedEventArgs e)
        {
            if (Files.SaveCheck())
            {
                this.Visibility = Visibility.Hidden;
                Level level = Files.LoadGame();
                var gameWindow = new PlayWindow(new KeybordMoveReader(), TBName.Text, level, this);
                gameWindow.Show();
            }         
        }
        public void UnlockLoad()
        {
            BTNContinueGame.Background = Brushes.LightGray;
        }
    }
}

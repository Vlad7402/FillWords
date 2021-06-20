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
            this.Visibility = Visibility.Hidden;
            var gameWindow = new PlayWindow(new KeybordMoveReader(), TBName.Text, this);
            gameWindow.Show();
        }
    }
}

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
    public partial class MainWindow : Window, IWriter
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ColourFoundedWords(int gorisontPass, int vertPass, int hight, int whight, int fildsize, char[,] fild)
        {
            throw new NotImplementedException();
        }

        public string GetPlayerName()
        {
            throw new NotImplementedException();
        }

        public void PrintErrorMassage(Errors error)
        {
            throw new NotImplementedException();
        }

        public void PrintGameTableBody(int hight, int whight, int gorisontNum, int vertNum, int gorisontPass, int vertPass)
        {
            throw new NotImplementedException();
        }

        public void PrintMenu()
        {
            throw new NotImplementedException();
        }

        public void ReColour(int gorID, int vertID, int gorisontPass, int vertPass, int hight, int whight, Logic.Colors color)
        {
            throw new NotImplementedException();
        }

        public void SetLetters(char[,] Letters, int hight, int whightint, int gorisontPass, int vertPass, int fildSize)
        {
            throw new NotImplementedException();
        }

        private void StartNewGame(object sender, RoutedEventArgs e)
        {
            var logic = new Logic.GameLogic();
        }
    }
}

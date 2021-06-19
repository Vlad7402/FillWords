using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FillWords.Logic;

namespace FillWords.Desktop
{
    /// <summary>
    /// Логика взаимодействия для PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow : Window, IWriter
    {
        private readonly IMoves movereader;
        public PlayWindow(IMoves movereader)
        {
            InitializeComponent();
            this.movereader = movereader;
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

        private void KeyPresed(object sender, KeyEventArgs e)
        {
        }
    }
}

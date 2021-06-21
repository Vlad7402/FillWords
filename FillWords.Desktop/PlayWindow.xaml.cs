using System;
using System.Threading;
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
        private readonly KeybordMoveReader moveReaderKey;
        private readonly MouseMoveReader moveReaderMouse;
        private readonly MainWindow mainWindow;
        private Level level;
        private List<Button> buttons = new List<Button>();
        private int fildSize;
        private List<Coordinates> selectedCells = new List<Coordinates>();
        private Coordinates currentCell;
        public PlayWindow(IMoves moveReader, string playerName, Level level, MainWindow mainWindow)
        {
            InitializeComponent();
            if (moveReader.Type== ReaderType.Keyboard)
            {
                moveReaderKey = (KeybordMoveReader)moveReader;
                moveReaderMouse = null;
            }
            else
            {
                //Должно было быть ещё считывание с мыши... Но не успел(
                moveReaderMouse = (MouseMoveReader)moveReader;
                moveReaderKey = null;
            }
            this.mainWindow = mainWindow;
            this.level = level;
            level.player = playerName;
            mainWindow.Visibility = Visibility.Hidden;
            fildSize = Level.GetFildSize(level.level);
            currentCell = GamePlay.SetCursorOnEmptyCell(level.GetLevelFild(), fildSize);
            Loaded += OnLoad;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            GamePlay.RedrowField(this, currentCell, fildSize, level.GetLevelFild());
            CanPlayBoard.Focus();
        }

        public void ColourFoundedWords(int gorisontPass, int vertPass, int hight, int whight, int fildsize, char[,] fild)
        {
            for (int i = 0; i < fildsize; i++)
            {
                for (int j = 0; j < fildsize; j++)
                {
                    if (fild[i, j] == '0') ReColour(j, i, gorisontPass, vertPass, hight, whight, Logic.Colors.Gray);
                }
            }
            CanPlayBoard.Focus();
        }
        public string GetPlayerName()
        {
            throw new Exception("Вроде не нужен");
        }
        public void PrintErrorMassage(Errors error)
        {
            TBNotification.Text = GetErrorMassege(error);
            CanPlayBoard.Focus();
        }
        private static string GetErrorMassege(Errors error)
        {
            string[] errorMasseges = new string[5];
            errorMasseges[0] = "Отсутствует словарь";
            errorMasseges[1] = "Отсутствует сохранение";
            errorMasseges[2] = "Слово есть в словаре, но не загадано на уровне";
            errorMasseges[3] = "Слово отсутствует в словаре";
            errorMasseges[4] = "Фича в разработке";
            return errorMasseges[(int)error];
        }
        private static Brush GetColor(Logic.Colors color)
        {
            var colors = new Brush[4];
            colors[0] = Brushes.Yellow;
            colors[1] = Brushes.Red;
            colors[2] = Brushes.Black;
            colors[3] = Brushes.Gray;
            return colors[(int)color];
        }
        public void PrintGameTableBody(int hight, int whight, int gorisontNum, int vertNum, int gorisontPass, int vertPass)
        {
            buttons.Clear();
            fildSize = vertNum;
            for (int i = 1; i <= gorisontNum; i++)
            {
                for (int j = 1; j <= vertNum; j++)
                    buttons.Add(GetGameCell(i,j, vertNum, gorisontNum, CanPlayBoard));
            }
        }
        public void PrintMenu()
        {
            Files.SaveGame(level);
            if (level.player != "Guest")
                mainWindow.UnlockLoad();

            mainWindow.Visibility = Visibility.Visible;
            this.Close();
        }
        public void ReColour(int gorID, int vertID, int gorisontPass, int vertPass, int hight, int whight, Logic.Colors color)
        {
            var s = gorID + vertID * fildSize;
            buttons[(vertID) + (gorID) * fildSize].Background = GetColor(color);
        }
        public void SetLetters(char[,] Letters, int hight, int whightint, int gorisontPass, int vertPass, int fildSize)
        {
            CanPlayBoard.Children.Clear();
            for (int i = 0; i < fildSize; i++)
            {
                for (int j = 0; j < fildSize; j++)
                {
                    if (Letters[i,j] != '0')
                    {
                        Button button = buttons[i + j * fildSize];
                        button.Content = Letters[i, j];
                        CanPlayBoard.Children.Add(button);
                    }
                }
            }
        }
        private void KeyPresed(object sender, KeyEventArgs e)
        {
            //Инкапсуляция здесь нас покинула
            moveReaderKey.key = e.Key;
            moveReaderKey.SelectedCells = selectedCells;
            GamePlay.ExecuteKeyDown(moveReaderKey,ref currentCell,ref selectedCells, this, fildSize,ref level);
            CanPlayBoard.Focus();
        }
        private void DelitError(object sender, RoutedEventArgs e)
        {
            TBNotification.Text = string.Empty;
            CanPlayBoard.Focus();
        }
        private void SaveAndExit(object sender, RoutedEventArgs e)
        {
            PrintMenu();
        }
        private static Button GetGameCell(int Xid, int Yid, int vertNum, int gorisontNum, Canvas CanPlayBoard)
        {
            var button = new Button();
            button.Height = CanPlayBoard.ActualHeight / vertNum;
            button.Width = CanPlayBoard.ActualWidth / gorisontNum;
            Thickness thickness = new Thickness(button.Width * (Xid - 1), button.Height * (Yid - 1),
                                                CanPlayBoard.ActualWidth - button.Width * (Xid - 1),
                                                CanPlayBoard.ActualHeight - button.Height * (Yid - 1));

            button.Margin = thickness;
            button.Name =  new string("BTNI" + Xid + "I" + Yid);
            return button;
        }
    }
}

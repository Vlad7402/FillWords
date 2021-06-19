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
    class KeybordMoveReader : IMoves
    {
        public Key Key { private get; set; }
        public Move GetMoove(int positionX, int positionY, char[,] fild, out Asic asic)
        {
            Move move;
            switch (Key)
            {
                case Key.W:
                case Key.Up:
                    move = Move.Up;
                    asic = Asic.X;
                    break;
                case Key.D:
                case Key.Right:
                    move = Move.Up;
                    asic = Asic.Y;
                    break;
                case Key.A:
                case Key.Left:
                    move = Move.Down;
                    asic = Asic.Y;
                    break;
                case Key.S:
                case Key.Down:
                    move = Move.Down;
                    asic = Asic.X;
                    break;
                case Key.Enter:
                    move = Move.Up;
                    asic = Asic.Aditional;
                    break;
                case Key.Escape:
                    move = Move.Down;
                    asic = Asic.Aditional;
                    break;
                default:
                    move = Move.Uncorrect;
                    asic = Asic.Uncorrect;
                    break;
            }
            if (!IsMoveAvailable(positionX, positionY, move, asic, fild))
                asic = Asic.Uncorrect;

            return move;
        }
        private static bool IsMoveAvailable(int positionX, int positionY, Move move, Asic asic, char[,] fild)
        {
            if (asic == Asic.Aditional || asic == Asic.Uncorrect) return true;

            if (asic == Asic.X)
            {
                if (positionX + (int)move < Math.Sqrt(fild.Length) && positionX + (int)move >= 0 && fild[positionY, positionX + (int)move] != '0') return true;
            }
            else if (asic == Asic.Y)
            {
                if (positionY + (int)move < Math.Sqrt(fild.Length) && positionY + (int)move >= 0 && fild[positionY + (int)move, positionX] != '0') return true;
            }

            return false;
        }
    }
}

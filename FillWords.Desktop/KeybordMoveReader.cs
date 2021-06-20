﻿using System;
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
    public class KeybordMoveReader : IMoves
    {
        public Key key { private get; set; }
        public void GetMoove()
        {
            Move move;
            switch (key)
            {
                case Key.W:
                case Key.Up:
                    move = Move.Up;
                    MoveInfo.asic = Asic.X;
                    break;
                case Key.D:
                case Key.Right:
                    move = Move.Up;
                    MoveInfo.asic = Asic.Y;
                    break;
                case Key.A:
                case Key.Left:
                    move = Move.Down;
                    MoveInfo.asic = Asic.Y;
                    break;
                case Key.S:
                case Key.Down:
                    move = Move.Down;
                    MoveInfo.asic = Asic.X;
                    break;
                case Key.Enter:
                    move = Move.Up;
                    MoveInfo.asic = Asic.Aditional;
                    break;
                case Key.Escape:
                    move = Move.Down;
                    MoveInfo.asic = Asic.Aditional;
                    break;
                default:
                    move = Move.Uncorrect;
                    MoveInfo.asic = Asic.Uncorrect;
                    break;
            }
            if (!IsMoveAvailable(MoveInfo.positionX, MoveInfo.positionY, move, MoveInfo.asic, MoveInfo.fild))
                MoveInfo.asic = Asic.Uncorrect;

            System.Threading.Thread.Sleep(250);
            MoveInfo.move = move;
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

        public ReaderType Type => ReaderType.Keyboard;
    }
}

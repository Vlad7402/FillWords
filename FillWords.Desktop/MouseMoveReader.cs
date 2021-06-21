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
    class MouseMoveReader : IMoves
    {
        public ReaderType Type => ReaderType.Mouse;

        public Move GetMoove(int positionX, int positionY, char[,] fild, out Asic asic)
        {
            throw new NotImplementedException();
        }
    }
}

namespace FillWords.Logic
{
    using System;
    using FillWords.Console;

    class Program
    {
        static void Main()
        {
            var buttons = new Button[4];
            buttons[0] = new Button("Новая игра", 65, 12);
            buttons[1] = new Button("Продолжить", 63, 14);
            buttons[2] = new Button("Рекорды", 61, 16);
            buttons[3] = new Button("Выход", 59, 18);
            while (true)
            {
                Writer.PrintMenu();
                var menu = new ButtonMenu(buttons, ConsoleColor.DarkYellow, ConsoleColor.Green, ConsoleColor.Black, ConsoleColor.White);
                int choosedPosition = menu.ChoosedButton;
                if (choosedPosition == 0) GameLogic.StartNewGame();
                if (choosedPosition == 1) GameLogic.LoadGame();
                if (choosedPosition == 2) Writer.PrintErrorMassage(Errors.InProcess);
                if (choosedPosition == 3) break;
            }
        }
    }
}

namespace FillWords.Console
{
    using System;
    using FillWords.Logic;
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
                var writer = new Writer();
                writer.PrintMenu();
                var gameLogic = new GameLogic(writer, new MoveReader());
                var menu = new ButtonMenu(buttons, ConsoleColor.DarkYellow, ConsoleColor.Green, ConsoleColor.Black, ConsoleColor.White);
                int choosedPosition = menu.ChoosedButton;
                if (choosedPosition == 0) gameLogic.StartNewGame();
                if (choosedPosition == 1) gameLogic.LoadGame();
                if (choosedPosition == 2) writer.PrintErrorMassage(Errors.InProcess);
                if (choosedPosition == 3) break;
            }
        }
    }
}

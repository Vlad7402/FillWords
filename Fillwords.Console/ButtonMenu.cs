namespace FillWords.Console
{
    using System;
    public class Button
    {
        public readonly string ButtonName;
        public readonly int PositionX;
        public readonly int PositionY;
        public Button(string buttonName, int positionX, int positionY)
        {
            ButtonName = buttonName;
            PositionX = positionX;
            PositionY = positionY;
        }
    }
    public class ButtonMenu
    {
        private readonly Button[] buttons;
        private readonly ConsoleColor backgroundColor;
        private readonly ConsoleColor foregroundColor;
        private readonly ConsoleColor selectedBackgroundColor;
        private readonly ConsoleColor selectedForegroundColor;
        private int SelectedButtonID = 0;
        public int ChoosedButton
        {
            get
            {
                Console.ResetColor();
                return GetSelectedButton();
            }
        }
        public ButtonMenu(Button[] buttons, ConsoleColor selectedBackgroundColor, ConsoleColor selectedForegroundColor, ConsoleColor unselectedBackgroundColor, ConsoleColor unselectedForegroundColor)
        {
            this.buttons = buttons;
            this.selectedBackgroundColor = selectedBackgroundColor;
            this.selectedForegroundColor = selectedForegroundColor;
            backgroundColor = unselectedBackgroundColor;
            foregroundColor = unselectedForegroundColor;
        }
        private int GetSelectedButton()
        {
            Console.CursorVisible = false;
            PrintAllButtons();
            PrintSelectedButton();
            while (true)
            {
                Asic asic;
                var move = MoveReader.GetMoove(SelectedButtonID, buttons.Length, out asic);
                if (asic == Asic.Y)
                {
                    Console.Beep(1000, 50);
                    PrintButton(SelectedButtonID);
                    SelectedButtonID += (int)move;
                    PrintSelectedButton();
                }
                if (asic == Asic.Aditional) break;
            }
            Console.Beep(1500, 100);
            return SelectedButtonID;
        }
        private void PrintButton(int buttonID)
        {
            Console.SetCursorPosition(buttons[buttonID].PositionX, buttons[buttonID].PositionY);
            Console.Write(buttons[buttonID].ButtonName);
        }
        private void PrintSelectedButton()
        {
            Console.BackgroundColor = selectedBackgroundColor;
            Console.ForegroundColor = selectedForegroundColor;
            PrintButton(SelectedButtonID);
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
        }
        private void PrintAllButtons()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                PrintButton(i);
            }
        }
    }
}

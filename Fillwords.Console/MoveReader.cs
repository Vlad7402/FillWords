namespace FillWords.Console
{
    using System;
    using FillWords.Logic;
    public class MoveReader: IMoves
    {
        private static void WaitForKey()
        {
            while (!Console.KeyAvailable)
            {
                System.Threading.Thread.Sleep(25);
            }
        }
        public Move GetMoove(int positionX, int positionY, char[,] fild, out Asic asic)
        {
            Move move = Move.Uncorrect;
            do
            {
                asic = Asic.Uncorrect;
                WaitForKey();
                ConsoleKeyInfo buttonPresed = Console.ReadKey(true);
                if (buttonPresed.Key == ConsoleKey.RightArrow) { move = Move.Up; asic = Asic.X; }

                if (buttonPresed.Key == ConsoleKey.LeftArrow) { move = Move.Down; asic = Asic.X; }

                if (buttonPresed.Key == ConsoleKey.UpArrow) { move = Move.Down; asic = Asic.Y; }

                if (buttonPresed.Key == ConsoleKey.DownArrow) { move = Move.Up; asic = Asic.Y; }

                if (buttonPresed.Key == ConsoleKey.Enter) { move = Move.Up; asic = Asic.Aditional; }

                if (buttonPresed.Key == ConsoleKey.Escape) { move = Move.Down; asic = Asic.Aditional; }

            } while (!IsMoveAvailable(positionX, positionY, move, asic, fild));
            return move;
        }
        private static bool IsMoveAvailable(int positionX, int positionY, Move move, Asic asic, char[,] fild)
        {
            if (asic == Asic.Aditional) return true;

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
        public static Move GetMoove(int positionX, int positionY, int hight, int whight, out Asic asic)
        {
            Move move = Move.Uncorrect;
            do
            {
                asic = Asic.Uncorrect;
                WaitForKey();
                ConsoleKeyInfo buttonPresed = Console.ReadKey(true);
                if (buttonPresed.Key == ConsoleKey.RightArrow) { move = Move.Down; asic = Asic.X; }

                if (buttonPresed.Key == ConsoleKey.LeftArrow) { move = Move.Up; asic = Asic.X; }

                if (buttonPresed.Key == ConsoleKey.UpArrow) { move = Move.Down; asic = Asic.Y; }

                if (buttonPresed.Key == ConsoleKey.DownArrow) { move = Move.Up; asic = Asic.Y; }

                if (buttonPresed.Key == ConsoleKey.Enter) { move = Move.Up; asic = Asic.Aditional; }

                if (buttonPresed.Key == ConsoleKey.Escape) { move = Move.Down; asic = Asic.Aditional; }

            } while (!IsMoveAvailable(positionX, positionY, hight, whight, move, asic));
            return move;
        }
        private static bool IsMoveAvailable(int positionX, int positionY, int hight, int whight, Move move, Asic asic)
        {
            if (asic == Asic.Aditional) return true;

            if (asic == Asic.X)
            {
                if (positionX + (int)move < whight && positionX + (int)move >= 0) return true;
            }
            else if (asic == Asic.Y)
            {
                if (positionY + (int)move < hight && positionY + (int)move >= 0) return true;
            }

            return false;
        }
        public static Move GetMoove(int position, int lenght, out Asic asic)
        {
            Move move = Move.Uncorrect;
            do
            {
                asic = Asic.Uncorrect;
                WaitForKey();
                ConsoleKeyInfo buttonPresed = Console.ReadKey(true);
                if (buttonPresed.Key == ConsoleKey.UpArrow) { move = Move.Down; asic = Asic.Y; }

                if (buttonPresed.Key == ConsoleKey.DownArrow) { move = Move.Up; asic = Asic.Y; }

                if (buttonPresed.Key == ConsoleKey.Enter) { move = Move.Up; asic = Asic.Aditional; }

                if (buttonPresed.Key == ConsoleKey.Escape) { move = Move.Down; asic = Asic.Aditional; }

            } while (!IsMoveAvailable(0, position, lenght, 0, move, asic));
            return move;
        }

        public ReaderType Type => throw new NotImplementedException();
    }
}

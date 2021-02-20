namespace FillWords.Logic
{
    using System.Collections.Generic;
    using FillWords.Console;

    static class GamePlay
    {
        public static int[,] ReturnWordCoordnates(Level level)
        {
            int fildSize = 3 + level.GetLevelNum() / 10;
            int hight = 3;
            int whight = 3;
            int gorisontNum = fildSize;
            int vertNum = fildSize;
            int gorisontPass = 1;
            int vertPass = 1;
            int positionX = 1;
            int positionY = 1;
            char[,] fild = level.GetLevelFild();
            SetCursorOnEmptyCell(fild, ref positionX, ref positionY, fildSize);
            Writer.PrintGameTableBody(hight, whight, gorisontNum, vertNum, gorisontPass, vertPass);
            Writer.ColourFoundedWords(gorisontPass, vertPass, hight, whight, fildSize, fild);
            Writer.ReColour((positionX), (positionY), gorisontPass, vertPass, hight, whight, Colors.Red);
            Writer.SetLetters(fild, hight, whight, gorisontPass, vertPass, fildSize);
            return GetSelectedCells(positionX, positionY, vertNum, gorisontNum, hight, whight, gorisontPass, vertPass, fild, fildSize);
        }
        public static void SetCursorOnEmptyCell(char[,] fild, ref int posX, ref int posY, int fildSize)
        {
            for (int i = 0; i < fildSize; i++)
            {
                for (int j = 0; j < fildSize; j++)
                {
                    if (fild[i, j] != '0')
                    {
                        posX = j + 1;
                        posY = i + 1;
                        break;
                    }
                }
            }
        }
        private static int[,] GetSelectedCells(int positionX, int positionY, int vertNum, int gorisontNum, int hight, int whight, int gorisontPass, int vertPass, char[,] fild, int fildsize)
        {
            while (true)
            {
                Asic asic;
                var move = MoveReader.GetMoove(positionX - 1, positionY - 1, fild, out asic);
                if (asic == Asic.X || asic == Asic.Y)
                {
                    Writer.ReColour(positionX, positionY, gorisontPass, vertPass, hight, whight, Colors.Black);
                    if (asic == Asic.X) positionX += (int)move;
                    else positionY += (int)move;

                    Writer.ColourFoundedWords(gorisontPass, vertPass, hight, whight, fildsize, fild);
                    Writer.ReColour(positionX, positionY, gorisontPass, vertPass, hight, whight, Colors.Red);
                    Writer.SetLetters(fild, hight, whight, gorisontPass, vertPass, fildsize);
                }
                if (asic == Asic.Aditional && move == Move.Up)
                {
                    List<int> positionsX = new List<int>();
                    List<int> positionsY = new List<int>();
                    positionsX.Add(positionX - 1);
                    positionsY.Add(positionY - 1);
                    Writer.ReColour(positionX, positionY, gorisontPass, vertPass, hight, whight, Colors.Yellow);
                    do
                    {
                        move = MoveReader.GetMoove(positionX - 1, positionY - 1, fild, out asic);
                        if (asic == Asic.X || asic == Asic.Y)
                        {
                            if (asic == Asic.X) positionX += (int)move;
                            else positionY += (int)move;

                            positionsX.Add(positionX - 1);
                            positionsY.Add(positionY - 1);
                            Writer.ReColour(positionX, positionY, gorisontPass, vertPass, hight, whight, Colors.Yellow);
                            Writer.SetLetters(fild, hight, whight, gorisontPass, vertPass, fildsize);
                        }
                    } while (!(asic == Asic.Aditional));
                    int[,] result = new int[positionsX.Count, 2];
                    for (int i = 0; i < positionsX.Count; i++)
                    {
                        result[i, 0] = positionsX[i];
                        result[i, 1] = positionsY[i];
                    }
                    return result;
                }
                if (asic == Asic.Aditional && move == Move.Down)
                {
                    return null;
                }
            }
        }
    }
}

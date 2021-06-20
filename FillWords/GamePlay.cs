namespace FillWords.Logic
{
    using System.Collections.Generic;
    using System.Threading;

    public class GamePlay
    {
        private readonly IWriter writer;
        private readonly IMoves moveReader;
        public GamePlay(IWriter writer, IMoves moveReader)
        {
            this.writer = writer;
            this.moveReader = moveReader;
        }
        public int[,] ReturnWordCoordnates(Level level)
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
            writer.PrintGameTableBody(hight, whight, gorisontNum, vertNum, gorisontPass, vertPass);
            writer.ColourFoundedWords(gorisontPass, vertPass, hight, whight, fildSize, fild);
            writer.ReColour((positionX), (positionY), gorisontPass, vertPass, hight, whight, Colors.Red);
            writer.SetLetters(fild, hight, whight, gorisontPass, vertPass, fildSize);
            return GetSelectedCells(positionX, positionY, vertNum, gorisontNum, hight, whight, gorisontPass, vertPass, fild, fildSize);
        }
        public void SetCursorOnEmptyCell(char[,] fild, ref int posX, ref int posY, int fildSize)
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
        private int[,] GetSelectedCells(int positionX, int positionY, int vertNum, int gorisontNum, int hight, int whight, int gorisontPass, int vertPass, char[,] fild, int fildsize)
        {
            while (true)
            {
                MoveInfo.positionX = positionX - 1;
                MoveInfo.positionX = positionY - 1;
                MoveInfo.fild = fild;
                Thread getMoove = new Thread(new ThreadStart(moveReader.GetMoove));
                getMoove.Priority = ThreadPriority.Lowest;
                getMoove.Start();
                //var move = moveReader.GetMoove(positionX - 1, positionY - 1, fild, out asic);
                if (MoveInfo.asic == Asic.X || MoveInfo.asic == Asic.Y)
                {
                    writer.ReColour(positionX, positionY, gorisontPass, vertPass, hight, whight, Colors.Black);
                    if (MoveInfo.asic == Asic.X) positionX += (int)MoveInfo.move;
                    else positionY += (int)MoveInfo.move;

                    writer.ColourFoundedWords(gorisontPass, vertPass, hight, whight, fildsize, fild);
                    writer.ReColour(positionX, positionY, gorisontPass, vertPass, hight, whight, Colors.Red);
                    writer.SetLetters(fild, hight, whight, gorisontPass, vertPass, fildsize);
                }
                if (MoveInfo.asic == Asic.Aditional && MoveInfo.move == Move.Up)
                {
                    List<int> positionsX = new List<int>();
                    List<int> positionsY = new List<int>();
                    positionsX.Add(positionX - 1);
                    positionsY.Add(positionY - 1);
                    writer.ReColour(positionX, positionY, gorisontPass, vertPass, hight, whight, Colors.Yellow);
                    do
                    {
                        MoveInfo.positionX = positionX - 1;
                        MoveInfo.positionX = positionY - 1;
                        MoveInfo.fild = fild;
                        getMoove.Start();
                        if (MoveInfo.asic == Asic.X || MoveInfo.asic == Asic.Y)
                        {
                            if (MoveInfo.asic == Asic.X) positionX += (int)MoveInfo.move;
                            else positionY += (int)MoveInfo.move;

                            positionsX.Add(positionX - 1);
                            positionsY.Add(positionY - 1);
                            writer.ReColour(positionX, positionY, gorisontPass, vertPass, hight, whight, Colors.Yellow);
                            writer.SetLetters(fild, hight, whight, gorisontPass, vertPass, fildsize);
                        }
                    } while (!(MoveInfo.asic == Asic.Aditional));
                    int[,] result = new int[positionsX.Count, 2];
                    for (int i = 0; i < positionsX.Count; i++)
                    {
                        result[i, 0] = positionsX[i];
                        result[i, 1] = positionsY[i];
                    }
                    return result;
                }
                if (MoveInfo.asic == Asic.Aditional && MoveInfo.move == Move.Down)
                {
                    return null;
                }
            }
        }
    }
    public static class MoveInfo
    {
        public static int positionX;
        public static int positionY;
        public static char[,] fild;
        public static Asic asic { get; set; }
        public static Move move { get; set; }
    }
}

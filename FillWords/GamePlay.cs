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
        public static Coordinates SetCursorOnEmptyCell(char[,] fild, int fildSize)
        {
            for (int i = 0; i < fildSize; i++)
            {
                for (int j = 0; j < fildSize; j++)
                {
                    if (fild[i, j] != '0')
                        return new Coordinates(j, i);
                }
            }
            return null;
        }
        private int[,] GetSelectedCells(int positionX, int positionY, int vertNum, int gorisontNum, int hight, int whight, int gorisontPass, int vertPass, char[,] fild, int fildsize)
        {
            while (true)
            {
                Asic asic;
                var move = moveReader.GetMoove(positionX - 1, positionY - 1, fild, out asic);
                if (asic == Asic.X || asic == Asic.Y)
                {
                    writer.ReColour(positionX, positionY, gorisontPass, vertPass, hight, whight, Colors.Black);
                    if (asic == Asic.X) positionX += (int)move;
                    else positionY += (int)move;

                    writer.ColourFoundedWords(gorisontPass, vertPass, hight, whight, fildsize, fild);
                    writer.ReColour(positionX, positionY, gorisontPass, vertPass, hight, whight, Colors.Red);
                    writer.SetLetters(fild, hight, whight, gorisontPass, vertPass, fildsize);
                }
                if (asic == Asic.Aditional && move == Move.Up)
                {
                    List<int> positionsX = new List<int>();
                    List<int> positionsY = new List<int>();
                    positionsX.Add(positionX - 1);
                    positionsY.Add(positionY - 1);
                    writer.ReColour(positionX, positionY, gorisontPass, vertPass, hight, whight, Colors.Yellow);
                    do
                    {
                        move = moveReader.GetMoove(positionX - 1, positionY - 1, fild, out asic);
                        if (asic == Asic.X || asic == Asic.Y)
                        {
                            if (asic == Asic.X) positionX += (int)move;
                            else positionY += (int)move;

                            positionsX.Add(positionX - 1);
                            positionsY.Add(positionY - 1);
                            writer.ReColour(positionX, positionY, gorisontPass, vertPass, hight, whight, Colors.Yellow);
                            writer.SetLetters(fild, hight, whight, gorisontPass, vertPass, fildsize);
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
        public static void RedrowField(IWriter writer, Coordinates selectedCell, int fildSize, char[,] fild)
        {
            writer.PrintGameTableBody(0, 0, fildSize, fildSize, 0, 0);
            writer.ColourFoundedWords(0, 0, 0, 0, fildSize, fild);
            writer.ReColour(selectedCell.X, selectedCell.Y, 0, 0, 0, 0, Colors.Red);
            writer.SetLetters(fild, 0, 0, 0, 0, fildSize);
        }
        public static void RecolourSelectedCells(IWriter writer, Coordinates selectedCell, int fildSize, char[,] fild, List<Coordinates> selectedCells)
        {
            RedrowField(writer, selectedCell, fildSize, fild);
            foreach (var Cell in selectedCells)
                writer.ReColour(Cell.X, Cell.Y, 0, 0, 0, 0, Colors.Yellow);
            writer.ReColour(selectedCell.X, selectedCell.Y, 0, 0, 0, 0, Colors.Red);
        }
        public static void ExecuteKeyDown(IMoves moveReaderKey, ref Coordinates currentCell,ref List<Coordinates> selectedCells, IWriter writer, int fildSize,ref Level level)
        {
            Asic asic;
            var move = moveReaderKey.GetMoove(currentCell.X, currentCell.Y, level.GetLevelFild(), out asic);
            if (asic == Asic.X || asic == Asic.Y)
            {
                if (asic == Asic.X)
                    currentCell = new Coordinates(currentCell.X + (int)move, currentCell.Y);
                else
                    currentCell = new Coordinates(currentCell.X, currentCell.Y + (int)move);

                if (selectedCells.Count != 0)
                {
                    selectedCells.Add(currentCell);
                    GamePlay.RecolourSelectedCells(writer, currentCell, fildSize, level.GetLevelFild(), selectedCells);
                }
                else
                    GamePlay.RedrowField(writer, currentCell, fildSize, level.GetLevelFild());

            }
            else if (asic == Asic.Aditional)
            {
                if (move == Move.Up)
                {
                    if (selectedCells.Count == 0)
                        selectedCells.Add(currentCell);

                    else
                    {
                        if (GameLogic.MakeStap(ref level, writer, selectedCells))
                            currentCell = GamePlay.SetCursorOnEmptyCell(level.GetLevelFild(), fildSize);

                        selectedCells.Clear();
                        GamePlay.RedrowField(writer, currentCell, fildSize, level.GetLevelFild());
                    }
                }
                else
                {
                    if (selectedCells.Count == 0)
                        writer.PrintMenu();

                    else selectedCells.Clear();
                }
            }
        }
    }
}

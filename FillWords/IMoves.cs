namespace FillWords.Logic
{
    public interface IMoves
    {
        public ReaderType Type { get; }

        public Move GetMoove(int positionX, int positionY, char[,] fild, out Asic asic);
    }
}

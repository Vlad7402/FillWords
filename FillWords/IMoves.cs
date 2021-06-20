namespace FillWords.Logic
{
    public interface IMoves
    {
        public ReaderType Type { get; }

        public void GetMoove();
    }
}

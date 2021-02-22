namespace FillWords.Logic
{
    public interface IWriter
    {
        public void PrintMenu();
        public void ReColour(int gorID, int vertID, int gorisontPass, int vertPass, int hight, int whight, Colors color);
        public void ColourFoundedWords(int gorisontPass, int vertPass, int hight, int whight, int fildsize, char[,] fild);
        public void SetLetters(char[,] Letters, int hight, int whightint, int gorisontPass, int vertPass, int fildSize);
        public string GetPlayerName();
        public void PrintErrorMassage(Errors error);
        public void PrintGameTableBody(int hight, int whight, int gorisontNum, int vertNum, int gorisontPass, int vertPass);
    }
}

namespace FillWords.Logic
{
    public class GameLogic
    {
        private readonly IWriter writer;
        private readonly IMoves moveReader;
        public GameLogic(IWriter writer, IMoves moveReader)
        {
            this.writer = writer;
            this.moveReader = moveReader;
        }
        public void StartNewGame()
        {
            Level level = new Level();
            if (Files.WordsCheck())
            {
                string Name = writer.GetPlayerName();
                if (Name != string.Empty)
                {
                    level.player = Name;
                }
                level.CreateLevel(1);
                Files.SaveGame(level);
                PlayTheGame(level);
            }
            else writer.PrintErrorMassage(Errors.VocabulararyError);
        }
        public void LoadGame()
        {
            Level level;
            if (Files.SaveCheck())
            {
                level = Files.LoadGame();
                PlayTheGame(level);
            }
            else writer.PrintErrorMassage(Errors.SaveError);
        }
        private void PlayTheGame(Level level)
        {
            var gamePlay = new GamePlay(writer, moveReader);
            while (true)
            {
                int[,] ChosedLettersCoordinates = gamePlay.ReturnWordCoordnates(level);
                if (ChosedLettersCoordinates != null)
                {
                    string word = level.GetWordOfCoordinates(ChosedLettersCoordinates);
                    if (level.CheckWordInLevel(word))
                    {
                        level.DeleteSelectedWord(ChosedLettersCoordinates);
                        if (level.IsGameEnd())
                        {
                            StarnNewLevel(level);
                            break;
                        }
                    }
                    else if (level.CheckWordInVocabulary(word))
                    {
                        writer.PrintErrorMassage(Errors.WordIsInVocabularyError);
                    }
                    else writer.PrintErrorMassage(Errors.WordIsOutError);

                    Files.SaveGame(level);
                }
                else
                {
                    Files.SaveGame(level);
                    break;
                }
            }
        }
        private void StarnNewLevel(Level oldLevel)
        {
            Level newLevel = new Level();
            newLevel.player = oldLevel.player;
            newLevel.CreateLevel(oldLevel.level + 1);
            Files.SaveGame(newLevel);
            PlayTheGame(newLevel);
        }
    }
}

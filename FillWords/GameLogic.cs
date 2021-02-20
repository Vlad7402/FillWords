namespace FillWords.Logic
{
    using FillWords.Console;

    static class GameLogic
    {
        public static void StartNewGame()
        {
            Level level = new Level();
            if (Files.WordsCheck())
            {
                string Name = Writer.GetPlayerName();
                if (Name != string.Empty)
                {
                    level.player = Name;
                }
                level.CreateLevel(1);
                Files.SaveGame(level);
                PlayTheGame(level);
            }
            else Writer.PrintErrorMassage(Errors.VocabulararyError);
        }
        public static void LoadGame()
        {
            Level level;
            if (Files.SaveCheck())
            {
                level = Files.LoadGame();
                PlayTheGame(level);
            }
            else Writer.PrintErrorMassage(Errors.SaveError);
        }
        private static void PlayTheGame(Level level)
        {
            while (true)
            {
                int[,] ChosedLettersCoordinates = GamePlay.ReturnWordCoordnates(level);
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
                        Writer.PrintErrorMassage(Errors.WordIsInVocabularyError);
                    }
                    else Writer.PrintErrorMassage(Errors.WordIsOutError);

                    Files.SaveGame(level);
                }
                else
                {
                    Files.SaveGame(level);
                    break;
                }
            }
        }
        private static void StarnNewLevel(Level oldLevel)
        {
            Level newLevel = new Level();
            newLevel.player = oldLevel.player;
            newLevel.CreateLevel(oldLevel.level + 1);
            Files.SaveGame(newLevel);
            PlayTheGame(newLevel);
        }
    }
}

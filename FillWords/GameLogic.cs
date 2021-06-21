using System.Collections.Generic;

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
        public void StartNewGame(IWriter writer)
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
        public static bool MakeStap(ref Level level, IWriter writer, List<Coordinates> letterCoordinates)
        {
            if (letterCoordinates.Count != 0)
            {
                string word = level.GetWordOfCoordinates(letterCoordinates);
                if (level.CheckWordInLevel(word))
                {
                    level.DeleteSelectedWord(letterCoordinates);
                    if (level.IsGameEnd())                   
                        level = GenerateNewLevel(level);

                    return true;
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
                writer.PrintMenu();
            }
            return false;
        }
        private void StarnNewLevel(Level oldLevel)
        {
            Level newLevel = new Level();
            newLevel.player = oldLevel.player;
            newLevel.CreateLevel(oldLevel.level + 1);
            Files.SaveGame(newLevel);
            PlayTheGame(newLevel);
        }
        private static Level GenerateNewLevel(Level oldLevel)
        {
            Level newLevel = new Level();
            newLevel.player = oldLevel.player;
            newLevel.CreateLevel(oldLevel.level + 1);
            Files.SaveGame(newLevel);
            return newLevel;
        }
    }
}

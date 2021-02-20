namespace FillWords.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    static class Files
    {
        public static string[] GetAllWords()
        {
            return File.ReadAllLines("word_rus_sort.txt");
        }
        public static void SaveGame(Level level)
        {
            if (level.player != "Guest") File.WriteAllText("GameSave.save", level.GetInfoForSave());
        }
        public static bool SaveCheck()
        {
            return File.Exists("GameSave.save");
        }
        public static bool WordsCheck()
        {
            return File.Exists("word_rus_sort.txt");
        }
        public static Level LoadGame()
        {
            string input = File.ReadAllText("GameSave.save");
            string[] levelInfo = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Level level = new Level();
            level.player = levelInfo[0];
            int levelNum = int.Parse(levelInfo[1]);
            level.SetLevelNum(levelNum);
            int counter = 0;
            int fildSize = 3 + levelNum / 10;
            char[,] fild = new char[fildSize, fildSize];
            for (int i = 0; i < fildSize; i++)
            {
                for (int j = 0; j < fildSize; j++)
                {
                    fild[i, j] = levelInfo[2][counter];
                    counter++;
                }
            }
            level.SetLevelFild(fild);
            List<string> words = new List<string>();
            for (int i = 3; i < levelInfo.Length; i++)
            {
                words.Add(levelInfo[i]);
            }
            level.SetLevelWords(words);
            return level;
        }
    }
}

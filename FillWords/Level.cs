namespace FillWords.Logic
{
    using System;
    using System.Collections.Generic;
    class Level
    {
        public string player { get; set; } = "Guest";
        public int level { get; private set; } = 0;
        private char[,] fild = null;
        private List<string> words = null;
        public void CreateLevel(int lvl)
        {
            words = GetWordsForLevel(GetWordLenght(lvl), GetWordNum(lvl), lvl, 24);
            fild = FillFild(words, lvl);
            level = lvl;
        }
        private static char[,] FillFild(List<string> input, int level)
        {
            int fildSize = GetFildSize(level);
            char[,] result = new char[fildSize, fildSize];
            Random getRandomNum = new Random();
            bool error = true;
            while (error)
            {
                char[,] backUp;
                error = false;
                result = new char[fildSize, fildSize];
                int Xcoordinare = getRandomNum.Next(fildSize);
                int Ycoordinare = getRandomNum.Next(fildSize);
                for (int i = 0; i < input.Count; i++)
                {
                    backUp = CopyToArray(result, fildSize);
                    int[] lastCoordinates = new int[] { Xcoordinare, Ycoordinare };
                    string word = input[i];
                    for (int k = 0; k <= word.Length; k++)
                    {
                        bool stepError = false;
                        result = CopyToArray(backUp, fildSize);
                        Xcoordinare = lastCoordinates[0];
                        Ycoordinare = lastCoordinates[1];
                        for (int j = 0; j < word.Length; j++)
                        {
                            if (IsMoveAvailable(result, Xcoordinare, Ycoordinare, 'x', fildSize) || IsMoveAvailable(result, Xcoordinare, Ycoordinare, 'y', fildSize))
                            {
                                int[] randomCoordinates = GetRandomCoordinates(result, Xcoordinare, Ycoordinare, fildSize);
                                Xcoordinare = randomCoordinates[0];
                                Ycoordinare = randomCoordinates[1];
                                result[Xcoordinare, Ycoordinare] = word[j];
                            }
                            else
                            {
                                stepError = true;
                                break;
                            }

                        }
                        if (k == word.Length) error = true;

                        if (!stepError)
                        {
                            break;
                        }
                    }
                    if (error) break;
                }
            }
            return result;
        }
        private static List<string> GetWordsForLevel(int wordsLenght, int wordsNum, int level, int longestWord)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < wordsNum; i++) AddRandomWord(result, wordsLenght);
            while (GetIDOfEqualWords(result) != -1)
            {
                result.RemoveAt(GetIDOfEqualWords(result));
                AddRandomWord(result, wordsLenght);
            }
            int numOfEmptyCelings = GetNumOfEmptyCelings(level);
            while (numOfEmptyCelings > 0)
            {
                if (numOfEmptyCelings == 1)
                {
                    result.RemoveAt(0);
                    AddRandomWord(result, wordsLenght + 1);
                }
                else if (numOfEmptyCelings > longestWord) AddRandomWord(result, longestWord);

                else AddRandomWord(result, numOfEmptyCelings);

                numOfEmptyCelings -= longestWord;
            }
            return result;
        }
        private static int GetWordNum(int lvl)
        {
            return 2 + lvl / 6;
        }
        private static int GetFildSize(int lvl)
        {
            return 3 + lvl / 10;
        }
        private static int GetWordLenght(int lvl)
        {
            return GetFildSize(lvl) * GetFildSize(lvl) / GetWordNum(lvl);
        }
        private static int GetNumOfEmptyCelings(int lvl)
        {
            return GetFildSize(lvl) * GetFildSize(lvl) - GetWordLenght(lvl) * GetWordNum(lvl);
        }
        private static List<string> GetWordsOflenght(int wordsLenght)
        {
            List<string> result = new List<string>();
            string[] allWords = Files.GetAllWords();
            for (int i = 0; i < allWords.Length; i++) if (allWords[i].Length == wordsLenght) result.Add(allWords[i]);

            return result;
        }
        private static string GetRandomWord(List<string> input)
        {
            Random getRandomNum = new Random();
            int randomNum = getRandomNum.Next(input.Count);
            return input[randomNum];
        }
        private static int GetIDOfEqualWords(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input.Count; j++)
                {
                    if (i == j) continue;
                    if (input[i] == input[j]) return i;
                }
            }
            return -1;
        }
        private static void AddRandomWord(List<string> input, int wordLenght)
        {
            List<string> wordsOfSameLenght = GetWordsOflenght(wordLenght);
            input.Add(GetRandomWord(wordsOfSameLenght));
        }
        private static bool IsMoveAvailable(char[,] input, int Xcoordinare, int Ycoordinare, char axis, int fildSize)
        {
            axis = char.ToLower(axis);
            if (axis == 'x')
            {
                if (Xcoordinare == 0) { if (char.IsControl(input[Xcoordinare + 1, Ycoordinare])) return true; }
                else if (Xcoordinare == fildSize - 1) { if (char.IsControl(input[Xcoordinare - 1, Ycoordinare])) return true; }
                else if (char.IsControl(input[Xcoordinare + 1, Ycoordinare]) || char.IsControl(input[Xcoordinare - 1, Ycoordinare])) return true;
            }
            else
            {
                if (Ycoordinare == 0) { if (char.IsControl(input[Xcoordinare, Ycoordinare + 1])) return true; }
                else if (Ycoordinare == fildSize - 1) { if (char.IsControl(input[Xcoordinare, Ycoordinare - 1])) return true; }
                else if (char.IsControl(input[Xcoordinare, Ycoordinare + 1]) || char.IsControl(input[Xcoordinare, Ycoordinare - 1])) return true;
            }
            return false;
        }
        private static int[] GetRandomCoordinates(char[,] input, int Xcoordinare, int Ycoordinare, int fildSize)
        {
            char asix;
            int delta;
            int randomNum;
            Random getRandomNum = new Random();
            if (IsMoveAvailable(input, Xcoordinare, Ycoordinare, 'x', fildSize) && IsMoveAvailable(input, Xcoordinare, Ycoordinare, 'y', fildSize))
            {
                randomNum = getRandomNum.Next(2);
                if (randomNum == 0) asix = 'x';
                else asix = 'y';
            }
            else if (IsMoveAvailable(input, Xcoordinare, Ycoordinare, 'x', fildSize)) asix = 'x';
            else asix = 'y';

            int coordinare;
            if (asix == 'x') coordinare = Xcoordinare;
            else coordinare = Ycoordinare;

            if (coordinare == 0) delta = 1;
            else if (coordinare == fildSize - 1) delta = -1;
            else
            {
                if (asix == 'x')
                {
                    if (char.IsLetter(input[Xcoordinare + 1, Ycoordinare]) && char.IsControl(input[Xcoordinare - 1, Ycoordinare])) delta = -1;
                    else if (char.IsLetter(input[Xcoordinare - 1, Ycoordinare]) && char.IsControl(input[Xcoordinare + 1, Ycoordinare])) delta = 1;
                    else
                    {
                        randomNum = getRandomNum.Next(2);
                        if (randomNum == 0) delta = 1;
                        else delta = -1;
                    }
                }
                else
                {
                    if (char.IsLetter(input[Xcoordinare, Ycoordinare + 1]) && char.IsControl(input[Xcoordinare, Ycoordinare - 1])) delta = -1;
                    else if (char.IsLetter(input[Xcoordinare, Ycoordinare - 1]) && char.IsControl(input[Xcoordinare, Ycoordinare + 1])) delta = 1;
                    else
                    {
                        randomNum = getRandomNum.Next(2);
                        if (randomNum == 0) delta = 1;
                        else delta = -1;
                    }
                }
            }

            if (asix == 'x') return new int[2] { Xcoordinare + delta, Ycoordinare };
            else return new int[2] { Xcoordinare, Ycoordinare + delta };
        }
        private static char[,] CopyToArray(char[,] input, int fildSize)
        {
            string inputStr = string.Empty;
            for (int i = 0; i < fildSize; i++) for (int j = 0; j < fildSize; j++) inputStr += input[i, j];

            int counter = 0;
            char[,] result = new char[fildSize, fildSize];
            for (int i = 0; i < fildSize; i++)
                for (int j = 0; j < fildSize; j++)
                {
                    result[i, j] = inputStr[counter];
                    counter++;
                }

            return result;
        }
        public bool CheckWordInLevel(string input)
        {
            return words.Contains(input);
        }
        public bool CheckWordInVocabulary(string input)
        {
            List<string> words = new List<string>();
            words.AddRange(Files.GetAllWords());
            return words.Contains(input);
        }
        public void DeleteSelectedWord(int[,] input)
        {
            for (int i = 0; i < input.Length / 2; i++) fild[input[i, 1], input[i, 0]] = '0';
        }
        public int GetLevelNum()
        {
            return level;
        }
        public char[,] GetLevelFild()
        {
            return fild;
        }
        public string GetInfoForSave()
        {
            string result = player + " " + level.ToString() + " ";
            int fildSize = GetFildSize(level);
            for (int i = 0; i < fildSize; i++)
            {
                for (int j = 0; j < fildSize; j++)
                {
                    result += fild[i, j];
                }
            }
            result += " ";
            for (int i = 0; i < words.Count; i++)
            {
                result += words[i];
                result += " ";
            }
            return result;
        }
        public void SetLevelFild(char[,] input)
        {
            fild = input;
        }
        public void SetLevelNum(int input)
        {
            level = input;
        }
        public void SetLevelWords(List<string> input)
        {
            words = input;
        }
        public string GetWordOfCoordinates(int[,] coordinates)
        {
            string result = string.Empty;
            for (int i = 0; i < coordinates.Length / 2; i++)
            {
                result += fild[coordinates[i, 1], coordinates[i, 0]];
            }
            return result;
        }
        public bool IsGameEnd()
        {
            int fildSize = GetFildSize(level);
            for (int i = 0; i < fildSize; i++)
            {
                for (int j = 0; j < fildSize; j++)
                {
                    if (fild[i, j] != '0') return false;
                }
            }
            return true;
        }
    }
}

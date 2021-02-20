namespace FillWords.Console
{
    using System;

    public enum Colors
    {
        Yellow, Red, Black, Gray
    }
    public enum Errors
    {
        VocabulararyError, SaveError, WordIsInVocabularyError, WordIsOutError, InProcess
    }
    public static class Writer
    {
        public static ConsoleColor GetColor(Colors color)
        {
            var colors = new ConsoleColor[4];
            colors[0] = ConsoleColor.Yellow;
            colors[1] = ConsoleColor.Red;
            colors[2] = ConsoleColor.Black;
            colors[3] = ConsoleColor.Gray;
            return colors[(int)color];
        }
        private static string GetErrorMassege(Errors error)
        {
            string[] errorMasseges = new string[5];
            errorMasseges[0] = "Отсутствует словарь";
            errorMasseges[1] = "Отсутствует сохранение";
            errorMasseges[2] = "Слово есть в словаре, но не загадано на уровне";
            errorMasseges[3] = "Слово отсутствует в словаре";
            errorMasseges[4] = "Фича в разработке";
            return errorMasseges[(int)error];
        }
        public static void PrintMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            string[] ASCIIBook = new string[] {"			   _____________________",
                                               "			.-||         |         ||-.",
                                               "			||||         |         ||||",
                                               "			||||         |         ||||",
                                               "			||||         |         ||||",
                                               "			||||         |         ||||",
                                               "			||||_________|_________||||",
                                               "			|||_____________________|||",
                                               "			`----------~___~---------''"};
            for (int i = 0; i < ASCIIBook.Length; i++) Console.WriteLine(ASCIIBook[i]);
            PrintRainbowLetters(29);
            PrintRainbowLetters(39);
            Console.SetCursorPosition(0, 9);
            Console.ForegroundColor = ConsoleColor.Yellow;
            string[] ASCIIList = new string[] { "                                                                 _____________",
                                                "                                                                (____________()",
                                                "                                                                / ~~~~~~~~~~ /",
                                                "                                                               / Новая игра /",
                                                "                                                              / ~~~~~~~~~~ /",
                                                "                                                             / Продолжить /",
                                                "                                                            / ~~~~~~~~~~ /",
                                                "                                                           / Рекорды    /",
                                                "                                                          / ~~~~~~~~~~ /",
                                                "                                                         / Выход      /",
                                                "                                                        /____________/",
                                                "                                                       (____________()"};
            for (int i = 0; i < ASCIIList.Length; i++) Console.WriteLine(ASCIIList[i]);
            string[] ASCIIShild = new string[] {"        .--.           .---.        .-.",
                                                "    .---|--|   .-.     | A |  .---. |~|    .--.",
                                                " .--|===|Ch|---|_|--.__| S |--|:::| |~|-==-|==|---.",
                                                " |%%|NT2|oc|===| |~~|%%| C |--|   |_|~|CATS|  |___|-.",
                                                " |  |   |ah|===| |==|  | I |  |:::|=| |    |GB|---|=|",
                                                " |  |   |ol|   |_|__|  | I |__|   | | |    |  |___| |",
                                                " |~~|===|--|===|~|~~|%%|~~~|--|:::|=|~|----|==|---|=|",
                                                " ^--^---'--^---^-^--^--^---'--^---^-^-^-==-^--^---^-'",};
            Console.SetCursorPosition(0, 16);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (int i = 0; i < ASCIIShild.Length; i++) Console.WriteLine(ASCIIShild[i]);
        }
        public static void PrintGameTableBody(int hight, int whight, int gorisontNum, int vertNum, int gorisontPass, int vertPass)
        {
            Console.Clear();
            PrintTableString(hight, whight, gorisontNum, gorisontPass, vertPass, 1, "┌┬┐");
            for (int i = 2; i <= vertNum; i++)
            {
                PrintTableString(hight, whight, gorisontNum, gorisontPass, vertPass, i, "├┼┤");
            }
        }
        private static void PrintTableString(int hight, int whight, int gorisontNum, int gorisontPass, int vertPass, int stringNum, string firstLine)
        {
            int possitionX = GetPosition(gorisontPass, 1, whight) - 1;
            int possitionY = GetPosition(vertPass, stringNum, hight) - 1;
            Console.SetCursorPosition(possitionX, possitionY);
            PrintLine(whight, gorisontNum, firstLine[0], firstLine[1], firstLine[2], '─');
            for (int j = 1; j <= hight; j++)
            {
                Console.SetCursorPosition(possitionX, possitionY + j);
                PrintLine(whight, gorisontNum, '│', '│', '│', ' ');
            }
            Console.SetCursorPosition(possitionX, possitionY + hight + 1);
            PrintLine(whight, gorisontNum, '└', '┴', '┘', '─');
        }
        private static void PrintLine(int whight, int gorisontNum, char lineStart, char lineCenter, char lineEnd, char lineInside)
        {
            Console.Write(lineStart);
            for (int i = 1; i <= gorisontNum; i++)
            {
                for (int j = 0; j < whight; j++)
                {
                    Console.Write(lineInside);
                }
                if (i != gorisontNum) Console.Write(lineCenter);
            }
            Console.Write(lineEnd);
        }
        private static void PrintRainbowLetters(int position)
        {
            RainbowLetters(position, 1, ConsoleColor.Red, 'F');
            RainbowLetters(position + 2, 1, ConsoleColor.Green, 'W');
            RainbowLetters(position + 1, 2, ConsoleColor.DarkRed, 'I');
            RainbowLetters(position + 3, 2, ConsoleColor.Cyan, 'O');
            RainbowLetters(position + 2, 3, ConsoleColor.DarkYellow, 'L');
            RainbowLetters(position + 4, 3, ConsoleColor.Blue, 'R');
            RainbowLetters(position + 3, 4, ConsoleColor.Yellow, 'L');
            RainbowLetters(position + 5, 4, ConsoleColor.DarkBlue, 'D');
            RainbowLetters(position + 6, 5, ConsoleColor.Magenta, 'S');
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void RainbowLetters(int positionX, int positionY, ConsoleColor letterColour, char letter)
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = letterColour;
            Console.WriteLine(letter);
            System.Threading.Thread.Sleep(75);
        }
        public static void PrintErrorMassage(Errors error)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(GetErrorMassege(error));
            Console.ReadKey();
            Console.Clear();
        }
        public static string GetPlayerName()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Как Вас зовут?");
            string result = Console.ReadLine();
            Console.Clear();
            return result;
        }
        public static void SetLetters(char[,] Letters, int hight, int whightint, int gorisontPass, int vertPass, int fildSize)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < fildSize; i++)
            {
                for (int j = 0; j < fildSize; j++)
                {
                    if (Letters[i, j] != '0')
                    {
                        Console.SetCursorPosition(GetPosition(gorisontPass, j + 1, whightint) + whightint / 2, GetPosition(vertPass, i + 1, hight) + hight / 2);
                        Console.Write(Letters[i, j]);
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
        }
        public static void ColourFoundedWords(int gorisontPass, int vertPass, int hight, int whight, int fildsize, char[,] fild)
        {
            for (int i = 0; i < fildsize; i++)
            {
                for (int j = 0; j < fildsize; j++)
                {
                    if (fild[i, j] == '0') ReColour(j + 1, i + 1, gorisontPass, vertPass, hight, whight, Colors.Gray);
                }
            }
        }
        public static void ReColour(int gorID, int vertID, int gorisontPass, int vertPass, int hight, int whight, Colors color)
        {
            Console.BackgroundColor = GetColor(color);
            for (int i = 0; i < hight; i++)
            {
                Console.SetCursorPosition(GetPosition(gorisontPass, gorID, whight), GetPosition(vertPass, vertID, hight) + i);
                for (int j = 0; j < whight; j++)
                {
                    Console.Write(" ");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 0);
        }
        private static int GetPosition(int pass, int ID, int WH)
        {
            return pass + 1 + (ID - 1) + (ID - 1) * WH;
        }
    }
}

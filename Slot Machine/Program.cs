using System;
using System.Collections.Generic;

namespace Slot_Machine
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> slotData = null;
            int coints = 50;
            GameModes playMode;
            int win;
            int counter = 0;


            var start = UIBase.PrintStartGame(coints);

            while (UIBase.IsUserPressY(start))
            {
                UIBase.PrintAvaliableGameModes(coints);
                do
                {
                    playMode = GetGameMode();

                    if (playMode == GameModes.Invalid)
                    {
                        UIBase.PrintPressValidKey();
                    }

                } while (playMode == GameModes.Invalid);

                int bid = GetGameModeBid(playMode);

                while ((coints - bid) <= 0 && counter < 3)
                {
                    UIBase.PrintNotEnoughtCoint();
                    playMode = GetGameMode();
                    bid = GetGameModeBid(playMode);
                    counter++;
                }

                if (counter >= 3)
                {
                    UIBase.PrintLoseMessage();
                    break;
                }

                coints -= bid;

                while (!UIBase.IsUserPressAnyKey())
                {
                    slotData = GenerateSlotNumbers();
                    UIBase.PrintSlotNumbers(slotData, playMode);
                }

                win = CalculatePrize(slotData, playMode);
                coints += win;

                UIBase.PrintWinMessage(win);


                if (coints > 0)
                {
                    UIBase.EmptyKeyAvaliableBuffer();
                    start = UIBase.PrintContinuePlay(coints);
                }
                else
                {
                    UIBase.PrintLoseMessage();
                    break;
                }

            }

        }

        /// <summary>
        /// Generating list with 0 to 2 values
        /// </summary>
        /// <returns>list with generated numbers</returns>
        static List<int> GenerateNewLineList()
        {
            Random rnd = new();
            List<int> list = new();

            for (int i = 0; i < 3; i++)
            {
                list.Add(rnd.Next(0, 3));
            }

            return list;
        }

        /// <summary>
        /// Generating a list contain 3 lists
        /// </summary>
        /// <returns>list of lists with generated numbers 0 to 2</returns>
        public static List<List<int>> GenerateSlotNumbers()
        {
            List<List<int>> slot = new();

            for (int i = 0; i < 3; i++)
            {
                slot.Add(GenerateNewLineList());
            }

            return slot;
        }

        /// <summary>
        /// Count all vertical matches
        /// </summary>
        /// <param name="data">3 lists block</param>
        /// <returns>
        /// 3X3 block of counters 
        /// every line of array represen a slot column and the value stored represent a counter. 
        /// first item will count aperances of number 0, second number 1 and third number 2 on the slot column 
        /// 3 slots columns returning 3 arrays.
        /// </returns>
        static int[,] VerticalMatches(List<List<int>> data)
        {
            int[,] matches = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    if (data[j][i] == 0)
                    {
                        matches[i, 0]++;
                    }

                    if (data[j][i] == 1)
                    {
                        matches[i, 1]++;
                    }

                    if (data[j][i] == 2)
                    {
                        matches[i, 2]++;
                    }
                }
            }

            return matches;
        }

        /// <summary>
        /// Count all diagonals matches
        /// </summary>
        /// <param name="data">3 lists block</param>
        /// <returns>
        /// 2X3 block of counters 
        /// every line of array represen a slots diagonal row and the value stored represent a counter. 
        /// first item will count aperances of number 0, second number 1 and third number 2 on the slot row 
        /// 2 slots vertical rows returning 2 arrays.
        /// </returns>
        static int[,] DiagonalMatches(List<List<int>> data)
        {
            int[,] matches = new int[,] { { 0, 0, 0 }, { 0, 0, 0 } };

            for (int i = 0, j = 2; i < 3; i++, j--)
            {
                if (data[i][i] == 2)
                {
                    matches[0, 2]++;
                }

                if (data[i][j] == 2)
                {
                    matches[1, 2]++;
                }

                if (data[i][i] == 1)
                {
                    matches[0, 1]++;
                }

                if (data[i][j] == 1)
                {
                    matches[1, 1]++;
                }
                if (data[i][i] == 0)
                {
                    matches[0, 0]++;
                }

                if (data[i][j] == 0)
                {
                    matches[1, 0]++;
                }
            }

            return matches;
        }

        /// <summary>
        /// Count all horizontal matches
        /// </summary>
        /// <param name="data">3 lists block</param>
        /// <returns>
        /// 3X3 block of counters 
        /// every line of array represen a slot row and the value stored represent a counter. 
        /// first item will count aperances of number 0, second number 1 and third number 2 on the slot row 
        /// 3 slots vertical rows  returning 3 arrays.
        /// </returns>
        static int[,] HorizontalMatches(List<List<int>> data)
        {
            int[,] matches = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

            for (var i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    if (data[i][j] == 0)
                    {
                        matches[i, 0]++;
                    }

                    if (data[i][j] == 1)
                    {
                        matches[i, 1]++;
                    }

                    if (data[i][j] == 2)
                    {
                        matches[i, 2]++;
                    }
                }
            }

            return matches;
        }

        /// <summary>
        /// Count center horizontal matches
        /// </summary>
        /// <param name="data">3 lists block</param>
        /// <returns>
        /// 1X3 block of counters 
        /// represen a slot center row and the value stored represent a counter. 
        /// first item will count aperances of number 0, second number 1 and third number 2 on the slot row 
        /// center vertical rows returning 1 arrays.
        /// </returns>
        static int[,] CenterHorizontalMatches(List<List<int>> data)
        {
            int[,] matches = new int[,] { { 0, 0, 0 } };

            for (var i = 0; i < 3; i++)
            {
                if (data[1][i] == 2)
                {
                    matches[0, 2]++;
                }
                if (data[1][i] == 1)
                {
                    matches[0, 1]++;
                }
                if (data[1][i] == 0)
                {
                    matches[0, 0]++;
                }
            }

            return matches;
        }

        /// <summary>
        /// Receiving block of counters and calculating prize coints
        /// </summary>
        /// <param name="arrayCounters"> aray contain match counters where each index represent mutching number</param>
        /// <returns>int represent prize at one row</returns>
        static int CalculateArrayWinCoints(int[,] arrayCounters)
        {
            int prize = 0;

            for (int j = 0; j < arrayCounters.Length / 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i != 2)
                    {
                        if (arrayCounters[j, i] == 3)
                            prize = 1;
                    }
                    else
                    {
                        if (arrayCounters[j, i] == 2)
                            prize = 2;

                        if (arrayCounters[j, i] == 3)
                            prize = 3;
                    }
                }
            }


            return prize;
        }

        /// <summary>
        /// Calculating prize by selected game mode
        /// </summary>
        /// <param name="data">generated slot numbers</param>
        /// <param name="playMode">plying mode</param>
        /// <returns></returns>
        public static int CalculatePrize(List<List<int>> data, GameModes playMode)
        {
            int prize = 0;
            int[,] temp;

            switch (playMode)
            {
                case GameModes.CenterLine:
                    temp = CenterHorizontalMatches(data);
                    prize = CalculateArrayWinCoints(temp);

                    break;

                case GameModes.Horizontal:
                    temp = HorizontalMatches(data);
                    prize = CalculateArrayWinCoints(temp);

                    break;

                case GameModes.Vertical:
                    temp = HorizontalMatches(data);
                    prize = CalculateArrayWinCoints(temp);
                    temp = VerticalMatches(data);
                    prize += CalculateArrayWinCoints(temp);

                    break;

                case GameModes.Diagonal:
                    temp = HorizontalMatches(data);
                    prize = CalculateArrayWinCoints(temp);
                    temp = VerticalMatches(data);
                    prize += CalculateArrayWinCoints(temp);
                    temp = DiagonalMatches(data);
                    prize += CalculateArrayWinCoints(temp);

                    break;
            }
            return prize;
        }

        /// <summary>
        /// Convert user input to GameModes enum item
        /// 1-> CenterLine
        /// 3-> Horizontal
        /// 6-> Vertical
        /// 8-> Diagonal
        /// wrong input -> Invalid
        /// </summary>
        /// <param name="key">user key input</param>
        /// <returns>GameModes enum item</returns>
        public static GameModes GetGameMode()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            GameModes gamemode = GameModes.Invalid;

            if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.D6 || key.Key == ConsoleKey.D8)
            {
                switch (key.Key)
                {

                    case ConsoleKey.D1:
                        gamemode = GameModes.CenterLine;
                        break;

                    case ConsoleKey.D3:
                        gamemode = GameModes.Horizontal;
                        break;

                    case ConsoleKey.D6:
                        gamemode = GameModes.Vertical;
                        break;

                    case ConsoleKey.D8:
                        gamemode = GameModes.Diagonal;
                        break;
                }
            }


            return gamemode;
        }

        /// <summary>
        /// Set GameModes enum item to number of coints
        /// Invalid - 0
        /// CenterLine - 1
        /// Horizontal -3
        /// Vertical - 6
        /// Diagonal - 8
        /// </summary>
        /// <param name="playMode"></param>
        /// <returns> number of coints </returns>
        public static int GetGameModeBid(GameModes playMode)
        {
            int bid = 0;

            switch (playMode)
            {
                case GameModes.Invalid:
                    break;
                case GameModes.CenterLine:
                    bid = 1;
                    break;
                case GameModes.Horizontal:
                    bid = 3;
                    break;
                case GameModes.Vertical:
                    bid = 6;
                    break;
                case GameModes.Diagonal:
                    bid = 8;
                    break;
                default:
                    break;
            }

            return bid;
        }
    }
}

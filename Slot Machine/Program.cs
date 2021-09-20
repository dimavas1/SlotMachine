using System;
using System.Collections.Generic;
using System.Threading;

namespace Slot_Machine
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> slotData = null;
            char play ='y';
            int coints = 50;
            int counter;
            GameModes playmode;
            int win;

            Console.WriteLine("Welcome to Slot Game");
            Console.WriteLine($"You have {coints} do you want to start play? [Y/N]\n");
            var start = Console.ReadKey();

            while (start.KeyChar.ToString().ToLower() == play.ToString().ToLower())
            {                
                PrintAvaliableGameModes(coints);

                int bid = int.Parse(Console.ReadLine());
                counter = 0;

                while ((coints-bid)<=0)
                {
                    Console.WriteLine("You don't have enought coints to bid");
                    Console.WriteLine("Please make new bid");
                    bid = int.Parse(Console.ReadLine());
                }

                for (int i = 0; i < Enum.GetNames(typeof(GameModes)).Length; i++)
                {
                    if ((int)(GameModes)Enum.GetValues(typeof(GameModes)).GetValue(i) == bid)
                    {
                        break;
                    }
                    counter++;
                }

                playmode = (GameModes)Enum.GetValues(typeof(GameModes)).GetValue(counter);
                coints += -bid;

                while (!Console.KeyAvailable)
                {
                    slotData = GenerateSlotNumbers();
                    PrintSlotNumbers(slotData, playmode);
                }

                win = CalculatePrize(slotData, playmode);
                coints += win;

                Console.WriteLine($"You Win {win}$");
                
                
                if (coints>0)
                {
                    Console.WriteLine($"You have {coints} coints left");
                    Console.WriteLine($"Do you want continue play? [Y/N]");
                    Console.ReadKey();
                    start = Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("You Lose, no more coints");
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
        static List<List<int>> GenerateSlotNumbers()
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
            int[,] matches = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }};
           
            for(int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    if (data[j][i]==0)
                    {
                        matches[i,0]++;
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
            int[,] matches = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }};

            for (int i = 0,j = 2; i < 3; i++,j--)
            {
                if (data[i][i] == 2)
                {
                    matches[0,2] ++;
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
            int[,] matches = new int[,] { { 0, 0, 0 }};

            for (var i = 0; i < 3; i++)
            {
                if (data[1][i] == 2)
                {
                    matches[0,2]++;
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
        /// Printing slot numbers and sign calculated rows and columns
        /// </summary>
        /// <param name="data">Generated slot numbers</param>
        /// <param name="bid">game mod to sign wich rows will be calculated in prize total</param>
        static void PrintSlotNumbers(List<List<int>> data, GameModes bid)
        {

            Thread.Sleep(10);

            Console.Clear();

            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    switch (bid)
                    {                     
                        case GameModes.Vertical:
                            Console.WriteLine(" " + "| | |" + " ");
                            Console.WriteLine("-" + string.Join(" ", data[i]) + "-");
                            break;
                        case GameModes.Diagonal:
                            Console.WriteLine("\\" + "| | |" + "/");
                            Console.WriteLine("-" + string.Join(" ", data[i]) + "-");
                            break;
                        case GameModes.Horizontal:
                            Console.WriteLine(" " + "     " + " ");
                            Console.WriteLine("-" + string.Join(" ", data[i]) + "-");
                            break;
                        default:
                            Console.WriteLine(" " + "     " + " ");
                            Console.WriteLine(" " + string.Join(" ", data[i]) + " ");
                            break;
                    }
                    
                }

                if (i == 1)
                {
                    Console.WriteLine("-" + string.Join(" ", data[i]) + "-");                    
                }

                if (i == 2)
                {
                    
                    switch (bid)
                    {
                        case GameModes.Vertical:
                            Console.WriteLine("-" + string.Join(" ", data[i]) + "-");
                            Console.WriteLine(" " + "| | |" + " ");
                            break;
                        case GameModes.Diagonal:
                            Console.WriteLine("-" + string.Join(" ", data[i]) + "-");
                            Console.WriteLine("/" + "| | |" + "\\");
                            break;
                        case GameModes.Horizontal:
                            Console.WriteLine("-" + string.Join(" ", data[i]) + "-");
                            Console.WriteLine(" " + "     " + " ");
                            break;
                        default:
                            Console.WriteLine(" " + string.Join(" ", data[i]) + " ");
                            Console.WriteLine(" " + "     " + " ");
                            break;
                    }
                }

            }
            
        }

        /// <summary>
        /// Receiving block of counters and calculating prize coints
        /// </summary>
        /// <param name="arrayCounters"> aray contain match counters where each index represent mutching number</param>
        /// <returns>int represent prize at one row</returns>
        static int CalculateArrayWinCoints(int[,] arrayCounters)
        {
            int prize = 0;

            for (int j = 0; j < arrayCounters.Length/3 ; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i!= 2)
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
        static int CalculatePrize(List<List<int>> data, GameModes playMode)
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
        /// Print avaliable modes for player
        /// </summary>
        /// <param name="currentCoints">coints avaliable for player</param>
        static void PrintAvaliableGameModes(int currentCoints)
        {
            if (currentCoints >= 8)
            {
                Console.WriteLine("\nMake your bid:");
                Console.WriteLine("1 coint for center line");
                Console.WriteLine("3 coints for all horizontal lines");
                Console.WriteLine("6 coints for all horizontal and all vertical lines");
                Console.WriteLine("8 coints for all horizontal lines, all vertical and diagonal lines");
            }

            if (currentCoints < 8 && currentCoints >= 6)
            {
                Console.WriteLine("\nMake your bid:");
                Console.WriteLine("1 coint for center line");
                Console.WriteLine("3 coints for all horizontal lines");
                Console.WriteLine("6 coints for all horizontal and all vertical lines");
            }

            if (currentCoints < 3 && currentCoints >= 1)
            {
                Console.WriteLine("\nMake your bid:");
                Console.WriteLine("1 coint for center line");
            }
        }

    }

    enum GameModes
    {
        CenterLine = 1,
        Horizontal = 3,
        Vertical = 6,
        Diagonal = 8
    }
}

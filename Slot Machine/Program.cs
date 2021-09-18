using System;
using System.Collections.Generic;
using System.Threading;

namespace Slot_Machine
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> slotData;
            char play ='y';
            int coints = 50;

            Console.WriteLine("Welcome to Slot Game");
            Console.WriteLine($"You have {coints} do you want to start play? [Y/N]");
            var start = Console.ReadKey();

            if (start.KeyChar.ToString().ToLower() == play.ToString().ToLower())
            {
                Console.WriteLine("Make your bid:");
                Console.WriteLine("1 coint for center line");
                Console.WriteLine("3 coints for all horizontal lines");
                Console.WriteLine("6 coints for all horizontal and all vertical lines");
                Console.WriteLine("8 coints for all horizontal lines, all vertical and diagonal lines");
                int bid = int.Parse(Console.ReadLine());
                coints = coints - bid;

                while (!Console.KeyAvailable)
                {
                    slotData = GenerateSlotNumbers();
                    PrintSlotNumbers(slotData, bid);

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
        /// <returns>list of lists with generated numbers</returns>
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
        /// 2d int contain matching numbers counters
        /// where the index is the matching number 
        /// </returns>
        static int[,] VerticalMatches(List<List<int>> data)
        {
            int[,] matches = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }};
           
            for(int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 2; j++)
                {
                    if (data[j][i]==0)
                    {
                        matches[j,i]++;
                    }

                    if (data[j][i] == 1)
                    {
                        matches[j, i]++;
                    }

                    if (data[j][i] == 2)
                    {
                        matches[j, i]++;
                    }
                }                
            }

            return matches;
        }

        /// <summary>
        /// Count all diagonals matches
        /// </summary>
        /// <param name="data">3 lists block</param>
        /// <returns>2d int 
        /// with counters matching numbers where index is the matching number
        /// </returns>
        static int[,] DiagonalMatches(List<List<int>> data)
        {
            int[,] matches = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }};

            for (int i = 0,j = 3; i < 3; i++,j--)
            {
                if (data[i][i] == 2)
                {
                    matches[0,2] ++;
                }

                if (data[j][j] == 2)
                {
                    matches[1, 2]++;
                }
                
                if (data[i][i] == 1)
                {
                    matches[0, 1]++;
                }

                if (data[j][j] == 1)
                {
                    matches[1, 1]++;
                }
                if (data[i][i] == 0)
                {
                    matches[0, 0]++;
                }

                if (data[j][j] == 0)
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
        /// <returns>2d int 
        /// with counters matching numbers where index is the matching number
        /// </returns>
        static int[,] HorizontalMatches(List<List<int>> data)
        {
            int[,] matches = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

            for (var i = 0; i < 3; i++)
            {
                
                for (int j = 0; j < 3; j++)
                {
                    if (data[i][j] == 2)
                    {
                        matches[i, 2]++;
                    }

                    if (data[i][j] == 1)
                    {
                        matches[i, 1]++;
                    }

                    if (data[i][j] == 0)
                    {
                        matches[i, 0]++;
                    }

                }
            }

            return matches;
        }

        /// <summary>
        /// Count center horizontal matches
        /// </summary>
        /// <param name="data">3 lists block</param>
        /// <returns>2d int 
        /// with counters matching numbers where index is the matching number
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
        /// Printing slot numbers on the screen
        /// </summary>
        /// <param name="data">generated numbers</param>
        static void PrintSlotNumbers(List<List<int>> data,int bid)
        {
            Thread.Sleep(10);

            Console.Clear();

            for (int i = 0; i < data.Count; i++)
            {
                if (i == 0 && bid >=3)
                {
                    //Console.WriteLine(" " + "^ ^ ^" + " ");
                    Console.WriteLine(" " + string.Join(" ", data[i]) + " ");
                }

                if (i == 1)
                {
                    
                    Console.WriteLine(">" + string.Join(" ", data[i]) + "<");
                }

                if (i == 2)
                {
                    Console.WriteLine(" " + string.Join(" ", data[i]) + " ");
                    //Console.WriteLine(" " + "^ ^ ^" + " ");
                }

            }
            
        }

        /// <summary>
        /// Calculate prize at one line
        /// </summary>
        /// <param name="arrayCounters"> aray contain match counters where each index represent mutching number</param>
        /// <returns>int represent prize at one row</returns>
        static int CalculatePrizeAtOneArray(int[,] arrayCounters)
        {
            int prize = 0;
            
            for (int i = 0; i < 3; i++)
            {
                if (i==0 && i==2)
                {
                    if (arrayCounters[0,i] == 3)
                        prize = 1;
                }
                else
                {
                    if (arrayCounters[0,i] == 2)
                        prize = 2;

                    if (arrayCounters[0,i] == 3)
                        prize = 3;   
                }
            }

            return prize;
        }

        static int CalculatePrize(List<List<int>> data, int bid)
        {
            int prize = 0;
            int[,] temp;

            switch (bid)
            {
                case 1:
                    temp = CenterHorizontalMatches(data);

                    prize = CalculatePrizeAtOneArray(temp);

                    break;

                case 3:
                    temp = HorizontalMatches(data);

                    for (int i = 0; i < 3; i++)
                    {
                       prize += CalculatePrizeAtOneArray(temp);
                    }
                    break;

                case 6:
                    temp = HorizontalMatches(data);

                    for (int i = 0; i < 3; i++)
                    {
                        prize += CalculatePrizeAtOneArray(temp);
                    }

                    temp = VerticalMatches(data);

                    for (int i = 0; i < 3; i++)
                    {
                        prize += CalculatePrizeAtOneArray(temp);
                    }

                    break;

                case 8:
                    temp = HorizontalMatches(data);

                    for (int i = 0; i < 3; i++)
                    {
                        prize += CalculatePrizeAtOneArray(temp);
                    }

                    temp = VerticalMatches(data);

                    for (int i = 0; i < 3; i++)
                    {
                        prize += CalculatePrizeAtOneArray(temp);
                    }

                    temp = DiagonalMatches(data);

                    for (int i = 0; i < 2; i++)
                    {
                        prize += CalculatePrizeAtOneArray(temp);
                    }
                    
                    break;

            }

            return prize;
        }

    }
}

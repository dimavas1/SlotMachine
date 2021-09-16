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

            while (!Console.KeyAvailable)
            {
                slotData = GenerateSlotNumbers(); 
                PrintSlotNumbers(slotData,1);
                
            } 
        }

        /// <summary>
        /// Generating list with 0 to 2 values
        /// </summary>
        /// <returns>new list</returns>
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
        /// <returns>list of lists</returns>
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
        /// <returns>one list with counted matching numbers</returns>
        static List<int> VerticalMatches(List<List<int>> data)
        {
            List<int> matches = new();
            int counter;

            for(int i = 0; i < data.Count; i++)
            {
                counter = 0;

                for (int j = 0; j < data[i].Count; j++)
                {
                    if (data[j][i]==2)
                    {
                        counter++;
                    }
                }

                matches.Add(counter);
            }

            return matches;
        }

        /// <summary>
        /// Count all vertical matches
        /// </summary>
        /// <param name="data">3 lists block</param>
        /// <returns>one list with counters matching numbers</returns>
        static List<int> DiagonalMatches(List<List<int>> data)
        {
            List<int> matches = new();
            int leftToRighCounter=0;
            int rightToLeftCounter=0;

            for (int i = 0,j=3; i < data.Count; i++,j--)
            {
                if (data[i][i]==2)
                {
                    leftToRighCounter++;
                }

                if (data[j][j] == 2)
                {
                    rightToLeftCounter++;
                }
            }

            matches.Add(leftToRighCounter);
            matches.Add(rightToLeftCounter);

            return matches;
        }

        /// <summary>
        /// Count all horizontal matches
        /// </summary>
        /// <param name="data">3 lists block</param>
        /// <returns>one list with counters matching numbers</returns>
        static List<int> HorizontalMatches(List<List<int>> data)
        {
            List<int> matches = new();
            int counter;

            for (var i = 0; i < data.Count; i++)
            {
                counter = 0;

                for (int j = 0; j < data[i].Count; j++)
                {
                    if (data[i][j] == 2)
                    {
                        counter++;
                    }
                }

                matches.Add(counter);
            }

            return matches;
        }

        /// <summary>
        /// Count center horizontal matches
        /// </summary>
        /// <param name="data">3 lists block</param>
        /// <returns>counter matching numbers</returns>
        static int CenterHorizontalMatches(List<List<int>> data)
        {
            int counter = 0;

            for (var i = 0; i < data[1].Count; i++)
            {
                if (data[1][i] == 2)
                {
                    counter++;
                }
            }
            return counter;
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

        //static int CalculatePrize(List<List<int>> data, int bid)
        //{
        //    int prize = 0;
        //    int temp;
            
        //    switch (bid)
        //    {
        //        case 1:
        //            temp = CenterHorizontalMatches(data);

        //            if (temp == 2)
        //                prize = 2;
        //            if (temp == 3)
        //            {

        //            }
        //            break;
        //    }
        //}

  


    }
}

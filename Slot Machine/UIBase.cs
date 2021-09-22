﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace Slot_Machine
{
    public class UIBase :ProgramBase
    {

        /// <summary>
        /// Print avaliable modes for player
        /// </summary>
        /// <param name="currentCoints">coints avaliable for player</param>
        public static void PrintAvaliableGameModes(int currentCoints)
        {
            Console.WriteLine("\nMake your bid:");

            if (currentCoints >= 1)
            {
                Console.WriteLine("1 coint for center line");
            }

            if (currentCoints >= 3)
            {
                Console.WriteLine("3 coints for all horizontal lines");
            }

            if (currentCoints >= 6)
            {
                Console.WriteLine("4 coint for center line");
            }

            if (currentCoints >= 8)
            {
                Console.WriteLine("8 coint for center line");
            }
        }

        /// <summary>
        /// Printing slot numbers and sign calculated rows and columns
        /// </summary>
        /// <param name="data">Generated slot numbers</param>
        /// <param name="bid">game mod to sign wich rows will be calculated in prize total</param>
        public static void PrintSlotNumbers(List<List<int>> data, GameModes gamemode)
        {

            Thread.Sleep(10);

            Console.Clear();

            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    switch (gamemode)
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

                    switch (gamemode)
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

            Console.WriteLine("Click any button to stop");
        }

        /// <summary>
        /// Printing message on the game start 
        /// </summary>
        /// <param name="coints">Coints received at start</param>
        public static void PrintStartGame(int coints)
        {
            Console.WriteLine("Welcome to Slot Game");
            Console.WriteLine($"You have {coints} do you want to start play? [Y/N]\n");
        }

        /// <summary>
        /// Printing message if there is not enought coints to bid
        /// </summary>
        public static void PrintNotEnoughtCoint()
        {
            Console.WriteLine("You don't have enought coints to bid");
            Console.WriteLine("Please make new bid");
        }

        /// <summary>
        /// Printing message about continue playing
        /// </summary>
        /// <param name="coints"> coint left to bid</param>
        public static void PrintContinuePlay(int coints)
        {
            Console.WriteLine($"You have {coints} coints left");
            Console.WriteLine($"Do you want continue play? [Y/N]");
        }

        /// <summary>
        /// Printig how many coint player win
        /// </summary>
        /// <param name="win"></param>
        public static void PrintWinMessage(int win)
        {
            Console.WriteLine($"You Win {win}$");
        }

        /// <summary>
        /// Printig lose message
        /// </summary>
        public static void PrintLoseMessage()
        {
            Console.WriteLine("You Lose, no more coints");
        }

        /// <summary>
        /// Printing message to user to select valid game mode
        /// </summary>
        public static void PrintPressValidKey()
        {
            Console.WriteLine("You have selected invalid mode.");
            Console.WriteLine("Please select again.");

        }

    }
}
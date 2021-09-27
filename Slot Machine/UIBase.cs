using Slot_Machine;
using System;
using System.Collections.Generic;
using System.Threading;

public class UIBase
{

    /// <summary>
    /// Print avaliable modes for player
    /// </summary>
    /// <param name="currentCoints">coints avaliable for player</param>
    public static void PrintAvaliableGameModes(int currentCoints)
    {
        Console.WriteLine("\nSelect Your Game Mode:");

        if (currentCoints >= 1)
        {
            Console.WriteLine($"1 for center line cost {Program.GetGameModeBid(GameModes.CenterLine)} coints");
        }

        if (currentCoints >= 3)
        {
            Console.WriteLine($"3 for previous mode and all horizontal lines cost {Program.GetGameModeBid(GameModes.Horizontal)} coints");
        }

        if (currentCoints >= 6)
        {
            Console.WriteLine($"6 for previous mode and all vertical lines cost {Program.GetGameModeBid(GameModes.Vertical)} coints");
        }

        if (currentCoints >= 8)
        {
            Console.WriteLine($"8 for previous mode and all diagonal lines cost {Program.GetGameModeBid(GameModes.Diagonal)} coints");
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
   /// <returns>input key from user Yes or No</returns>
    public static ConsoleKeyInfo PrintStartGame(int coints)
    {
        Console.WriteLine("Welcome to Slot Game");
        Console.WriteLine($"You have {coints} do you want to start play? [Y/N]\n");

        return Console.ReadKey();
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
    /// <returns>An input Key from User</returns>
    public static ConsoleKeyInfo PrintContinuePlay(int coints)
    {
        Console.WriteLine($"You have {coints} coints left");
        Console.WriteLine($"Do you want continue play? [Y/N]");
        return Console.ReadKey();
    }

    /// <summary>
    /// Printig how many coint player win
    /// </summary>
    /// <param name="win">winning sum</param>
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

    /// <summary>
    /// Compare ConsoleKeyIfo input to ConsoleKey.Y
    /// </summary>
    /// <param name="key">Selected input</param>
    /// <returns>True if it's a ConsoleKey.Y else False</returns>
    public static bool IsUserPressY(ConsoleKeyInfo key)
    {
        return key.Key == ConsoleKey.Y;
    }

    /// <summary>
    /// Return bolean if any key was pressed 
    /// </summary>
    /// <returns>True if pressed</returns>
    public static bool IsUserPressAnyKey()
    {
        return Console.KeyAvailable;
    }

    /// <summary>
    /// Empty KeyAvaliable Buffer after Console.KeyAvailable command
    /// </summary>
    public static void EmptyKeyAvaliableBuffer()
    {
        Console.ReadKey();
    }

}
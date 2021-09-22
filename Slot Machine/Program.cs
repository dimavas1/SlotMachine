using System;
using System.Collections.Generic;

namespace Slot_Machine
{
    class Program : UIBase
    {
        static void Main(string[] args)
        {
            List<List<int>> slotData = null;            
            int coints = 50;
            GameModes playmode;
            int win;

            PrintStartGame(coints);
            var start = Console.ReadKey();

            while (start.Key == ConsoleKey.Y)
            {                
                PrintAvaliableGameModes(coints);
                do
                {
                    playmode = GetGameMode(Console.ReadKey());

                    if (playmode == GameModes.Invalid)
                    {
                        PrintPressValidKey();
                    }

                } while (playmode == GameModes.Invalid);
                
                int bid = GetGameModeBid(playmode);
                
                while ((coints-bid)<=0)
                {
                    PrintNotEnoughtCoint();
                    playmode = GetGameMode(Console.ReadKey());
                    bid = GetGameModeBid(playmode);

                }

                coints += -bid;

                while (!Console.KeyAvailable)
                {
                    slotData = GenerateSlotNumbers();
                    PrintSlotNumbers(slotData, playmode);
                }

                win = CalculatePrize(slotData, playmode);
                coints += win;

                PrintWinMessage(win);
                
                
                if (coints>0)
                {
                    PrintContinuePlay(coints);
                    Console.ReadKey();
                    start = Console.ReadKey();
                }
                else
                {
                    PrintLoseMessage();
                    break;
                }
               
            } 
                        
        }
    }
}

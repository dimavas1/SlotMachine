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
            int counter = 0;

            
            var start = PrintStartGame(coints);

            while (start.Key == ConsoleKey.Y)
            {                
                PrintAvaliableGameModes(coints);
                do
                {
                    playmode = GetGameMode();

                    if (playmode == GameModes.Invalid)
                    {
                        PrintPressValidKey();
                    }

                } while (playmode == GameModes.Invalid);
                
                int bid = GetGameModeBid(playmode);
                
                while ((coints-bid)<=0 && counter < 3)
                {
                    PrintNotEnoughtCoint();
                    playmode = GetGameMode();
                    bid = GetGameModeBid(playmode);
                    counter++;
                }

                if (counter >= 3)
                {
                    PrintLoseMessage();
                    break;
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
                    Console.ReadKey();
                    start = PrintContinuePlay(coints);
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

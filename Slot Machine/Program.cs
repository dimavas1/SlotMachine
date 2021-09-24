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
            GameModes playmode;
            int win;
            int counter = 0;

            
            var start = UIBase.PrintStartGame(coints);

            while (start.Key == ConsoleKey.Y)
            {
                UIBase.PrintAvaliableGameModes(coints);
                do
                {
                    playmode = UIBase.GetGameMode();

                    if (playmode == GameModes.Invalid)
                    {
                        UIBase.PrintPressValidKey();
                    }

                } while (playmode == GameModes.Invalid);
                
                int bid = UIBase.GetGameModeBid(playmode);
                
                while ((coints-bid)<=0 && counter < 3)
                {
                    UIBase.PrintNotEnoughtCoint();
                    playmode = UIBase.GetGameMode();
                    bid = UIBase.GetGameModeBid(playmode);
                    counter++;
                }

                if (counter >= 3)
                {
                    UIBase.PrintLoseMessage();
                    break;
                }

                coints += -bid;

                while (!Console.KeyAvailable)
                {
                    slotData = UIBase.GenerateSlotNumbers();
                    UIBase.PrintSlotNumbers(slotData, playmode);
                }

                win = UIBase.CalculatePrize(slotData, playmode);
                coints += win;

                UIBase.PrintWinMessage(win);
                
                
                if (coints>0)
                {                    
                    Console.ReadKey();
                    start = UIBase.PrintContinuePlay(coints);
                }
                else
                {
                    UIBase.PrintLoseMessage();
                    break;
                }
               
            } 
                        
        }
    }
}

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

            while (start.Key == ConsoleKey.Y)
            {
                UIBase.PrintAvaliableGameModes(coints);
                do
                {
                    playMode = UIBase.GetGameMode();

                    if (playMode == GameModes.Invalid)
                    {
                        UIBase.PrintPressValidKey();
                    }

                } while (playMode == GameModes.Invalid);

                int bid = UIBase.GetGameModeBid(playMode);

                while ((coints - bid) <= 0 && counter < 3)
                {
                    UIBase.PrintNotEnoughtCoint();
                    playMode = UIBase.GetGameMode();
                    bid = UIBase.GetGameModeBid(playMode);
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
                    UIBase.PrintSlotNumbers(slotData, playMode);
                }

                win = UIBase.CalculatePrize(slotData, playMode);
                coints += win;

                UIBase.PrintWinMessage(win);


                if (coints > 0)
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

using System;

namespace Slot_Machine
{
    internal class Program
    {
        public static readonly Random random = new Random();
        static void Main(string[] args)
        {
            const int SLOT_MACHINE_LENGTH = 3;
            const int RANDOM_MAX = 9;
            const char YES_CHAR = 'Y';

            int randomNumber = 0;
            //test

            int[,] slotMachine2dArray = new int[SLOT_MACHINE_LENGTH, SLOT_MACHINE_LENGTH];

            bool gameActive = true;
            while (gameActive)
            {
                
                for (int row = 0; row < SLOT_MACHINE_LENGTH; row++)
                {
                    for (int col = 0; col < SLOT_MACHINE_LENGTH; col++)
                    {
                        randomNumber = random.Next(0, RANDOM_MAX);
                        slotMachine2dArray[row, col] = randomNumber;
                        Console.Write(slotMachine2dArray[row, col] + " ");
                    }
                    Console.WriteLine();
                }
                //Row win/lose
                for (int row = 0; row < SLOT_MACHINE_LENGTH; row++)
                {
                    bool isWin = true;

                    for (int col = 0; col < SLOT_MACHINE_LENGTH - 1; col++)
                    {
                        if (slotMachine2dArray[row, col] != slotMachine2dArray[row, col + 1])
                        {
                            isWin = false;
                        }
                    }
                    if (isWin)
                    {
                        Console.WriteLine("You win row " + (row + 1));
                    }
                    else
                    {
                        Console.WriteLine("You lose row " + (row + 1));
                    }

                }

                //Column win/lose
                for (int col = 0; col < SLOT_MACHINE_LENGTH; col++)
                {
                    bool isWin = true;

                    for (int row = 0; row < SLOT_MACHINE_LENGTH - 1; row++)
                    {
                        if (slotMachine2dArray[row, col] != slotMachine2dArray[row + 1, col])
                        {
                            isWin = false;

                        }
                    }
                    if (isWin)
                    {
                        Console.WriteLine("You win column " + (col + 1));
                    }
                    else
                    {
                        Console.WriteLine("You lose column " + (col + 1));
                    }

                }


                Console.WriteLine("Do you want to play again? Press Y or y");
                char replay = char.ToUpper(Console.ReadKey().KeyChar);
                gameActive = (replay == YES_CHAR);
                Console.Clear();
            }
        }
    }
}
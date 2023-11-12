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
            const int STARTING_BALANCE = 25;
            const int BET = 3;
            const char YES_CHAR = 'Y';

            int balance = STARTING_BALANCE;
            int randomNumber = 0;
            //test

            int[,] slotMachine2dArray = new int[SLOT_MACHINE_LENGTH, SLOT_MACHINE_LENGTH];

            bool gameActive = true;
            while (gameActive)
            {
                Console.WriteLine("Your balance is: " + balance);
                balance = balance - BET;
                for (int row = 0; row < SLOT_MACHINE_LENGTH; row++)
                {
                    for (int col = 0; col < SLOT_MACHINE_LENGTH; col++)
                    {
                        randomNumber = random.Next(0, RANDOM_MAX + 1);
                        slotMachine2dArray[row, col] = randomNumber;
                        Console.Write(slotMachine2dArray[row, col] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Your balance after betting is: " + balance);

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
                        balance++;
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
                        balance++;
                    }
                    else
                    {
                        Console.WriteLine("You lose column " + (col + 1));
                    }

                }


                bool isWinRL = true;
                bool isWinLR = true;

                //Diag win/lose
                for (int i = 0; i < SLOT_MACHINE_LENGTH - 1; i++)
                {
                    if (slotMachine2dArray[i, i] != slotMachine2dArray[i + 1, i + 1])
                    {
                        isWinLR = false;
                    }

                    if (slotMachine2dArray[i, SLOT_MACHINE_LENGTH - i - 1] != slotMachine2dArray[i + 1, SLOT_MACHINE_LENGTH - i - 2])
                    {
                        isWinRL = false;
                    }
                }

                if (isWinLR)
                {
                    Console.WriteLine("You win horizontally left to right");
                    balance++;
                }
                else
                {
                    Console.WriteLine("You lose horizontally left to right");
                }

                if (isWinRL)
                {
                    Console.WriteLine("You win horizontally right to left");
                    balance++;
                }
                else
                {
                    Console.WriteLine("You lose horizontally right to left");
                }

                Console.WriteLine("Do you want to play again? Press Y or y");
                char replay = char.ToUpper(Console.ReadKey().KeyChar);
                gameActive = (replay == YES_CHAR);
                Console.Clear();
            }
        }
    }
}
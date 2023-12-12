using System;

namespace Slot_Machine
{
    internal class Program
    {
        public static readonly System.Random random = new Random();

        static void Main(string[] args)
        {
			GameUI ui = new GameUI();
			GameLogic logic = new GameLogic();
            const int SLOT_MACHINE_LENGTH = 3;
            const int CENTER_ROW = (SLOT_MACHINE_LENGTH - 1) / 2;
            const int RANDOM_MAX = 9;
            const int STARTING_BALANCE = 100;
            const int MIN_BET = 1;
            const int BET_TWO = 2;
            const int BET_THREE = 3;
            const int MAX_BET = 4;
            const int WIN_AMOUNT = 35;
            const ConsoleKey PLAY_AGAIN_KEY = ConsoleKey.Enter;

            int bet = 1; //default bet
            int balance = STARTING_BALANCE;
            int randomNumber = 0;

            int[,] slotMachine2dArray = new int[SLOT_MACHINE_LENGTH, SLOT_MACHINE_LENGTH];

            bool gameActive = true;
            while (gameActive)
            {
				ui.DisplayGameInstructions(balance);
                
                while (true)
                {
					bet = GameLogic.selectBet("Enter your bet: 1,2,3 or 4: ", balance);

                Console.WriteLine($"You have bet {bet} credits.");

                balance = balance - bet;
                Console.WriteLine();
                Console.WriteLine("---------------");
                for (int row = 0; row < SLOT_MACHINE_LENGTH; row++)
                {
                    Console.Write("|- ");
                    for (int col = 0; col < SLOT_MACHINE_LENGTH; col++)
                    {
                        randomNumber = random.Next(0, RANDOM_MAX + 1);
                        slotMachine2dArray[row, col] = randomNumber;
                        Console.Write(slotMachine2dArray[row, col] + " -");
                        if (col < 2)
                        {
                            Console.Write(" ");
                        }

                    }
                    Console.Write("|");
                    Console.WriteLine();
                    if (row < SLOT_MACHINE_LENGTH - 1)
                    {
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("---------------");
                Console.WriteLine();

                Console.WriteLine($"Your balance after betting is: {balance}");

                bool isWin = true;

                //Center row win logic - bet 1
                if (bet == MIN_BET)
                {
                    for (int col = 0; col < SLOT_MACHINE_LENGTH - 1; col++)
                    {
                        if (slotMachine2dArray[CENTER_ROW, col] != slotMachine2dArray[CENTER_ROW, col + 1]) ;
                        {
                            isWin = false;
                            break;
                        }
                    }

                    if (isWin)
                    {
                        Console.WriteLine($"You win the center row +{WIN_AMOUNT} credits has been added to your balance!");
                    }
                }


                //Row win logic - bet 2
                if (bet == BET_TWO)
                {
                    for (int row = 0; row < SLOT_MACHINE_LENGTH; row++)
                    {
                        isWin = true;

                        for (int col = 0; col < SLOT_MACHINE_LENGTH - 1; col++)
                        {
                            if (slotMachine2dArray[row, col] != slotMachine2dArray[row, col + 1])
                            {
                                isWin = false;

                            }
                        }

                        if (isWin)
                        {
                            Console.WriteLine($"You win row {(row + 1)} +{WIN_AMOUNT} credits has been added to your balance!");
                            balance += WIN_AMOUNT;
                        }
                    }
                }

                //Column win logic - bet 3
                if (bet == BET_THREE)
                {
                    for (int col = 0; col < SLOT_MACHINE_LENGTH; col++)
                    {
                        isWin = true;

                        for (int row = 0; row < SLOT_MACHINE_LENGTH - 1; row++)
                        {
                            if (slotMachine2dArray[row, col] != slotMachine2dArray[row + 1, col])
                            {
                                isWin = false;

                            }
                        }

                        if (isWin)
                        {
                            Console.WriteLine($"You win column {(col + 1)} +{WIN_AMOUNT} credits has been added to your balance!");
                            balance += WIN_AMOUNT;
                            break;
                        }
                    }
                }

                bool isWinRL = true;
                bool isWinLR = true;

                // Diagonal win logic - bet 4
                if (bet == MAX_BET)
                {
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
                        Console.WriteLine($"You win diagonally, from left to right. +{WIN_AMOUNT} credits has been added to your balance!");
                        balance += WIN_AMOUNT;
                    }
                    if (isWinRL)
                    {
                        Console.WriteLine($"You win diagonally, from right to left. +{WIN_AMOUNT} credits has been added to your balance!");
                        balance += WIN_AMOUNT;
                    }
                }

                Console.WriteLine("Do you want to play again? Press Enter");
                ConsoleKeyInfo replay = Console.ReadKey();
                gameActive = (replay.Key == PLAY_AGAIN_KEY);
                Console.Clear();
            }
        }
    }
	public class GameUI
	{
		public void DisplayGameInstructions(int balance)
		{
		Console.WriteLine("Welcome to Anub's Slot Machine. Below are instructions on how to play...");
        Console.WriteLine("-----------------------------------------------------------------------------");
        Console.WriteLine("This is a 3x3 slot machine where 3 matches numbers in a line win.");
        Console.WriteLine($" - Bet 1 checks the center row for a win.");
		Console.WriteLine($" - Bet 2 checks rows for a win.");
		Console.WriteLine($" - Bet 3 checks columns for a win.");
		Console.WriteLine($" - Bet 4 checks diagonals for a win.");
		Console.WriteLine("-----------------------------------------------------------------------------");
		Console.WriteLine($"Your balance is: {balance}");
		Console.WriteLine("-----------------------------------------------------------------------------");
		}
	}
	public class GameLogic
	{
		public static int selectBet(string prompt, int balance)
		{
			while (true) // Loop infinitely until a break or return. Used to continuously prompt for input until a valid integar is entered.
       		{
			Console.WriteLine(prompt);
			string input = Console.ReadLine();
			if(int.TryParse(input, out int bet) && bet > 0 && bet < 5)
			{
			   if (bet <= balance)
			   {
				   return bet;
			   }
			}
			Console.WriteLine("Invalid Input. Please enter a valid bet, from 1-4.");
			}
		}
	}
}
}

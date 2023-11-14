using System.Diagnostics;

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
            const int MIN_BET = 1;
            const int MAX_BET = 3;
            const int WIN_AMOUNT = 35;
            const int TOP_ROW = 1;
            const int CENTER_ROW = 2;
            const int BOTTOM_ROW = 3;
            const ConsoleKey PLAY_AGAIN_KEY = ConsoleKey.Enter;

            int bet = 1; //default bet
            int lastBet = bet;
            int balance = STARTING_BALANCE;
            int randomNumber = 0;

            int[,] slotMachine2dArray = new int[SLOT_MACHINE_LENGTH, SLOT_MACHINE_LENGTH];

            Console.WriteLine("Welcome to Anub's Slot Machine. Below are instructions on how to play...\n");
            Console.WriteLine("A bet of 1 checks the center row for a win.");
            Console.WriteLine("A bet of 2 checks the center and bottom row for a win.");
            Console.WriteLine("A bet of 3 checks all rows, columns and both diagonals (left to right and right to left).");

            bool gameActive = true;
            while (gameActive)
            {

                Console.WriteLine("You must place a max bet (3) to win all lines.\n");
                Console.WriteLine($"Your balance is: {balance}");
                Console.WriteLine("Use the up/down arrows to select the amount to bet. Press enter to confirm.\n");
  

                int cursorTopPosition = Console.CursorTop; // Remember the cursor position

                bet = lastBet; // Start with the last bet
                Console.WriteLine($"What would you like to bet: {bet} credits\n");
                Console.WriteLine("Press Enter to play!");

                while (true)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.UpArrow) // Cycle up bet: 1,2,3 -> 1,2,3
                    {
                        bet = bet % MAX_BET + 1;
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow) // Cycle down bet: 1 -> 3,2,1 -> 3,2,1
                    {
                        bet = (bet == MIN_BET) ? MAX_BET : bet - 1;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter) // When user presses Enter 
                    {
                        Console.WriteLine(); // Move to the next line after confirming the bet
                        break;
                    }

                    // Update the current line with the new bet
                    Console.SetCursorPosition(0, cursorTopPosition);
                    Console.Write($"Current bet: {bet} credits");
                    Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft)); // Clear the rest of the line

                }

                lastBet = bet; // Store bet for next round
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
                        switch (bet)
                        {
                            case 1:
                                if (row == CENTER_ROW) //Center horizontal row
                                {
                                    Console.WriteLine($"You win the center row! +{WIN_AMOUNT} credits has been added to your balance!");
                                    balance += WIN_AMOUNT;
                                }
                                break;
                            case 2:
                                if (row == BOTTOM_ROW || row == CENTER_ROW) //Center and bottom horizontal rows
                                {
                                    Console.WriteLine($"You win the bottom row! +{WIN_AMOUNT} credits has been added to your balance!");
                                    balance += WIN_AMOUNT;
                                }
                                break;
                            case 3:
                                if (row == BOTTOM_ROW || row == CENTER_ROW || row == TOP_ROW) //All 3 horizontal rows
                                {
                                    Console.WriteLine($"You win the top row! +{WIN_AMOUNT} credits has been added to your balance!");
                                    balance += WIN_AMOUNT;
                                }
                                break;
                        }
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
                        switch (bet)
                        {
                            // The commented code below does not apply as the user will not win any vertical columns if they
                            // don't use MAX_BET which is 3. This code was copied over from the row win logic and is N/A here. 
                            //case 1:
                            //    if (col == 1) //Center row
                            //    {
                            //        Console.WriteLine("You win col " + (col + 1) + " +WIN_AMOUNT credits has been added to your balance!");
                            //        balance += WIN_AMOUNT;
                            //    }
                            //    break;
                            //case 2:
                            //    if (col == 1 || col == 2) //Center and bottom rows
                            //    {
                            //        Console.WriteLine("You win col " + (col + 1) + " +WIN_AMOUNT credits has been added to your balance!");
                            //        balance += WIN_AMOUNT;
                            //    }
                            //    break;
                            case 3:
                                if (col == 1 || col == 2 || col == 3) //All 3 vertical columens
                                {
                                    Console.WriteLine($"You win column {(col + 1)} +{WIN_AMOUNT} credits has been added to your balance!");
                                    balance += WIN_AMOUNT;
                                }
                                break;
                        }
                    }

                }

                bool isWinRL = true;
                bool isWinLR = true;

                // Diagonal win/lose
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
                if (bet == 3)
                {
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
}
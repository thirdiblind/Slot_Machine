using System;

namespace Slot_Machine
{
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
}


using System;

namespace Slot_Machine
{
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


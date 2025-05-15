using System;
using System.Collections.Generic;
using PlayerManagerMVC.Model;

namespace PlayerManagerMVC.View
{
    public class PlayerView
    {
        public void ShowMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("----\n");
            Console.WriteLine("1. Insert player");
            Console.WriteLine("2. List all players");
            Console.WriteLine("3. List players with score greater than");
            Console.WriteLine("4. Sort players");
            Console.WriteLine("0. Quit\n");
            Console.Write("Your choice > ");
        }

        public void DisplayPlayers(IEnumerable<Player> playersToList)
        {
            Console.WriteLine("Players:");
            Console.WriteLine("--------");

            foreach (Player p in playersToList)
            {
                Console.WriteLine($"{p.Name} with a score of ({p.Score})");
            }
            Console.WriteLine();
        }

        public (string name, int score) GetNewPlayerInfo()
        {
            Console.Write("Enter player name: ");
            string name = Console.ReadLine();

            Console.Write("Enter player score: ");
            int score = int.Parse(Console.ReadLine());

            return (name, score);
        }

        public int GetMinimumScore()
        {
            Console.Write("Enter minimum score: ");
            return int.Parse(Console.ReadLine());
        }

        public PlayerOrder GetSortOrder()
        {
            Console.WriteLine("Sorting options:");
            Console.WriteLine("1. By score");
            Console.WriteLine("2. By name");
            Console.WriteLine("3. By name (reverse)");
            Console.Write("Your choice > ");
            int choice = int.Parse(Console.ReadLine());

            return Enum.Parse<PlayerOrder>(Console.ReadLine());
        }

        public void WaitForKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void DisplayGoodbye()
        {
            Console.WriteLine("Bye!");
        }

        public void DisplayError(string message)
        {
            Console.WriteLine($"Error: {message}");
        }
    }
}
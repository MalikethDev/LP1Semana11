using System;
using System.Collections.Generic;

namespace PlayerManagerMVC
{
    public static class PlayerView
    {
        public static void ShowMenu()
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

        public static void ListPlayers(IEnumerable<Player> players)
        {
            Console.WriteLine("\nList of players");
            Console.WriteLine("---------------\n");
            foreach (var p in players)
            {
                Console.WriteLine($" -> {p.Name} with a score of {p.Score}");
            }
            Console.WriteLine();
        }

        public static void ShowSortMenu()
        {
            Console.WriteLine("Player order");
            Console.WriteLine("------------");
            Console.WriteLine($"{(int)PlayerOrder.ByScore}. Order by score");
            Console.WriteLine($"{(int)PlayerOrder.ByName}. Order by name");
            Console.WriteLine($"{(int)PlayerOrder.ByNameReverse}. Order by name (reverse)");
            Console.Write("> ");
        }

        public static PlayerOrder AskSortOption()
        {
            while (true)
            {
                if (Enum.TryParse(Console.ReadLine(), out PlayerOrder order))
                    return order;

                Console.Write("Invalid option. Try again: ");
            }
        }

        public static string AskString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public static int AskInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;

                Console.WriteLine("Please enter a valid integer.");
            }
        }
    }
}
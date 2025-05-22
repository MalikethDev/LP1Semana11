using System;
using System.Collections.Generic;
using System.IO;

namespace PlayerManagerMVC2
{
    /// <summary>
    /// The player listing program.
    /// </summary>
    public class Program
    {
        /// The list of all players
        private readonly List<Player> playerList;

        // Comparer for comparing player by name (alphabetical order)
        private readonly IComparer<Player> compareByName;

        // Comparer for comparing player by name (reverse alphabetical order)
        private readonly IComparer<Player> compareByNameReverse;

        /// <summary>
        /// Program begins here.
        /// </summary>
        /// <param name="args">Filename expected</param>
        private static void Main(string[] args)
        {
            // Ensure a filename was passed in
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Error: Please provide a file name as an argument.");
                Environment.Exit(1);
            }

            string fileName = args[0];

            // Create and start the program
            Program prog = new Program(fileName);
            prog.Start();
        }

        /// <summary>
        /// Creates a new instance of the player listing program.
        /// </summary>
        private Program(string fileName)
        {
            // Initialize player comparers
            compareByName = new CompareByName(true);
            compareByNameReverse = new CompareByName(false);

            // Load players from file
            playerList = LoadPlayersFromFile(fileName);
        }

        /// <summary>
        /// Start the player listing program instance
        /// </summary>
        private void Start()
        {
            string option;

            do
            {
                ShowMenu();
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        InsertPlayer();
                        break;
                    case "2":
                        ListPlayers(playerList);
                        break;
                    case "3":
                        ListPlayersWithScoreGreaterThan();
                        break;
                    case "4":
                        SortPlayerList();
                        break;
                    case "0":
                        Console.WriteLine("Bye!");
                        break;
                    default:
                        Console.Error.WriteLine("\n>>> Unknown option! <<<\n");
                        break;
                }

                Console.Write("\nPress any key to continue...");
                Console.ReadKey(true);
                Console.WriteLine("\n");

            } while (option != "0");
        }

        /// <summary>
        /// Shows the main menu.
        /// </summary>
        private void ShowMenu()
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

        /// <summary>
        /// Inserts a new player in the player list.
        /// </summary>
        private void InsertPlayer()
        {
            Console.WriteLine("\nInsert player");
            Console.WriteLine("-------------\n");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Score: ");
            if (!int.TryParse(Console.ReadLine(), out int score))
            {
                Console.WriteLine("Invalid score. Please enter an integer.");
                return;
            }

            Player newPlayer = new Player(name, score);
            playerList.Add(newPlayer);
        }

        /// <summary>
        /// Show all players in a list of players.
        /// </summary>
        private static void ListPlayers(IEnumerable<Player> playersToList)
        {
            Console.WriteLine("\nList of players");
            Console.WriteLine("-------------\n");

            foreach (Player p in playersToList)
            {
                Console.WriteLine($" -> {p.Name} with a score of {p.Score}");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Show all players with a score higher than a user-specified value.
        /// </summary>
        private void ListPlayersWithScoreGreaterThan()
        {
            Console.Write("\nMinimum score player should have? ");
            if (!int.TryParse(Console.ReadLine(), out int minScore))
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            IEnumerable<Player> filteredPlayers = GetPlayersWithScoreGreaterThan(minScore);
            ListPlayers(filteredPlayers);
        }

        /// <summary>
        /// Get players with a score higher than a given value.
        /// </summary>
        private IEnumerable<Player> GetPlayersWithScoreGreaterThan(int minScore)
        {
            foreach (Player p in playerList)
            {
                if (p.Score > minScore)
                    yield return p;
            }
        }

        /// <summary>
        ///  Sort player list by the order specified by the user.
        /// </summary>
        private void SortPlayerList()
        {
            Console.WriteLine("Player order");
            Console.WriteLine("------------");
            Console.WriteLine($"{(int)PlayerOrder.ByScore}. Order by score");
            Console.WriteLine($"{(int)PlayerOrder.ByName}. Order by name");
            Console.WriteLine($"{(int)PlayerOrder.ByNameReverse}. Order by name (reverse)");
            Console.WriteLine("");
            Console.Write("> ");

            if (!Enum.TryParse(Console.ReadLine(), out PlayerOrder playerOrder))
            {
                Console.Error.WriteLine("\n>>> Invalid option. <<<\n");
                return;
            }

            switch (playerOrder)
            {
                case PlayerOrder.ByScore:
                    playerList.Sort();
                    break;
                case PlayerOrder.ByName:
                    playerList.Sort(compareByName);
                    break;
                case PlayerOrder.ByNameReverse:
                    playerList.Sort(compareByNameReverse);
                    break;
                default:
                    Console.Error.WriteLine("\n>>> Unknown player order! <<<\n");
                    break;
            }
        }

        /// <summary>
        /// Loads player data from the given file.
        /// </summary>
        private List<Player> LoadPlayersFromFile(string fileName)
        {
            var list = new List<Player>();

            try
            {
                string[] lines = File.ReadAllLines(fileName);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length == 2)
                    {
                        string name = parts[0].Trim();
                        if (int.TryParse(parts[1].Trim(), out int score))
                        {
                            list.Add(new Player(name, score));
                        }
                        else
                        {
                            Console.Error.WriteLine($"Invalid score in line: {line}");
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine($"Invalid format in line: {line}");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine($"File error: {ex.Message}");
                Environment.Exit(1);
            }

            return list;
        }
    }
}
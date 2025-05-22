using System;
using System.Collections.Generic;

namespace PlayerManagerMVC2
{
    public class PlayerController
    {
        private readonly List<Player> playerList;
        private readonly IComparer<Player> compareByName;
        private readonly IComparer<Player> compareByNameReverse;

        public PlayerController()
        {
            compareByName = new CompareByName(true);
            compareByNameReverse = new CompareByName(false);

            playerList = new List<Player>() {
                new Player("Best player ever", 100),
                new Player("An even better player", 500)
            };
        }

        public void Run()
        {
            string option;
            do
            {
                PlayerView.ShowMenu();
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        InsertPlayer();
                        break;
                    case "2":
                        PlayerView.ListPlayers(playerList);
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
                Console.WriteLine();

            } while (option != "0");
        }

        private void InsertPlayer()
        {
            Console.WriteLine("\nInsert player");
            Console.WriteLine("-------------\n");

            string name = PlayerView.AskString("Name: ");
            int score = PlayerView.AskInt("Score: ");

            playerList.Add(new Player(name, score));
        }

        private void ListPlayersWithScoreGreaterThan()
        {
            int minScore = PlayerView.AskInt("\nMinimum score player should have? ");
            var filtered = GetPlayersWithScoreGreaterThan(minScore);
            PlayerView.ListPlayers(filtered);
        }

        private IEnumerable<Player> GetPlayersWithScoreGreaterThan(int minScore)
        {
            foreach (var p in playerList)
            {
                if (p.Score > minScore)
                    yield return p;
            }
        }

        private void SortPlayerList()
        {
            PlayerView.ShowSortMenu();
            PlayerOrder order = PlayerView.AskSortOption();

            switch (order)
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
    }
}

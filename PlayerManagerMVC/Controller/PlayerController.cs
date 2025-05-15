using System;
using System.Collections.Generic;
using PlayerManagerMVC.Model;
using PlayerManagerMVC.View;

namespace PlayerManagerMVC.Controller
{
    /// <summary>
    /// The player controller.
    /// </summary>
    public class PlayerController
    {
        // The list of all players
        private readonly List<Player> playerList;

        // Comparer for comparing player by name (alphabetical order)
        private readonly IComparer<Player> compareByName;

        // Comparer for comparing player by name (reverse alphabetical order)
        private readonly IComparer<Player> compareByNameReverse;

        // The view to show the players
        private readonly PlayerView view;

        /// <summary>
        /// Creates a new instance of the player controller.
        /// </summary>
        public PlayerController()
        {
            // Initialize the view
            view = new PlayerView();
            // Initialize the player list
            compareByName = new CompareByName(true);
            compareByNameReverse = new CompareByName(false);

            // Initialize the player list with two players using collection
            // initialization syntax
            playerList = new List<Player>() {
                new Player("Best player ever", 100),
                new Player("An even better player", 500)
            };
        }
        public void Start()
        {
            string option;
            do
            {
                view.ShowMenu();
                option = Console.ReadLine();
                // Check if the user input is null or empty

                // Perform action based on user option
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
                        view.DisplayGoodbye();
                        break;
                    default:
                        break;
                }
            } while (option != "0");
        }

        private void InsertPlayer()
        {
            // Get player information from the user
            var (name, score) = view.GetNewPlayerInfo();

            // Create a new player and add it to the list
            var player = new Player(name, score);
            playerList.Add(player);

            // Show the updated list of players
            view.DisplayPlayers(playerList);
        }

        private void ListPlayers(List<Player> players)
        {
            // Show the list of players
            view.DisplayPlayers(playerList);
        }

        private void ListPlayersWithScoreGreaterThan()
        {
            // Get the minimum score from the user
            int minScore = view.GetMinimumScore();

            // Get players with score greater than the specified score
            IEnumerable<Player> players = GetPlayerWithScoreGreaterThan(minScore);

            // Show the list of players with score greater than the specified score
            view.DisplayPlayers(players);
        }

        private IEnumerable<Player> GetPlayerWithScoreGreaterThan(int score)
        {
            // Get players with score greater than the specified score
            foreach (var player in playerList)
            {
                if (player.Score > score)
                {
                    yield return player;
                }
            }
        }

        private void SortPlayerList()
        {
            PlayerOrder option = view.GetSortOrder();

            // Sort the player list based on user choice
            switch (option)
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
                    view.DisplayError("Invalid sorting option.");
                    break;
            }
        }
    }
}
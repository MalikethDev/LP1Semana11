using System;
using System.Collections.Generic;
using System.IO;
using PlayerManagerMVC2.Model;
using PlayerManagerMVC2.View;

namespace PlayerManagerMVC2.Controller
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
        public PlayerController(PlayerView, string filename)
        {
            // Initialize the view
            this.view = view;
            // Initialize the player list
            compareByName = new CompareByName(true);
            compareByNameReverse = new CompareByName(false);

            // Load players from file
            playerList = new List<Player>() {
            LoadPlayersFromFile(filename);
        }


        private void LoadPlayersFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"The file {filename} does not exist.");
            }

            var players = new List<Player>();
            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    {
                        players.Add(new Player(parts[0], score));
                    }
                }
            }
            return players;
        }
        }
        public void Start()
        {
            string option;
            do
            {
                view.ShowMenu();
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        InsertPlayer();
                        break;
                    case "2":
                        ListPlayers();
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
                        view.DisplayError("Unknown option!");
                        break;
                }

                if (option != "0")
                    view.WaitForKey();

            } while (option != "0");
        }

        private void InsertPlayer()
        {
            var (name, score) = view.GetNewPlayerInfo();
            playerList.Add(new Player(name, score));
        }

        private void ListPlayers()
        {
            view.DisplayPlayers(playerList);
        }

        private void ListPlayersWithScoreGreaterThan()
        {
            int minScore = view.GetMinimumScore();
            IEnumerable<Player> filtered = GetPlayersWithScoreGreaterThan(minScore);
            view.DisplayPlayers(filtered);
        }

        private IEnumerable<Player> GetPlayersWithScoreGreaterThan(int minScore)
        {
            foreach (Player p in playerList)
            {
                if (p.Score > minScore)
                {
                    yield return p;
                }
            }
        }

        private void SortPlayerList()
        {
            view.DisplaySortMenu();
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    playerList.Sort(compareByName);
                    break;
                case "2":
                    playerList.Sort(compareByNameReverse);
                    break;
                default:
                    view.DisplayError("Unknown option!");
                    break;
            }
        }
    }
}
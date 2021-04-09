using System;
using System.Collections.Generic;
using System.Linq;

namespace NewServer
{
    public static class GameManager
    {
        public static Dictionary<int, Player> playerList = new Dictionary<int, Player>();

        private static float playerSpeed = 0.1f;

        public static float PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }

        public static void JoinGame(int connectionID, Player player)
        {
            NetworkSend.InstantiateNetworkPlayer(connectionID, player);
        }

        public static void CreatePlayer(int connectionID)
        {
            Player player = new Player()
            {
                connectionID = connectionID,
                inGame = true
            };
            if(!playerList.Any(x=>x.Key == connectionID))
                playerList.Add(connectionID, player);
            Console.WriteLine("Player {0} has been added to the games.", connectionID);
            JoinGame(connectionID, player);
        }
    }
}

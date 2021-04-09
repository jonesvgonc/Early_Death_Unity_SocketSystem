using KaymakNetwork;

namespace NewServer
{
    internal static class NetworkSend
    {
        enum ServerPackets
        {
            SWelcomeMsg = 1,
            SInstantiatePlayer = 2,
            SPlayerMove = 3,
        }

        public static void WelcomeMsg(int connectionId, string msg)
        {
            ByteBuffer buffer = new ByteBuffer(4);

            buffer.WriteInt32((int)ServerPackets.SWelcomeMsg);

            buffer.WriteInt32(connectionId);

            buffer.WriteString(msg);

            NetworkConfig.socket.SendDataTo(connectionId, buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        private static ByteBuffer PlayerData(int connectionID, Player player)
        {
            ByteBuffer buffer = new ByteBuffer(4);
            buffer.WriteInt32((int)ServerPackets.SInstantiatePlayer);
            buffer.WriteInt32(connectionID);

            return buffer;
        }

        public static void InstantiateNetworkPlayer(int connectionID, Player player)
        {
            for (int i = 1; i <= GameManager.playerList.Count; i++)
            {
                if(GameManager.playerList[i] != null)
                {
                    if(GameManager.playerList[i].inGame)
                    {
                        if(i != connectionID)
                        {
                            NetworkConfig.socket.SendDataTo(connectionID, PlayerData(i, player).Data, PlayerData(i, player).Head);
                        }
                    }
                }
            }

            NetworkConfig.socket.SendDataToAll(PlayerData(connectionID, player).Data, PlayerData(connectionID, player).Head);
        }

        public static void SendPlayerMovement(int connectionID, float x, float y, float eulerX, float eulerY, float eulerZ)
        {
            ByteBuffer buffer = new ByteBuffer(4);
            buffer.WriteInt32((int)ServerPackets.SPlayerMove);
            buffer.WriteInt32(connectionID);
            buffer.WriteSingle(x);
            buffer.WriteSingle(y);
            buffer.WriteSingle(eulerX);
            buffer.WriteSingle(eulerY);
            buffer.WriteSingle(eulerZ);

            for (int i = 1; i <= GameManager.playerList.Count; i++)
            {
                if (GameManager.playerList[i] != null)
                {
                    if (GameManager.playerList[i].inGame)
                    {
                        if (i != connectionID)
                        {
                            NetworkConfig.socket.SendDataTo(GameManager.playerList[i].connectionID, buffer.Data, buffer.Head);
                        }
                    }
                }
            }

            buffer.Dispose();
        }
    }
}

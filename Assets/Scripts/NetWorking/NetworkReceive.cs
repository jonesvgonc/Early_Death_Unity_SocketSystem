using KaymakNetwork;
using KaymakNetwork.Network.Client;
using UnityEngine;

namespace Assets.Scripts.NetWorking
{
    enum ServerPackets
    {
        SWelcomeMsg = 1,
        SInstantiatePlayer = 2,
        SPlayerMove = 3,
    }

    internal static class NetworkReceive
    {
        internal static void PacketRouter()
        {
            NetworkConfig.socket.PacketId[(int)ServerPackets.SWelcomeMsg] = new Client.DataArgs(Packet_WelcomeMsg);
            NetworkConfig.socket.PacketId[(int)ServerPackets.SInstantiatePlayer] = new Client.DataArgs(Packet_InstantiateNetworkPlayer);
            NetworkConfig.socket.PacketId[(int)ServerPackets.SPlayerMove] = new Client.DataArgs(Packet_PlayerMove);
        }

        private static void Packet_WelcomeMsg(ref byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer(data);

            int connectionID = buffer.ReadInt32();

            string msg = buffer.ReadString();
            buffer.Dispose();

            NetworkManager.Instance.MyConnectionID = connectionID;

            NetworkSend.SendPing();
        }

        private static void Packet_InstantiateNetworkPlayer(ref byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer(data);
            int connectID = buffer.ReadInt32();
            bool isMyPlayer = false;

            if(connectID == NetworkManager.Instance.MyConnectionID)
            {
                isMyPlayer = true;
            }

            NetworkManager.Instance.InstantiateNetworkPlayer(connectID, isMyPlayer);
        }

        private static void Packet_PlayerMove(ref byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer(data);
            int connectID = buffer.ReadInt32();
            float x = buffer.ReadSingle();
            float y = buffer.ReadSingle();

            float eulerX = buffer.ReadSingle();
            float eulerY = buffer.ReadSingle();
            float eulerZ = buffer.ReadSingle();

            buffer.Dispose();

            if (!GameManager.Instance.playerList.ContainsKey(connectID)) return;

            GameManager.Instance.playerList[connectID].transform.position = new Vector3(x, y, GameManager.Instance.playerList[connectID].transform.position.z);
            GameManager.Instance.playerList[connectID].transform.eulerAngles = new Vector3(eulerX, eulerY, eulerZ);

        }
    }
}

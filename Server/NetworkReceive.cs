using System;
using System.Numerics;
using KaymakNetwork;

namespace NewServer
{
    public enum ClientPackets
    {
        CPing = 1,
        CLocalPosition,
    }

    public static class NetworkReceive
    {
        internal static void PacketRouter()
        {
            NetworkConfig.socket.PacketId[(int)ClientPackets.CPing] = Packet_Ping;
            NetworkConfig.socket.PacketId[(int)ClientPackets.CLocalPosition] = Packet_LocalPosition;
        }

        private static void Packet_Ping(int connectionID, ref byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer(data);

            string msg = buffer.ReadString();

            Console.WriteLine(msg);

            GameManager.CreatePlayer(connectionID);

            buffer.Dispose();
        }

        private static void Packet_LocalPosition(int connectionID, ref byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer(data);
            Vector2 localPosition = new Vector2();
            Vector3 localRotation = new Vector3();
            localPosition.X = buffer.ReadSingle();
            localPosition.Y = buffer.ReadSingle();

            localRotation.X = buffer.ReadSingle();
            localRotation.Y = buffer.ReadSingle();
            localRotation.Z = buffer.ReadSingle();

            buffer.Dispose();

            InputManager.TryToMove(connectionID, localPosition, localRotation);
        }
    }
}

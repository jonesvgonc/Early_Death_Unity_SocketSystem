using KaymakNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.NetWorking
{
    public enum ClientPackets
    {
        CPing = 1,
        CLocalPosition,
    }

    internal static class NetworkSend
    {
        public static void SendPing()
        {
            ByteBuffer buffer = new ByteBuffer(4);

            buffer.WriteInt32((int)ClientPackets.CPing);
            buffer.WriteString("Hello, I am the client, thank you");
            NetworkConfig.socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendNetworkPosition(Vector2 localPosition, Vector3 localRotation)
        {
            ByteBuffer buffer = new ByteBuffer(4);

            buffer.WriteInt32((int)ClientPackets.CLocalPosition);
            buffer.WriteSingle(localPosition.X);
            buffer.WriteSingle(localPosition.Y);

            buffer.WriteSingle(localRotation.X);
            buffer.WriteSingle(localRotation.Y);
            buffer.WriteSingle(localRotation.Z);

            NetworkConfig.socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        //public static void SendKeyInput(InputManager.keys pressedKey, InputManager.keys pressedSecondKey)
        //{
        //    ByteBuffer buffer = new ByteBuffer(4);

        //    buffer.WriteInt32((int)ClientPackets.CKeyInput);
        //    buffer.WriteByte((byte)pressedKey);
        //    buffer.WriteByte((byte)pressedSecondKey);
        //    NetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        //    buffer.Dispose();
        //}



    }


}

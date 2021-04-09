using KaymakNetwork.Network.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.NetWorking
{
    internal static class NetworkConfig
    {
        internal static Client socket;
        internal static void InitNetwork()
        {
            if (!ReferenceEquals(socket, null)) return;

            socket = new Client(100);
            NetworkReceive.PacketRouter();
        }

        internal static void ConnectToServer()
        {
            socket.Connect("localhost", 5555);
        }

        internal static void DisconnectFromServer()
        {
            socket.Dispose();
        }

    }
}

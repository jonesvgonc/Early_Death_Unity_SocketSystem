using System;
using System.Numerics;

namespace NewServer
{
    public class InputManager
    {
        public enum Keys
        {
            None,
            W,
            A,
            S,
            D
        }

        public static void TryToMove(int connectionID, Vector2 localPosition, Vector3 localRotation)
        {
            GameManager.playerList[connectionID].position = new Vector3(localPosition.X, localPosition.Y, 0);
            GameManager.playerList[connectionID].eulerAngles = localRotation;
            NetworkSend.SendPlayerMovement(connectionID, GameManager.playerList[connectionID].position.X, GameManager.playerList[connectionID].position.Y, GameManager.playerList[connectionID].eulerAngles.X, GameManager.playerList[connectionID].eulerAngles.Y, GameManager.playerList[connectionID].eulerAngles.Z);
        }
    }
}
using System;
using System.Numerics;
namespace BrickServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet) 
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            Server.clients[_fromClient].SendIntoGame(_username);
        }

        public static void SetScore(int _fromClient, Packet _packet) //player movement //currentScore
        {
            //int _clientIdCheck = _packet.ReadInt();
            int _currentScore = _packet.ReadInt();
            Console.WriteLine(_currentScore);

          Console.WriteLine($"Player \"{Server.clients[_fromClient].player.username}\" (ID: {_fromClient}) has currentScore ({_currentScore})!");
          Server.clients[_fromClient].player.SetInputScore(_currentScore);
        }

        public static void SetMove(int _fromClient, Packet _packet)
        {
            bool _moveResponse = _packet.ReadBool();
            if(Server.clients[_fromClient].player != null)
            {
                Server.clients[_fromClient].player.setPressedMove(_fromClient, _moveResponse);
                Console.WriteLine($"Player \"{Server.clients[_fromClient].player.username}\" (ID: {_fromClient}) has move Response:  ({_moveResponse})!");
            }
        }

        public static void SetTime(int _fromClient, Packet _packet)
        {
            int _currentTime = _packet.ReadInt();
            if(Server.clients[_fromClient].player != null)
            {
                Server.clients[_fromClient].player.SetDurationTime(_currentTime);
                Console.WriteLine($"Player \"{Server.clients[_fromClient].player.username}\" (ID: {_fromClient}) has currentTime ({_currentTime})!");
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BrickServer
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }

        }
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }


        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
              // Console.WriteLine($"Serversend.cs spawn player. username is {_player.username} sent to Client number" + _toClient);

                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.currentScore);
                _packet.Write(_player.totalScore);
                _packet.Write(_player.isMovePressed);

                SendTCPData(_toClient, _packet);
            }
        }

       public static void currentScore(int _ClientId, int _currentScore) //should also sent other persons' score
        {
           
            using (Packet _packet = new Packet((int)ServerPackets.currentScore))
            {
                _packet.Write(_ClientId);
               _packet.Write(_currentScore);

                SendTCPDataToAll(_packet); 
                //SendTCPData(_ClientId, _packet);
            }
        }

        public static void currentTime(int _ClientId, int _currentTime)
        {
            using(Packet _packet = new Packet((int)ServerPackets.currentTime))
            {
                _packet.Write(_ClientId);
                _packet.Write(_currentTime);
                SendTCPDataToAll(_packet);
            }
        }

        public static void pressedMove(int _ClientId, bool _isMovePressed)
        {
            if(_isMovePressed == true)
            {
                Console.WriteLine(Server.clients[_ClientId].player.username + " is pressed " + _isMovePressed.ToString() + "in ServerSend.cs");

            }
         
            using(Packet _packet = new Packet((int)ServerPackets.isMovePressed))
            {
                _packet.Write(_ClientId);
                _packet.Write(_isMovePressed);
                SendTCPDataToAll(_packet);
            }

        }

        #endregion
    }
}
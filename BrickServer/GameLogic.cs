using System;
using System.Collections.Generic;
using System.Text;

namespace BrickServer
{
    public class GameLogic
    {
        public static void Update()
        {

            foreach(Client _client in Server.clients.Values)
            {
                if(_client.player != null)
                {
                    // _client.player.UpdateScore();
                    _client.SendCurrentValuetoGame();
                   // _client.SendIntoGame();
                }
            }
            ThreadManager.UpdateMain();
        }
    }
}

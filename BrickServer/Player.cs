using System;
using System.Numerics;

namespace BrickServer
{
    public class Player
    {
        public int id;
        public string username;
        public int currentScore;
        public int totalScore;
        public bool isMovePressed = false;
        public int playerDurationLeftForRound;


        public Player(int _id, string _username, int _currScor, int _totScor)
        {
            id = _id;
            username = _username;
            totalScore = _totScor;
            currentScore = _currScor;
            isMovePressed = false;
            Console.WriteLine($"Player is created. Current ID is {this.id.ToString()}.");
        }

        public void SetInputScore(int _currentScore)
        {
            currentScore = _currentScore;
            totalScore = totalScore + _currentScore; //next step: send current score to count
            Console.WriteLine($"player.cs setinput score");
            UpdateValues();
        }

        public void SetDurationTime(int _currentTime)
        {
            playerDurationLeftForRound = _currentTime;
            UpdateValues();
        }

        public void setPressedMove(int _fromClient, bool _moveResponse)
        {
            Server.clients[_fromClient].player.isMovePressed = _moveResponse;
            ServerSend.pressedMove(_fromClient, _moveResponse);
            //isMovePressed = _moveResponse;
            Console.WriteLine(Server.clients[_fromClient].player.username + "move is " + _moveResponse.ToString()+ "in player.cs");
            UpdateValues();
        }

        public void UpdateValues() //mve
        {
            ServerSend.currentScore(id, currentScore); //playerpos
            ServerSend.currentTime(id, playerDurationLeftForRound);
            //ServerSend.pressedMove(id, isMovePressed);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.Core;
using WhoPK.GameLogic.World.Area;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Commands.Movement
{
    public class Communication : ICommunication
    {
        private readonly IWriteToClient _writeToClient;
        private readonly ICache _cache;


        public Communication(IWriteToClient writeToClient, ICache cache)
        {
            _writeToClient = writeToClient;
            _cache = cache; 
        }

        public void Say(Room room, Player character, string message)
        {
            foreach (var player in room.Players)
            {
                if (character.Name != player.Name)
                {
                    _writeToClient.WriteLine($"{character.Name} says '{message}'", player.ConnectionId);
                }
            }
        }

        public void Tell(Player fromCharacter, Player toCharacter, string message)
        {
            throw new NotImplementedException();
        }

        public void Yell(Area area, Player character, string message)
        {
            throw new NotImplementedException();
        }
    }
}

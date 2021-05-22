using Artemis;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.Core;
using WhoPK.GameLogic.Core.Component;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Commands.Movement
{
    public class Movement : IMovement
    {
        private readonly IClientMessenger _writeToClient;
        private readonly IRoomActions _roomActions;
        private readonly ICache _cache;

        public Movement(IClientMessenger writeToClient, ICache cache, IRoomActions roomActions)
        {
            _writeToClient = writeToClient;
            _cache = cache;
            _roomActions = roomActions;
   
           
        }
        public void Move(Entity entity, string direction)
        {
            var location = entity.GetComponent<LocationComponent>();
            //var room = _cache.GetRoom(character.RoomId);
            //if (CharacterCanMove(character) == false)
            //{
            //    _writeToClient.WriteLine("You are too exhausted to move", character.ConnectionId);
            //    return;
            //}

            //var getExitToNextRoom = FindExit(room, direction);

            //if (getExitToNextRoom == null)
            //{
            //    _writeToClient.WriteLine("You can't go that way", character.ConnectionId);
            //    return;
            //}

            //var getNextRoom = _cache.GetRoom(getExitToNextRoom.RoomId);

            //NotifyRoomLeft(room, character, direction);

            //NotifyRoomEnter(getNextRoom, character, direction);

            //UpdateCharactersLocation(getExitToNextRoom, character);

            // _roomActions.Look(getNextRoom, character);
          
        }

        public void MoveNorth(Entity entity, string args)
        {
            this.Move(entity, "north");
        }

        public void MoveSouth(Entity entity, string args)
        {
            this.Move(entity, "south");
        }

        public void MoveEast(Entity entity, string args)
        {
            this.Move(entity, "east");
        }

        public void MoveWest(Entity entity, string args)
        {
            this.Move(entity, "west");
        }

        public void MoveUp(Entity entity, string args)
        {
            this.Move(entity, "up");
        }

        public void MoveDown(Entity entity, string args)
        {
            this.Move(entity, "down");
        }

        public Exit FindExit(Room room, string direction)
        {
            switch (direction)
            {
                case "North":
                    return room.Exits.North;
                case "North East":
                    return room.Exits.NorthEast;
                case "East":
                    return room.Exits.East;
                case "South East":
                    return room.Exits.SouthEast;
                case "South":
                    return room.Exits.South;
                case "South West":
                    return room.Exits.SouthWest;
                case "West":
                    return room.Exits.West;
                case "North West":
                    return room.Exits.NorthWest;
                case "Down":
                    return room.Exits.Down;
                case "Up":
                    return room.Exits.Up;
                default: {return null;}
            }
        }

        public void NotifyRoomLeft(Room room, Player character, string direction)
        {
            foreach (var player in room.Players)
            {
                if (character.Name != player.Name)
                {
                    _writeToClient.WriteLine($"{character.Name} walks {direction.ToLower()}.", player.ConnectionId);
                }
            }
        }

        public void NotifyRoomEnter(Room room, Player character, string direction)
        {
            foreach (var player in room.Players)
            {
                if (character.Name != player.Name)
                {
                    _writeToClient.WriteLine($"{character.Name} walks in.", player.ConnectionId);
                }            
            }
        }

        public void UpdateCharactersLocation(Exit exit, Player character)
        {
            character.RoomId = exit.RoomId;
        }

        public bool CharacterCanMove(Player character)
        {
            return character.Stats.MovePoints > 0;
        }

        public void MoveNorth(Room room, Player character)
        {
            throw new NotImplementedException();
        }
    }
}

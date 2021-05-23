using Artemis;
using Artemis.Attributes;
using Artemis.System;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Commands.Movement;
using WhoPK.GameLogic.Core.Component;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Core.System
{
    public class MovementSystem : EntityProcessingSystem
    {
        ICache _cache;
        IClientMessenger _messenger;
        public MovementSystem() : base(Aspect.All(typeof(LocationComponent), typeof(MovementComponent))) 
        {
            _cache = EntitySystem.BlackBoard.GetEntry<ICache>("Cache"); ;
            _messenger = EntitySystem.BlackBoard.GetEntry<IClientMessenger>("ClientMessenger");
        }

        public override void Process(Entity e)
        {
            MovementComponent movementRequest = e.GetComponent<MovementComponent>();

            Console.WriteLine($"Handling movement request for {e.Id}");
            LocationComponent location = e.GetComponent<LocationComponent>();
            var currentRoom = _cache.GetRoom(location.RoomId);

            //Teleporting
            if (movementRequest.roomId != null)
            {
                location.RoomId = (int)movementRequest.roomId;
            }

            //Regular movement
            if (movementRequest.direction != Direction.None)
            {
                location.RoomId = currentRoom.ExitMap[movementRequest.direction].RoomId;
            }
        }

        public void SendRoomLeft(Room room, Entity e, string direction)
        {
            DescriptiontComponent desc = e.GetComponent<DescriptiontComponent>();
            if (desc != null)
            {
                foreach (var player in room.Players)
                {
                    if (desc.Name != player.Name)
                    {
                        //TODO:  Handle verbs for movement affects (fly, mounted, crawl, etc)
                        _messenger.WriteLine($"{desc.Name} walks {nameof(direction).ToLower()}.", player.ConnectionId);
                    }
                }
            }
        }

        public void SendRoomEnter(Room room, Entity e, string direction)
        {
            DescriptiontComponent desc = e.GetComponent<DescriptiontComponent>();
            if (desc != null)
            {
                foreach (var player in room.Players)
                {
                    if (desc.Name != player.Name)
                    {
                        //TODO:  Handle verbs for movement affects (fly, mounted, crawl, etc)
                        _messenger.WriteLine($"{desc.Name} walks in.", player.ConnectionId);
                    }
                }
            }
        }
    }
}

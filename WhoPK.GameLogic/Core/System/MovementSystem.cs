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
        ICache cache;
        public MovementSystem(ICache cache) : base(Aspect.All(typeof(LocationComponent), typeof(MovementComponent))) 
        {
            this.cache = cache;
        }

        public override void Process(Entity e)
        {
            MovementComponent movementRequest = e.GetComponent<MovementComponent>();
            //float v = velocity.Speed;

            LocationComponent location = e.GetComponent<LocationComponent>();

            var currentRoom = cache.GetRoom(location.RoomId);
            //if (currentRoom.Exits.North.Keyword == movementRequest.direction)
            Console.WriteLine("handling movement request");
            //float r = velocity.AngleAsRadians;

            //float xn = transform.X + (TrigLUT.Cos(r) * v * world.Delta);
            //float yn = transform.Y + (TrigLUT.Sin(r) * v * world.Delta);

            //transform.SetLocation(xn, yn);
        }
    }
}

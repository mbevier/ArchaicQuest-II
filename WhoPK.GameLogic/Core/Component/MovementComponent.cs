using Artemis;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Core.Component
{
    public class MovementComponent : ComponentPoolable
    {
        public Direction direction;
        public int? roomId;
        public int speed;
    }
}

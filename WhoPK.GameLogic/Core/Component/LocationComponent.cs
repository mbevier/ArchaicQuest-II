using Artemis;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.World.Area;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Core.Component
{
    public class LocationComponent : ComponentPoolable
    {
        public int RoomId;
        public int AreaId;
    }
}

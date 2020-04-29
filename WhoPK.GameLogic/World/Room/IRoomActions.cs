using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Character;

namespace WhoPK.GameLogic.World.Room
{
   public interface IRoomActions
    {
        void Look(Room room, Player player);
        string FindValidExits(Room room);
        string DisplayItems(Room room);
        string DisplayMobs(Room room);
    }
}

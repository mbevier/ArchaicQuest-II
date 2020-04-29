using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Commands.Movement
{
    public interface IMovement
    {
        void Move(Room room, Player character, string direction);

        void MoveNorth(Room room, Player character);
    }
}

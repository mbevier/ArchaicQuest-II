using Artemis;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Commands.Movement
{
    public interface IMovement
    {
        void Move(Entity entity, string direction);

        void MoveNorth(Entity entity, string args);
        void MoveSouth(Entity entity, string args);
        void MoveEast(Entity entity, string args);
        void MoveWest(Entity entity, string args);
        void MoveUp(Entity entityr, string args);
        void MoveDown(Entity entity, string args);

    }
}

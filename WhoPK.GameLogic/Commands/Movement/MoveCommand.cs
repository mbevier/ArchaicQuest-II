using Artemis;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.Core.Component;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Commands
{
    public class MoveCommand : ICommand
    {
        readonly Direction _direction;
        public MoveCommand (Direction dir)
        {
            _direction = dir;
        }
        public virtual void Execute(Entity e, string argument)
        {
            var movement = e.GetComponent<MovementComponent>();
            movement.direction = _direction;
        }
    }
}

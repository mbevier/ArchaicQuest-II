using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Character;

namespace WhoPK.GameLogic.Commands
{
    public class MoveCommand : ICommand
    {
        string _direction;
        public MoveCommand (Player player, string direction)
        {
            _direction = direction;
        }
        public virtual void Execute(Player character, string argument)
        {
            //move character
            throw new NotImplementedException();
        }
    }
}

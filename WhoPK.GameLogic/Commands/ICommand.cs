using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Character;

namespace WhoPK.GameLogic.Commands
{
    public interface ICommand
    {
        void Execute(Player character, string argument);
    }

    public interface IGameStateCommand : ICommand
    {

    }
}

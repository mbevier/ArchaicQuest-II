using Artemis;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Character;

namespace WhoPK.GameLogic.Commands
{
    public interface ICommand
    {
        void Execute(Entity e, string argument);
    }

    public interface IGameStateCommand : ICommand
    {

    }
}

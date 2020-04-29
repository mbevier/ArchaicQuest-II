using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.World.Room;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhoPK.GameLogic.Commands
{
    public interface ICommands
    {
        void CommandList(string key, string options, Player player, Room room);
        void ProcessCommand(string command, Player player, Room room);
    }
}

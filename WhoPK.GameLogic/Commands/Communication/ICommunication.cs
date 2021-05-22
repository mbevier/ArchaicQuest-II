using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.World.Area;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Commands.Movement
{
    public interface ICommunication
    {
        //TODO:  Add language
        void Say(Room room, Player character, string message);

        void Tell(Player fromCharacter, Player toCharacter, string message);

        void Yell(Area area, Player character, string message);
    }
}

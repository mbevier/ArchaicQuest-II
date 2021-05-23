using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.Core;

namespace WhoPK.GameLogic.World.Room
{
    public class RoomActions:IRoomActions
    {

        private readonly IClientMessenger _writeToClient;
        public RoomActions(IClientMessenger writeToClient)
        {
            _writeToClient = writeToClient;
        }
        /// <summary>
        /// Displays current room 
        /// </summary>
        public void Look(Room room, Player player)
        {

            var exits = string.Join(", ", room.ExitMap.Select(x => x.Key)).Trim();
            var items = DisplayItems(room);
            var mobs = DisplayMobs(room);

            var roomDesc = new StringBuilder();

            roomDesc
                .Append($"<p class=\"room-title\">{room.Title}<br /></p>")
                .Append($"<p class=\"room-description\">{room.Description}</p>")
                .Append($"<p>{items}</p>")
                .Append($"<p>{mobs}</p>")
                .Append($"<p class=\"room-exit\"> <span class=\"room-exits\">[</span>Exits: <span class=\"room-exits\">{exits}</span><span class=\"room-exits\">]</span></p>");

           _writeToClient.WriteLine(roomDesc.ToString(), player.ConnectionId);
        }

        public string DisplayItems(Room room)
        {
            var items = string.Empty;

            foreach (var item in room.Items)
            {
                items += item.Name + " lies here.";
            }

            return items;

        }

        public string DisplayMobs(Room room)
        {
            var mobs = string.Empty;

            foreach (var mob in room.Mobs)
            {
                mobs += mob.Name + " is here.";
            }

            return mobs;

        }

    }
}

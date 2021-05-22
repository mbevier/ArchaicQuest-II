using Artemis;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.Core;
using WhoPK.GameLogic.Core.Component;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Commands
{
    public class Visual : IVisual
    {
        private IRoomActions _roomActions;
        private ICache _cache;
        private readonly IClientMessenger _writeToClient;

        public Visual(IRoomActions roomActions, ICache cache, IClientMessenger writeToClient)
        {
            _roomActions = roomActions;
            _cache = cache;
            _writeToClient = writeToClient;
        }


        public void Look(Entity entity, string args)
        {
            var roomId = entity.GetComponent<LocationComponent>().RoomId;
            var connectionId = entity.GetComponent<PlayerInputComponent>().connectionId;
            var room = _cache.GetRoom(roomId);
            var exits = _roomActions.FindValidExits(room);
            var items = _roomActions.DisplayItems(room);
            var mobs = _roomActions.DisplayMobs(room);

            var roomDesc = new StringBuilder();

            roomDesc
                .Append($"<p class=\"room-title\">{room.Title}<br /></p>")
                .Append($"<p class=\"room-description\">{room.Description}</p>")
                .Append($"<p>{items}</p>")
                .Append($"<p>{mobs}</p>")
                .Append($"<p class=\"room-exit\"> <span class=\"room-exits\">[</span>Exits: <span class=\"room-exits\">{exits}</span><span class=\"room-exits\">]</span></p>");

            _writeToClient.WriteLine(roomDesc.ToString(), connectionId);
        }
    }
}

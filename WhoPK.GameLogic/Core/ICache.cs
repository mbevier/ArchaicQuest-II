using System;
using System.Collections.Concurrent;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Core
{
    public interface ICache
    {     
        /// <summary>
        /// Add player to cache
        /// </summary>
        /// <returns>returns player Cache</returns>
        bool AddPlayer(string id, Player player);
        Player GetPlayer(string id);
        ConcurrentDictionary<string, Player> GetPlayerCache();
        bool PlayerAlreadyExists(Guid id);

        bool AddRoom(int id, Room room);

        Room GetRoom(int id);
        bool UpdateRoom(int id, Room room, Player player);
        void ClearRoomCache();
    }
}
using Artemis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Core
{
    public class Cache : ICache
    {
        private readonly ConcurrentDictionary<string, Player> _playerCache = new ConcurrentDictionary<string, Player>();
        private readonly ConcurrentDictionary<int, Room> _roomCache = new ConcurrentDictionary<int, Room>();
        private readonly ConcurrentDictionary<string, Entity> _playerEntityCache = new ConcurrentDictionary<string, Entity>();
        public bool AddPlayer(string id, Player player)
        {
            return _playerCache.TryAdd(id, player);
        }

        public bool AddPlayerEntity(string id, Entity player)
        {
            return _playerEntityCache.TryAdd(id, player);
        }

        public Player GetPlayer(string id)
        {
            _playerCache.TryGetValue(id, out Player player);

            return player;
        }
        public Entity GetPlayerEntity(string id)
        {
            _playerEntityCache.TryGetValue(id, out Entity player);

            return player;
        }


        /// <summary>
        /// Only for the main loop
        /// </summary>
        /// <returns></returns>
        public ConcurrentDictionary<string, Player> GetPlayerCache()
        {
            return _playerCache;
        }

        public bool PlayerAlreadyExists(Guid id)
        {
            return _playerCache.Values.Any(x => x.Id.Equals(id));
        }

        public bool AddRoom(int id, Room room)
        {
            return _roomCache.TryAdd(id, room);
        }

        public Room GetRoom(int id)
        {
            _roomCache.TryGetValue(id == 0 ? 1 : id, out Room room);

            return room;
        }

        public bool UpdateRoom(int id, Room room, Player player)
        {
            var existingRoom = room;
            var newRoom = room;
            newRoom.Players.Add(player);


            return _roomCache.TryUpdate(id, existingRoom, newRoom);
        }

        public void ClearRoomCache()
        {
            _roomCache.Clear();

        }


    }
}

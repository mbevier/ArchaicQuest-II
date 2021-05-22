using WhoPK.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhoPK.GameLogic.World.Room
{
    public class AddRoom: IAddRoom
    {

        private IDataBase _db { get; }
        public AddRoom(IDataBase db)
        {
            _db = db;

        }

        public Room MapRoom(Room room)
        {
            var newRoom = new Room()
            {
                Title = room.Title,
                Description = room.Description,
                AreaId = room.AreaId,
                Coords = new Coordinates()
                {
                    X = room.Coords.X,
                    Y = room.Coords.Y,
                    Z = room.Coords.Z
                },
                Exits = room.Exits,
                Emotes = room.Emotes,
                InstantRePop = room.InstantRePop,
                UpdateMessage = room.UpdateMessage,
                Items = room.Items,
                Mobs = room.Mobs,
                RoomObjects = room.RoomObjects,
                Type = room.Type,
                DateUpdated = DateTime.Now,
                DateCreated = DateTime.Now
            };

            MapRoomId(newRoom);

            if(room.Id != -1)
            {
                newRoom.Id = room.Id;
            }

            return newRoom;

        }
        public void MapRoomId(Room room)
        {
            var northRoom = room.Exits[Direction.North].Coords;
            if (northRoom != null)
            {
                room.Exits[Direction.North].RoomId = GetRoomFromCoords(northRoom) != null ? GetRoomFromCoords(northRoom).Id : -1;
            }

            var eastRoom = room.Exits[Direction.East]?.Coords;
            if (eastRoom != null)
            {
                room.Exits[Direction.North].RoomId = GetRoomFromCoords(eastRoom) != null ? GetRoomFromCoords(eastRoom).Id : -1;
            }

            var southRoom = room.Exits[Direction.South]?.Coords;
            if (southRoom != null)
            {
                room.Exits[Direction.South].RoomId = GetRoomFromCoords(southRoom) != null ? GetRoomFromCoords(southRoom).Id : -1;
            }

            var westRoom = room.Exits[Direction.West]?.Coords;
            if (westRoom != null)
            {
                room.Exits[Direction.West].RoomId = GetRoomFromCoords(westRoom) != null ? GetRoomFromCoords(westRoom).Id : -1;
            }
        }

        public Room GetRoomFromCoords(Coordinates coords)
        {
            return _db.GetCollection<Room>(DataBase.Collections.Room).FindOne(x => x.Coords.X.Equals(coords.X) && x.Coords.Y.Equals(coords.Y) && x.Coords.Z.Equals(coords.Z));
        }
    }
}

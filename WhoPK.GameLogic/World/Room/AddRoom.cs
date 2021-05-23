﻿using WhoPK.DataAccess;
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
            var northRoom = room.ExitMap.ContainsKey(Direction.North) ? room.ExitMap[Direction.North].Coords : null;
            if (northRoom != null)
            {
                room.ExitMap[Direction.North].RoomId = GetRoomFromCoords(northRoom) != null ? GetRoomFromCoords(northRoom).Id : -1;
            }

            var eastRoom = room.ExitMap.ContainsKey(Direction.East) ? room.ExitMap[Direction.East].Coords : null;
            if (eastRoom != null)
            {
                room.ExitMap[Direction.East].RoomId = GetRoomFromCoords(eastRoom) != null ? GetRoomFromCoords(eastRoom).Id : -1;
            }

            var southRoom = room.ExitMap.ContainsKey(Direction.South) ? room.ExitMap[Direction.South].Coords : null;
            if (southRoom != null)
            {
                room.ExitMap[Direction.South].RoomId = GetRoomFromCoords(southRoom) != null ? GetRoomFromCoords(southRoom).Id : -1;
            }

            var westRoom = room.ExitMap.ContainsKey(Direction.West) ? room.ExitMap[Direction.West].Coords : null;
            if (westRoom != null)
            {
                room.ExitMap[Direction.West].RoomId = GetRoomFromCoords(westRoom) != null ? GetRoomFromCoords(westRoom).Id : -1;
            }
        }

        public Room GetRoomFromCoords(Coordinates coords)
        {
            return _db.GetCollection<Room>(DataBase.Collections.Room).FindOne(x => x.Coords.X.Equals(coords.X) && x.Coords.Y.Equals(coords.Y) && x.Coords.Z.Equals(coords.Z));
        }
    }
}

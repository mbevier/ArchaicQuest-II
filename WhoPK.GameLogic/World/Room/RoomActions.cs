﻿using System;
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

            var exits = FindValidExits(room);
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


        /// <summary>
        /// Displays valid exits
        /// </summary>
        public string FindValidExits(Room room)
        {
            var exits = new List<string>();
            var exitList = string.Empty;

            if (room.Exits.NorthWest != null)
            {
                exits.Add(room.Exits.NorthWest.Name);
            }

            if (room.Exits.North != null)
            {
                exits.Add(room.Exits.North.Name);
            }

            if (room.Exits.NorthEast != null)
            {
                exits.Add(room.Exits.NorthEast.Name);
            }

            if (room.Exits.East != null)
            {
                exits.Add(room.Exits.East.Name);
            }

            if (room.Exits.SouthEast != null)
            {
                exits.Add(room.Exits.SouthEast.Name);
            }

            if (room.Exits.South != null)
            {
                exits.Add(room.Exits.South.Name);
            }

            if (room.Exits.SouthWest != null)
            {
                exits.Add(room.Exits.SouthWest.Name);
            }

            if (room.Exits.West != null)
            {
                exits.Add(room.Exits.West.Name);
            }

            if (exits.Count <= 0)
            {
                exits.Add("None");
            }

            foreach (var exit in exits)
            {
                exitList += exit + ", ";
            }

            exitList = exitList.Remove(exitList.Length - 2);


            return exitList;

        }


    }
}

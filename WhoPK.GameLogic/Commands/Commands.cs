﻿using WhoPK.GameLogic.Character;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Commands.Movement;
using WhoPK.GameLogic.World.Room;
using System.Linq;

namespace WhoPK.GameLogic.Commands
{
   public class Commands: ICommands
    {
        private readonly IMovement _movement;
        private readonly IRoomActions _roomActions;

        public Commands(IMovement movement, IRoomActions roomActions)
        {
            _movement = movement;
            _roomActions = roomActions;
        }
 
        public void CommandList(string key, string options, Player player, Room room)
        {
            switch (key)
            {
                case "north west":
                case "nw":
                    _movement.Move(room, player, "North West");
                    break;
                case "north east":
                case "ne":
                    _movement.Move(room, player, "North East");
                    break;
                case "south east":
                case "se":
                    _movement.Move(room, player, "South East");
                    break;
                case "south west":
                case "sw":
                    _movement.Move(room, player, "South West");
                    break;
                case "north":
                case "n":
                    _movement.Move(room, player, "North");
                    break;
                case "east":
                case "e":
                    _movement.Move(room, player, "East");
                    break;
                case "south":
                case "s":
                    _movement.Move(room, player, "South");
                    break;
                case "west":
                case "w":
                    _movement.Move(room, player, "West");
                    break;
                case "up":
                case "u":
                    _movement.Move(room, player, "Up");
                    break;
                case "down":
                case "d":
                    _movement.Move(room, player, "Down");
                    break;
                case "look":
                case "l":
                    _roomActions.Look(room, player);
                    break;
            }
        }    

        public void ProcessCommand(string command, Player player, Room room)
        {

            var cleanCommand = command.Trim().ToLower();
            CommandList(cleanCommand, String.Empty, player, room);
        }
    }


}


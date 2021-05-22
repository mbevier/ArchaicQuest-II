using WhoPK.GameLogic.Character;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Commands.Movement;
using WhoPK.GameLogic.World.Room;
using System.Linq;
using WhoPK.GameLogic.Core;
using Microsoft.AspNet.SignalR.Messaging;
using Artemis;
using WhoPK.GameLogic.Core.Component;

namespace WhoPK.GameLogic.Commands
{
   public class CommandManager : ICommandManager
    {
        IInterpreter _interpreter;
        IMovement _movement;
        IVisual _visual;
        public CommandManager(IInterpreter interpreter, IMovement movement, IVisual visual)
        {
            _interpreter = interpreter;
            _movement = movement;
            _visual = visual;
            LoadCommands();
        }

        private void LoadCommands()
        {
            //TODO:  Have these tag with component for further processing?  hmmm
            CommandList = new Dictionary<string, Action<Entity, string>>()
            {
                { "north", new Action<Entity, string>(_movement.MoveNorth) },
                { "south", new Action<Entity, string>(_movement.MoveSouth) },
                { "east", new Action<Entity, string>(_movement.MoveEast) },
                { "west", new Action<Entity, string>(_movement.MoveWest) },
                { "look", new Action<Entity, string>(_visual.Look) },
            };
        }
        public Dictionary<string, Action<Entity, string>> CommandList { get; set; }      

        public bool ProcessCommand(string command, Entity entity)
        {
            var userId = entity.GetComponent<PlayerInputComponent>().userId;
            var commandFound = false;
            var cleanCommand = command.Trim().ToLower();
            var interpretedCommand = _interpreter.Interpret(cleanCommand);
            foreach (KeyValuePair<string, Action<Entity, String>> cmd in CommandList)
            {
                if (cmd.Key.StartsWith(command))
                {
                    cmd.Value(entity, interpretedCommand.argument);
                    commandFound = true;
                }
            }
            return commandFound;
        }
    }
    //Render system - Event, location, visibility

}


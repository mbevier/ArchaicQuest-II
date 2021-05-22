using Artemis;
using Artemis.Manager;
using Artemis.System;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Commands;
using WhoPK.GameLogic.Core.Component;

namespace WhoPK.GameLogic.Core.System
{
    [Artemis.Attributes.ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Update, Layer = 1)]
    public class PlayerInputSystem : EntityProcessingSystem
    {
        private ICommandManager _commandManager;
        public PlayerInputSystem(ICommandManager commandManager) : base(Aspect.All(typeof(PlayerInputComponent))) 
        {
            _commandManager = commandManager;
        }

        public override void Process(Entity entity)
        {
            var commandStack = entity.GetComponent<PlayerInputComponent>().commands;
            //var player = entity.GetComponent<Player>
            if (entity.GetComponent<PlayerInputComponent>().commands.Count > 0)
            {
                var command = commandStack.Pop();
                var processed = _commandManager.ProcessCommand(command, entity);
                //_commandManager.ProcessCommand(command, player.Value);
                //var room = _cache.GetRoom(player.Value.RoomId);

                //TODO:  Player becomes an entity rather than a class
                //_commandManager.ProcessCommand(command, player.Value);
            }
        }
    }
}

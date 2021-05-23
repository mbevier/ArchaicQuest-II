using Artemis;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using WhoPK.GameLogic.Commands;
using WhoPK.GameLogic.Core.System;

namespace WhoPK.GameLogic.Core
{
    public class World: IWorld
    {
        private IClientMessenger _writeToClient;
        private ICache _cache;
        private ICommandManager _commandManager;
        private PlayerInputSystem _playerInputSystem;
        private MovementSystem _movementSystem;
        public EntityWorld EntityWorld { get; set; }

        public void Start()
        {
        }

        public void Update()
        {
            // process all dependent systems.
            EntityWorld.Update();
        }
    }

}

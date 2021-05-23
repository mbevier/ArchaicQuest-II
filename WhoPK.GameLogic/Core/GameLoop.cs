using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhoPK.GameLogic.Commands;
using WhoPK.GameLogic.Hubs;
using Microsoft.AspNet.SignalR;
using WhoPK.GameLogic.Core.System;
using Artemis;
using Artemis.System;

namespace WhoPK.GameLogic.Core
{

    public class GameLoop : IGameLoop
    {

        private IClientMessenger _messenger;
        private ICache _cache;
        private ICommandManager _commandManager;
        private EntityWorld _gameWorld;

        public GameLoop(IClientMessenger writeToClient, ICache cache, ICommandManager commandManager, EntityWorld world)
        {
            _messenger = writeToClient;
            _cache = cache;
            _commandManager = commandManager;
            _gameWorld = world;

            InitializeSystems();
        }

        private void InitializeSystems()
        {
            EntitySystem.BlackBoard.SetEntry<ICommandManager>("CommandManager", _commandManager);
            EntitySystem.BlackBoard.SetEntry<ICache>("Cache", _cache);
            _gameWorld.InitializeAll(true);
        }

        public async Task Start()
        {
            try
            {
                _messenger.WriteLine("start game loop ");
                while (true)
                {
                    await Task.Delay(50);
                    _gameWorld.Update();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
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

namespace WhoPK.GameLogic.Core
{

    public class GameLoop : IGameLoop
    {

        private IClientMessenger _writeToClient;
        private ICache _cache;
        private ICommandManager _commandManager;
        private PlayerInputSystem _playerInputSystem;
        private MovementSystem _movementSystem;
        private EntityWorld _gameWorld;

        public GameLoop(IClientMessenger writeToClient, ICache cache, ICommandManager commandManager)
        {
            _writeToClient = writeToClient;
            _cache = cache;
            _commandManager = commandManager;
            _gameWorld = new EntityWorld();
            _playerInputSystem = new PlayerInputSystem(_commandManager);
            _movementSystem = new MovementSystem(_cache, _writeToClient);
        }


        public async Task UpdateTime()
        {

            //var players = _cache.GetPlayerCache();
            //var validPlayers = players.Where(x => x.Value.Buffer.Count > 0);

            //foreach (var player in validPlayers)
            //{
            //    _writeToClient.WriteLine("update", player.Value.ConnectionId);

            //}

        }

        public async Task Start()
        {
            _gameWorld.SystemManager.Systems.Add(_playerInputSystem);
            _gameWorld.SystemManager.Systems.Add(_movementSystem);
            try
            {
                _writeToClient.WriteLine("start game loop ");
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


        public async Task UpdatePlayers()
        {
            while (true)
            {
                await Task.Delay(125);
                _playerInputSystem.Process();
            }
        }

    }


}
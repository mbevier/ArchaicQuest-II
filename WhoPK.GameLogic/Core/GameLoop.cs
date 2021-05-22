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

namespace WhoPK.GameLogic.Core
{

    public class GameLoop : IGameLoop
    {

        private IWriteToClient _writeToClient;
        private ICache _cache;
        private ICommandManager _commandManager;
        private PlayerInputSystem _playerInputSystem;

        public GameLoop(IWriteToClient writeToClient, ICache cache, ICommandManager commandManager)
        {
            _writeToClient = writeToClient;
            _cache = cache;
            _commandManager = commandManager;
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
            try
            {
                _writeToClient.WriteLine("start game loop ");
                while (true)
                {
                    await Task.Delay(50);
                    //var players = _cache.GetPlayerCache();
                    //var commandQueuedPlayers = players.Where(x => x.Value.Buffer.Count > 0);

                    _playerInputSystem.Process();
                    //foreach (var player in commandQueuedPlayers)
                    //{
                    //    var commandExecuted = _commandManager.ProcessCommand(player.Value.Buffer.Pop(), player.Value);
                    //    if (!commandExecuted) _writeToClient.WriteLine("Huh?");
                    //}
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
                //var players = _cache.GetPlayerCache();
                //var validPlayers = players.Where(x => x.Value.Buffer.Count > 0);

                //foreach (var entity in validEntities)
                //{

                //    var command = player.Value.Buffer.Pop();
                //    //var room = _cache.GetRoom(player.Value.RoomId);
                //    _commandManager.ProcessCommand(command, player.Value);

                //}
            }
        }

    }


}
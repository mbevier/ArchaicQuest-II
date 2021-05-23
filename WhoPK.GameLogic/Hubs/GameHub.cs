
using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Threading.Tasks;
using WhoPK.DataAccess;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.Commands;
using WhoPK.GameLogic.Core;
using WhoPK.GameLogic.World.Room;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Extensions.Logging;
using LiteDB;
using Artemis;
using WhoPK.GameLogic.Core.Component;

namespace WhoPK.GameLogic.Hubs
{
    public class GameHub : Hub
    {
        private readonly ILogger<GameHub> _logger;
        private IDataBase _db { get; }
        private ICache _cache { get; }
        private readonly IClientMessenger _writeToClient;
        private int start = 0;
        private EntityWorld _world;
        public GameHub(IDataBase db, ICache cache, ILogger<GameHub> logger, IClientMessenger writeToClient, EntityWorld world)
        {
            _logger = logger;
            _db = db;
            _cache = cache;
            _writeToClient = writeToClient;
            _world = world;
        }

 
        /// <summary>
        /// Do action when user connects 
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {

            if (start == 0)
            {
                start = 1;
                await Clients.All.SendAsync("SendMessage", "", "Starting loop");
            
            }
    
            
            await Clients.All.SendAsync("SendMessage", "", "Someone has entered the realm");
        }

        /// <summary>
        /// Do action when user disconnects 
        /// </summary>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
           // await Clients.All.SendAsync("SendAction", "user", "left");
        }

        /// <summary>
        /// get message from client
        /// </summary>
        /// <returns></returns>
        public async Task SendToServer(string message, string connectionId)
        {
            _logger.LogInformation($"Player sent {message}, hub ID{connectionId}");
            var player = _cache.GetPlayer(Context.ConnectionId);
            var playerEntity = _cache.GetPlayerEntity(Context.ConnectionId);
            var playerInput = playerEntity.GetComponent<PlayerInputComponent>();
            if (playerInput != null)
            {
                playerInput.commands.Push(message);
            }
            player.Buffer.Push(message);

          //   var room = _cache.GetRoom(player.RoomId);

          //  GetRoom(connectionId, player);

         //   _commands.ProcessCommand(message, player, room);

          //   await Clients.All.SendAsync("SendMessage", "user x", message);
        }

        /// <summary>
        /// Send message to all clients
        /// </summary>
        /// <returns></returns>
        public async Task Send(string message)
        {
            _logger.LogInformation($"Player sent {message}");
            await Clients.All.SendAsync("SendMessage", "user x", message);
        }

        /// <summary>
        /// Send message to specific client
        /// </summary>
        /// <returns></returns>
        public async Task SendToClient(string message, string hubId)
        {
            _logger.LogInformation($"Player sent {message}");
            await Clients.Client(hubId).SendAsync("SendMessage", message);
        }

        public async void Welcome(string id)
        {
            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(location);

            var motd = File.ReadAllText(directory + "/motd");
 
           await SendToClient(motd, Context.ConnectionId);
        }
 

        public void CreateCharacter(string name = "Liam")
        {
            var newPlayer = new Player()
            {
                Name = name
            };

            _db.Save(newPlayer, DataBase.Collections.Players);

        }

        public async void AddCharacter(string hubId, Guid characterId)
        {

            var player = GetCharacter(Context.ConnectionId, characterId);
            Entity playerEntity = _world.CreateEntity(); // you can pass an unique ID as first parameter.
            var playerInput = new PlayerInputComponent();
            playerInput.connectionId = player.ConnectionId;
            playerInput.userId = player.AccountId;
            playerEntity.AddComponent(playerInput);
            AddCharacterToCache(Context.ConnectionId, player, playerEntity);
            await SendToClient($"Welcome {player.Name}. Your adventure awaits you.", Context.ConnectionId);

            GetRoom(Context.ConnectionId, player);
        }

        /// <summary>
        /// Find Character in DB and add to cache
        /// Check if player is already in cache 
        /// if so kick em off
        /// </summary>
        /// <param name="hubId">string</param>
        /// <param name="characterId">guid</param>
        /// <returns>Player Character</returns>
        private Player GetCharacter(string hubId, Guid characterId)
        {
            var player = _db.GetById<Player>(characterId, DataBase.Collections.Players);
            player.ConnectionId = hubId;
            player.LastCommandTime = DateTime.Now;
            player.LastLoginTime = DateTime.Now;

            return player;
        }

        private void AddCharacterToCache(string hubId, Player character, Entity playerEntity)
        {
             
            if (_cache.PlayerAlreadyExists(character.Id))
            {
                // log char off
                // remove from _cache
                // return
            }

            _cache.AddPlayer(hubId, character);
            _cache.AddPlayerEntity(hubId, playerEntity);

        }

        private void GetRoom(string hubId, Player character)
        {
           var room = _cache.GetRoom(1);

            new RoomActions(_writeToClient).Look(room, character);

          //  return room;
        }


    }
}

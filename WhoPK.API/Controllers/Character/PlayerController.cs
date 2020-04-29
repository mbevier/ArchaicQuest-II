﻿
using WhoPK.DataAccess;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.Character.Equipment;
using WhoPK.GameLogic.Character.Model;
using WhoPK.GameLogic.Character.Status;
using WhoPK.GameLogic.Item;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WhoPK.GameLogic.Account;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WhoPK.Controllers
{
    public class PlayerController : Controller
    {
        private IDataBase _db { get; }
        public PlayerController(IDataBase db)
        {
            _db = db;
        }
        [HttpPost]
        [Route("api/Character/Player")]
        public void Post([FromBody] Player player)
        {


            if (!ModelState.IsValid)
            {
                var exception = new Exception("Invalid player");
                throw exception;
            }


            var newPlayer = new Player()
            {
                AccountId = player.AccountId,
                Id = Guid.NewGuid(),
                Name = player.Name,
                Status = CharacterStatus.Status.Standing,
                Level = 1,
                ArmorRating = new ArmourRating()
                {
                    Armour = 1,
                    Magic = 1
                },
                Affects = new Affects(),
                AlignmentScore = 0,
                Attributes = player.Attributes,
                MaxAttributes = player.Attributes,
                Inventory = new List<Item>(),
                Equipped = new Equipment(),
                ClassName = player.ClassName,
                Config = null,
                Description = player.Description,
                Gender = player.Gender,
                Stats = new Stats()
                {
                    HitPoints = player.Attributes.Attribute[GameLogic.Effect.EffectLocation.Constitution] * 2, //create formula to handle these stats
                    MovePoints = player.Attributes.Attribute[GameLogic.Effect.EffectLocation.Dexterity] * 2,  // only for testing
                    ManaPoints = player.Attributes.Attribute[GameLogic.Effect.EffectLocation.Intelligence] * 2,
                },
                MaxStats = player.Stats,
                Money = new Gold()
                {
                    Amount = 100
                },
                Race = player.Race,
                JoinedDate = DateTime.Now,
                LastLoginTime = DateTime.Now,

            };


            if (!string.IsNullOrEmpty(player.Id.ToString()) && player.Id != Guid.Empty)
            {

                var foundItem = _db.GetById<Character>(player.Id, DataBase.Collections.Players);

                if (foundItem == null)
                {
                    throw new Exception("player Id does not exist");
                }

                newPlayer.Id = player.Id;
            }

            var account = _db.GetById<Account>(player.AccountId, DataBase.Collections.Account);
            account.Characters.Add(newPlayer.Id);
            _db.Save(account, DataBase.Collections.Account);
            _db.Save(newPlayer, DataBase.Collections.Players);

        }

        [HttpGet]
        [Route("api/Character/player")]
        public List<Character> Get([FromQuery] string query)
        {

            var mobs = _db.GetCollection<Character>(DataBase.Collections.Mobs).FindAll().Where(x => x.Name != null);

            if (string.IsNullOrEmpty(query))
            {
                return mobs.ToList();
            }

            return mobs.Where(x => x.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) != -1).ToList();

        }





    }
}

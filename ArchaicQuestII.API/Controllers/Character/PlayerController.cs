﻿
using System;
using System.Collections.Generic;
using System.Linq;
using ArchaicQuestII.DataAccess;
using Microsoft.AspNetCore.Mvc;
using ArchaicQuestII.Engine.Character.Status;
using ArchaicQuestII.GameLogic.Character;
using ArchaicQuestII.GameLogic.Character.Equipment;
using ArchaicQuestII.GameLogic.Item;
 


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArchaicQuestII.Controllers
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
                Name = player.Name,
                Status = Status.Standing,
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
                Stats = new Stats(),
                MaxStats = player.Stats,
                Money = new GameLogic.Character.Money()
                {
                    Gold = 100
                },
                Race = player.Race,
                JoinedDate = new DateTime()
                
            };


            if (!string.IsNullOrEmpty(player.Id.ToString()) && player.Id != -1)
            {

                var foundItem = _db.GetById<Character>(player.Id, DataBase.Collections.Players);

                if (foundItem == null)
                {
                    throw new Exception("player Id does not exist");
                }

                newPlayer.Id = player.Id;
            }



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
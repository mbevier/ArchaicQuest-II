﻿using System;
using WhoPK.DataAccess;
using WhoPK.GameLogic.Character.Race;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WhoPK.API.Controllers.Character
{
    public class RaceController
    {

        private IDataBase _db { get; }
        public RaceController(IDataBase db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("api/Character/Race")]
        public void Post(Race race)
        {
            _db.Save(race, DataBase.Collections.Race);
        }

        [HttpGet]
        [Route("api/Character/Race/{id:int}")]
        public Race Get(Guid id)
        {
            return _db.GetById<Race>(id, DataBase.Collections.Race);
        }

        [HttpGet]
        [Route("api/Character/Race")]
        public List<Race> Get()
        {
            return _db.GetList<Race>(DataBase.Collections.Race);
        }
    }
}

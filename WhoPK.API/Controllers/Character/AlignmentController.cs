﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoPK.DataAccess;
using WhoPK.GameLogic.Character.Alignment;
using Microsoft.AspNetCore.Mvc;

namespace WhoPK.API.Character
{
    public class AlignmentController
    {
        private IDataBase _db { get; }
        public AlignmentController(IDataBase db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("api/Character/Alignment")]
        public void Post(Alignment align)
        {
            _db.Save(align, DataBase.Collections.Alignment);
        }

        [HttpGet]
        [Route("api/Character/Alignment/{id:int}")]
        public Alignment Get(Guid id)
        {
            return _db.GetById<Alignment>(id, DataBase.Collections.Alignment);
        }

        [HttpGet]
        [Route("api/Character/Alignment")]
        public List<Alignment> Get()
        {
            return _db.GetCollection<Alignment>(DataBase.Collections.Alignment).FindAll().OrderBy(x => x.Name).ToList();
        }
    }
}

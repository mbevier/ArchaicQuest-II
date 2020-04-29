using System;
using System.Collections.Generic;
using WhoPK.DataAccess;
using WhoPK.GameLogic.Core;
using Microsoft.AspNetCore.Mvc;

namespace WhoPK.API.Controllers.Character
{
    public class StatusController
    {

        private IDataBase _db { get; }
        public StatusController(IDataBase db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("api/Character/Status")]
        public void Post(Option status)
        {
            _db.Save(status, DataBase.Collections.Status);
        }

        [HttpGet]
        [Route("api/Character/Status/{id:int}")]
        public Option Get(Guid id)
        {
            return _db.GetById<Option>(id, DataBase.Collections.Status);
        }

        [HttpGet]
        [Route("api/Character/Status")]
        public List<Option> Get()
        {
            return _db.GetList<Option>(DataBase.Collections.Status);
        }
    }
}

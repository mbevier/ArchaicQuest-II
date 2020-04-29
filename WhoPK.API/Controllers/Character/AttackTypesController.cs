using System;
using System.Collections.Generic;
using WhoPK.DataAccess;
using WhoPK.GameLogic.Core;
using Microsoft.AspNetCore.Mvc;

namespace WhoPK.API.Controllers.Character
{
    public class AttackTypesController
    {
        private IDataBase _db { get; }
        public AttackTypesController(IDataBase db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("api/Character/AttackType")]
        public void Post(OptionDescriptive attackType)
        {
            _db.Save(attackType, DataBase.Collections.AttackType);
        }

        [HttpGet]
        [Route("api/Character/AttackType/{id:int}")]
        public OptionDescriptive Get(Guid id)
        {
            return _db.GetById<OptionDescriptive>(id, DataBase.Collections.AttackType);
        }

        [HttpGet]
        [Route("api/Character/AttackType")]
        public List<OptionDescriptive> Get()
        {
            return _db.GetList<OptionDescriptive>(DataBase.Collections.AttackType);
        }
    }

}

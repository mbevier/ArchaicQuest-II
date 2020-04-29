﻿using WhoPK.DataAccess;
using WhoPK.GameLogic.Item;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WhoPK.GameLogic.Character.Equipment;
using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.World.Area;
using WhoPK.GameLogic.World.Room;
using WhoPK.GameLogic.Account;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WhoPK.Controllers.Dashboard
{
    public class QuickStats
    {
        public int ItemCount { get; set; }
        public int MobCount { get; set; }
        public int AreaCount { get; set; }
        public int RoomCount { get; set; }
        public int QuestCount { get; set; }
    }

    public class LineGraphStats
    {
        public List<LineStats> Data { get; set; } = new List<LineStats>();
    }

    public class LineStats
    {
        public string Name { get; set; }
        public List<Series> Series { get; set; } = new List<Series>();
    }

    public class Series
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
 

    public class DashboardController : Controller
    {
        private IDataBase _db { get; }
        public DashboardController(IDataBase db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("api/dashboard/quickStats")]
        public JsonResult get()
        {
            var stats = new QuickStats
            {
                ItemCount = _db.GetCollection<Item>(DataBase.Collections.Items).Count(),
                MobCount = _db.GetCollection<Character>(DataBase.Collections.Mobs).Count(),
                AreaCount = _db.GetCollection<Area>(DataBase.Collections.Area).Count(),
                RoomCount = _db.GetCollection<Room>(DataBase.Collections.Room).Count(),
                QuestCount = 0
            };

            return Json(stats);

        }

        [HttpGet]
        [Route("api/dashboard/AccountStats")]
        public JsonResult AccountStats()
        {


            var lineGraphStats = new LineGraphStats();
            var AccountStats = new LineStats()
            {
                Name = "Accounts"
            };

          

            var accounts = _db.GetCollection<Account>(DataBase.Collections.Account).FindAll();
            
      
           var thisMonth =  accounts.Where(X => X.DateJoined.Month.Equals(DateTime.Now.Month)).Count();
            var data = new Series();
            data.Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) + " " + DateTime.Now.Year;
            data.Value = thisMonth;
            AccountStats.Series.Add(data);


            var months = 6;

            for (int i = 1; i < months; i++)
            {
                var LastMonth = accounts.Where(X => X.DateJoined.Month.Equals(DateTime.Now.AddMonths(-i).Month)).Count();
                var LastMonthData = new Series
                {
                    Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-i).Month) + " " + DateTime.Now.AddMonths(-i).Year,
                    Value = LastMonth
                };

                AccountStats.Series.Add(LastMonthData);
            }

            //Characters

            var CharStats = new LineStats()
            {
                Name = "Characters"
            };

            var players = _db.GetCollection<Player>(DataBase.Collections.Players).FindAll();





            var CharThisMonth = players.Where(X => X.JoinedDate.Month.Equals(DateTime.Now.Month)).Count();
            var CharData = new Series();
            CharData.Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) + " " + DateTime.Now.Year;
            CharData.Value = CharThisMonth;
            CharStats.Series.Add(CharData);

            for (int i = 1; i < months; i++)
            {
                var LastMonth = players.Where(X => X.JoinedDate.Month.Equals(DateTime.Now.AddMonths(-i).Month)).Count();
                var LastMonthData = new Series
                {
                    Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-i).Month) + " " + DateTime.Now.AddMonths(-i).Year,
                    Value = LastMonth
                };

                CharStats.Series.Add(LastMonthData);
            }

            CharStats.Series.Reverse();
            AccountStats.Series.Reverse();

            lineGraphStats.Data.Add(AccountStats);
            lineGraphStats.Data.Add(CharStats);

            return Json(lineGraphStats);

        }



    }
}

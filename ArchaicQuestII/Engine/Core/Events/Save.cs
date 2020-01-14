using ArchaicQuestII.Log;
using LiteDB;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArchaicQuestII.Core.World;
using ArchaicQuestII.Engine.Account;
using ArchaicQuestII.Engine.Character.Model;
using ArchaicQuestII.Engine.Item;
using ArchaicQuestII.Engine.World;
using ArchaicQuestII.Engine.World.Area.Model;
using ArchaicQuestII.Engine.World.Room;
using ArchaicQuestII.Engine.World.Room.Model;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace ArchaicQuestII.Core.Events
{
    public class DB : IDB
    {
        private ILogger<DB> _logger { get; set; }
        private LiteDatabase _db { get; set; }
        public DB(LiteDatabase db, ILogger<DB> logger)
        {
            _logger = logger;
            _db = db;
        }


        public bool Save<T>(T data, string collectionName)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            try
            {
                using (var db = new LiteDatabase(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyData.db")))
                {
                    db.GetCollection<T>(collectionName).Upsert(data);

                }

                return true;

            }
            catch (Exception ex)
            {
                _logger.Error("Error Saving " + ex.Message);
                return false;
            }

        }

        public List<T> GetCollection<T>(string collectionName)
        {
            try
            {
                using (var db = new LiteDatabase(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyData.db")))
                {
                    return db.GetCollection<T>(collectionName).FindAll().ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error finding all " + collectionName + " error: " + ex.Message);
            }

            return new List<T>();
        }


        public T FindById<T>(string id, string collectionName)
        {

            try
            {
                using (var db = new LiteDatabase(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyData.db")))
                {
                    return db.GetCollection<T>(collectionName).FindById(id);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error finding " + collectionName + " With ID " + id + " error: " + ex.Message);
            }

            return default(T);

        }

        public LiteCollection<T> GetColumn<T>(string collectionName)
        {
            try
            {
                using (var db = new LiteDatabase(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyData.db")))
                {
                    return db.GetCollection<T>(collectionName);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error finding " + collectionName + " error: " + ex.Message);
            }

            return null;
        }


        public List<Area> GetAreas()
        {

            using (var db = new LiteDatabase(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyData.db")))
            {
                var col = db.GetCollection<Area>("Area");
                var data = col.FindAll().ToList();
                var roomCol = db.GetCollection<Room>("Room");

                //This can/should be improved - will do for now
                foreach (var area in data)
                {
                    var getRooms = roomCol.FindAll().Where(x => x.AreaId.Equals(area.Id)).ToList();
                    area.Rooms = getRooms;
                }

                return data;

            }
        }





    }
}

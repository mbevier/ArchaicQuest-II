using System.Collections.Generic;
using ArchaicQuestII.Engine.World.Area.Model;
using LiteDB;

namespace ArchaicQuestII.Core.Events
{
    public interface IDB
    {
        T FindById<T>(string id, string collectionName);
        List<Area> GetAreas();
        List<T> GetCollection<T>(string collectionName);
        LiteCollection<T> GetColumn<T>(string collectionName);
        bool Save<T>(T data, string collectionName);
    }
}
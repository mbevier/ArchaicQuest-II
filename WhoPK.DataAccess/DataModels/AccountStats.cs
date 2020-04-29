using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhoPK.DataAccess.DataModels
{
    public class AccountStats
    {
        public int MobKills { get; set; }
        public int PlayerKills { get; set; }
        public int Deaths { get; set; }
        public int TotalPlayTime { get; set; }
        public int ExploredRooms { get; set; }
    }
}

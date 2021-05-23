using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhoPK.GameLogic.Core
{
   public interface IGameLoop
    {
        Task Start();
        //Task UpdateTime();

        //Task UpdatePlayers();
        //  void UpdatePlayers();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//copy pasta https://stackoverflow.com/a/20595549/1395510
namespace WhoPK.GameLogic.Core
{
   public interface IClientMessenger
    {
            void WriteLine(string message, string id);
        void WriteLine(string message);
    }
}

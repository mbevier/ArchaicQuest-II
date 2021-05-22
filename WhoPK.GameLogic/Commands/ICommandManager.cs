using Artemis;
using WhoPK.GameLogic.Character;

namespace WhoPK.GameLogic.Commands
{
    public interface ICommandManager
    {
        public bool ProcessCommand(string command, Entity entity);
    }
}
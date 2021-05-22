using Artemis;
using WhoPK.GameLogic.Character;

namespace WhoPK.GameLogic.Commands
{
    public interface IVisual
    {
        public void Look(Entity entity, string args);
    }
}
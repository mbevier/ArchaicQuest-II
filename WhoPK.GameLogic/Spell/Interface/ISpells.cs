using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Spell.Interface
{
    public interface ISpells
    {
        void DoSpell(Model.Spell spell, Player origin, Player target, Room room = null);
    }
}


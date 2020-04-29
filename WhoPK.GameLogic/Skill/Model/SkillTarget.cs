using WhoPK.GameLogic.Character;
using WhoPK.GameLogic.World.Room;

namespace WhoPK.GameLogic.Skill.Model
{
    public class SkillTarget
    {
        public Skill Skill { get; set; }
        public Player Origin { get; set; }
        public Player Target { get; set; }
        public Room Room { get; set; }
 
    }
}

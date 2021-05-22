using WhoPK.GameLogic.Core;
using WhoPK.GameLogic.Skill.Enum;
using WhoPK.GameLogic.Skill.Model;
using WhoPK.GameLogic.Skill.Type;
using System;
using System.Collections.Generic;

namespace WhoPK.GameLogic.Spell
{
    public class SpellEffect
    {
        private static IClientMessenger _writer;
        private static SkillTarget _skillTarget;
        private static int _value;

        public SpellEffect(IClientMessenger writer, SkillTarget skillTarget, int value)
        {
            _writer = writer;
            _skillTarget = skillTarget;
            _value = value;
        }

        public Dictionary<SkillType, Action> Type { get; set; } = new Dictionary<SkillType, Action>
        {
            {SkillType.Affect, () => new SkillAffect(_writer, _skillTarget, _value).CauseAffect()}
        };

    }
}

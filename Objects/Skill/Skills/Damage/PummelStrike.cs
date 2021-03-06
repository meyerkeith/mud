﻿using Objects.Global;
using Objects.Language;
using static Objects.Damage.Damage;

namespace Objects.Skill.Skills.Damage
{
    public class PummelStrike : BaseDamageSkill
    {
        public PummelStrike() : base(nameof(PummelStrike),
          GlobalReference.GlobalValues.DefaultValues.DiceForSkillLevel(70).Die,
          GlobalReference.GlobalValues.DefaultValues.DiceForSkillLevel(70).Sides,
          DamageType.Bludgeon)
        {
            PerformerNotificationSuccess = new TranslationMessage("Taking pummel of your weapon you strike it against {target}.");
            RoomNotificationSuccess = new TranslationMessage("{performer} strikes their pummel against {target}.");
            TargetNotificationSuccess = new TranslationMessage("{performer} hits you with their pummel.");
        }

        public override string TeachMessage {get;} = "Take the pummel of your weapon and the opponent over the head like this. //thud";
    }
}

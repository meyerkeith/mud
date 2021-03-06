﻿using Objects.Global;
using Objects.Language;
using static Objects.Damage.Damage;

namespace Objects.Skill.Skills.Damage
{
    public class SunShine : BaseDamageSkill
    {
        public SunShine() : base(nameof(SunShine),
            GlobalReference.GlobalValues.DefaultValues.DiceForSkillLevel(60).Die,
            GlobalReference.GlobalValues.DefaultValues.DiceForSkillLevel(60).Sides,
            DamageType.Bludgeon)
        {
            PerformerNotificationSuccess = new TranslationMessage("Taking a step back you charge towards {target}.  Startled they widen their stance and brace for the impact allowing you slide between their legs and hit them where the sun don't shine causing them to howl in pain.");
            RoomNotificationSuccess = new TranslationMessage("{performer} takes a step back and charges toward {target}.  Taken off guard {target} barely has time to widen their stance and brace for impact as {performer} slides beneath their legs. {target} howles in pain.");
            TargetNotificationSuccess = new TranslationMessage("{performer} charges toward you.  You quickly attempt to brace for the impact but it never comes instead {performer} slides between your legs and hits you where the sun don't shine.");
        }

        public override string TeachMessage {get;} = "Trick your opponent to lower their guard and make them pay for it.";
    }
}

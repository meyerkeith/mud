﻿using Objects.Ability.Interface;
using Objects.Command;
using Objects.Command.Interface;
using Objects.Damage.Interface;
using Objects.Effect;
using Objects.Effect.Interface;
using Objects.Global;
using Objects.Language.Interface;
using Objects.Mob;
using Objects.Mob.Interface;
using Objects.Room.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Objects.Ability
{
    public abstract class Ability : IAbility
    {
        [ExcludeFromCodeCoverage]
        public ITranslationMessage RoomNotificationSuccess { get; set; }
        [ExcludeFromCodeCoverage]
        public ITranslationMessage TargetNotificationSuccess { get; set; }
        [ExcludeFromCodeCoverage]
        public ITranslationMessage PerformerNotificationSuccess { get; set; }

        [ExcludeFromCodeCoverage]
        public ITranslationMessage RoomNotificationFailure { get; set; }
        [ExcludeFromCodeCoverage]
        public ITranslationMessage TargetNotificationFailure { get; set; }
        [ExcludeFromCodeCoverage]
        public ITranslationMessage PerformerNotificationFailure { get; set; }


        [ExcludeFromCodeCoverage]
        public IEffect Effect { get; set; }
        [ExcludeFromCodeCoverage]
        public IEffectParameter Parameter { get; set; } = new EffectParameter();
        [ExcludeFromCodeCoverage]
        public string AbilityName { get; set; }


        [ExcludeFromCodeCoverage]
        public IResult PerformAbility(IMobileObject performer, ICommand command)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        public IResult AbilityFailed(IMobileObject performer, IMobileObject target)
        {
            throw new NotImplementedException();
        }


        [ExcludeFromCodeCoverage]
        public virtual bool MeetRequirments(IMobileObject performer, IMobileObject target)
        {
            return true;
        }

        [ExcludeFromCodeCoverage]
        public virtual bool IsSuccessful(IMobileObject performer, IMobileObject target)
        {
            return true;
        }

        [ExcludeFromCodeCoverage]
        public virtual IResult RequirementsFailureMessage { get => new Result("Unspecified requirements failure", true); }

        [ExcludeFromCodeCoverage]
        public virtual void AdditionalEffect(IMobileObject performer, IMobileObject target)
        {

        }
    }
}


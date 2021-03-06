﻿using Objects.Effect.Interface;
using System.Diagnostics.CodeAnalysis;
using Objects.Room.Interface;
using Objects.Interface;
using Objects.Item.Interface;
using Objects.Mob.Interface;
using Objects.Die.Interface;
using Objects.Language.Interface;
using static Objects.Damage.Damage;
using Objects.Item.Items.Interface;
using Objects.Global.Direction;
using static Objects.Global.Direction.Directions;

namespace Objects.Effect
{
    public class EffectParameter : IEffectParameter
    {
        [ExcludeFromCodeCoverage]
        public Objects.Damage.Interface.IDamage Damage { get; set; }
        [ExcludeFromCodeCoverage]
        public IMobileObject Defender { get; set; }
        [ExcludeFromCodeCoverage]
        public IMobileObject Attacker { get; set; }
        [ExcludeFromCodeCoverage]
        public IBaseObject Target { get; set; }
        [ExcludeFromCodeCoverage]
        public IDice Dice { get; set; }
        [ExcludeFromCodeCoverage]
        public DamageType DamageType { get; set; }
        [ExcludeFromCodeCoverage]
        public IItem Item { get; set; }
        [ExcludeFromCodeCoverage]
        public IBaseObjectId ObjectId { get; set; }
        [ExcludeFromCodeCoverage]
        public IRoom ObjectRoom { get; set; }
        [ExcludeFromCodeCoverage]
        public IBaseObjectId RoomId { get; set; }
        [ExcludeFromCodeCoverage]
        public ITranslationMessage PerformerMessage { get; set; }
        [ExcludeFromCodeCoverage]
        public ITranslationMessage TargetMessage { get; set; }
        [ExcludeFromCodeCoverage]
        public ITranslationMessage RoomMessage { get; set; }
        [ExcludeFromCodeCoverage]
        public IMobileObject Performer { get; set; }
        [ExcludeFromCodeCoverage]
        public IContainer Container { get; set; }
        [ExcludeFromCodeCoverage]
        public Direction Direction { get; set; }
        [ExcludeFromCodeCoverage]
        public string Description { get; set; }
    }
}
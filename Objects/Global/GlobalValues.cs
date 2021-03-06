﻿using Objects.Command.World;
using Objects.Command.World.Interface;
using Objects.Global.CanMobDoSomething.Interface;
using Objects.Global.Commands.Interface;
using Objects.Global.Weapon;
using Objects.Global.Weapon.Interface;
using Objects.Global.DefaultValues.Interface;
using Objects.Global.Engine.Interface;
using Objects.Global.Exp.Interface;
using Objects.Global.FindObjects.Interface;
using Objects.Global.GameDateTime;
using Objects.Global.GameDateTime.Interface;
using Objects.Global.Guild.Interface;
using Objects.Global.Interface;
using Objects.Global.Language.Interface;
using Objects.Global.LevelDifference;
using Objects.Global.LevelDifference.Interface;
using Objects.Global.Logging.Interface;
using Objects.Global.Map.Interface;
using Objects.Global.MoneyToCoins.Interface;
using Objects.Global.MultiClassBonus.Interface;
using Objects.Global.Notify.Interface;
using Objects.Global.Random.Interface;
using Objects.Global.Serialization.Interface;
using Objects.Global.Settings.Interface;
using Objects.Global.StringManuplation.Interface;
using Objects.Global.TickTimes.Interface;
using Objects.Global.UpTime.Interface;
using Objects.Global.ValidateAsset.Interface;
using Objects.World.Interface;
using Shared.FileIO;
using Shared.FileIO.Interface;
using Shared.PerformanceCounters;
using Shared.PerformanceCounters.Interface;
using Shared.TagWrapper;
using Shared.TagWrapper.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Objects.Global
{
    public class GlobalValues : IGlobalValues
    {
        [ExcludeFromCodeCoverage]
        public DateTime StartTime { get; set; }

        [ExcludeFromCodeCoverage]
        public uint TickCounter { get; set; } = 0;

        [ExcludeFromCodeCoverage]
        public List<ICounters> CountersLog { get; set; }

        #region Classes
        [ExcludeFromCodeCoverage]
        public ICanMobDoSomething CanMobDoSomething { get; set; }

        [ExcludeFromCodeCoverage]
        public ICommandList CommandList { get; set; }

        [ExcludeFromCodeCoverage]
        public ICounters Counters { get; set; }

        [ExcludeFromCodeCoverage]
        public IWeaponId WeaponId { get; set; }

        [ExcludeFromCodeCoverage]
        public IDefaultValues DefaultValues { get; set; }

        [ExcludeFromCodeCoverage]
        public IEngine Engine { get; set; }

        [ExcludeFromCodeCoverage]
        public IEvaluateLevelDifference EvaluateLevelDifference { get; set; }

        [ExcludeFromCodeCoverage]
        public IExperience Experience { get; set; }

        [ExcludeFromCodeCoverage]
        public IFindObjects FindObjects { get; set; }

        [ExcludeFromCodeCoverage]
        public IInGameDateTime GameDateTime { get; set; }

        [ExcludeFromCodeCoverage]
        public IGameStats GameStats { get; set; }

        [ExcludeFromCodeCoverage]
        public IGuildAbilities GuildAbilities { get; set; }

        [ExcludeFromCodeCoverage]
        public ILogger Logger { get; set; }

        [ExcludeFromCodeCoverage]
        public IMap Map { get; set; }

        [ExcludeFromCodeCoverage]
        public IMoneyToCoins MoneyToCoins { get; set; }

        [ExcludeFromCodeCoverage]
        public IMultiClassBonus MultiClassBonus { get; set; }

        [ExcludeFromCodeCoverage]
        public INotify Notify { get; set; }

        [ExcludeFromCodeCoverage]
        public IParser Parser { get; set; }

        [ExcludeFromCodeCoverage]
        public IRandom Random { get; set; }

        [ExcludeFromCodeCoverage]
        public IRandomDropGenerator RandomDropGenerator { get; set; }

        [ExcludeFromCodeCoverage]
        public ISettings Settings { get; set; }

        [ExcludeFromCodeCoverage]
        public ISerialization Serialization { get; set; }

        [ExcludeFromCodeCoverage]
        public IStringManipulator StringManipulator { get; set; }

        [ExcludeFromCodeCoverage]
        public ITickTimes TickTimes { get; set; }

        [ExcludeFromCodeCoverage]
        public ITime Time { get; set; }

        [ExcludeFromCodeCoverage]
        public ITranslator Translator { get; set; }

        [ExcludeFromCodeCoverage]
        public IUpTime UpTime { get; set; }

        [ExcludeFromCodeCoverage]
        public IValidateAsset ValidateAsset { get; set; }

        [ExcludeFromCodeCoverage]
        public IWorld World { get; set; }


        #region Shared
        [ExcludeFromCodeCoverage]
        public IFileIO FileIO { get; set; }

        [ExcludeFromCodeCoverage]
        public ITagWrapper TagWrapper { get; set; }
        #endregion Shared
        #endregion Classes

        public void Initilize()
        {
            StartTime = DateTime.Now;

            //got to do this before the command list
            TagWrapper = new TagWrapper();
            World = new World.World();
            Settings = new Settings.Settings();
            Translator = new Language.Translator(new Language.TranslatorAlgorithm());





            CanMobDoSomething = new CanMobDoSomething.CanMobDoSomething();
            CommandList = new Commands.CommandList();
            Counters = new Counters();
            CountersLog = new List<ICounters>();
            DefaultValues = new DefaultValues.DefaultValues();
            Engine = new Engine.Engine();
            EvaluateLevelDifference = new EvaluateLevelDifference();
            Experience = new Exp.Experience();
            FileIO = new Shared.FileIO.FileIO();
            FindObjects = new FindObjects.FindObjects();
            GameDateTime = new InGameDateTime(new Time());
            GameStats = new GameStats();
            GuildAbilities = new Guild.GuildAbilities();
            Logger = new Logging.Logger();
            Map = new Map.Map();
            MoneyToCoins = new MoneyToCoins.MoneyToCoins();
            MultiClassBonus = new MultiClassBonus.MultiClassBonus();
            Notify = new Notify.Notify();
            Parser = new Commands.Parser();
            Random = new Random.Random();
            RandomDropGenerator = new Random.RandomDropGenerator();
            Serialization = new Serialization.JsonSerialization();
            StringManipulator = new StringManuplation.StringManipulator();
            TickTimes = new TickTimes.TickTimes();
            Time = new Time();
            UpTime = new UpTime.UpTime();
            ValidateAsset = new ValidateAsset.ValidateAsset();




            //do these last as they depend on things above
            WeaponId = new WeaponId();
        }
    }
}
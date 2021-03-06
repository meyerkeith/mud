﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Objects.Command;
using Objects.Command.Interface;
using Objects.Command.PC;
using Objects.Global;
using Objects.Mob.Interface;
using Objects.Personality.Interface;
using Objects.Room.Interface;
using Shared.TagWrapper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using static Objects.Damage.Damage;
using static Objects.Item.Items.Equipment;
using static Shared.TagWrapper.TagWrapper;

namespace ObjectsUnitTest.Command.PC
{
    [TestClass]
    public class CraftUnitTest
    {
        IMobileObjectCommand command;
        Mock<ITagWrapper> tagWrapper;
        Mock<INonPlayerCharacter> npc;
        Mock<IPlayerCharacter> pc;
        Mock<ICommand> mockCommand;
        Mock<IRoom> room;
        Mock<ICraftsman> craftsman;
        List<IParameter> parameters;
        Mock<IParameter> position;
        Mock<IParameter> level;
        Mock<IParameter> keyword;
        Mock<IParameter> sentenceDescription;
        Mock<IParameter> shortDescription;
        Mock<IParameter> lookDescription;
        Mock<IParameter> examineDescription;
        Mock<IParameter> damageType;

        [TestInitialize]
        public void Setup()
        {
            GlobalReference.GlobalValues = new GlobalValues();

            tagWrapper = new Mock<ITagWrapper>();
            npc = new Mock<INonPlayerCharacter>();
            pc = new Mock<IPlayerCharacter>();
            mockCommand = new Mock<ICommand>();
            room = new Mock<IRoom>();
            craftsman = new Mock<ICraftsman>();
            parameters = new List<IParameter>();
            position = new Mock<IParameter>();
            level = new Mock<IParameter>();
            keyword = new Mock<IParameter>();
            sentenceDescription = new Mock<IParameter>();
            shortDescription = new Mock<IParameter>();
            lookDescription = new Mock<IParameter>();
            examineDescription = new Mock<IParameter>();
            damageType = new Mock<IParameter>();

            tagWrapper.Setup(e => e.WrapInTag(It.IsAny<string>(), TagType.Info)).Returns((string x, TagType y) => (x));
            pc.Setup(e => e.Room).Returns(room.Object);
            pc.Setup(e => e.KeyWords).Returns(new List<string>() { "pc" });
            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>());
            npc.Setup(e => e.Personalities).Returns(new List<IPersonality>() { craftsman.Object });
            npc.Setup(e => e.Level).Returns(2);
            mockCommand.Setup(e => e.Parameters).Returns(parameters);
            level.Setup(e => e.ParameterValue).Returns("1");
            keyword.Setup(e => e.ParameterValue).Returns("keyword");
            sentenceDescription.Setup(e => e.ParameterValue).Returns("sentenceDescription");
            shortDescription.Setup(e => e.ParameterValue).Returns("shortDescription");
            lookDescription.Setup(e => e.ParameterValue).Returns("lookDescription");
            examineDescription.Setup(e => e.ParameterValue).Returns("examineDescription");
            damageType.Setup(e => e.ParameterValue).Returns("slash");

            GlobalReference.GlobalValues.TagWrapper = tagWrapper.Object;

            command = new Craft();
        }

        [TestMethod]
        public void Craft_Instructions()
        {
            IResult result = command.Instructions;

            Assert.IsTrue(result.AllowAnotherCommand);
            Assert.AreEqual("Craft [Position] [Level] [Keyword] [\"SentenceDescription\"] [\"ShortDescription\"] [\"LookDescription\"] [\"ExamineDescription\"] {DamageType}", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_CommandTrigger()
        {
            IEnumerable<string> result = command.CommandTrigger;
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains("Craft"));
        }

        [TestMethod]
        public void Craft_PerformCommand_NotPc()
        {
            IResult result = command.PerformCommand(npc.Object, mockCommand.Object);
            Assert.IsTrue(result.AllowAnotherCommand);
            Assert.AreEqual("Only player characters can have craftsman craft items.", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_NoCraftMan()
        {
            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsTrue(result.AllowAnotherCommand);
            Assert.AreEqual("There is no craftsman to make anything for you.", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_NotEnoughParameters()
        {
            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsTrue(result.AllowAnotherCommand);
            Assert.AreEqual("Please provide all the parameters needed for the craftsman to make your item.", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_NotEnoughParametersForWeapon()
        {
            position.Setup(e => e.ParameterValue).Returns("wield");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsTrue(result.AllowAnotherCommand);
            Assert.AreEqual("Damage type is required for weapons.", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_Arms()
        {
            position.Setup(e => e.ParameterValue).Returns("arms");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Arms, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Acid)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_Body()
        {
            position.Setup(e => e.ParameterValue).Returns("body");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Body, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Acid)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_Feet()
        {
            position.Setup(e => e.ParameterValue).Returns("feet");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Feet, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Acid)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_Finger()
        {
            position.Setup(e => e.ParameterValue).Returns("finger");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Finger, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Acid)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_Hand()
        {
            position.Setup(e => e.ParameterValue).Returns("hand");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Hand, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Acid)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_Head()
        {
            position.Setup(e => e.ParameterValue).Returns("Head");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Head, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Acid)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_Held()
        {
            position.Setup(e => e.ParameterValue).Returns("held");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Held, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Acid)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_Legs()
        {
            position.Setup(e => e.ParameterValue).Returns("legs");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Legs, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Acid)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_Neck()
        {
            position.Setup(e => e.ParameterValue).Returns("neck");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Neck, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Acid)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_Waist()
        {
            position.Setup(e => e.ParameterValue).Returns("waist");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Waist, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Acid)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_WieldBludgeon()
        {
            damageType.Setup(e => e.ParameterValue).Returns("bludgeon");

            position.Setup(e => e.ParameterValue).Returns("wield");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Wield, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Bludgeon)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_WieldPierce()
        {
            damageType.Setup(e => e.ParameterValue).Returns("pierce");

            position.Setup(e => e.ParameterValue).Returns("wield");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Wield, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Pierce)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_WieldSlash()
        {
            position.Setup(e => e.ParameterValue).Returns("wield");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Wield, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Slash)).Returns(new Result("", false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_ArgumentException()
        {
            position.Setup(e => e.ParameterValue).Returns("error");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Wield, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Slash)).Returns(new Result(null, false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsTrue(result.AllowAnotherCommand);
            Assert.AreEqual(@"Available positions are Wield,
                          Head,
                          Neck,
                          Arms,
                          Hand,
                          Finger,
                          Body,
                          Waist,
                          Legs,
                          Feet,
                          Held ", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_ArgumentException2()
        {
            damageType.Setup(e => e.ParameterValue).Returns("error");

            position.Setup(e => e.ParameterValue).Returns("wield");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Wield, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Slash)).Returns(new Result(null, false));

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsTrue(result.AllowAnotherCommand);
            Assert.AreEqual(@"Available damage types are Bludgeon,
                             Pierce,
                             Slash", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_MakeItem_Error()
        {
            position.Setup(e => e.ParameterValue).Returns("wield");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);


            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            craftsman.Setup(e => e.Build(npc.Object, pc.Object, AvalableItemPosition.Wield, 1, "keyword", "sentenceDescription", "shortDescription", "lookDescription", "examineDescription", DamageType.Slash)).Throws(new Exception());

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsTrue(result.AllowAnotherCommand);
            Assert.AreEqual("Please verify all parameters and try again.", result.ResultMessage);
        }

        [TestMethod]
        public void Craft_PerformCommand_CraftmanNotHighEnoughLevel()
        {
            level.Setup(e => e.ParameterValue).Returns("3");
            room.Setup(e => e.NonPlayerCharacters).Returns(new List<INonPlayerCharacter>() { npc.Object });
            position.Setup(e => e.ParameterValue).Returns("Head");
            parameters.Add(position.Object);
            parameters.Add(level.Object);
            parameters.Add(keyword.Object);
            parameters.Add(sentenceDescription.Object);
            parameters.Add(shortDescription.Object);
            parameters.Add(lookDescription.Object);
            parameters.Add(examineDescription.Object);
            parameters.Add(damageType.Object);

            IResult result = command.PerformCommand(pc.Object, mockCommand.Object);
            Assert.IsTrue(result.AllowAnotherCommand);
            Assert.AreEqual(null, result.ResultMessage);
            npc.Verify(e => e.EnqueueCommand("Tell pc That is above my skill level.  You will need to find someone a higher level to craft such an item."));
        }
    }
}

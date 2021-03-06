﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Objects.Command.Interface;
using Moq;
using Shared.TagWrapper.Interface;
using Objects.Mob.Interface;
using Objects.Global;
using static Shared.TagWrapper.TagWrapper;
using Objects.Command.PC;
using System.Collections.Generic;
using System.Linq;
using static Objects.Mob.MobileObject;
using Objects.Global.Engine.Interface;
using Objects.Global.Engine.Engines.Interface;

namespace ObjectsUnitTest.Command.PC
{
    [TestClass]
    public class RelaxUnitTest
    {
        IMobileObjectCommand command;
        Mock<ITagWrapper> tagWrapper;
        Mock<IMobileObject> mob;
        Mock<ICommand> mockCommand;

        [TestInitialize]
        public void Setup()
        {
            GlobalReference.GlobalValues = new GlobalValues();

            tagWrapper = new Mock<ITagWrapper>();
            tagWrapper.Setup(e => e.WrapInTag(It.IsAny<string>(), TagType.Info)).Returns((string x, TagType y) => (x));
            GlobalReference.GlobalValues.TagWrapper = tagWrapper.Object;

            mob = new Mock<IMobileObject>();
            mockCommand = new Mock<ICommand>();
            command = new Relax();

            mockCommand.Setup(e => e.Parameters).Returns(new List<IParameter>());
        }

        [TestMethod]
        public void Relax_Instructions()
        {
            IResult result = command.Instructions;
            Assert.IsTrue(result.AllowAnotherCommand);
            Assert.AreEqual("Relax", result.ResultMessage);
        }

        [TestMethod]
        public void Relax_CommandTrigger()
        {
            IEnumerable<string> result = command.CommandTrigger;
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains("Relax"));
        }

        [TestMethod]
        public void Relax_PerformCommand()
        {
            Mock<IEngine> engine = new Mock<IEngine>();
            Mock<IEvent> evnt = new Mock<IEvent>();

            engine.Setup(e => e.Event).Returns(evnt.Object);

            GlobalReference.GlobalValues.Engine = engine.Object;

            IResult result = command.PerformCommand(mob.Object, mockCommand.Object);
            Assert.IsFalse(result.AllowAnotherCommand);
            Assert.AreEqual("You lay down and relax.", result.ResultMessage);
            mob.VerifySet(e => e.Position = CharacterPosition.Relax);
            evnt.Verify(e => e.Relax(mob.Object), Times.Once);
        }
    }
}

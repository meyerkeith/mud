﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Objects.Die.Interface;
using Objects.Global;
using Objects.Global.DefaultValues.Interface;
using Objects.Magic.Spell.Damage;
using Shared.TagWrapper.Interface;
using static Shared.TagWrapper.TagWrapper;

namespace ObjectsUnitTest.Magic.Spell.Damage
{
    [TestClass]
    public class SmiteUnitTest
    {
        Smite smite;
        Mock<IDefaultValues> defaultValues;
        Mock<IDice> dice;
        Mock<ITagWrapper> tagWrapper;
        [TestInitialize]
        public void Setup()
        {
            defaultValues = new Mock<IDefaultValues>();
            dice = new Mock<IDice>();
            tagWrapper = new Mock<ITagWrapper>();

            defaultValues.Setup(e => e.DiceForSpellLevel(100)).Returns(dice.Object);
            tagWrapper.Setup(e => e.WrapInTag(It.IsAny<string>(), TagType.Info)).Returns((string x, TagType y) => (x));

            GlobalReference.GlobalValues.DefaultValues = defaultValues.Object;
            GlobalReference.GlobalValues.TagWrapper = tagWrapper.Object;

            smite = new Smite();
        }

        [TestMethod]
        public void Smite()
        {
            defaultValues.Verify(e => e.DiceForSpellLevel(100), Times.Exactly(2));
            Assert.AreEqual("{performer} looks at you with anger in his eyes.  Sundenly you are knocked back by a great force.", smite.PerformerNotificationSuccess.Message);
            Assert.AreEqual("{performer} looks at {target} with a great anger in their eyes.  {target} is knocked back by some type of invisable force.", smite.RoomNotificationSuccess.Message);
            Assert.AreEqual("{performer} looks at you with great anger.  Suddenly a great force knocks you back.", smite.TargetNotificationSuccess.Message);
        }
    }
}

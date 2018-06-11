﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Objects.Mob.Interface;
using Objects.Personality.Personalities;
using Objects.Personality.Personalities.ResponderMisc.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectsUnitTest.Personality.Personalities
{
    [TestClass]
    public class ResponderUnitTest
    {
        Responder responder;
        Mock<INonPlayerCharacter> npc;
        Mock<IResponse> response;
        List<IOptionalWords> optionalWords;

        [TestInitialize]
        public void Setup()
        {
            responder = new Responder();
            npc = new Mock<INonPlayerCharacter>();
            response = new Mock<IResponse>();
            optionalWords = new List<IOptionalWords>();

            responder.NonPlayerCharacter = npc.Object;
            npc.SetupSequence(e => e.DequeueMessage()).Returns("<Communication>mobName tells you hello there</Communication>")
                                                    .Returns("<Communication>mobName say hello there</Communication>")
                                                    .Returns("<Communication>mobName shouts hello there</Communication>");
            response.Setup(e => e.Message).Returns("returnMessge");
            response.Setup(e => e.RequiredWordSets).Returns(optionalWords);
            response.Setup(e => e.Match(new List<string>() { "hello", "there" })).Returns(true);
        }

        [TestMethod]
        public void Responder_Process_ThreeMessage()
        {
            responder.Process(npc.Object, null);
        }
    }
}
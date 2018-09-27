﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Objects.GameDateTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectsUnitTest.GameDateTime
{
    [TestClass]
    public class GameDateTimeUnitTest
    {
        Objects.GameDateTime.GameDateTime gameDateTime;

        [TestInitialize]
        public void Setup()
        {
            gameDateTime = new Objects.GameDateTime.GameDateTime(new DateTime(2015, 11, 7, 16, 43, 0, DateTimeKind.Utc));
        }

        [TestMethod]
        public void GameDateTime_Constructor()
        {
            Assert.AreEqual(1, gameDateTime.Day);
            Assert.AreEqual(1, gameDateTime.Month);
            Assert.AreEqual(1, gameDateTime.Year);
            Assert.AreEqual(0, gameDateTime.Hour);
            Assert.AreEqual(0, gameDateTime.Minute);

            //Convert enums to their int versions for comparison
            Assert.AreEqual(0, (int)gameDateTime.DayName);
            Assert.AreEqual(0, (int)gameDateTime.MonthName);
            Assert.AreEqual(0, (int)gameDateTime.YearName);
        }

        [TestMethod]
        public void GameDateTime_Non0Constructor()
        {
            gameDateTime = new Objects.GameDateTime.GameDateTime(new DateTime(2015, 11, 14, 5, 9, 2, DateTimeKind.Utc));

            Assert.AreEqual(2, gameDateTime.Day);
            Assert.AreEqual(2, gameDateTime.Month);
            Assert.AreEqual(2, gameDateTime.Year);
            Assert.AreEqual(2, gameDateTime.Hour);
            Assert.AreEqual(2, gameDateTime.Minute);

            //Convert enums to their int versions for comparison
            Assert.AreEqual(1, (int)gameDateTime.DayName);
            Assert.AreEqual(1, (int)gameDateTime.MonthName);
            Assert.AreEqual(1, (int)gameDateTime.YearName);
        }

        [TestMethod]
        public void GameDateTime_AddDays_1()
        {
            gameDateTime.AddDays(1);

            Assert.AreEqual(2, gameDateTime.Day);
            Assert.AreEqual(1, gameDateTime.Month);
            Assert.AreEqual(1, gameDateTime.Year);
            Assert.AreEqual(0, gameDateTime.Hour);
            Assert.AreEqual(0, gameDateTime.Minute);

            //Convert enums to their int versions for comparison
            Assert.AreEqual(1, (int)gameDateTime.DayName);
            Assert.AreEqual(0, (int)gameDateTime.MonthName);
            Assert.AreEqual(0, (int)gameDateTime.YearName);
        }

        [TestMethod]
        public void GameDateTime_AddDays_30()
        {
            gameDateTime.AddDays(30);

            Assert.AreEqual(1, gameDateTime.Day);
            Assert.AreEqual(2, gameDateTime.Month);
            Assert.AreEqual(1, gameDateTime.Year);
            Assert.AreEqual(0, gameDateTime.Hour);
            Assert.AreEqual(0, gameDateTime.Minute);

            //Convert enums to their int versions for comparison
            Assert.AreEqual(0, (int)gameDateTime.DayName);
            Assert.AreEqual(1, (int)gameDateTime.MonthName);
            Assert.AreEqual(0, (int)gameDateTime.YearName);
        }

        [TestMethod]
        public void GameDateTime_AddDays_360()
        {
            gameDateTime.AddDays(360);

            Assert.AreEqual(1, gameDateTime.Day);
            Assert.AreEqual(1, gameDateTime.Month);
            Assert.AreEqual(2, gameDateTime.Year);
            Assert.AreEqual(0, gameDateTime.Hour);
            Assert.AreEqual(0, gameDateTime.Minute);

            //Convert enums to their int versions for comparison
            Assert.AreEqual(0, (int)gameDateTime.DayName);
            Assert.AreEqual(0, (int)gameDateTime.MonthName);
            Assert.AreEqual(1, (int)gameDateTime.YearName);
        }

        [TestMethod]
        public void GameDateTime_LessThan_True()
        {
            Assert.IsTrue(gameDateTime.IsLessThan(new Objects.GameDateTime.GameDateTime(DateTime.Now)));
        }

        [TestMethod]
        public void GameDateTime_GreaterThan_True()
        {
            Assert.IsTrue(new Objects.GameDateTime.GameDateTime(DateTime.Now).IsGreaterThan(gameDateTime));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GameDateTime_GameDataNull()
        {
            gameDateTime.IsLessThan(null);
        }

        [TestMethod]
        public void GameDateTime_DifferentYear()
        {
            Assert.IsTrue(new Objects.GameDateTime.GameDateTime().IsLessThan(new Objects.GameDateTime.GameDateTime() { Year = 9 }));
        }

        [TestMethod]
        public void GameDateTime_DifferentMonth()
        {
            Assert.IsTrue(new Objects.GameDateTime.GameDateTime().IsLessThan(new Objects.GameDateTime.GameDateTime() { Month = 9 }));
        }

        [TestMethod]
        public void GameDateTime_DifferentDay()
        {
            Assert.IsTrue(new Objects.GameDateTime.GameDateTime().IsLessThan(new Objects.GameDateTime.GameDateTime() { Day = 9 }));
        }

        [TestMethod]
        public void GameDateTime_DifferentHour()
        {
            Assert.IsTrue(new Objects.GameDateTime.GameDateTime().IsLessThan(new Objects.GameDateTime.GameDateTime() { Hour = 9 }));
        }

        [TestMethod]
        public void GameDateTime_DifferentMinute()
        {
            Assert.IsTrue(new Objects.GameDateTime.GameDateTime().IsLessThan(new Objects.GameDateTime.GameDateTime() { Minute = 9 }));
        }

        [TestMethod]
        public void GameDateTime_Equal()
        {
            Assert.IsFalse(new Objects.GameDateTime.GameDateTime().IsLessThan(new Objects.GameDateTime.GameDateTime()));
            Assert.IsFalse(new Objects.GameDateTime.GameDateTime().IsGreaterThan(new Objects.GameDateTime.GameDateTime()));
        }
    }
}

using DIKUArcade.Math;
using NUnit.Framework;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using Breakout;
using DIKUArcade.Events;
using DIKUArcade;

namespace BreakoutTests {

    [TestFixture]

    public class PowerUpsC1Test {

        private PowerUps powerUps;

        public PowerUpsC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            powerUps = new PowerUps();
        }

        [SetUp]
        public void InitiatePowerUpsC1() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            powerUps = new PowerUps();
        }

        [Test]
        public void TestC1LifePowerUp() {
            powerUps.LifePowerUp();
            Assert.Pass();
        }
        [Test]
        public void TestC1WidePowerUp() {
            powerUps.WidePowerUp();
            Assert.Pass();
        }
        

        [Test]
        public void TestPlayerSpeedPowerUp() {
            powerUps.PlayerSpeedPowerUp();
            Assert.Pass();
        }


        [Test]
        public void TestDoubleSpeedPowerUp() {
            powerUps.DoubleSpeedPowerUp();
            Assert.Pass();
        }


        [Test]
        public void TestMoreTimePowerUp() {
            powerUps.MoreTimePowerUp();
            Assert.Pass();
        }
    }
}
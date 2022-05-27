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
    public class PowerUpsTests {
        private Player player;
        
        public PowerUpsTests() {
            player = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            player.isWide = false;
            player.isFast = false;
            ball = new Ball();
            ball.isFast = false;
            GameRunning.GetInstance();
        }

        [SetUp]
        public void InitializePowerUps() {
            player = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            player.isWide = false;
            plaier.isFast = false;
            ball = new Ball();
            ball.isFast = false;
            GameRunning.GetInstance();
        }

        //Testing if player´s life is incremented by 1 after calling LifePowerUp().
        [Test]
        public void TestLifePowerUp() {
            player.life = 1;
            PowerUps.LifePowerUp();
            Assert.AreEqual(2, player.life);
        }

        //Testing if player´s width is doubled after calling WidePowerUp().
        [Test]
        public void TestWidePowerUp() {
            player.shape.Extent = new Vec2F(0.16f, 0.020f);
            PowerUps.WidePowerUp();
            Assert.AreEqual(0.32f, player.shape.Extent.X);
        }

        //Testing if player´s speed is doubled after calling PlayerSpeedPowerUp().
        [Test]
        public void TestPlayerSpeedPowerUp() {
            player.MOVEMENT_SPEED = 0.02f;
            PowerUps.PlayerSpeedPowerUp();
            Assert.AreEqual(0.04f, player.MOVEMENT_SPEED);
        }

        //Testing if ball´s speed is doubled after calling DoubleSpeedPowerUp().
        [Test]
        public void TestDoubleSpeedPowerUp() {
            ball.shape.Direction = (0.1f,0.01f);
            PowerUps.DoubleSpeedPowerUp();
            Assert.AreEqual((0.2f,0.02f), ball.shape.Direction);
        }

        //Testing if the timer has gone up 10 seconds after calling MoreTimePowerUp().
        [Test]
        public void TestMoreTimePowerUp() {
            GameRunning.startTime = 150;
            PowerUps.MoreTimePowerUp();
            Assert.AreEqual((160), GameRunning.startTime);
        }
    }
}
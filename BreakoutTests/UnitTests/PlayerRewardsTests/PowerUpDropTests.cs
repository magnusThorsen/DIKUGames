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
    public class PowerUpDropTests {
        
        public PowerUpDropTests() {
            shape = new Shape();
            image = new Image();
            Yvelocity = 0.1f;
            player = new Player();
        }

        [SetUp]
        public void InitializePowerUpDrop() {
            shape = new Shape();
            image = new Image();
            Yvelocity = 0.1f;
            player = new Player();
        }

        //Testing if the right picture renders with the right PowerUp after calling PowerUpDrop().
        [Test]
        public void TestPowerUpDrop() {
            PowerUpDrop.randNumber = 1;
            PowerUpDrop.PowerUpDrop(shape, image);
            Assert.AreEqual(Image, new Image(Path.Combine("Assets", "Images", "DoubleSpeedPowerUp.png")));
        }

        //Testing if a PowerUp drops vertical after calling PowerUpMove().
        [Test]
        public void TestPowerUpMoveDrop() {
            shape.Position = new Vec2F(0.5f,0.5f);
            shape.Direction = new Vec2F(0.0f,-0.01f);;
            Assert.AreEqual((0.0f, 0.49f), shape.Position);
        }

        //Testing if a player can consume (collide with) a power up after calling Consume().
        [Test]
        public void TestPowerUpConsumeDrop() {
            PowerUpDrop.Consume(player, 1);
            Assert.AreEqual(PowerUpDrop.pwUp, DoubleSpeedPowerUp());
        }
    }
}
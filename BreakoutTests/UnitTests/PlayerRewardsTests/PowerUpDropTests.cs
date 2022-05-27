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
        private PowerUpDrop powerUpDrop;
        private Player player;
        
        public PowerUpDropTests() {
            powerUpDrop = new PowerUpDrop( 
                        // powerUpDrop is instantiated with positions and image
                        new DynamicShape(new Vec2F(0.06f, 0.06f), new Vec2F(0.06f, 0.06f)),
                        new Image(Path.Combine("Assets", "Images", "RocketPickUp.png")));
            player = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
        }

        [SetUp]
        public void InitializePowerUpDrop() {
            powerUpDrop = new PowerUpDrop( 
                        // powerUpDrop is instantiated with positions and image
                        new DynamicShape(new Vec2F(0.06f, 0.06f), new Vec2F(0.06f, 0.06f)),
                        new Image(Path.Combine("Assets", "Images", "RocketPickUp.png")));
            player = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.425f, 0.03f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
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
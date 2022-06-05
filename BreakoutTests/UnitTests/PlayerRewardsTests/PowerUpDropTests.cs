using DIKUArcade.Math;
using NUnit.Framework;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using Breakout;
using DIKUArcade.Events;
using DIKUArcade;
using Breakout.BreakoutStates;

namespace BreakoutTests {

    [TestFixture]
    public class PowerUpDropTests {
        private PowerUps powerUps;
        private DynamicShape shape;
        private Image image;
        private Player player;
        private PowerUpDrop drop;


        public PowerUpDropTests() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            powerUps = new PowerUps();
            shape = new DynamicShape(new Vec2F(0.425f, 0.03f), new Vec2F(0.16f, 0.020f));
            image = new Image(Path.Combine("Assets", "Images", "player.png"));
            player = new Player(shape, image);
            drop = new PowerUpDrop( 
                        new DynamicShape(new Vec2F(0.425f, 0.01f), new Vec2F(0.06f, 0.06f)),
                        new Image(Path.Combine("Assets", "Images", "RocketPickUp.png")));
        }

        [SetUp]
        public void InitiatePowerUps() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            powerUps = new PowerUps();
            shape = new DynamicShape(new Vec2F(0.425f, 0.03f), new Vec2F(0.16f, 0.020f));
            image = new Image(Path.Combine("Assets", "Images", "player.png"));
            player = new Player(shape, image);
            drop = new PowerUpDrop( 
                        new DynamicShape(new Vec2F(0.425f, 0.1f), new Vec2F(0.06f, 0.06f)),
                        new Image(Path.Combine("Assets", "Images", "RocketPickUp.png")));
        }


        [Test]
        public void TestMove() {
            Assert.AreEqual(drop.shape.Position.Y, 0.1f);
            drop.Move();
            Assert.True(0.0001 > drop.shape.Position.Y - 0.089f);
        }

        [Test]
        public void TestConsume() {
            for (int i = 0; i < 100; i++) {
                drop.Move();
                drop.Consume(player,1);
            }
            Assert.AreEqual(drop.IsDeleted(), true); 
        }
    }
}
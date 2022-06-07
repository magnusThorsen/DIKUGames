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

    public class PowerUpDropC1Test {

        private PowerUps powerUps;
        private DynamicShape shape;
        private Image image;
        private Player player;
        private PowerUpDrop drop;

        public PowerUpDropC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            powerUps = new PowerUps();
            shape = new DynamicShape(new Vec2F(0.425f, 0.03f), new Vec2F(0.16f, 0.020f));
            image = new Image(Path.Combine("Assets", "Images", "player.png"));
            player = new Player(shape, image);
            drop = new PowerUpDrop( 
                        new DynamicShape(new Vec2F(0.425f, 0.1f), new Vec2F(0.06f, 0.06f)),
                        new Image(Path.Combine("Assets", "Images", "RocketPickUp.png")));
        }

        [SetUp]
        public void InitiatePowerUpsC1() {
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
        public void TestC1Collision0() {
            for (int i = 0; i < 100; i++) {
                drop.Move();
                drop.Consume(player,10);
            }
            Assert.AreEqual(drop.IsDeleted(), true); 
        }


        [Test]
        public void TestC1Collision1() {
            for (int i = 0; i < 100; i++) {
                drop.Move();
                drop.Consume(player,1);
            }
            Assert.AreEqual(drop.IsDeleted(), true); 
        }

        [Test]
        public void TestC1Collision2() {
            for (int i = 0; i < 100; i++) {
                drop.Move();
                drop.Consume(player,2);
            }
            Assert.AreEqual(drop.IsDeleted(), true); 
        }

        [Test]
        public void TestC1Collision3() {
            for (int i = 0; i < 100; i++) {
                drop.Move();
                drop.Consume(player,3);
            }
            Assert.AreEqual(drop.IsDeleted(), true); 
        }
        [Test]
        public void TestC1Collision4() {
            for (int i = 0; i < 100; i++) {
                drop.Move();
                drop.Consume(player,4);
            }
            Assert.AreEqual(drop.IsDeleted(), true); 
        }
        [Test]
        public void TestC1Collision5nomatch() {
            for (int i = 0; i < 100; i++) {
                drop.Move();
                drop.Consume(player,5);
            }
            Assert.AreEqual(drop.IsDeleted(), true); 
        }
        [Test]
        public void TestC1NoCollision() {
            drop.shape.Position = new Vec2F(0.5f,0.5f);
            drop.Move();
            drop.Consume(player, 1);
            Assert.False(drop.IsDeleted());
        }

    }
}
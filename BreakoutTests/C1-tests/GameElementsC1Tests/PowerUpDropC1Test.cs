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

        private PowerUpDrop powerUpDrop;
        private Player player;

        public PowerUpDropC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            powerUpDrop = new PowerUpDrop( 
                        new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.06f, 0.06f)),
                        new Image(Path.Combine("Assets", "Images", "RocketPickUp.png")));
            player  = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
                player.shape.Position = new Vec2F(0.1f,0.1f);
        }

        [SetUp]
        public void InitiatePowerUpsC1() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            powerUpDrop = new PowerUpDrop( 
                        new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.06f, 0.06f)),
                        new Image(Path.Combine("Assets", "Images", "RocketPickUp.png")));
            player  = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            player.shape.Position = new Vec2F(0.1f,0.1f);
        }

        [Test]
        public void TestC1Collision0() {
            //powerUpDrop.Move();
            System.Console.WriteLine(powerUpDrop.shape.Position);
            System.Console.WriteLine(player.shape.Position);
            powerUpDrop.Consume(player, 0);
            Assert.True(powerUpDrop.IsDeleted());
        }


        [Test]
        public void TestC1Collision1() {
            powerUpDrop.Move();
            powerUpDrop.Consume(player, 1);
            Assert.True(powerUpDrop.IsDeleted());
        }

        [Test]
        public void TestC1Collision2() {
            powerUpDrop.Move();
            powerUpDrop.Consume(player, 2);
            Assert.True(powerUpDrop.IsDeleted());
        }

        [Test]
        public void TestC1Collision3() {
            powerUpDrop.Move();
            powerUpDrop.Consume(player, 3);
            Assert.True(powerUpDrop.IsDeleted());
        }
        [Test]
        public void TestC1Collision4() {
            powerUpDrop.Move();
            powerUpDrop.Consume(player, 4);
            Assert.True(powerUpDrop.IsDeleted());
        }
        [Test]
        public void TestC1Collision5nomatch() {
            powerUpDrop.Move();
            powerUpDrop.Consume(player, 5);
            Assert.True(powerUpDrop.IsDeleted());
        }
        [Test]
        public void TestC1NoCollision() {
            powerUpDrop.shape.Position = new Vec2F(0.5f,0.5f);
            powerUpDrop.Move();
            powerUpDrop.Consume(player, 1);
            Assert.False(powerUpDrop.IsDeleted());
        }



    }
}
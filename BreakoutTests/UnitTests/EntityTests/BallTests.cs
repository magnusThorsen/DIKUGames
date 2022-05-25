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
    public class BallTest {
        private float Xvelocity;
        private float Yvelocity;
        private static Vec2F extend; 
        public DynamicShape shape {get;}
        private bool moving;
        private Entity entity;

        public BallTest(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            entity = new Entity(shape, image);
            Xvelocity = 0.01f;
            Yvelocity = 0.01f;
            moving = false;
            shape.Direction = new Vec2F(0.0f,0.01f);
            extend = new Vec2F(0.04f, 0.04f);
        }

        [SetUp]
         public InitiateBall(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            entity = new Entity(shape, image);
            Xvelocity = 0.01f;
            Yvelocity = 0.01f;
            moving = false;
            shape.Direction = new Vec2F(0.0f,0.01f);
            extend = new Vec2F(0.04f, 0.04f);
        }

        [Test]
        public void TestMoveball() {
            startPos = ball.Position;
            ball.moving = true;
            ball.Move(p, b);
            Assert.AreNotEqual(startPos, ball.Position);
        }

        [Test]
        public void TestBounceBlock() {
            ball.Position = new Vec2F(0.5f, 0.5f);
            ball.Direction.Y = 0.01f;
            block = new Block(new Vec2F(0.5f, 0.5f), image);
            Assert.AreEqual(ball.Direction.Y, (-0.01f));            
        }

        [Test]
        public void TestBouncePlayer() {
            ball.Position = new Vec2F(0.1f, 0.1f);
            ball.Direction.Y = -0.01f;
            player = new Player(new Vec2F(0.1f, 0.1f), image);
            Assert.AreEqual(ball.Direction.Y, 0.01f);    
        }

        [Test]
        public void TestBounceWallLeft() {
            startDirection = ball.Direction.X;
            ball.Position.X = -0.1f;
            Assert.AreEqual(startDirection, (-ball.Direction.X));
        }

        [Test]
        public void TestBounceWallRight() {
            startDirection = ball.Direction.X;
            ball.Position.X = 1.0f;
            Assert.AreEqual(startDirection, (-ball.Direction.X));
        }

        [Test]
        public void TestBounceWallTop() {
            startDirection = ball.Direction.Y;
            ball.Position.Y = 1.0f;
            Assert.AreEqual(startDirection, (-ball.Direction.Y));
        }

        [Test]
        public void TestWallBottom() {
            ball.Position.Y = -0.1f;
            Assert.True(ball.entity.IsDeleted());
        }

        [Test]
        public void TestResetBall() {
            ball.Position = (0.7f, 0.05f);
            ball.Reset();
            Assert.AreEqual(ball.Position, (0.3f, 0.03f));
        }
    }
}
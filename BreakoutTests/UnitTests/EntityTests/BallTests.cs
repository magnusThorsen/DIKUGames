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

        private Ball ball; 

        public BallTest() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            ball = new Ball (
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            
        }

        [SetUp]
        public void InitiateBall() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            ball = new Ball (
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
        }

        [Test]
        public void TestMoveball() {
            var startPos = ball.shape.Position;
            ball.moving = true;
            ball.Move(p, b);
            Assert.AreNotEqual(startPos, ball.shape.Position);
        }

        [Test]
        public void TestBounceBlock() {
            ball.shape.Position = new Vec2F(0.5f, 0.5f);
            ball.shape.Direction.Y = 0.01f;
            block = new Block(new Vec2F(0.5f, 0.5f), image);
            Assert.AreEqual(ball.shape.Direction.Y, (-0.01f));            
        }

        [Test]
        public void TestBouncePlayer() {
            ball.shape.Position = new Vec2F(0.1f, 0.1f);
            ball.shape.Direction.Y = -0.01f;
            player = new Player(new Vec2F(0.1f, 0.1f), image);
            Assert.AreEqual(ball.shape.Direction.Y, 0.01f);    
        }

        [Test]
        public void TestBounceWallLeft() {
            var startDirection = ball.shape.Direction.X;
            ball.shape.Position.X = -0.1f;
            Assert.AreEqual(startDirection, (-ball.shape.Direction.X));
        }

        [Test]
        public void TestBounceWallRight() {
            var startDirection = ball.shape.Direction.X;
            ball.shape.Position.X = 1.0f;
            Assert.AreEqual(startDirection, (-ball.shape.Direction.X));
        }

        [Test]
        public void TestBounceWallTop() {
            var startDirection = ball.shape.Direction.Y;
            ball.shape.Position.Y = 1.0f;
            Assert.AreEqual(startDirection, (-ball.shape.Direction.Y));
        }

        [Test]
        public void TestWallBottom() {
            ball.shape.Position.Y = -0.1f;
            Assert.True(ball.entity.IsDeleted());
        }

        [Test]
        public void TestResetBall() {
            ball.shape.Position = (0.7f, 0.05f);
            ball.Reset();
            Assert.AreEqual(ball.shape.Position, (0.3f, 0.03f));
        }
    }
}
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
        private Player player;
        private EntityContainer<Block> blocks;
        private Block block;

        public BallTest() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            ball = new Ball (
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            player = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            blocks = new EntityContainer<Block>(288);
            block = new PowerUpBlock(
                            new DynamicShape(new Vec2F(
                                0.5f, 0.5f), 
                            new Vec2F(1.0f/12, (1.0f/12)/3f)),
                            new Image(Path.Combine("Assets", "Images", "purple-block.png"))
                            );
        }

        [SetUp]
        public void InitiateBall() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            ball = new Ball (
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            player = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            blocks = new EntityContainer<Block>(288);
            block = new PowerUpBlock(
                            new DynamicShape(new Vec2F(
                                0.5f, 0.5f), 
                            new Vec2F(1.0f/12, (1.0f/12)/3f)),
                            new Image(Path.Combine("Assets", "Images", "purple-block.png"))
                            );
        }

        [Test]
        public void TestMoveball() {
            var startPos = ball.shape.Position;
            BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.InputEvent, 
                            Message = "LAUNCH_BALL",
                        }
            );
            ball.Move(player, blocks);
            Assert.AreNotEqual(startPos, ball.shape.Position);
        }

        [Test]
        public void TestBounceBlock() {
            ball.shape.Direction.Y = 0.01f;
            ball.shape.Position = new Vec2F(0.5f, 0.46f);
            block.shape.Position = new Vec2F(0.5f, 0.5f);
            Assert.AreEqual(ball.shape.Direction.Y, (-0.01f));            
        }

        [Test]
        public void TestBouncePlayer() {
            ball.shape.Position = new Vec2F(0.5f, 0.5f);
            ball.shape.Direction.Y = -0.01f;
            ball.Move(player, blocks);
            Assert.AreEqual(ball.shape.Direction.Y, 0.01f);    
        }

        [Test]
        public void TestBounceWallLeft() {
            var startDirection = ball.shape.Direction.X;
            ball.shape.Position.X = -0.1f;
            ball.Move(player, blocks);
            Assert.AreEqual(startDirection, (-ball.shape.Direction.X));
        }

        [Test]
        public void TestBounceWallRight() {
            var startDirection = ball.shape.Direction.X;
            ball.shape.Position.X = 1.0f;
            ball.Move(player, blocks);
            Assert.AreEqual(startDirection, (-ball.shape.Direction.X));
        }

        [Test]
        public void TestBounceWallTop() {
            var startDirection = ball.shape.Direction.Y;
            ball.shape.Position.Y = (1.0f-ball.shape.Extent.Y);
            ball.Move(player, blocks);
            Assert.AreEqual(startDirection, (-ball.shape.Direction.Y));
        }

        [Test]
        public void TestBounceWallBottom() {
            ball.shape.Position.Y = 0.1f;
             ball.shape.Direction.Y = -1;
            ball.Move(player, blocks);
            Assert.IsTrue(block.IsDeleted() == true);
        }

        [Test]
        public void TestResetBall() {
            ball.shape.Position = new Vec2F(0.7f, 0.05f);
            ball.Reset();
            Assert.AreEqual(ball.shape.Position, new Vec2F(0.3f, 0.03f));
        }
    }
}
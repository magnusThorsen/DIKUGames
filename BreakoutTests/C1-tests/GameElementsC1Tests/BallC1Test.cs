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
    public class BallC1Test {

        private Ball ball; 
        private Player player;
        private EntityContainer<Block> blocks;
        private Block block;

        public BallC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            ball = new Ball (
                new DynamicShape(new Vec2F(0.5f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            ball.moving = true;
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, ball);
            player = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            blocks = new EntityContainer<Block>(288);
            block = new PowerUpBlock(
                            new DynamicShape(new Vec2F(
                                0.9f, 0.9f), 
                            new Vec2F(1.0f/12, (1.0f/12)/3f)),
                            new Image(Path.Combine("Assets", "Images", "purple-block.png"))
                            );
            blocks.AddEntity(block);
        }

        [SetUp]
        public void InitiateBall() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            ball = new Ball (
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, ball);
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
            blocks.AddEntity(block);
        }



        [Test]
        public void CoverMoveballMoving() {
            var startPos = ball.shape.Position;
            BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.InputEvent, 
                            Message = "LAUNCH_BALL",
                        }
            );
            BreakoutBus.GetBus().ProcessEventsSequentially(); 
            ball.Move(player, blocks);
            Assert.AreNotEqual(startPos, ball.shape.Position);
        }

        [Test]
        public void CoverMoveballnotMovingNotWide() {
            player.isWide = true;
            ball.moving = false;
            ball.Move(player, blocks);
 
            Assert.AreEqual(0.5625f, ball.shape.Position.X);
            Assert.AreEqual(0.049999997f, ball.shape.Position.Y);
        }

        [Test]
        public void CoverMoveballnotMovingWide() {
            player.isWide = true;
            ball.moving = false;
            ball.Move(player, blocks);
 
            Assert.AreEqual(0.5625f, ball.shape.Position.X);
            Assert.AreEqual(0.049999997f, ball.shape.Position.Y);
        }



        [Test]
        public void TestBounceBlockBot() {
            ball.moving = true;
            ball.shape.Direction.Y = 0.1f;
            ball.shape.Direction.X = 0.0f;
            ball.shape.Position = new Vec2F(0.5f, 0.4f);
            block.shape.Position = new Vec2F(0.5f, 0.5f);
            player.shape.Position = new Vec2F(10.0f, 10.5f);
            ball.Move(player, blocks);
            ball.Move(player, blocks);
            Assert.AreEqual(ball.shape.Direction.Y, (-0.1f));            
        }


        [Test]
        public void TestBounceBlockTop() {
            ball.moving = true;
            ball.shape.Direction.Y = -0.1f;
            ball.shape.Direction.X = 0.0f;
            ball.shape.Position = new Vec2F(0.5f, 0.5f);
            block.shape.Position = new Vec2F(0.5f, 0.4f);
            player.shape.Position = new Vec2F(10.0f, 10.5f);
            ball.Move(player, blocks);
            ball.Move(player, blocks);
            Assert.AreEqual(ball.shape.Direction.Y, (0.1f));     
        }

        [Test]
        public void TestBounceBlockLeft() {
            ball.moving = true;
            ball.shape.Direction.X = 0.1f;
            ball.shape.Direction.Y = 0.0f;
            ball.shape.Position = new Vec2F(0.4f, 0.5f);
            block.shape.Position = new Vec2F(0.5f, 0.5f);
            player.shape.Position = new Vec2F(10.0f, 10.5f);
            ball.Move(player, blocks);
            Assert.AreEqual(ball.shape.Direction.X, (-0.1f));     
        }

        [Test]
        public void TestBounceBlockRight() {
            ball.moving = true;
            ball.shape.Direction.X = -0.1f;
            ball.shape.Direction.Y = 0.0f;
            ball.shape.Position = new Vec2F(0.5f, 0.5f);
            block.shape.Position = new Vec2F(0.4f, 0.5f);
            player.shape.Position = new Vec2F(10.0f, 10.5f);
            ball.Move(player, blocks);
            Assert.AreEqual(ball.shape.Direction.X, (0.1f));        
        }



        [Test]
        public void TestBouncePlayerWideFalseRight() {
            ball.moving = true;
            player.isWide = false;
            ball.shape.Position = new Vec2F(0.51f, 0.51f);
            ball.shape.Direction.Y = -0.1f;
            ball.Move(player, blocks);
            ball.Move(player, blocks);
            Assert.True(0.0001f > ball.shape.Direction.X - 0.01f);    
        }

        [Test]
        public void TestBouncePlayerWideTrueLeft() {
            ball.moving = true;
            player.isWide = true;
            ball.shape.Position = new Vec2F(4.997f, 0.6f);
            player.shape.Position = new Vec2F(5.0f, 0.5f);
            ball.shape.Direction.Y = -0.1f;
            ball.Move(player, blocks);
            System.Console.WriteLine(ball.shape.Position);
            System.Console.WriteLine(player.shape.Position);
            ball.Move(player, blocks);
            System.Console.WriteLine(ball.shape.Position);
            System.Console.WriteLine(player.shape.Position);
            Assert.AreEqual(ball.shape.Direction.X, -0.01f);  
            Assert.AreEqual(ball.shape.Direction.Y, 0.01f);  
        }


        [Test]
        public void TestBouncePlayerMiddle() {
            ball.moving = true;
            ball.shape.Position = new Vec2F(0.50f, 0.51f);
            ball.shape.Direction.Y = -0.01f;
            ball.Move(player, blocks);
            ball.Move(player, blocks);
            Assert.AreEqual(ball.shape.Direction.Y, -0.01f);  
        }


        [Test]
        public void TestBounceWallLeft() {
            ball.moving = true;
            var startDirection = ball.shape.Direction.X;
            ball.shape.Position.X = -0.1f;
            ball.Move(player, blocks);
            ball.Move(player, blocks);
            Assert.AreEqual(startDirection, (-ball.shape.Direction.X));
        }


        [Test]
        public void TestBounceWallTop() {
            ball.moving = true;
            var startDirection = ball.shape.Direction.Y;
            ball.shape.Position.Y = (1.0f-ball.shape.Extent.Y);
            ball.Move(player, blocks);
            ball.Move(player, blocks);
            Assert.AreEqual(startDirection, (-ball.shape.Direction.Y));
        }

        [Test]
        public void TestBounceWallBottom() {
            ball.moving = true;
            ball.shape.Position.X = 1.0f;
            ball.shape.Position.Y = -3.0f;
            ball.shape.Direction.Y = -0.1f;
            ball.Move(player, blocks);
            Assert.True(ball.IsDeleted());
        }

        [Test]
        public void TestResetBall() {
            ball.shape.Position = new Vec2F(0.7f, 0.05f);
            ball.Reset();
            Assert.AreEqual(ball.shape.Position.X, 0.3f);
            Assert.AreEqual(ball.shape.Position.Y, 0.03f);
        }

        [Test]
        public void ProcessEventTest1() {
            ball.moving = false;
            ball.ProcessEvent(new GameEvent {
                            EventType = GameEventType.InputEvent, 
                            Message = "LAUNCH_BALL",
                            });
            Assert.True(ball.moving);
        }

        [Test]
        public void ProcessEventTest2NoCase() {
            ball.moving = false;
            ball.ProcessEvent(new GameEvent {
                            EventType = GameEventType.InputEvent, 
                            Message = "No Match",
                            });
            Assert.False(ball.moving);
        }

        [Test]
        public void ProcessEventTest2NotRightEventtype() {
            ball.moving = false;
            ball.ProcessEvent(new GameEvent {
                            EventType = GameEventType.PlayerEvent, 
                            Message = "No Match",
                            });
            Assert.False(ball.moving);
        }




        [Test]
        public void ProcessEventTest3() {
            ball.ProcessEvent(new GameEvent {
                            EventType = GameEventType.InputEvent, 
                            Message = "IncSpeed",
                            });
            Assert.True(ball.isFast);
        }


        [Test]
        public void TestUpdateBallPowerups() {
            ball.ProcessEvent(new GameEvent {
                            EventType = GameEventType.InputEvent, 
                            Message = "IncSpeed",
                            });
            ball.timeSpeed = -100;
            ball.UpdateBallPowerups();
            Assert.False(ball.isFast);
        }


        [Test]
        public void TestBounceBlockForeach() {
            blocks.ClearContainer();
            ball.Move(player, blocks);
            Assert.False(ball.isFast);
        }
    }
}
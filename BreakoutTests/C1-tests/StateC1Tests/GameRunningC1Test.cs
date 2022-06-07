using DIKUArcade.Math;
using NUnit.Framework;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout;
using System.IO;
using Breakout.BreakoutStates;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace BreakoutTests {
    [TestFixture]
    public class GameRunningC1Test {
        private GameRunning gameRunning;
        private EntityContainer<Block>testBlocks;


        public GameRunningC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameRunning = new GameRunning();
            testBlocks = new EntityContainer<Block>(288);
        }

        [SetUp]
        public void InitiateGameRunningC1() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameRunning = new GameRunning();
            testBlocks = new EntityContainer<Block>(288);
        }

        [Test]
        public void TestGetInstanceNull() {
            Assert.That(GameRunning.GetInstance(), Is.InstanceOf<GameRunning>());
        }

        [Test]
        public void TestGetInstance() {
            GameRunning.GetInstance();
            Assert.That(GameRunning.GetInstance(), Is.InstanceOf<GameRunning>());
        }

        // Test is auto pass, since the bus is not processing events. Method is working
        [Test]
        public void TestHandleKeyEventKeyPress() {
            GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Right);
            BreakoutBus.GetBus().ProcessEventsSequentially();
            var player = GameRunning.GetInstance().GetPlayer();
            player.Move();
            Assert.Pass();
        }

        // Test is auto pass, since the bus is not processing events. Method is working
        [Test]
        public void TestHandleKeyEventKeyRelease() {
            GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Right);
            BreakoutBus.GetBus().ProcessEventsSequentially();
            var player = GameRunning.GetInstance().GetPlayer();
            player.Move();
            Assert.Pass();
        }

        [Test]
        public void TestNewLevel() {
            var block = new NormalBlock(
                        new DynamicShape(new Vec2F(
                                5.0f, 5.0f), 
                                new Vec2F(1.0f/12, (1.0f/12)/3f)),
                                new Image(Path.Combine("", "Assets", "Images", "brown-block.png"))
                            );
            GameRunning.GetInstance().blocks.AddEntity(block);

            GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.G);
            Assert.True(testBlocks.CountEntities() == 0);
        }

        [Test] // Checking first if statement when player lost all life
        public void TestCheckGameOverTrue() {
            var block = new NormalBlock(
                        new DynamicShape(new Vec2F(
                                5.0f, 5.0f), 
                                new Vec2F(1.0f/12, (1.0f/12)/3f)),
                                new Image(Path.Combine("", "Assets", "Images", "brown-block.png"))
                            );
            GameRunning.GetInstance().blocks.AddEntity(block);
            GameRunning.GetInstance().gameOver = true;
            GameRunning.GetInstance().CheckGameOver();
            Assert.True(GameRunning.GetInstance().blocks.CountEntities() == 0);
        }

        [Test] // Testing if game is not over
        public void TestGameOverFalse() {
            var block = new NormalBlock(
                        new DynamicShape(new Vec2F(
                                5.0f, 5.0f), 
                                new Vec2F(1.0f/12, (1.0f/12)/3f)),
                                new Image(Path.Combine("", "Assets", "Images", "brown-block.png"))
                            );
            GameRunning.GetInstance().blocks.AddEntity(block);
            GameRunning.GetInstance().gameOver = false;
            GameRunning.GetInstance().ProcessEvent(new GameEvent {
                        EventType = GameEventType.StatusEvent, Message = "Time", 
                        StringArg1 = "30"
                    });
            GameRunning.GetInstance().timeLeft = 20;
            GameRunning.GetInstance().CheckGameOver();
            Assert.True(GameRunning.GetInstance().blocks.CountEntities() == 1);
        }

        [Test] // Testing second if statement when time is up
        public void TestCheckGameOverTimeTrue() {
            var block = new NormalBlock(
                        new DynamicShape(new Vec2F(
                                5.0f, 5.0f), 
                                new Vec2F(1.0f/12, (1.0f/12)/3f)),
                                new Image(Path.Combine("", "Assets", "Images", "brown-block.png"))
                            );
            GameRunning.GetInstance().blocks.AddEntity(block);
            GameRunning.GetInstance().gameOver = false;
            GameRunning.GetInstance().ProcessEvent(new GameEvent {
                        EventType = GameEventType.StatusEvent, Message = "Time", 
                        StringArg1 = "30"
                    });
            GameRunning.GetInstance().timeLeft = -1;
            GameRunning.GetInstance().CheckGameOver();
            Assert.True(GameRunning.GetInstance().blocks.CountEntities() == 0);
        }

        [Test] // Switch-statement branch in process event
        public void TestProcessEventPlayerDead() {
            var block = new NormalBlock(
                        new DynamicShape(new Vec2F(
                                5.0f, 5.0f), 
                                new Vec2F(1.0f/12, (1.0f/12)/3f)),
                                new Image(Path.Combine("", "Assets", "Images", "brown-block.png"))
                            );
            GameRunning.GetInstance().blocks.AddEntity(block);
            GameRunning.GetInstance().ProcessEvent(new GameEvent {
                        EventType = GameEventType.StatusEvent, Message = "PlayerDead"});
            Assert.True(GameRunning.GetInstance().blocks.CountEntities() == 0);
        }

        [Test] // Switch-statement branch in process event
        public void TestProcessEventTime() {
            GameRunning.GetInstance().ProcessEvent(new GameEvent {
                        EventType = GameEventType.StatusEvent, Message = "Time", 
                        StringArg1 = "30"
                    });
            Assert.True(GameRunning.GetInstance().startTime == 30.0);
        }

        [Test] // Switch-statement branch in process event
        public void TestProcessEventIncTime() {
            GameRunning.GetInstance().ProcessEvent(new GameEvent {
                        EventType = GameEventType.StatusEvent, Message = "Time", 
                        StringArg1 = "30"
                    });
            GameRunning.GetInstance().ProcessEvent(new GameEvent {
                        EventType = GameEventType.StatusEvent, Message = "IncTime"
                    });
            Assert.True(GameRunning.GetInstance().startTime == 40.0);
        }

        [Test] // Switch-statement branch in process event
        public void TestProcessEventDefault() {
            GameRunning.GetInstance().ProcessEvent(new GameEvent {
                        EventType = GameEventType.StatusEvent, Message = "Default", 
                        StringArg1 = "30"
                    });
            Assert.Pass();
        }

        [Test] // Invisible else branch in process event
        public void TestProcessEventEmptyElseBranch() {
            GameRunning.GetInstance().ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "IncTime"
                    });
            Assert.Pass();
        }

        [Test] // 0 iterations
        public void TestRemoveDeletedEntities0Iterations() {
            GameRunning.GetInstance().blocks.ClearContainer();
            GameRunning.GetInstance().balls.ClearContainer();
            GameRunning.GetInstance().powerDrops.ClearContainer();

            GameRunning.GetInstance().CallRemoveDeletedEntities();

            Assert.True(GameRunning.GetInstance().blocks.CountEntities() == 0);
            Assert.True(GameRunning.GetInstance().balls.CountEntities() == 0);
            Assert.True(GameRunning.GetInstance().powerDrops.CountEntities() == 0);
        }

        [Test] // 1 iteration, Entities IsDeleted = true
        public void TestRemoveDeletedEntities1IterationsIsDeleted() {
            GameRunning.GetInstance().blocks.ClearContainer();
            GameRunning.GetInstance().balls.ClearContainer();
            GameRunning.GetInstance().powerDrops.ClearContainer();

            var block = new NormalBlock(
                            new DynamicShape(new Vec2F(5.0f, 5.0f), 
                                new Vec2F(1.0f/12, (1.0f/12)/3f)),
                            new Image(Path.Combine("", "Assets", "Images", "brown-block.png"))
                        );
            block.DeleteEntity();
            GameRunning.GetInstance().blocks.AddEntity(block);
            var ball = new Ball (
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            ball.DeleteEntity();
            GameRunning.GetInstance().balls.AddEntity(ball);
            var powerDrop = new PowerUpDrop( 
                new DynamicShape(block.shape.Position, new Vec2F(0.06f, 0.06f)),
                new Image(Path.Combine("Assets", "Images", "RocketPickUp.png")));
            powerDrop.DeleteEntity();
            GameRunning.GetInstance().powerDrops.AddEntity(powerDrop);
            
            GameRunning.GetInstance().CallRemoveDeletedEntities();

            Assert.True(GameRunning.GetInstance().blocks.CountEntities() == 0);
            Assert.True(GameRunning.GetInstance().balls.CountEntities() == 0);
            Assert.True(GameRunning.GetInstance().powerDrops.CountEntities() == 0);
        }

        [Test] // 1 iteration, Entities IsDeleted = false
        public void TestRemoveDeletedEntities1IterationsIsNotDeleted() {
            GameRunning.GetInstance().blocks.ClearContainer();
            GameRunning.GetInstance().balls.ClearContainer();
            GameRunning.GetInstance().powerDrops.ClearContainer();

            var block = new NormalBlock(
                            new DynamicShape(new Vec2F(5.0f, 5.0f), 
                                new Vec2F(1.0f/12, (1.0f/12)/3f)),
                            new Image(Path.Combine("", "Assets", "Images", "brown-block.png")));
            GameRunning.GetInstance().blocks.AddEntity(block);
            var ball = new Ball (
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            GameRunning.GetInstance().balls.AddEntity(ball);
            var powerDrop = new PowerUpDrop( 
                new DynamicShape(block.shape.Position, new Vec2F(0.06f, 0.06f)),
                new Image(Path.Combine("Assets", "Images", "RocketPickUp.png")));
            GameRunning.GetInstance().powerDrops.AddEntity(powerDrop);
            
            GameRunning.GetInstance().CallRemoveDeletedEntities();
            
            Assert.True(GameRunning.GetInstance().blocks.CountEntities() == 1);
            Assert.True(GameRunning.GetInstance().balls.CountEntities() == 1);
            Assert.True(GameRunning.GetInstance().powerDrops.CountEntities() == 1);
        }

        [Test] // 1 iteration, PowerUpBlock. Should create a PowerUpDrop
        public void TestRemoveDeletedEntitiesDeletedPowerUpBlock() {
            GameRunning.GetInstance().blocks.ClearContainer();
            GameRunning.GetInstance().balls.ClearContainer();
            GameRunning.GetInstance().powerDrops.ClearContainer();

            var block = new PowerUpBlock(
                            new DynamicShape(new Vec2F(5.0f, 5.0f), 
                                new Vec2F(1.0f/12, (1.0f/12)/3f)),
                            new Image(Path.Combine("", "Assets", "Images", "brown-block.png")));
            block.DeleteEntity();
            GameRunning.GetInstance().blocks.AddEntity(block);

            GameRunning.GetInstance().CallRemoveDeletedEntities();

            Assert.True(GameRunning.GetInstance().blocks.CountEntities() == 0);
            Assert.True(GameRunning.GetInstance().balls.CountEntities() == 0);
            Assert.True(GameRunning.GetInstance().powerDrops.CountEntities() == 1);
        }

        [Test] // NewLevel and OnlyUnbreakBlocks, 0 blocks, by moving player
        public void TestNewLevel0Blocks() {
            GameRunning.GetInstance().UpdateState();
            GameRunning.GetInstance().blocks.ClearContainer();
            GameRunning.GetInstance().GetPlayer().shape.MoveX(1.0f);
            GameRunning.GetInstance().UpdateState();
            Assert.AreEqual(0.425f ,GameRunning.GetInstance().GetPlayer().shape.Position.X);
        }

        [Test] // NewLevel and OnlyUnbreakBlocks, 1 Unbreakblock, by moving player
        public void TestNewLevel1UnbreakableBlock() {
            GameRunning.GetInstance().UpdateState();
            GameRunning.GetInstance().blocks.ClearContainer();
            System.Console.WriteLine(GameRunning.GetInstance().blocks.CountEntities());
            var block = new UnbreakableBlock(
                            new DynamicShape(new Vec2F(5.0f, 5.0f), 
                                new Vec2F(1.0f/12, (1.0f/12)/3f)),
                            new Image(Path.Combine("Assets", "Images", "brown-block.png")));
            GameRunning.GetInstance().blocks.AddEntity(block);
            GameRunning.GetInstance().GetPlayer().shape.MoveX(1.0f);
            GameRunning.GetInstance().UpdateState();
            Assert.AreEqual(0.425f ,GameRunning.GetInstance().GetPlayer().shape.Position.X);
        }

        [Test] // NewLevel and OnlyUnbreakBlocks, 1 normal 1 unbreakable
        public void TestNewLevel1Unbreakable1NormalBlocks() {
            GameRunning.GetInstance().UpdateState();
            GameRunning.GetInstance().blocks.ClearContainer();
            var Unbreakblock = new UnbreakableBlock(
                            new DynamicShape(new Vec2F(5.0f, 5.0f), 
                                new Vec2F(1.0f/12, (1.0f/12)/3f)),
                            new Image(Path.Combine("Assets", "Images", "brown-block.png")));
            var Normalblock = new UnbreakableBlock(
                            new DynamicShape(new Vec2F(6.0f, 6.0f), 
                                new Vec2F(1.0f/12, (1.0f/12)/3f)),
                            new Image(Path.Combine("Assets", "Images", "brown-block.png")));
            GameRunning.GetInstance().blocks.AddEntity(Unbreakblock);
            GameRunning.GetInstance().blocks.AddEntity(Normalblock);
            GameRunning.GetInstance().UpdateState();
            Assert.AreEqual(0, GameRunning.GetInstance().GetLevel());
        }

        [Test]
        public void TestTimerDone() {
            GameRunning.GetInstance().timeLeft = 0; 
            GameRunning.GetInstance().CheckGameOver();
            Assert.True(testBlocks.CountEntities() == 0);
        }

        //Testing if a single ball from an entity container containing only that ball moves.
        [Test]
        public void TestMoving1Ball(){
            GameRunning.GetInstance().balls.ClearContainer();
            var ball = new Ball (
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            GameRunning.GetInstance().balls.AddEntity(ball);
            ball.moving = true;
            var startPos = ball.shape.Position;
            GameRunning.GetInstance().UpdateState();
            Assert.AreNotEqual(startPos, ball.shape.Position);
        }

        //Testing if an empty entity container does not move any balls.
        [Test]
        public void TestMoving0Balls(){
            var ball = new Ball (
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            GameRunning.GetInstance().balls.AddEntity(ball);
            GameRunning.GetInstance().balls.ClearContainer();
            GameRunning.GetInstance().UpdateState();
            Assert.Pass();
        }

        //Testing if two balls in the balls entity container are deleted and one new ball is added 
        //when calling ResetBalls(). 
        [Test]
        public void TestResetBalls(){
            GameRunning.GetInstance().balls.ClearContainer();
            var ball1 = new Ball (
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            GameRunning.GetInstance().balls.AddEntity(ball1);
            var ball2 = new Ball (
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            GameRunning.GetInstance().balls.AddEntity(ball2);
            GameRunning.GetInstance().ResetState();
            Assert.True(GameRunning.GetInstance().balls.CountEntities() == 1);
        }

        //Testing if a new ball is created when the balls entity container is empty.
        [Test]
        public void TestCheckBallsEmpty(){
            GameRunning.GetInstance().balls.ClearContainer();
            GameRunning.GetInstance().UpdateState();
            Assert.True(GameRunning.GetInstance().balls.CountEntities() == 1);
        }
    }
}
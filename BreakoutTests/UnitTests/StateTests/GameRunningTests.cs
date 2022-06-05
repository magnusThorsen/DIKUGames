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
    public class TestGameRunning {
        private GameRunning gameRunning;
        private EntityContainer<Block>testBlocks;


        public TestGameRunning() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameRunning = new GameRunning();
            GameRunning.GetInstance();
            testBlocks = new EntityContainer<Block>(288);
        }

        [SetUp]
        public void InitiateGameRunning() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameRunning = new GameRunning();
            GameRunning.GetInstance();
            testBlocks = new EntityContainer<Block>(288);
        }

        [Test]
        public void TestGetInstance() {
            Assert.That(GameRunning.GetInstance(), Is.InstanceOf<GameRunning>());
        }

        [Test]
        public void TestClassesInGameRunning() {
            Assert.True(GameRunning.GetInstance().GetPlayer() != null);
            Assert.True(GameRunning.GetInstance().GetLevelLoader() != null);
            Assert.True(GameRunning.GetInstance().GetPointsField() != null);
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

        [Test]
        public void TestGameOver() {
            var block = new NormalBlock(
                        new DynamicShape(new Vec2F(
                                5.0f, 5.0f), 
                                new Vec2F(1.0f/12, (1.0f/12)/3f)),
                                new Image(Path.Combine("", "Assets", "Images", "brown-block.png"))
                            );
            GameRunning.GetInstance().blocks.AddEntity(block);
            GameRunning.GetInstance().gameOver = true;
            GameRunning.GetInstance().CheckGameOver();
            Assert.True(testBlocks.CountEntities() == 0);
        }

        [Test]
        public void TestTimerDone() {
            GameRunning.GetInstance().timeLeft = 0; 
            GameRunning.GetInstance().CheckGameOver();
            Assert.True(testBlocks.CountEntities() == 0);
        }




    }
}
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
        private EntityContainer<Ball>testBalls;
        private KeyboardAction keyPress;
        private KeyboardKey key;


        public TestGameRunning() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameRunning = new GameRunning();
            GameRunning.GetInstance();
            testBlocks = new EntityContainer<Block>(288);
            testBalls = new EntityContainer<Ball>(gameRunning.maxBalls);
        }

        [SetUp]
        public void InitiateGameRunning() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameRunning = new GameRunning();
            GameRunning.GetInstance();
            testBlocks = new EntityContainer<Block>(288);
            testBalls = new EntityContainer<Ball>(gameRunning.maxBalls);
        }

        [Test]
        public void TestGetInstance() {
            Assert.That(GameRunning.GetInstance(), Is.InstanceOf<GameRunning>());
        }

        [Test]
        public void TestClassesInGameRunning() {
            Assert.True(gameRunning.GetPlayer() != null);
            Assert.True(gameRunning.GetLevelLoader != null);
            Assert.True(gameRunning.GetPointsField() != null);
        }

        [Test]
        public void TestNewLevel() {
            testBlocks = gameRunning.blocks;
            testBalls = gameRunning.balls;
            keyPress = KeyboardAction.KeyPress;
            key = KeyboardKey.G;
            gameRunning.HandleKeyEvent(keyPress, key);
            key = KeyboardKey.Space;
            gameRunning.HandleKeyEvent(keyPress, key);
            Assert.True(gameRunning.blocks != testBlocks);
            Assert.True(gameRunning.balls != testBalls);
        }

        [Test]
        public void TestGameOver() {
            gameRunning.gameOver = true;
            gameRunning.CheckGameOver();
            testBlocks = gameRunning.blocks;
            Assert.True(testBlocks.CountEntities() == 0);
        }

        [Test]
        public void TestTimerDone() {
            gameRunning.timeLeft = 0; 
            gameRunning.CheckGameOver();
            Assert.True(testBlocks.CountEntities() == 0);
        }




    }
}
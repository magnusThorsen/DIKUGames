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
    public class PowerUpsTests {
        private StateMachine stateMachine;
        private GameRunning gameRunning;
        private PowerUps powerUps;
        private DynamicShape shape;
        private Image image;
        private Player player;


        public PowerUpsTests() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            powerUps = new PowerUps();
            stateMachine = new StateMachine();
            gameRunning = new GameRunning();
            shape = new DynamicShape(new Vec2F(0.425f, 0.03f), new Vec2F(0.16f, 0.020f));
            image = new Image(Path.Combine("Assets", "Images", "player.png"));
            player = new Player(shape, image);
            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        }

        [SetUp]
        public void InitiatePowerUps() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            powerUps = new PowerUps();
            stateMachine = new StateMachine();
            gameRunning = new GameRunning();
            shape = new DynamicShape(new Vec2F(0.425f, 0.03f), new Vec2F(0.16f, 0.020f));
            image = new Image(Path.Combine("Assets", "Images", "player.png"));
            player = new Player(shape, image);
            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        }



        [Test]
        public void TestLifePowerUp() {
            powerUps.LifePowerUp();
            Assert.Pass(); // Testing if PowerUp can be called
        }


        [Test]
        public void TestWidePowerUp() {
            powerUps.WidePowerUp();
            Assert.Pass(); // Testing if PowerUp can be called
        }


        [Test]
        public void TestPlayerSpeedPowerUp() {
            powerUps.PlayerSpeedPowerUp();
            Assert.Pass(); // Testing if PowerUp can be called
        }

        [Test]
        public void TestDoubleSpeedPowerUp() {
            powerUps.DoubleSpeedPowerUp();
            Assert.Pass(); // Testing if PowerUp can be called
        }

        [Test]
        public void TestMoreTimePowerUp() {
            powerUps.MoreTimePowerUp();
            Assert.Pass(); // Testing if PowerUp can be called
        }
    }
}
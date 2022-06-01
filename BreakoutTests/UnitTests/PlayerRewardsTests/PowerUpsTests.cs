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


        public PowerUpsTests() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            powerUps = new PowerUps();
            stateMachine = new StateMachine();
            gameRunning = new GameRunning();
        }

        [SetUp]
        public void InitiatePowerUps() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            powerUps = new PowerUps();
            stateMachine = new StateMachine();
            gameRunning = new GameRunning();
        }



        [Test]
        public void TestLifePowerUp() {
            stateMachine.SwitchState(GameStateType.GameRunning);
            GameRunning.GetInstance();
            powerUps.LifePowerUp();
            BreakoutBus.GetBus().ProcessEventsSequentially(); 
            var player = gameRunning.GetPlayer();
            Assert.AreEqual(4, player.life);
        }

        [Test]
        public void TestWidePowerUp() {

        }


        [Test]
        public void TestPlayerSpeedPowerUp() {

        }

        [Test]
        public void TestDoubleSpeedPowerUp() {

        }

        [Test]
        public void TestMoreTimePowerUp() {

        }
    }
}
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
        private StateMachine stateMachine;
        private GameRunning gameRunning;

        public TestGameRunning() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            gameRunning = new GameRunning();
            GameRunning.GetInstance();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }

        [SetUp]
        public void InitiateGameRunning() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            gameRunning = new GameRunning();
            GameRunning.GetInstance();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }

        [Test]
        public void TestGetInstance() {
            Assert.That(GameRunning.GetInstance(), Is.InstanceOf<GameRunning>());
        }


        [Test]
            public void TestHandleKeyEvent() {
                stateMachine.SwitchState(GameStateType.GameRunning);
                stateMachine.ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Escape);
                BreakoutBus.GetBus().ProcessEventsSequentially(); 
                Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
            }
    }
}
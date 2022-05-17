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
        public TestGameRunning() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }
        

        [SetUp]
        public void InitiateGameRunning() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }

        [Test]
        public void TestSwitchState() {
            stateMachine.SwitchState(GameStateType.GameRunning);
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
        }
    }
}
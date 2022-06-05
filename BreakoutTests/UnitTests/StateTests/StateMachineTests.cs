using DIKUArcade.Math;
using NUnit.Framework;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout;
using System.IO;
using Breakout.BreakoutStates;
using DIKUArcade.Events;

namespace BreakoutTests {
    public class TestStateMachine {
        private StateMachine stateMachine;
        public TestStateMachine() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }
        

        [SetUp]
        public void InitiateStateMachine() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }

        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }


        [Test]
        public void TestSwitchState() {
            stateMachine.SwitchState(GameStateType.GameRunning);
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
        }

        [Test]
        public void TestOneActiveState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
            Assert.True(stateMachine.ActiveState.GetType() != typeof(GameRunning));
            Assert.True(stateMachine.ActiveState.GetType() != typeof(GamePaused));
            Assert.True(stateMachine.ActiveState.GetType() != typeof(GameLost));
            Assert.True(stateMachine.ActiveState.GetType() != typeof(GameWon));

        }


    }
}
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
    public class TestGamePaused {
        private StateMachine stateMachine;
        private GamePaused gamePaused;
        public TestGamePaused() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            gamePaused = new GamePaused();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }

        [SetUp]
        public void InitiateGamePaused() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            gamePaused = new GamePaused();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }
        
        [Test]
        public void TestGetInstance() {
            Assert.That(GamePaused.GetInstance(), Is.InstanceOf<GamePaused>());
        }


        [Test]
        public void TestHandleKeyEvent() {
            stateMachine.SwitchState(GameStateType.GamePaused);
            System.Console.WriteLine(stateMachine.ActiveState);
            stateMachine.ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
            stateMachine.ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
            BreakoutBus.GetBus().ProcessEventsSequentially(); 
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
        }
    }
}
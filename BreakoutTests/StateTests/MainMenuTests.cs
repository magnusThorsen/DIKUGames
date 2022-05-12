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
    public class TestMainMenu {
        private StateMachine stateMachine;
        private MainMenu mainMenu;
        public TestMainMenu() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            mainMenu = new MainMenu();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }
        

        [SetUp]
        public void InitiateMainMenu() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            mainMenu = new MainMenu();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }

        [Test]
        public void TestSwitchState() {
            stateMachine.SwitchState(GameStateType.GameRunning);
            stateMachine.SwitchState(GameStateType.MainMenu);
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        [Test]
        public void TestHandleKeyEvent() {
            stateMachine.ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
            BreakoutBus.GetBus().ProcessEventsSequentially(); 
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
        }

    }
}
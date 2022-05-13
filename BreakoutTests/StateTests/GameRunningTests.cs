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
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }
    }

    [SetUp]
    public void InitiateGameRunning() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        stateMachine = new StateMachine();
        gameRunning = new GameRunning();
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
    }

    [Test]
    public void TestSwitchState1() {
        stateMachine.SwitchState(GameStateType.MainMenu);
        stateMachine.SwitchState(GameStateType.GameRunning);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }

    [Test]
    public void TestSwitchState2() {
        var gameRunning = gameRunning.CheckGameOver=true;
        BreakoutBus.GetBus().ProcessEventsSequentially(); 
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
        public void TestHandleKeyEvent() {
            stateMachine.ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Escape);
            BreakoutBus.GetBus().ProcessEventsSequentially(); 
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
        }


}
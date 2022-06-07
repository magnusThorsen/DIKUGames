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
    public class TestC1StateMachine {
        private StateMachine stateMachine;
        public TestC1StateMachine() {
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
        public void TestSwitchState() {
            stateMachine.SwitchState(GameStateType.GameRunning);
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
        }

        [Test]
        public void TestProcessEventCorrectly() {
            stateMachine.ProcessEvent(
                new GameEvent{
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING"
                    });
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
        }

        [Test]
        public void TestProcessEventNotGameStatEvent() {
            stateMachine.ProcessEvent(
                new GameEvent{
                    EventType = GameEventType.InputEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING"
                    });
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        [Test]
        public void TestProcessEventWrongMessage() {
            stateMachine.ProcessEvent(
                new GameEvent{
                    EventType = GameEventType.GameStateEvent,
                    Message = "WRONG",
                    StringArg1 = "GAME_RUNNING"
                    });
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        [Test]
        public void TestProcessEventWrongNotState() {
            stateMachine.ProcessEvent(
                new GameEvent{
                    EventType = GameEventType.GameStateEvent,
                    Message = "WRONG",
                    StringArg1 = "NOT_STATE"
                    });
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }


    }
}
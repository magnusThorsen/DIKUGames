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
    public class TestC1StateTransformer {
        public TestC1StateTransformer() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
        }
        

        [SetUp]
        public void InitiateStateMachine() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
        }

        [Test]
        public void TestStringToState() {
            Assert.True(StateTransformer.TransformStringToState("GAME_PAUSED") == GameStateType.GamePaused);
            Assert.True(StateTransformer.TransformStringToState("GAME_RUNNING") == GameStateType.GameRunning);
            Assert.True(StateTransformer.TransformStringToState("MAIN_MENU") == GameStateType.MainMenu);
            Assert.True(StateTransformer.TransformStringToState("GAME_WON") == GameStateType.GameWon);
            Assert.True(StateTransformer.TransformStringToState("GAME_LOST") == GameStateType.GameLost);
            Assert.Throws<ArgumentException>(() => StateTransformer.TransformStringToState("NOT_STATE"));
        }

        [Test]
        public void TestStateToString() {
            Assert.That(StateTransformer.TransformStateToString(GameStateType.GamePaused) == "GAME_PAUSED");
            Assert.That(StateTransformer.TransformStateToString(GameStateType.GameRunning) == "GAME_RUNNING");
            Assert.That(StateTransformer.TransformStateToString(GameStateType.MainMenu) == "MAIN_MENU");
            Assert.Throws<ArgumentException>(() => StateTransformer.TransformStateToString(GameStateType.GameWon));
        }

    }
}
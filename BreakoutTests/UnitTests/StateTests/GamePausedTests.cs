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
        private GamePaused gamePaused;
        public TestGamePaused() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gamePaused = new GamePaused();
        }

        [SetUp]
        public void InitiateGamePaused() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gamePaused = new GamePaused();
        }
        
        [Test]
        public void TestGetInstance() {
            Assert.That(GamePaused.GetInstance(), Is.InstanceOf<GamePaused>());
        }


        [Test]
        public void TestHandleKeyEvent() {
            gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
            Assert.True(gamePaused.SelectedButton == "Resume");
        }
    }
}
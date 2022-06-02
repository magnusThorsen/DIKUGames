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
    public class TestGameLost {
        private GameLost gameLost;
        public TestGameLost() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameLost = new GameLost();
        }

        [SetUp]
        public void InitiateGameLost() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameLost = new GameLost();
        }
        
        [Test]
        public void TestGetInstance() {
            Assert.That(GameLost.GetInstance(), Is.InstanceOf<GameLost>());
        }


        [Test]
        public void TestHandleKeyEvent() {
            gameLost.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
            Assert.True(gameLost.SelectedButton == "Main Menu");
        }
    }
}
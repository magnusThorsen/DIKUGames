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
    public class TestGameWon {
        private GameWon gameWon;
        public TestGameWon() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameWon = new GameWon();
        }

        [SetUp]
        public void InitiateGameWon() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameWon = new GameWon();
        }
        
        [Test]
        public void TestGetInstance() {
            Assert.That(GameWon.GetInstance(), Is.InstanceOf<GameWon>());
        }


        [Test]
        public void TestHandleKeyEvent() {
            gameWon.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
            Assert.True(gameWon.SelectedButton == "Main Menu");
        }
    }
}
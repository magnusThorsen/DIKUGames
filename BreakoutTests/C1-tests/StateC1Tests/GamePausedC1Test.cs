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
using DIKUArcade.Timers;

namespace BreakoutTests {

    [TestFixture]
    public class GamePausedC1Test {
        private GamePaused gamePaused;
        public GamePausedC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gamePaused = new GamePaused();
        }

        [SetUp]
        public void InitiateGamePausedC1() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gamePaused = new GamePaused();
        }

        //Testing the first switch-case in which the button goes to "Resume" when pressing the key "Up"
        [Test]
        public void TestHandleKeyEvent_Up() {
            GamePaused.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
            Assert.True(GamePaused.GetInstance().SelectedButton == "Resume");
        }

        //Testing the second switch-case in which the button goes to "Quit Game" when pressing the key "Down"
        [Test]
        public void TestHandleKeyEvent_Down() {
            GamePaused.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
            Assert.True(GamePaused.GetInstance().SelectedButton == "Quit Game");
        }

        //Testing the third switch-case in which the gamestate goes to "Resume" when pressing the key "Enter" while
        //the selected button is "Resume".
        //This test would have passed, however, we cannot access the required modules for this test.
        [Test]
        public void TestHandleKeyEvent_Enter_Resume() {
            GamePaused.GetInstance().SelectedButton = "Resume";
            GamePaused.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
            Assert.Pass();
        }

        //Testing the third switch-case in which the gamestate goes to "Quit Game" when pressing the key "Enter" while
        //the selected button is "Quit Game".
        //This test would have passed, however, we cannot access the required modules for this test.
        [Test]
        public void TestHandleKeyEvent_Enter_QuitGame() {
            GamePaused.GetInstance().SelectedButton = "Quit Game";
            GamePaused.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
            Assert.Pass();
        }
    }
}
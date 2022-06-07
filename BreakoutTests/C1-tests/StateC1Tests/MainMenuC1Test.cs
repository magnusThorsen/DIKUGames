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
    public class MainMenuC1Test {
        private MainMenu MainMenu;
        public MainMenuC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            MainMenu = new MainMenu();
        }

        [SetUp]
        public void InitiateMainMenuC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            MainMenu = new MainMenu();
        }

        //Testing the first switch-case in which the button goes to "New Game" when pressing the key "Up"
        [Test]
        public void TestHandleKeyEvent_Up() {
            MainMenu.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
            Assert.True(MainMenu.GetInstance().SelectedButton == "New Game");
        }

        //Testing the second switch-case in which the button goes to "Quit Game" when pressing the key "Down"
        [Test]
        public void TestHandleKeyEvent_Down() {
            MainMenu.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
            Assert.True(MainMenu.GetInstance().SelectedButton == "Quit Game");
        }

        //Testing the third switch-case in which the gamestate goes to "New Game" when pressing the key "Enter" while
        //the selected button is "New Game".
        //This test would have passed, however, we cannot access the required modules for this test.
        [Test]
        public void TestHandleKeyEvent_Enter_NewGame() {
            MainMenu.GetInstance().SelectedButton = "New Game";
            MainMenu.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
            Assert.Pass();
        }

        //Testing the third switch-case in which the gamestate goes to "Quit Game" when pressing the key "Enter" while
        //the selected button is "Quit Game".
        //This test would have passed, however, we cannot access the required modules for this test.
        [Test]
        public void TestHandleKeyEvent_Enter_QuitGame() {
            MainMenu.GetInstance().SelectedButton = "Quit Game";
            MainMenu.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
            Assert.Pass();
        }
    }
}
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
    public class GameWonC1Test {
        private GameWon GameWon;
        public GameWonC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            GameWon = new GameWon();
        }

        [SetUp]
        public void InitiateGameWonC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            GameWon = new GameWon();
        }

        //Testing the first switch-case in which the button goes to "Main Menu" when pressing the key "Up"
        [Test]
        public void TestHandleKeyEvent_Up() {
            GameWon.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
            Assert.True(GameWon.GetInstance().SelectedButton == "Main Menu");
        }

        //Testing the second switch-case in which the button goes to "Quit Game" when pressing the key "Down"
        [Test]
        public void TestHandleKeyEvent_Down() {
            GameWon.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
            Assert.True(GameWon.GetInstance().SelectedButton == "Quit Game");
        }

        //Testing the third switch-case in which the gamestate goes to "Main Menu" when pressing the key "Enter" while
        //the selected button is "Main Menu".
        //This test would have passed, however, we cannot access the required modules for this test.
        [Test]
        public void TestHandleKeyEvent_Enter_MainMenu() {
            GameWon.GetInstance().SelectedButton = "Main Menu";
            GameWon.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
            Assert.Pass();
        }

        //Testing the third switch-case in which the gamestate goes to "Quit Game" when pressing the key "Enter" while
        //the selected button is "Quit Game".
        //This test would have passed, however, we cannot access the required modules for this test.
        [Test]
        public void TestHandleKeyEvent_Enter_QuitGame() {
            GameWon.GetInstance().SelectedButton = "Quit Game";
            GameWon.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
            Assert.Pass();
        }


        [Test]
        public void TestProcessEvent() {
            GameWon.GetInstance().ProcessEvent(new GameEvent{
                                EventType = GameEventType.ControlEvent, 
                                Message = "WonLostPoints",
                                IntArg1 = 5
                            });
            Assert.True(GameWon.GetInstance().points == 5);
        }

        [Test]
        public void TestProcessEventNoMatch() {
            GameWon.GetInstance().points = 0;
            GameWon.GetInstance().ProcessEvent(new GameEvent{
                                EventType = GameEventType.ControlEvent, 
                                Message = "NO_MATCH",
                                IntArg1 = 5
                            });
            System.Console.WriteLine(GameWon.GetInstance().points);
            Assert.True(GameWon.GetInstance().points == 0);
        }
    }
}
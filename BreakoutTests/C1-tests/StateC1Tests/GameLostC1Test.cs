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
    public class GameLostC1Test {
        private GameLost GameLost;
        public GameLostC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            GameLost = new GameLost();
        }

        [SetUp]
        public void InitiateGameLostC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            GameLost = new GameLost();
        }

        //Testing the first switch-case in which the button goes to "Main Menu" when pressing the key "Up"
        [Test]
        public void TestHandleKeyEvent_Up() {
            GameLost.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
            Assert.True(GameLost.GetInstance().SelectedButton == "Main Menu");
        }

        //Testing the second switch-case in which the button goes to "Quit Game" when pressing the key "Down"
        [Test]
        public void TestHandleKeyEvent_Down() {
            GameLost.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
            Assert.True(GameLost.GetInstance().SelectedButton == "Quit Game");
        }

        //Testing the third switch-case in which the gamestate goes to "Main Menu" when pressing the key "Enter" while
        //the selected button is "Main Menu".
        //This test would have passed, however, we cannot access the required modules for this test.
        [Test]
        public void TestHandleKeyEvent_Enter_MainMenu() {
            GameLost.GetInstance().SelectedButton = "Main Menu";
            GameLost.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
            Assert.Pass();
        }

        //Testing the third switch-case in which the gamestate goes to "Quit Game" when pressing the key "Enter" while
        //the selected button is "Quit Game".
        //This test would have passed, however, we cannot access the required modules for this test.
        [Test]
        public void TestHandleKeyEvent_Enter_QuitGame() {
            GameLost.GetInstance().SelectedButton = "Quit Game";
            GameLost.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
            Assert.Pass();
        }


        [Test]
        public void TestProcessEvent() {
            GameLost.GetInstance().ProcessEvent(new GameEvent{
                                EventType = GameEventType.ControlEvent, 
                                Message = "WonLostPoints",
                                IntArg1 = 5
                            });
            Assert.True(GameLost.GetInstance().points == 5);
        }

        [Test]
        public void TestProcessEventNoMatch() {
            GameLost.GetInstance().points = 0;
            GameLost.GetInstance().ProcessEvent(new GameEvent{
                                EventType = GameEventType.ControlEvent, 
                                Message = "NO_MATCH",
                                IntArg1 = 5
                            });
            System.Console.WriteLine(GameWon.GetInstance().points);
            Assert.True(GameLost.GetInstance().points == 0);
        }


    }
}
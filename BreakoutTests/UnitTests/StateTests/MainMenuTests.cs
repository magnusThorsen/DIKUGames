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
    public class TestMainMenu {
        private MainMenu mainMenu;
        public TestMainMenu() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            mainMenu = new MainMenu();
        }
        

        [SetUp]
        public void InitiateMainMenu() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            mainMenu = new MainMenu();
        }

         [Test]
        public void TestGetInstance() {
            Assert.That(MainMenu.GetInstance(), Is.InstanceOf<MainMenu>());
        }

        [Test]
        public void TestHandleKeyEvent() {
            MainMenu.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
            System.Console.WriteLine(mainMenu.SelectedButton);
            Assert.True(MainMenu.GetInstance().SelectedButton == "New Game");
        }

    }
}
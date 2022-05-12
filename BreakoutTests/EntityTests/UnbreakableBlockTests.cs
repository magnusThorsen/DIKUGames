using DIKUArcade.Math;
using NUnit.Framework;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using Breakout;
using DIKUArcade.Events;
using DIKUArcade;

namespace BreakoutTests {

    [TestFixture]
    public class UnbreakableBlockTest {
        private UnbreakableBlock block;
        private Shape shape;
        private Image image;

        public UnbreakableBlockTest() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            shape = new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(1.0f/12, (1.0f/12)/3f));
            image = new Image(@"../Breakout/Assets/Images/blue-block.png");
            block = new UnbreakableBlock(shape, image);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, block);
        }

        [SetUp]
        public void InitiateNormalBlock() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            shape = new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(1.0f/12, (1.0f/12)/3f));
            image = new Image(@"../Breakout/Assets/Images/blue-block.png");
            block = new UnbreakableBlock(shape, image);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, block);

        }

        //Testing if a block´s health decreases as it should.
        [Test]
        public void TestDecHealth() {
            var gameEvent = new GameEvent{EventType = GameEventType.InputEvent, IntArg1 = 1};
            block.ProcessEvent(gameEvent);
            Assert.AreEqual(block.GetHealth(), 100);
        }

        public void TestGetValue() {
            Assert.IsTrue(block.GetValue() == 1);
        }

        public void TestSetValue() {
            block.SetValue(4); 
            Assert.IsTrue(block.GetValue() == 4);
        }

        public void TestGetPosition() {
            Assert.IsTrue(block.GetPosition() == new Vec2F(0.5f, 0.5f));
        }
    }
}
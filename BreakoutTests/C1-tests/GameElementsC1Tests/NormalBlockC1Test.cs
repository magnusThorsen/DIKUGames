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
    public class NormalBlockC1Test {
        private NormalBlock block;
        private Shape shape;
        private Image image;

        public NormalBlockC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            shape = new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(1.0f/12, (1.0f/12)/3f));
            image = new Image(@"../Breakout/Assets/Images/blue-block.png");
            block = new NormalBlock(shape, image);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, block);
        }

        [SetUp]
        public void InitiateNormalBlock() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            shape = new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(1.0f/12, (1.0f/12)/3f));
            image = new Image(@"../Breakout/Assets/Images/blue-block.png");
            block = new NormalBlock(shape, image);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, block);
        }

        [Test]
        public void TestHealthUnder1(){
            var gameEvent = new GameEvent{EventType = GameEventType.InputEvent, IntArg1 = 1};
            block.ProcessEvent(gameEvent);
            Assert.AreEqual(block.GetHealth(), 0);
            Assert.IsTrue(block.IsDeleted() == true);
        }

        [Test]
        public void TestHealthOver1(){
            var gameEvent = new GameEvent{EventType = GameEventType.InputEvent, IntArg1 = 0};
            block.ProcessEvent(gameEvent);
            Assert.AreEqual(block.GetHealth(), 1);
            Assert.IsTrue(block.IsDeleted() == false);
        }        
    }
}
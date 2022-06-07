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
    public class PowerUpBlockTest {
        private PowerUpBlock block;
        private Shape shape;
        private Image image;

        public PowerUpBlockTest() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            shape = new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(1.0f/12, (1.0f/12)/3f));
            image = new Image(@"../Breakout/Assets/Images/blue-block.png");
            block = new PowerUpBlock(shape, image);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, block);
        }

        [SetUp]
        public void InitiateNormalBlock() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            shape = new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(1.0f/12, (1.0f/12)/3f));
            image = new Image(@"../Breakout/Assets/Images/blue-block.png");
            block = new PowerUpBlock(shape, image);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, block);

        }

        //Testing if a block´s health decreases as it should.
        [Test]
        public void TestDecHealth() {
            var gameEvent = new GameEvent{EventType = GameEventType.InputEvent, IntArg1 = 1};
            block.ProcessEvent(gameEvent);
            Assert.AreEqual(block.GetHealth(), 0);
        }

        //Testing if a block is being destroyed (deleted) after having 0 or under in health.
        [Test]
        public void TestDeleteBlock() {
            var gameEvent = new GameEvent{EventType = GameEventType.InputEvent, IntArg1 = 1};
            block.ProcessEvent(gameEvent);
            Assert.IsTrue(block.IsDeleted() == true);
        }

        [Test]
        public void TestGetValue() {
            Assert.IsTrue(block.GetValue() == 1);
        }


        [Test]
        public void TestSetValue() {
            block.SetValue(4); 
            Assert.IsTrue(block.GetValue() == 4);
        }


        [Test]
        public void TestGetPosition() {
            Assert.IsTrue(block.GetPosition().X == 0.5f);
            Assert.IsTrue(block.GetPosition().Y == 0.5f);
        }

        [Test]
        public void TestIsPowerUp() {
            Assert.IsTrue(block.IsPowerUp());
        }

    }
}
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
    public class HardenedBlockC1Test {
        private HardenedBlock block;
        private Shape shape;
        private Image image;

        public HardenedBlockC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            shape = new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(1.0f/12, (1.0f/12)/3f));
            image = new Image(Path.Combine("Assets", "Images", "darkgreen-block-damaged.png"));
            block = new HardenedBlock(shape, image, "darkgreen-block.png");
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, block);
        }

        [SetUp]
        public void InitiateHardenedBlock() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            shape = new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(1.0f/12, (1.0f/12)/3f));
            image = new Image(Path.Combine("Assets", "Images", "darkgreen-block-damaged.png"));
            block = new HardenedBlock(shape, image, "darkgreen");
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, block);
        }


        //Testing if the block has 1 health and changes image when taking 1 damage.
        //The change of image is tested succesfully visually.
        [Test]
        public void TestHealthIs1(){
            var gameEvent = new GameEvent{EventType = GameEventType.InputEvent, IntArg1 = 1};
            block.ProcessEvent(gameEvent);
            Assert.AreEqual(block.GetHealth(), 1);
        }


        //Testing if the block has 0 health and deleted after taking 1 damage twice.
        [Test]
        public void TestHealthUnder1(){
            var gameEvent = new GameEvent{EventType = GameEventType.InputEvent, IntArg1 = 1};
            block.ProcessEvent(gameEvent);
            block.ProcessEvent(gameEvent);
            Assert.AreEqual(block.GetHealth(), 0);
            Assert.IsTrue(block.IsDeleted() == true);
        }

        //Testing if the block still has 2 in health and is not deleted after taking 0 damage.
        [Test]
        public void TestHealthOver1(){
            var gameEvent = new GameEvent{EventType = GameEventType.InputEvent, IntArg1 = 0};
            block.ProcessEvent(gameEvent);
            Assert.AreEqual(block.GetHealth(), 2);
            Assert.IsTrue(block.IsDeleted() == false);
        }      
    }
}
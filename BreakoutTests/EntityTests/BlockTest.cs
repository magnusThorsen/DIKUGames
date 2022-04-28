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
    public class BlockTest {
        private Block block;
        private Shape shape;
        private Image image;

        public BlockTest() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            shape = new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(1.0f/12, (1.0f/12)/3f));
            image = new Image(@"../Breakout/Assets/Images/blue-block.png");
            block = new Block(shape, image);
        }

        [SetUp]
        public void InitiateBlock() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            shape = new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(1.0f/12, (1.0f/12)/3f));
            image = new Image(@"../Breakout/Assets/Images/blue-block.png");
            block = new Block(shape, image);

        }

        //Testing if a block´s health decreases as it should.
        [Test]
        public void TestDecHealth() {
            block.BlockHit();
            Assert.AreEqual(block.GetHealth(), 0);
        }

        //Testing if a block is being destroyed (deleted) after having 0 or under in health.
        [Test]
        public void TestDeleteBlock() {
            block.BlockHit();
            Assert.IsTrue(block.IsDeleted() == true);
        }
    }
}
using DIKUArcade.Math;
using NUnit.Framework;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga;
using System.IO;
using DIKUArcade.Events;
using DIKUArcade;

namespace BreakoutTests {
    [TestFixture]
    public class BlockTest {
        private Blocks block;
        private Shape shape;
        private Image image;

        public BlockTest() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            shape = new Shape(new Vec2F(0.5f, 0.9f), new Vec2F(0.1f, 0.1f));
            image = new Image(Path.Combine("Assets", "Images", "blue-block.png"));
            block = new Blocks(shape, image);
        }

        [SetUp]
        public void InitiateBlock() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            shape = new DynamicShape(new Vec2F(0.5f, 0.9f), new Vec2F(0.1f, 0.1f));
            image = new Image(Path.Combine("Assets", "Images", "blue-block.png"));
            block = new Blocks(shape, image);

        }

        //Testing if a block´s health decreases as it should.
        [Test]
        public void TestDecHealth() {
            block.DecHealth();
            Assert.AreEqual(block.Health(), 0);
        }

        //Testing if a block is being destroyed (deleted) after having 0 or under in health.
        [Test]
        public void TestDeleteBlock() {
            Assert.True(block, IsDeleted());
        }
    }
}
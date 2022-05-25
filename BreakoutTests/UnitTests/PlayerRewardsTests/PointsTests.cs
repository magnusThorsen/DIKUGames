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
    public class PointsTest {
        private int points;
        private Text display;

        public PointsTest() {
            points = 0;
            display = new Text (points.ToString(), position, extent);
            display.SetColor(new Vec3I(255, 0, 0));
        }

        [SetUp]
        public void InitiatePoints() {
            points = 0;
            display = new Text (points.ToString(), position, extent);
            display.SetColor(new Vec3I(255, 0, 0));

        }

        //Testing if points can be added as it should.
        [Test]
        public void TestAddPoints() {
            points.AddPoints(5);
            Assert.AreEqual(points, 5);
        }

        //Testing if points can be resetted to 0 as it should.
        [Test]
        public void TestResetPoints() {
            points.AddPoints(5);
            points.ResetPoints();
            Assert.AreEqual(points, 0);
        }

        //Testing if the amount points can be returned as it should.
        public void TestGetPoints() {
            points.AddPoints(5);
            Assert.AreEqual(points.GetPoints(), 5);
        }
    }
}
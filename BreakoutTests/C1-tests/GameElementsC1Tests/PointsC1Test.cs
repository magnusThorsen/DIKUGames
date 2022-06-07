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
    public class PointsC1Test {
        private Points point;
        private Text display;

        public PointsC1Test() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            point = new Points(new Vec2F(0.6f,0.5f), new Vec2F(0.5f,0.5f));
            display = new Text (point.ToString(), new Vec2F(0.5f,0.5f),new Vec2F(0.5f,0.5f));
            display.SetColor(new Vec3I(255, 0, 0));
        }

        [SetUp]
        public void InitiatePointsC1() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            point = new Points(new Vec2F(0.6f,0.5f), new Vec2F(0.5f,0.5f));
            display = new Text (point.ToString(), new Vec2F(0.5f,0.5f), new Vec2F(0.5f,0.5f));
            display.SetColor(new Vec3I(255, 0, 0));
        }

        //Testing if points can be added as it should.
        [Test]
        public void TestAddPoints() {
            point.AddPoints(5);
            Assert.AreEqual(point.GetPoints(), 5);
        }

        //Testing if points can be resetted to 0 as it should.
        [Test]
        public void TestResetPoints() {
            point.AddPoints(5);
            point.ResetPoints();
            Assert.AreEqual(point.GetPoints(), 0);
        }

        //Testing if the amount points can be returned as it should.
        public void TestGetPoints() {
            point.AddPoints(5);
            Assert.AreEqual(point.GetPoints(), 5);
        }

        public void TestProcessEventAddPoints(){
            point.ProcessEvent(new GameEvent {
                        EventType = GameEventType.GraphicsEvent, IntArg1 = 5, 
                    });
            Assert.AreEqual(point.GetPoints(), 5);
        }

        public void TestProcessEventInvisibleElse(){
            point.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, IntArg1 = 5, 
                    });
            Assert.AreEqual(point.GetPoints(), 0);
        }
    }
}
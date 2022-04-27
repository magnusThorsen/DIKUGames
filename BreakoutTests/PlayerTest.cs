using NUnit.Framework;
using DIKUArcade.Math;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout;
using System.IO;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade;
using System.Collections.Generic;

namespace BreakoutTests;

[TestFixture]
public class TestPlayer{

        private Player player;
        private DynamicShape shape;
        private Image image;
        private Player player2;
        private Entity entity;

    public TestPlayer() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        shape = new DynamicShape(new Vec2F(0.425f, 0.03f), new Vec2F(0.16f, 0.020f));
        image = new Image(Path.Combine("Assets", "Images", "player.png"));
        player = new Player(shape, image);
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        player2 = new Player(shape, image);
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player2);
        entity = new Entity(shape, image);
    }

    [SetUp]
    public void InitiatePlayer()
    {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        shape = new DynamicShape(new Vec2F(0.425f, 0.03f), new Vec2F(0.16f, 0.020f));
        image = new Image(Path.Combine("Assets", "Images", "player.png"));
        player = new Player(shape, image);
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        player2 = new Player(shape, image);
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player2);
        entity = new Entity(shape, image);
    }

    [Test]
    public void TestPlayerCenter()
    {
        Assert.IsTrue(player.GetPosition().X > 0.4f && player.GetPosition().X < 0.6);
    }

    [Test]
    public void TestRightBorder() {
        Assert.IsTrue(player.GetPosition().X < 1.0f);
        for (int i = 0; i < 100 ; i++) {
            player.SetMoveRight(true);
        }
        Assert.IsTrue(player.GetPosition().X < 1.0f);
    }

    [Test]
    public void TestLeftBorder() {
        Assert.IsTrue(player.GetPosition().X > 0.0f);
        for (int i = 0; i < 100 ; i++) {
            player.SetMoveLeft(true);
        }
        Assert.IsTrue(player.GetPosition().X > 0.0f);
    }

    [Test]
    public void TestPlayerBottomHalf() {
        Assert.IsTrue(player.GetPosition().Y < 0.5f);
    }

    [Test]
    public void TestBorderStutter() {

    }

    [Test]
    public void TestShapeRectangular() {
        Assert.True();
    }

    [Test]
    public void TestPlayerEntity() {
        Assert.AreEqual(typeof(Player), typeof(Entity));
    }


    [Test]
    public void TestMoveLeftPress() {
        Assert.AreEqual(player.GetPosition().X, 0.425f);
        var gameEvent = new GameEvent{EventType = GameEventType.PlayerEvent, Message = "KeyPress", 
            IntArg1 = (int) KeyboardKey.Left};
        player.ProcessEvent(gameEvent);
        player.Move();
        Assert.AreEqual(player.GetPosition().X, 0.405f);

    }

    [Test]
    public void TestMoveRightPress() {
        Assert.AreEqual(player.GetPosition().X, 0.425f);
        var gameEvent = new GameEvent{EventType = GameEventType.PlayerEvent, Message = "KeyPress", 
            IntArg1 = (int) KeyboardKey.Right};
        player.ProcessEvent(gameEvent);
        player.Move();
        Assert.IsTrue(player.GetPosition().X - 0.445f < 0.0002f);
    }

}
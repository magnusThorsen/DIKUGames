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
public class PlayerC1Test{

        private Player player;
        private DynamicShape shape;
        private Image image;
        private Player player2;
        private Entity entity;

    public PlayerC1Test() {
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
    public void TestPlayerMoveIsWide()
    {
        player.isWide = true;
        player.Move();
        Assert.IsTrue(player.C1GetWindowLimit() == 0.681f);
    }

    [Test]
    public void TestPlayerMoveIsNotWideInsideBounds(){
        player.shape.Position = new Vec2F(0.5f,0.5f);
        player.isWide = false;
        player.Move();
        Assert.IsTrue(player.C1GetWindowLimit() == 0.845f);
    }


    [Test]
    public void TestPlayerMoveIsWideOutsideBounds(){
        player.shape.Position = new Vec2F(10.0f,0.5f);
        player.isWide = true;
        player.Move();
        System.Console.WriteLine(player.C1GetWindowLimit());
        Assert.IsTrue(player.C1GetWindowLimit() == 0.681f);
        Assert.True(shape.Position.X == 0.681f);
    }


    [Test]
    public void TestPlayerProcessKeyPressLeft(){
        var firstPos = player.shape.Position;
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "KeyPress", 
                        IntArg1 = (int) KeyboardKey.Left
                    });
        player.Move();
        Assert.True(player.shape.Position.X == firstPos.X - 0.02f);
    }

    [Test]
    public void TestPlayerProcessKeyPressRight(){
        var firstPos = player.shape.Position;
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "KeyPress", 
                        IntArg1 = (int) KeyboardKey.Right
                    });
        player.Move();
        Assert.True(player.shape.Position.X == firstPos.X + 0.02f);


    }


    [Test]
    public void TestPlayerProcessKeyReleaseLeft(){
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "KeyPress", 
                        IntArg1 = (int) KeyboardKey.Left
                    });  
        player.Move();
        var firstPos = player.shape.Position;
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "KeyRelease", 
                        IntArg1 = (int) KeyboardKey.Left
                    });
        player.Move();
        Assert.True(player.shape.Position.X == firstPos.X);
    }

    [Test]
    public void TestPlayerProcessKeyReleaseRight(){
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "KeyPress", 
                        IntArg1 = (int) KeyboardKey.Right
                    });
        player.Move();
        var firstPos = player.shape.Position;
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "KeyRelease", 
                        IntArg1 = (int) KeyboardKey.Right
                    });
        player.Move();
        Assert.True(player.shape.Position.X == firstPos.X);
    }


    [Test]
    public void TestPlayerProcessIncLife(){
        var firstLife = player.life;
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "IncLife", 
                    });
        Assert.True(player.life == firstLife+1);
    }

    [Test]
    public void TestPlayerProcessDecLife(){
        player.life = 1;
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "DecLife", 
                    });
        Assert.True(player.life == 0);
    }


    [Test]
    public void TestPlayerProcessIncWidth(){
        player.isWide = false;
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "IncWidth", 
                    });
        Assert.True(player.isWide);
    }


    [Test]
    public void TestPlayerProcessIncSpeed(){
        player.isFast = false;
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "IncSpeed", 
                    });
        Assert.True(player.isFast);
    }

    [Test]
    public void TestPlayerProcessNoCase(){
        player.isFast = false;
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "No Match", 
                    });
        Assert.False(player.isFast);
    }

    [Test]
    public void TestPlayerProcessWrongEventtype(){
        player.isFast = false;
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.InputEvent, Message = "IncSpeed", 
                    });
        Assert.False(player.isFast);
    }



    [Test]
    public void TestUpdatePlayerPowerups(){
        player.timeWidth = -100;
        player.timeSpeed = -100;
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "IncSpeed", 
                    });
        player.ProcessEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "IncWidth", 
                    });
        player.C1UpdatePlayerPowerups();
        Assert.True(player.isWide);
        Assert.True(player.isFast);

    }
}
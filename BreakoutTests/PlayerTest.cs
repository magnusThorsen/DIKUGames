using NUnit.Framework;
using DIKUArcade.Math;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout;
using System.IO;
using DIKUArcade.Events;
using DIKUArcade;
using System.Collections.Generic;

namespace BreakoutTests;

public class Tests{

    [SetUp]
    public void Setup()
    {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        DynamicShape shape = new DynamicShape(new Vec2F(0.5f, 0.1f), new Vec2F(0.1f, 0.1f));
        Image image = new Image(Path.Combine("Assets", "Images", "Player.png"));
        Player player = new Player(shape, image);
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}
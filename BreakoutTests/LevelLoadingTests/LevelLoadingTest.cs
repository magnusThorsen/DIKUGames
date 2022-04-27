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

[TestFixture]
public class LevelLoadingTest{
    private LevelLoader levelLoader;
    private EntityContainer<Block> blocks;
    public LevelLoadingTest() {   
    }

    [SetUp]
    public void Setup(){
        blocks = new EntityContainer<Block>(288);
        levelLoader = new LevelLoader();
    }

    [Test]
    public void TestFileNotFound(){
        Assert.AreEqual(Blocks, levelLoader.LoadLevel("FileNot Found.txt"));
    }


    [Test]
    public void TestCorrectPosition(){
        blocks = levelLoader.LoadLevel("/../../Breakout/Assets/Images/level1.txt");
        Assert.AreEqual(blocks[0].shape.Position, Vec2F(0.0f + 1 * 1.0f/12, 0.9f - 2 * (1.0f/12)/3));
    }
}
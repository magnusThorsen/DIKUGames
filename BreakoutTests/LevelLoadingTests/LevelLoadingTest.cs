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
        Assert.AreEqual(Blocks, levelLoader.LoadLevel("NotFile.txt"));
    }


    [Test]
    public void TestCorrectPosition(){
        Assert.AreEqual(Blocks, levelLoader.LoadLevel("NotFile.txt"));
    }
}
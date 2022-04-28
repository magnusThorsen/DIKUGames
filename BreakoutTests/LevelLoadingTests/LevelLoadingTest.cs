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
        Assert.AreEqual(blocks, levelLoader.LoadLevel("FileNot Found.txt"));
    }


    [Test]
    public void TestCorrectPosition(){
        Block a;
        int b;
        blocks = levelLoader.LoadLevel("/../../Breakout/Assets/Images/level1.txt");
        foreach(Block elm in blocks){
            if (b != 1){
            
            }
        }
        Assert.AreEqual(blocks.First().shape.Position, Vec2F(0.0f + 1 * 1.0f/12, 0.9f - 2 * (1.0f/12)/3));
    }

    [Test]
    public void TestMapaDataType(){
        Assert.AreEqual(typeof(List<char>), typeof(levelLoader.map));
    }
    
    [Test]
    public void TestMetaDataType(){
        Assert.AreEqual(typeof(List<string>), typeof(levelLoader.meta));
    }

    [Test]
    public void TestLegendDataType(){
        Assert.AreEqual(typeof(List<string>), typeof(levelLoader.legend));
    }

    [Test]
    public void TestMapFirstElm(){
        Assert.AreEqual(levelLoader.map.First(), "-/n");
    }

    [Test]
    public void TestMetaFirstElm(){
        Assert.AreEqual(levelLoader.map.First(), "Name: LEVEL 1/n");
    }

    [Test]
    public void TestLegendFirstElm(){
        Assert.AreEqual(levelLoader.map.First(), "%) blue-block.png/n");
    }



}
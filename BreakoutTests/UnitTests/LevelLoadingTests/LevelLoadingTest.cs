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
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        blocks = new EntityContainer<Block>(288);
        levelLoader = new LevelLoader(); 
    }

    [SetUp]
    public void Setup(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        blocks = new EntityContainer<Block>(288);
        levelLoader = new LevelLoader();
    }

    [Test]
    public void TestFileNotFound(){
        Assert.AreEqual(blocks, levelLoader.LoadLevel("FileNot Found.txt"));
    }


    [Test]
    public void TestCorrectPosition(){
        blocks = levelLoader.LoadLevel("../../../../../../Breakout/Assets/Levels/level1.txt");
        var c = new Vec2F(0.5f,0.5f);
        var b = 1;
        var a = new NormalBlock(new DynamicShape(new Vec2F(0.5f, 0.5f), 
                                        new Vec2F(1.0f/12, (1.0f/12)/3f)),
                                        new Image(@"Assets/Images/blue-block.png"));;
        foreach(Block elm in blocks){
            if (b==1){
                b++;
                c = elm.shape.Position;
            }
        }
        Assert.AreEqual(c.X, 0.0f + 1.0f * 1.0f/12.0f);
        Assert.AreEqual(c.Y, 0.9f - 2.0f * (1.0f/12.0f)/3.0f);
    }

    [Test]
    public void TestMapDataType(){
        var emptyList = new List<char>{};
        Assert.AreEqual(emptyList, levelLoader.map);
    }
    
    [Test]
    public void TestMetaDataType(){
        var emptyList = new List<string>{};
        Assert.AreEqual(emptyList, levelLoader.meta);
    }

    [Test]
    public void TestLegendDataType(){
        var emptyList = new List<string>{};
        Assert.AreEqual(emptyList, levelLoader.legend);
    }

    [Test]
    public void TestMapFirstElm(){
        levelLoader.OnlyUsedForTestingPrivateReadAscii("../../../../../../Breakout/Assets/Levels/level1.txt");
        var b = 1;
        var a = ' ';
        foreach(char elm in levelLoader.map){
            if (b==1){
                b++;
                a=elm;
            }
        }
        Assert.AreEqual(a, '-');
    }

    [Test]
    public void TestMetaFirstElm(){
        levelLoader.OnlyUsedForTestingPrivateReadAscii("../../../../../../Breakout/Assets/Levels/level1.txt");
        var b = 1;
        var a = "";
        foreach(string elm in levelLoader.meta){
            if (b==1){
                b++;
                a=elm;
            }
        }
        Assert.AreEqual(a, "Name: LEVEL 1");
    }

    [Test]
    public void TestLegendFirstElm(){
        levelLoader.OnlyUsedForTestingPrivateReadAscii("../../../../../../Breakout/Assets/Levels/level1.txt");
        var b = 1;
        var a = "";
        foreach(string elm in levelLoader.legend){
            if (b==1){
                b++;
                a=elm;
            }
        }
        Assert.AreEqual(a, "%) blue-block.png");
    }



}
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


namespace BreakoutTests{

        [TestFixture]
    public class LevelLoadingC1sTest{
        private LevelLoader levelLoader;
        private EntityContainer<Block> blocks;
        public LevelLoadingC1sTest() {  
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            blocks = new EntityContainer<Block>(288);
            levelLoader = new LevelLoader(); 
        }

        [SetUp]
        public void InitiateLeveLoaderTest(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            blocks = new EntityContainer<Block>(288);
            levelLoader = new LevelLoader();
        }

        [Test]
        public void C1Test(){
            //levelLoader.LoadLevel(Path.Combine("/", "/", "/", "/","Assets", "Images", "level1.txt"));
        }

    }



}
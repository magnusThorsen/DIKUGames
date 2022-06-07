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
        private bool hasNorm;
        private bool hasHard;
        private bool hasUnbreak;
        private bool hasPower;
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


        ///The following two tests excersise all statement branches 

        [Test]
        public void CreateAllMetaElementsC1Test(){
            blocks = levelLoader.LoadLevel("../../../../../../BreakoutTests/Assets/Levels/level1.txt");
            Assert.True(blocks.CountEntities() != 0);
        }



        [Test]
        public void EmptyAsciiC1Test(){
            blocks = levelLoader.LoadLevel("../../../../../../BreakoutTests/Assets/Levels/level3.txt");
            Assert.True(blocks.CountEntities() == 0);
        }


        [Test]
        public void NoMatchLegendC1Test(){
            blocks = levelLoader.LoadLevel("../../../../../../BreakoutTests/Assets/Levels/level3.txt");
            Assert.True(blocks.CountEntities() == 0);
        }

    


        [Test]
        public void C1AllBlocks (){
            hasNorm = false;
            hasHard = false;
            hasUnbreak = false;
            hasPower = false;
            blocks = levelLoader.LoadLevel("../../../../../../BreakoutTests/Assets/Levels/level1.txt");

            foreach(Block block in blocks){
                if (block is NormalBlock){
                    hasNorm = true;
                }
            }
            foreach(Block block in blocks){
                if (block is HardenedBlock){
                    hasHard = true;
                }
            }
            foreach(Block block in blocks){
                if (block is UnbreakableBlock){
                    hasUnbreak = true;
                }
            }
            foreach(Block block in blocks){
                if (block is PowerUpBlock){
                    hasPower = true;
                }
            }
            Assert.True(hasNorm);
            Assert.True(hasHard);
            Assert.True(hasUnbreak);
            Assert.True(hasPower);
        }


    }



}
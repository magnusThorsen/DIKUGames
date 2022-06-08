using DIKUArcade;
using DIKUArcade.GUI;
using System;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System.Collections.Generic;


namespace Breakout { 
    /// <summary>
    ///  This class loads new levels from only an ascii-file.
    /// </summary>  

    public class LevelLoader {

        public List<char> map {get; private set;}
        public List<string> meta {get; private set;}
        public List<string> legend {get; private set;}
        private int x;
        private int y;
        private EntityContainer<Block> Blocks {get;}
        private int currBlockValue;

        IDictionary<string, string> metaDic;
        IDictionary<char, DIKUArcade.Graphics.Image> legendImageDic;
        IDictionary<char, string> legendStringDic;



        public LevelLoader(){
            x = 0;
            y = 0;
            Blocks = new EntityContainer<Block>(288);
            map = new List<char>{};
            meta = new List<string>{};
            legend = new List<string>{};
            metaDic = new Dictionary<string,string>();
            legendImageDic = new Dictionary<char, DIKUArcade.Graphics.Image>();
            legendStringDic = new Dictionary<char, string>();
            currBlockValue = 0;
        }

        /// <summary>
        /// Takes an ascii file and inserts it into three lists, map, meta and legends. 
        /// Only Takes Working Ascii Files, but handles FileNotFound.
        /// </summary>
        /// <param name="filename">A string that is the name of an Ascii file</param>
        private void ReadAscii(string filename){
            string[] FileLines = System.IO.File.ReadAllLines(Path.Combine("Assets", "Levels", 
                filename));
                
                //Adding Map section to map
                int StartMapIndex = Array.IndexOf(FileLines, "Map:")+1;
                int EndMapIndex = Array.IndexOf(FileLines, "Map/")-1;
                for (int Enumerator = StartMapIndex; Enumerator <= EndMapIndex; Enumerator++){
                    foreach(char elm  in FileLines[Enumerator]){
                        if (elm != '/'){
                            map.Add(elm);
                        }
                    }
                }

                //Adding Meta section to Meta
                int StartMetaIndex = Array.IndexOf(FileLines, "Meta:")+1;
                int EndMetaIndex = Array.IndexOf(FileLines, "Meta/")-1;
                for (int Enumerator = StartMetaIndex; Enumerator <= EndMetaIndex; Enumerator++){
                        meta.Add(FileLines[Enumerator]);
                }

                //Adding Legend section to legend
                int StartLegendIndex = Array.IndexOf(FileLines, "Legend:")+1;
                int EndLegendIndex = Array.IndexOf(FileLines, "Legend/")-1;
                for (int Enumerator = StartLegendIndex; Enumerator <= EndLegendIndex; Enumerator++){
                        legend.Add(FileLines[Enumerator]);
                }
        }

                            
        /// <summary>
        /// Adds blocks to the EntityContainer blocks, and handles meta data accordingly.
        /// </summary>
        private void AddBlocks(){
            foreach (char charElm in map){
                currBlockValue++;
                if (IsMeta(charElm)) {
                    AddMetaElement(charElm);
                } else {
                    AddNormalBlock(charElm);
                }
                IncXnY();
            }
        }

        /// <summary>
        /// Adds a metaelent
        /// </summary>
        /// <param name="c">the char to decide what legend.key to use</param>
        private void AddMetaElement(char c) {
            foreach (var legendElm in legendStringDic){
                foreach(var metaElm in metaDic){
                    if (c == legendElm.Key && legendElm.Key.ToString() == 
                        metaElm.Value && metaElm.Key == "Hardened") {

                        string textPart = legendElm.Value.Substring(0, legendElm.Value.Length-10);
                        var newBlock = new HardenedBlock(
                            new DynamicShape(new Vec2F(
                                0.0f + x * 1.0f/12, 0.9f - y * (1.0f/12)/3f), 
                            new Vec2F(1.0f/12, (1.0f/12)/3f)),
                            new Image(Path.Combine("Assets", "Images", legendElm.Value)),
                            textPart);
                        newBlock.SetValue(currBlockValue);
                        Blocks.AddEntity(newBlock);
                        BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, newBlock);
                    }

                    if (c == legendElm.Key && legendElm.Key.ToString() == 
                        metaElm.Value && metaElm.Key == "Unbreakable") {
                        var newBlock = new UnbreakableBlock(
                            new DynamicShape(new Vec2F(
                                0.0f + x * 1.0f/12, 0.9f - y * (1.0f/12)/3f), 
                            new Vec2F(1.0f/12, (1.0f/12)/3f)),
                            new Image(Path.Combine("Assets", "Images", legendElm.Value))
                            );
                    
                        newBlock.SetValue(currBlockValue);
                        Blocks.AddEntity(newBlock);
                        BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, newBlock);
                    }

                    if (c == legendElm.Key && legendElm.Key.ToString() == 
                        metaElm.Value && metaElm.Key == "PowerUp") {
                        var newBlock = new PowerUpBlock(
                            new DynamicShape(new Vec2F(
                                0.0f + x * 1.0f/12, 0.9f - y * (1.0f/12)/3f), 
                            new Vec2F(1.0f/12, (1.0f/12)/3f)),
                            new Image(Path.Combine("Assets", "Images", legendElm.Value))
                            );
                    
                        newBlock.SetValue(currBlockValue);
                        Blocks.AddEntity(newBlock);
                        BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, newBlock);
                    }
                }
            }
        }

        /// <summary>
        /// Adds the normal blocks
        /// </summary>
        /// <param name="charElm">the char to chek with the LegegendImageDic elm.Key with.</param>
        private void AddNormalBlock(char charElm){
            foreach (var elm in legendImageDic){
                        if (charElm == elm.Key) {
                            var newBlock = new NormalBlock(
                                            new DynamicShape(new Vec2F(
                                                0.0f + x * 1.0f/12, 0.9f - y * (1.0f/12)/3f), 
                                            new Vec2F(1.0f/12, (1.0f/12)/3f)),
                                            elm.Value);
                            newBlock.SetValue(currBlockValue);
                            Blocks.AddEntity(newBlock);
                            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, newBlock);
                        }   
                    }
        }




        /// <summary>
        /// Handles meta chars. Checks if a char is in the meta sections og the Ascii file.
        /// </summary>
        /// <param name="c">A char</param>
        /// <returns></returns>
        private bool IsMeta(char c){
            foreach(var metaDicElm in metaDic){
                if (c.ToString() == metaDicElm.Value){
                    return true;
                }
            }
            return false;
        }



        /// <summary>
        /// increments x and if x is bigger than 12 it resets x to 0 and increments y.
        /// </summary>
        private void IncXnY(){
            x++;
            if (x == 12){
                y++;
                x=0;
            }
        }
        
        /// <summary>
        /// Reads the specified Asciifile and calls the ReadAscii function on it, Adds the blocks 
        /// and returns an entitycontainer now woth blocks
        /// </summary>
        /// <param name="filename">A string with the name of an Ascii file</param>
        /// <returns></returns>
        public EntityContainer<Block> LoadLevel(string filename){

            //clear the levelloader
            Reset();

            //Read the ascii into lists, fill the dictionaries and add the blocks
            ReadAscii(filename);
            FillMetaDic();
            FillLegendDics();
            AddBlocks();
            AddTimer();
            return Blocks;
        } 


        /// <summary>
        /// Fills the metaDic
        /// </summary>
        private void FillMetaDic(){
            foreach (string elm in meta){
                int IndexOfSplit = elm.IndexOf(":");
                string key = elm.Substring(0, IndexOfSplit);
                string val = elm.Substring(IndexOfSplit+2, elm.Length-IndexOfSplit-2);
                try{
                    metaDic.Add(key, val);
                }
                catch{
                    Console.WriteLine("Duplicate key in meta");}
            }
        }


        /// <summary>
        /// Fills the legend dics
        /// </summary>
        private void FillLegendDics(){
            foreach (string elm in legend){
                char key = elm[0];
                string val = elm.Substring(3, elm.Length-3);
                try{
                    legendImageDic.Add(key, new Image(Path.Combine("Assets", "Images", val)));
                    legendStringDic.Add(key, val);
                }
                catch{
                    Console.WriteLine("Duplicate key in legend");}
            }
        }


        /// <summary>
        /// Resets the LevelLoader
        /// </summary>
        private void Reset(){
            map.Clear();
            meta.Clear();
            legend.Clear();
            metaDic.Clear();
            legendImageDic.Clear();
            legendStringDic.Clear();
            x = 0;
            y = 0;
            //currBlockValue = 0;
        }




        /// <summary>
        /// May only be used to test AsciiReader. 
        /// </summary>
        /// <param name="filename">The file to test AsciiReaderWith</param>
        public void OnlyUsedForTestingPrivateReadAscii(string filename){
            ReadAscii(filename);
        }


        /// <summary>
        /// Adds a timer to the level if the meta segment specifies it, by creating an event.
        /// </summary>
        private void AddTimer(){
            foreach(var metaElm in metaDic){
                if (metaElm.Key == "Time"){
                    BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.StatusEvent, Message = "Time", 
                        StringArg1 = metaElm.Value
                    });
                }

            }
        }

    }
}

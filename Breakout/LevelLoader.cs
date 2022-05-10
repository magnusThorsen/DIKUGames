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

    public class LevelLoader {

        public List<char> map {get; private set;}
        public List<string> meta {get; private set;}
        public List<string> legend {get; private set;}
        private int x;
        private int y;
        private EntityContainer<Entity> Blocks {get;}
        private int currBlockValue;

        IDictionary<string, string> metaDic;
        IDictionary<char, DIKUArcade.Graphics.Image> legendImageDic;
        IDictionary<char, string> legendStringDic;



        public LevelLoader(){
            x = 0;
            y = 0;
            Blocks = new EntityContainer<Entity>(288);
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
            /*try{
            }
            catch (FileNotFoundException ex){
                System.Console.WriteLine("In ReadAscii:" + ex);
            }*/

            string[] FileLines = System.IO.File.ReadAllLines(Path.Combine("Assets", "Levels", filename));
                
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
                if (MetaHandler(charElm)) {
                    AddMetaElement(charElm);
                } else {
                    foreach (var elm in legendImageDic){
                        if (charElm == elm.Key) {
                            var newBlock = new Block(
                                            new DynamicShape(new Vec2F(
                                                0.0f + x * 1.0f/12, 0.9f - y * (1.0f/12)/3f), 
                                            new Vec2F(1.0f/12, (1.0f/12)/3f)),
                                            elm.Value);
                            newBlock.SetValue(currBlockValue);
                            Blocks.AddEntity(newBlock);
                            currBlockValue++;
                            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, newBlock);
                        }   
                    }
                }
                IncXnY();
            }
        }

        private void AddMetaElement(char c) {
            foreach(var metaElm in metaDic){
                if(Char.ToString(c) == metaElm.Value){
                    switch (metaElm.Key) {
                        case "Hardened": 
                            
                            foreach (var legendElm in legendStringDic){
                                if (c == legendElm.Key) {
                                    
                                    string textPart = legendElm.Value.Substring(11, legendElm.Value.Length-5);
                                    var newBlock = new HardenedBlock(
                                                    new DynamicShape(new Vec2F(
                                                        0.0f + x * 1.0f/12, 0.9f - y * (1.0f/12)/3f), 
                                                    new Vec2F(1.0f/12, (1.0f/12)/3f)),
                                                    new Image(Path.Combine("Assets", "Images", legendElm.Value)),
                                                    textPart);
                                    newBlock.SetValue(currBlockValue);
                                    Blocks.AddEntity(newBlock);
                                    currBlockValue++;
                                    BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, newBlock);
                                }
                            }
                            break;
                        default:break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles meta chars. Checks if a char is in the meta sections og the Ascii file.
        /// </summary>
        /// <param name="c">A char</param>
        /// <returns></returns>
        private bool MetaHandler(char c){
            foreach(string strElm in meta){
                if (c == strElm[strElm.Length-1]){
                    return true;
                } else {return false;}
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
        public EntityContainer<Entity> LoadLevel(string filename){
            
            //clear the levelloader
            Reset();

            //Read the ascii into lists, fill the dictionaries and add the blocks
            ReadAscii(filename);
            FillMetaDic();
            FillLegendDics();
            AddBlocks();
            return Blocks;
        } 




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


        private void Reset(){
            map.Clear();
            meta.Clear();
            legend.Clear();
            metaDic.Clear();
            legendImageDic.Clear();
            legendStringDic.Clear();
            x = 0;
            y = 0;
            currBlockValue = 0;
        }




        /// <summary>
        /// May only be used to test AsciiReader. 
        /// </summary>
        /// <param name="filename">The file to test AsciiReaderWith</param>
        public void OnlyUsedForTestingPrivateReadAscii(string filename){
            ReadAscii(filename);
        }


    }
}

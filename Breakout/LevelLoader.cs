using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
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
        private EntityContainer<Block> Blocks {get;}

        public LevelLoader(){
            x = 0;
            y = 0;
            Blocks = new EntityContainer<Block>(288);
            map = new List<char>{};
            meta = new List<string>{};
            legend = new List<string>{};
        }

        /// <summary>
        /// Takes an ascii file and inserts it into three strings, map, meta and legends. Only Takes Working Ascii Files, but handles FileNotFound.
        /// </summary>
        /// <param name="filename">A string that is the name of an Ascii file</param>
        private void ReadAscii(string filename){
            bool a = true;
            int b = 0;
            try{
                string[] FileLines = System.IO.File.ReadAllLines(filename);
            foreach (string line in FileLines){  
                switch (line){
                    case "Map:": case "Map/": case "Meta:": case "Meta/": case "Legend:": case "Legend/":
                        a = false;
                        b++;
                        break;
                    default: 
                        a = true;
                        break;
                }
                if (a){
                    switch (b){
                        case 1:
                            foreach (char c in line){
                                map.Add(c); 
                            }
                            break;
                        case 3:
                            meta.Add(line);
                            break;
                        case 5:
                            legend.Add(line);
                            break;

                        default: break;
                    }
                }

                
            }
            }
            catch (FileNotFoundException ex){
                System.Console.WriteLine(ex);
            }
        }
                            
        /// <summary>
        /// Adds blocks to the EntityContainer blocks, and handles meta data accordingly.
        /// </summary>
        private void AddBlocks(){
            string pngStr = new string("");
            foreach (char charElm in map){
                switch(charElm){
                    case '/': case 'n': break;
                    case '-': 
                        IncXnY();
                        break;
                    default:
                        if (MetaHandler(charElm)){   
                            foreach(string strElm in legend){
                                if (charElm == strElm[0]){
                                    pngStr = strElm.Remove(0, 3);
                                    var newBlock = new Block(
                                        new DynamicShape(new Vec2F(0.0f + x * 1.0f/12, 0.9f - y * (1.0f/12)/3f), 
                                        new Vec2F(1.0f/12, (1.0f/12)/3f)),
                                        new Image(@"Assets/Images/" + pngStr));
                                    Blocks.AddEntity(newBlock);
                                    BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, newBlock);
                                }
                            }
                        }
                        IncXnY();
                    break;
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
                    return false;
                } else {return true;}
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
            ReadAscii(filename);
            AddBlocks();
            return Blocks;
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

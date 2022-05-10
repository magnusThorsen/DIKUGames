using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System.Collections.Generic;

namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState{

        private static GameRunning instance = null;
        private Player player{get;set;} 
        private LevelLoader levelLoader;
        private EntityContainer<Block> blocks{get;set;}
        private bool gameOver;
        private int level;

        /// <summary>
        /// GetInstance sets up the GameRunning
        /// </summary>
        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }

        /// <summary>
        /// Initializes all fields, and what else is needed for the methods
        /// !!Games constructor, lav player osv.
        /// </summary>
        public void InitializeGameState() {
            player = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.425f, 0.03f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

            blocks = new EntityContainer<Block>(288);
            levelLoader = new LevelLoader();
            gameOver = false;
            level = 0;
        }

        /// <summary>
        /// Når spillet er slut og der skal startes et nyt så resetter den det hele(fra main menu)
        /// </summary>
        public void ResetState() {
            blocks.ClearContainer();
            this.level = 0;
            gameOver = false;
        }

        /// <summary>
        /// Kalde alle update i game. If statementet sprøer den om spillet er slut hvis ja event til main menu
        /// </summary>
        public void UpdateState() {
            player.Move();
            NewLevel();
            if(gameOver){
                BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.StatusEvent, Message = "GameOver", 
                    });
            }
        }


        /// <summary>
        /// Samme som game
        /// </summary>
        public void RenderState() {
            player.Render();
            blocks.RenderEntities();
        }



        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
                default: break;
            }  
        }
        

        private void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Escape: 
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_PAUSED"
                        }
                    );
                    break;
                case KeyboardKey.Right:
                    BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "KeyPress", 
                        IntArg1 = (int) key
                    });
                    break;
                case KeyboardKey.Left: 
                    BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "KeyPress", 
                        IntArg1 = (int) key
                    });
                    break; 

                case KeyboardKey.G: 
                    blocks.ClearContainer();
                    NewLevel();
                    break;

                default:
                    break;
            }
        }  


        private void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Right:
                    BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "KeyRelease", 
                        IntArg1 = (int) key
                    });
                    break;
                case KeyboardKey.Left: 
                    BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "KeyRelease", 
                        IntArg1 = (int) key
                    });
                    break;     
                default: 
                    break;
            } 
        }


        /// <summary>
        /// Creates a batch of blocks if the entitylist blocks is empty.
        /// </summary>
        private void NewLevel(){
            if (blocks.CountEntities() <= 0) {
                try{
                    this.level++;
                    string levelstring = "level" + this.level + ".txt";
                    blocks = levelLoader.LoadLevel(levelstring);
                }
                catch{
                    ResetState();
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE",
                            StringArg1 = "MAIN_MENU"
                        }
                    );
                }
            
            }
        }

        /// <summary>
        /// checks if the game is over. 
        /// </summary>
        private void CheckGameOver() {
            gameOver = false;
        }

    }

}
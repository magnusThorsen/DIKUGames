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


    public class Game : DIKUGame, IGameEventProcessor {
        
        private Player player; 
        private LevelLoader levelLoader;
        private EntityContainer<Block> blocks {get; set;}

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {
                 GameEventType.InputEvent, 
            GameEventType.PlayerEvent, GameEventType.GameStateEvent});
            player = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.425f, 0.03f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            window.SetKeyEventHandler(KeyHandler);

            blocks = new EntityContainer<Block>(288);
            levelLoader = new LevelLoader();


        }
        /// <summary>
        /// Handles KeyboardActions and KeyboardKeys
        /// </summary>
        /// <param name="action">A KeyBoardAction</param>
        /// <param name="key">A KeyBoardKey</param>
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
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

        /// <summary>
        /// Renders everything
        /// </summary>
        public override void Render() {
            player.Render();
            blocks.RenderEntities();
        }

        /// <summary>
        /// Updates everything
        /// </summary>
        public override void Update() {
            player.Move();
            NewLevel();
            BreakoutBus.GetBus().ProcessEventsSequentially(); 
        }


        /// <summary>
        /// Sends out the appropriate EventType for the KeyBoardKey pressed.
        /// </summary>
        /// <param name="key">The pressed key</param>
        public void KeyPress(KeyboardKey key) { // Initiating keypresses on given keys with switch
            switch (key) {
                case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.InputEvent, Message = "escape", 
                        IntArg1 = (int) key
                    });
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
                default: break;
            }
        }

        /// <summary>
        /// Sends out the appropriate EventType for the KeyBoardKey released.
        /// </summary>
        /// <param name="key">The released key</param>
        public void KeyRelease(KeyboardKey key) {
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
        /// Processes all events in the bus and responds accordingly.
        /// </summary>
        /// <param name="gameEvent"></param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.InputEvent) { //Checks if it a InputEvent
                switch (gameEvent.Message) { //switches on message, only does something with 
                                             //KeyPress and KeyRelease
                    case "escape": 
                        window.CloseWindow();
                        break;
                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// Creates a batch of blocks if the entitylist blocks is empty.
        /// </summary>
        public void NewLevel(){
            if (blocks.CountEntities() <= 0) {
                blocks = levelLoader.LoadLevel(@"Assets/Levels/level1.txt");
            }
        }
    }
}
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout;
using DIKUArcade.Timers;
using System;

namespace Breakout.BreakoutStates {
    
    /// <summary>
    ///  This class represents the state that is active when the game is running.
    /// </summary>  
    public class GameRunning : IGameState, IGameEventProcessor {

        private static GameRunning instance = null;
        private Player player{get; set;} 
        private LevelLoader levelLoader;
        public EntityContainer<Block> blocks {get; private set;}
        public bool gameOver{get;set;}
        private int level;
        private Points points;
        public EntityContainer<Ball> balls{get;private set;}
        public int maxBalls;
        public EntityContainer<PowerUpDrop> powerDrops;
        private double currentTime;
        private Text timeText;
        public int timeLeft;
        public double startTime;
        private bool hasTime;

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
        /// </summary>
        public void InitializeGameState() {
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
            player = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.425f, 0.03f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
            
            //elements
            blocks = new EntityContainer<Block>(288);
            levelLoader = new LevelLoader();
            gameOver = false;
            level = 0;
            points = new Points(new Vec2F(0.6f,0.5f), new Vec2F(0.5f,0.5f));
            BreakoutBus.GetBus().Subscribe(GameEventType.GraphicsEvent, points);
            maxBalls = 10;
            balls = new EntityContainer<Ball>(maxBalls);
            balls.AddEntity(CreateBall());
            powerDrops = new EntityContainer<PowerUpDrop>();

            //Time related
            currentTime = 0.0;
            startTime = 0.0;
            timeLeft = 0;
            timeText = new Text("", new Vec2F(0.05f, 0.2f), new Vec2F(0.5f, 0.5f));
            timeText.SetColor(new Vec3I(255,255,255));
            hasTime = false;
        }

        /// <summary>
        /// Resets the game. Used when game is over, or new level loads
        /// </summary>
        public void ResetState() {
            blocks.ClearContainer();
            powerDrops.ClearContainer();
            level = 0;
            gameOver = false;
            player.Reset();
            ResetBalls();
            points.ResetPoints();
            hasTime = false;
        }

        /// <summary>
        /// Updates all game functionality
        /// </summary>
        public void UpdateState() {
            HandleTime();
            player.Move();
            MoveBalls();
            RemoveDeletedEntities();
            CheckBallsEmpty();
            CheckGameOver();
            NewLevel();
            PowerUpIterate();
        }


        /// <summary>
        /// Renders all in game
        /// </summary>
        public void RenderState() {
            player.Render();
            blocks.RenderEntities();
            balls.RenderEntities();
            points.RenderPoints();
            powerDrops.RenderEntities();
            timeText.RenderText();
        }


        /// <summary>
        /// Delegates the KeyEvents to the PressKey() or ReleaseKey()
        /// </summary>
        /// <param name="action"> the action performed </param>
        /// <param name="key">the key pressed</param>
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
        
        /// <summary>
        /// Creates the relevant GameEvents.
        /// </summary>
        /// <param name="key">the key switch with</param>
        private void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Escape: 
                    GamePaused();
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

                case KeyboardKey.G: // To skip a level 
                    blocks.ClearContainer();
                    NewLevel();
                    break;

                case KeyboardKey.Space: // Launch ball
                    BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.InputEvent, 
                        Message = "LAUNCH_BALL"
                    });
                    break;
                default:
                    break;
            }
        }  

        /// <summary>
        /// Creates the relevant GameEvents.
        /// </summary>
        /// <param name="key">the key released</param>
        private void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Right: // Move player right
                    BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "KeyRelease", 
                        IntArg1 = (int) key
                    });
                    break;
                case KeyboardKey.Left: // Move player left 
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
            if (blocks.CountEntities() <= 0 || OnlyUnbreakBlocks()) {
                try{                
                    StaticTimer.RestartTimer();    
                    ResetBalls();
                    player.Reset();
                    level++;
                    //The current level as string
                    string levelstring = "level" + this.level + ".txt"; 
                    blocks = levelLoader.LoadLevel(levelstring);
                    hasTime=false;
                    powerDrops.ClearContainer();
                }
                catch{ //catches no more levels, and as such ends the game.
                    GameWon();
                }
            
            }
        }

        /// <summary>
        /// checks if the game is over. 
        /// </summary>
        public void CheckGameOver() {
            if(gameOver){
                GameLost();
            }
            if (hasTime && timeLeft < 0){
                GameLost();
            }
        }


        /// <summary>
        /// Processes all events in the EventBus, but only matches on relevant.
        /// </summary>
        /// <param name="gameEvent"></param>
        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.StatusEvent) { 
                    switch (gameEvent.Message) {  
                        case "PlayerDead":
                            GameLost();
                            break;
                        case "Time":
                            startTime = int.Parse(gameEvent.StringArg1);
                            hasTime = true;
                            break;
                        case "IncTime": 
                            startTime = startTime+10;
                            break;
                        default:
                            break;
                    }
            }
        }


        /// <summary>
        /// Removes all deleted entities.
        /// </summary>
        private void RemoveDeletedEntities() {
            //deletes all blocks that are deleted
            blocks.Iterate(block => {
                if (block.IsDeleted()) { 
                    if (block.IsPowerUp() == true) { // Checks if block is a PowerUpBlock
                        powerDrops.AddEntity(new PowerUpDrop( 
                            // powerUpDrop is instantiated with positions and image
                            new DynamicShape(block.shape.Position, new Vec2F(0.06f, 0.06f)),
                            new Image(Path.Combine("Assets", "Images", "RocketPickUp.png"))));
                    }
                    block.DeleteEntity();
                }
            });
            // Deletes all balls that are deleted
            balls.Iterate(ball => {
                if (ball.IsDeleted()) {
                    ball.DeleteEntity();
                }
            });
            // Deletes all drops that are deleted 
            powerDrops.Iterate(drop => {
                if (drop.IsDeleted()) {
                    drop.DeleteEntity();
                }
            });
        }


        /// <summary>
        /// Checks if there are only unbreakable blocks in level
        /// </summary>
        /// <returns></returns>
        private bool OnlyUnbreakBlocks() {
            bool onlyUnbreakBlocks = true;
            foreach(Block block in blocks) { 
                if (block is not UnbreakableBlock) {
                    onlyUnbreakBlocks = false;
                }
            }
            return onlyUnbreakBlocks; 
        }

        /// <summary>
        /// Creates a ball 
        /// </summary>
        /// <returns> Returns a ball </returns>
        public Ball CreateBall() {
            Ball ball = new Ball (
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, ball);
            return ball;
        }

        /// <summary>
        /// Moves all balls in the game
        /// </summary>
        private void MoveBalls() {
            foreach (Ball ball in balls) { 
                ball.Move(player, blocks);
            }
        }

        /// <summary>
        /// Deletes all balls and creates a new
        /// </summary>
        private void ResetBalls() {
            balls.Iterate(ball => {
                    ball.DeleteEntity(); 
            });
            balls.AddEntity(CreateBall());
        }

        /// <summary>
        /// Checks if there af no more balls in game and decreases life of player. 
        /// Creates new ball
        /// </summary>
        private void CheckBallsEmpty() {
            if (balls.CountEntities() <= 0) {
                BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "DecLife"
                    });
                balls.AddEntity(CreateBall());
            }
        }

        /// <summary>
        /// Game over and lost. Sends points for use in lose screen. State is changed
        /// </summary>
        private void GameLost() {
            SendPoints();
            ResetState();
            BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_LOST"
                        }
            );
        }


        /// <summary>
        /// Game over and won. Sends points for use in win screen. State is changed
        /// </summary>
        private void GameWon() {
            SendPoints();
            ResetState();
            BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_WON"
                        }
            );
        }


        /// <summary>
        /// Game pauses. Changes state to pause screen. Pauses timer. 
        /// </summary>
        private void GamePaused() {
            StaticTimer.PauseTimer();
            BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_PAUSED"
                        }
            );
        }


        /// <summary>
        /// Sends points to game over screens
        /// </summary>
        private void SendPoints() {
            BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.ControlEvent, 
                            Message = "GamePoints",
                            IntArg1 = points.GetPoints()
                        }
            );
        }
        

        /// <summary>
        /// Moves all powerups and lets player catch them
        /// </summary>
        public void PowerUpIterate() {
            foreach (PowerUpDrop Drop in powerDrops) {
                Drop.Move();
                Drop.Consume(player,Drop.powerUpNumber);
            }
        }


        /// <summary>
        /// Creates time in game 
        /// </summary>
        public void HandleTime() {
            if (hasTime) {
                currentTime = StaticTimer.GetElapsedSeconds();
                timeLeft = Convert.ToInt32(startTime-currentTime);
                timeText = new Text(timeLeft.ToString(), new Vec2F(0.05f, 0.5f), 
                    new Vec2F(0.5f, 0.5f));
                timeText.SetColor(new Vec3I(255,0,0));
            } else {
                timeText = new Text("", new Vec2F(0.05f, 0.5f), new Vec2F(0.5f, 0.5f));
                timeText.SetColor(new Vec3I(255,0,0));
            }
        }


        /// <summary>
        /// Returns player for testing
        /// </summary>
        /// <returns> Returns a player </returns>
        public Player GetPlayer() {
            return player;
        }

        /// <summary>
        /// Returns LevelLoader for testing
        /// </summary>
        /// <returns> Returns a player </returns>
        public LevelLoader GetLevelLoader() {
            return levelLoader;
        }

        /// <summary>
        /// Returns points for testing
        /// </summary>
        /// <returns> Returns a player </returns>
        public Points GetPointsField() {
            return points;
        }

        /// <summary>
        /// Makes RemoveDeletedEntities able for testing
        /// </summary>
        public void CallRemoveDeletedEntities() {
            RemoveDeletedEntities();
        }

        /// <summary>
        /// Makes level able for testing
        /// /// </summary>
        /// <returns> Returns an int </returns>
        public int GetLevel() {
            return level;
        }

        /// <summary>
        /// Makes hasTime able for testing
        /// </summary>
        /// <returns> Returns a bool </returns>
        public bool GetHasTime() {
            return hasTime;
        }
    }
}
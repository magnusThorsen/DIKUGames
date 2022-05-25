using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout;

namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState, IGameEventProcessor {

        private static GameRunning instance = null;
        private Player player{get;set;} 
        private LevelLoader levelLoader;
        private EntityContainer<Block> blocks {get; set;}
        public bool gameOver{get;set;}
        private int level;
        private Points points;
        private EntityContainer<Ball> balls;
        private int maxBalls;

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
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
            player = new Player( // player is instantiated with positions and image
                new DynamicShape(new Vec2F(0.425f, 0.03f), new Vec2F(0.16f, 0.020f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

            blocks = new EntityContainer<Block>(288);
            levelLoader = new LevelLoader();
            gameOver = false;
            level = 0;
            points = new Points(new Vec2F(0.7f,0.5f), new Vec2F(0.5f,0.5f));
            BreakoutBus.GetBus().Subscribe(GameEventType.GraphicsEvent, points);
            maxBalls = 10;
            //ball = new Ball(
            //    new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
            //    new Image(Path.Combine("Assets", "Images", "ball2.png")));
            //BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, ball);
            maxBalls = 10;
            balls = new EntityContainer<Ball>(maxBalls);
            balls.AddEntity(CreateBall());
            
        }

        /// <summary>
        /// Når spillet er slut og der skal startes et nyt så resetter den det hele(fra main menu)
        /// </summary>
        public void ResetState() {
            blocks.ClearContainer();
            level = 0;
            gameOver = false;
            player.Reset();
            ResetBalls();
            points.ResetPoints();
            balls.AddEntity(CreateBall());
        }

        /// <summary>
        /// Kalde alle update i game. If statementet sprøer den om spillet er slut hvis ja event til main menu
        /// </summary>
        public void UpdateState() {
            player.Move();
            MoveBalls();
            CheckBallsEmpty();
            RemoveDeletedEntities();
            CheckGameOver();
            NewLevel();
        }


        /// <summary>
        /// Samme som game
        /// </summary>
        public void RenderState() {
            player.Render();
            blocks.RenderEntities();
            balls.RenderEntities();
            points.RenderPoints();
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

                case KeyboardKey.G: 
                    blocks.ClearContainer();
                    NewLevel();
                    break;

                case KeyboardKey.Space:
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
            if (blocks.CountEntities() <= 0 || OnlyUnbreakBlocks()) {
                try{                    
                    ResetBalls();
                    player.Reset();
                    level++;
                    string levelstring = "level" + this.level + ".txt";
                    blocks = levelLoader.LoadLevel(levelstring);
                }
                catch{ //catches no more levels, and as such ends the game.
                    GameWon();
                }
            
            }
        }

        /// <summary>
        /// checks if the game is over. 
        /// </summary>
        private void CheckGameOver() {
            if(gameOver){
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
                        default:
                            break;
                    }
                }
        }


        /// <summary>
        /// removes all deleted Blocks in block.
        /// </summary>
        private void RemoveDeletedEntities(){
            //deletes all blocks that are deleted
            blocks.Iterate(block => {
                if (block.IsDeleted()){
                    block.DeleteEntity();
                }
            });
            //deletes all balls that are deleted
            balls.Iterate(ball => {
                if (ball.IsDeleted()){
                    ball.DeleteEntity();
                }
            });
        }


        /// <summary>
        /// Checks if there are onl unbreakable blocks in blocks
        /// </summary>
        /// <returns></returns>
        private bool OnlyUnbreakBlocks(){
            bool onlyUnbreakBlocks = true;
            foreach(Block block in blocks) {
                if (block is not UnbreakableBlock){
                    onlyUnbreakBlocks = false;
                }
            }
            return onlyUnbreakBlocks;
        }
    
        public Ball CreateBall(){
            Ball ball = new Ball(
                new DynamicShape(new Vec2F(0.49f, 0.05f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, ball);
            return ball;
        }


        private void MoveBalls(){
            foreach (Ball ball in balls){
                ball.Move(player, blocks);
            }
        }

        private void ResetBalls(){
            foreach (Ball ball in balls){
                ball.DeleteEntity();
            }
            balls.AddEntity(CreateBall());
        }



        private void CheckBallsEmpty(){
            if (balls.CountEntities() <= 0) {
                BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "DecLife"
                    });
                balls.AddEntity(CreateBall());
            }
        }



        private void GameLost(){
            SendPoints();
            ResetState();
            BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_LOST"
                        }
            );
        }


        private void GameWon(){
            System.Console.WriteLine("gamerunning won points: " + points.GetPoints());
            SendPoints();
            ResetState();
            BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_WON"
                        }
            );
        }

        private void GamePaused(){
            BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_PAUSED"
                        }
            );
        }


        private void SendPoints(){
            System.Console.WriteLine("gamerunning points: " + points.GetPoints());
            BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.ControlEvent, 
                            Message = "GamePoints",
                            IntArg1 = points.GetPoints()
                        }
            );
        }


    }

}
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Timers;

namespace Breakout {

    public class Player : Entity, IGameEventProcessor {
        public Shape shape { get;}
        private float moveLeft; 
        private float moveRight;
        private float MOVEMENT_SPEED;
        private int life;
        private Text display;
        private int timeSpeed;
        private int timeWidth;
        private float windowLimit;
        private bool isWide;


        public Player(Shape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            moveLeft = 0.0f;
            moveRight = 0.0f;
            MOVEMENT_SPEED = 0.02f;
            Shape = shape;
            shape.Position = new Vec2F(0.425f, 0.03f);
            life = 3;
            isWide = false;
            display = new Text ("HP: " + life.ToString(), new Vec2F(0.05f, -0.4f), new Vec2F(0.5f,0.5f));
            display.SetColor(new Vec3I(255, 0, 0));
        }

        /// <summary>
        /// renders the player and points
        /// </summary>
        public void Render() {
            this.RenderEntity();
            display.SetText("HP: " + life.ToString());
            display.RenderText();
        }
        
        /// <summary>
        /// checks if the player can move.
        /// </summary>
        public void Move() {
            if (isWide == true)
                windowLimit = 0.645f;
            if (shape.Position.X + shape.AsDynamicShape().Direction.X > 0.0f && shape.Position.X + 
            shape.AsDynamicShape().Direction.X < windowLimit) {
                shape.Move();
            } 
            UpdatePlayerPowerups();
        }

        /// <summary>
        /// sets the left movement bool
        /// </summary>
        /// <param name="val">if the move right field should change or not</param>
        private void SetMoveLeft(bool val) {
            if (val == true) {
                moveLeft -= MOVEMENT_SPEED;  
                UpdateDirection(); 
            }
            else {
                moveLeft = 0.0f;
                UpdateDirection(); 
            }

        }
        
        /// <summary>
        /// Sets the right movement bool
        /// </summary>
        /// <param name="val">if the move right field should change or not</param>
        private void SetMoveRight(bool val) {
            if (val == true) {
                moveRight += MOVEMENT_SPEED;  
                UpdateDirection();
            }
            else {
                moveRight = 0.0f;
                UpdateDirection(); 
            }
        }   

        /// <summary>
        /// updates the direction of the player
        /// </summary>
        private void UpdateDirection() {
            shape.AsDynamicShape().Direction.X = moveRight + moveLeft;
        }

        /// <summary>
        /// returns the players position
        /// </summary>
        /// <returns>the players position</returns>
        public Vec2F GetPosition() {
            return shape.Position;
        }

        /// <summary>
        /// processes all events. only takes PlayerEvents.
        /// </summary>
        /// <param name="gameEvent">the gamevent to process.</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.PlayerEvent) { 
                switch (gameEvent.Message) {  
                    case "KeyPress":
                        KeyPress((KeyboardKey)gameEvent.IntArg1);
                        break;
                    case "KeyRelease":
                        KeyRelease((KeyboardKey)gameEvent.IntArg1);
                        break;
                    case "IncLife":
                        IncLife();
                        break;
                    case "DecLife":
                        DecLife();
                        break;
                    case "IncWidth":
                        IncWidth();
                        break;
                    case "DecWidth":
                        DecWidth();
                        break;
                    case "IncSpeed":
                        IncSpeed();
                        break;
                    case "DecSpeed":
                        DecSpeed();
                        break;
                    default:
                        break;
                }
            } 
        }

        /// <summary>
        /// calls SetMoveRight or SetMoveLeft accordinly to the pressed key
        /// </summary>
        /// <param name="key">pressed key</param>
        private void KeyPress(KeyboardKey key) { // Initiating keypresses on given keys with switch
            switch (key) {
                case KeyboardKey.Right:
                    SetMoveRight(true); // Starting movement on press
                    break;
                case KeyboardKey.Left: 
                    SetMoveLeft(true); // Starting movement on press
                    break; 
                default: break;
            }
        }

        /// <summary>
        /// calls SetMoveRight or SetMoveLeft accordinly to the released key
        /// </summary>
        /// <param name="key">released key</param>
        private void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Right:
                    SetMoveRight(false); // Stopping movement on release
                    break;
                case KeyboardKey.Left:
                    SetMoveLeft(false); // Stopping movement on release
                    break;
                default: 
                    break;
            } 
        }

        /// <summary>
        /// Resets the player
        /// </summary>
        public void Reset(){
            shape.Position = new Vec2F(0.425f, 0.03f);
            life = 3;
        }


        private void DecLife(){
            life--;
            if (life <= 0){
                BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.StatusEvent, 
                            Message = "PlayerDead"
                        }
                    );              
            }

        }

        private void IncLife(){
            life++;
        }

        private void IncWidth() {
            isWide = true;
            this.shape.ScaleX(2.0f);
            timeWidth = System.Convert.ToInt32(StaticTimer.GetElapsedSeconds());
        }

        private void DecWidth() {
            this.shape.ScaleX(0.5f);
        }

        private void IncSpeed() {
            MOVEMENT_SPEED*=2.0f;
            timeSpeed = System.Convert.ToInt32(StaticTimer.GetElapsedSeconds());
        }

        private void DecSpeed() {
            MOVEMENT_SPEED*=0.5f;
        }


        private void UpdatePlayerPowerups(){
            if (timeWidth == System.Convert.ToInt32(StaticTimer.GetElapsedSeconds() + 10)){
                DecWidth();
            }
            if (timeSpeed == System.Convert.ToInt32(StaticTimer.GetElapsedSeconds() + 10)){
                DecSpeed();
            }
        }



    }




}
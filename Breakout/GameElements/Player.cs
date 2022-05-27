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
        public bool isWide;
        private bool isFast;

        public Player(Shape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            moveLeft = 0.0f;
            moveRight = 0.0f;
            MOVEMENT_SPEED = 0.02f;
            Shape = shape;
            shape.Position = new Vec2F(0.425f, 0.03f);
            life = 3;
            isWide = false;
            isFast = false;
            display = new Text ("HP: " + life.ToString(), new Vec2F(0.05f, -0.4f), new Vec2F(0.5f,0.5f));
            display.SetColor(new Vec3I(255, 0, 0));
            timeSpeed=-100;
            timeWidth=-100;
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
                windowLimit = 0.681f;
            else
                windowLimit = 0.845f;
            if (shape.Position.X + shape.AsDynamicShape().Direction.X > 0.0f && shape.Position.X + 
            shape.AsDynamicShape().Direction.X < windowLimit) {
                shape.Move();
            }
            else if (isWide == true && shape.Position.X + shape.AsDynamicShape().Direction.X > windowLimit){
                shape.Position.X = 0.681f;
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
                    case "IncSpeed":
                        IncSpeed();
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
            MOVEMENT_SPEED = 0.02f;
            Shape = shape;
            isFast = false;
            shape.Extent = new Vec2F(0.16f, 0.020f);
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
            if (isWide == false) {
                isWide = true;
                this.shape.ScaleX(2.0f);
                timeWidth = System.Convert.ToInt32(StaticTimer.GetElapsedSeconds());
            }
        }

        private void IncSpeed() {
            if (isFast == false) {
                isFast = true;
                MOVEMENT_SPEED*=2.0f;
                timeSpeed = System.Convert.ToInt32(StaticTimer.GetElapsedSeconds());
            }
        }

        private void UpdatePlayerPowerups(){
                if (isWide && timeWidth + 10  < System.Convert.ToInt32(StaticTimer.GetElapsedSeconds())){
                    timeWidth = -100;
                    isWide = false;
                    shape.Extent = new Vec2F(0.16f, 0.020f);
                }
                if (isFast && timeSpeed + 10 < System.Convert.ToInt32(StaticTimer.GetElapsedSeconds())){
                    timeSpeed = -100;
                    isFast = false;
                    MOVEMENT_SPEED = 0.02f;
                }
        }
    }
}

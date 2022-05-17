using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout {

    public class Player : Entity, IGameEventProcessor {
        public Shape shape { get;}
        private float moveLeft; 
        private float moveRight;
        private const float MOVEMENT_SPEED = 0.02f;
        private int life;

        public Player(Shape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            moveLeft = 0.0f;
            moveRight = 0.0f; 
            Shape = shape;
            shape.Position = new Vec2F(0.425f, 0.03f);
            life = 1;
        }

        /// <summary>
        /// renders the player
        /// </summary>
        public void Render() {
            this.RenderEntity();
        }
        
        /// <summary>
        /// checks if the player can move.
        /// </summary>
        public void Move() {
            if (shape.Position.X + shape.AsDynamicShape().Direction.X > 0.0f && shape.Position.X + 
            shape.AsDynamicShape().Direction.X < 0.845f) {
                shape.Move();
            } 
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
                        System.Console.WriteLine("player dec life");
                        DecLife();
                        break;
                    case "IncWidth":
                        IncWidth();
                        break;
                    case "DecWidth":
                        DecWidth();
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
        }


        public void DecLife(){
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

        public void IncLife(){
            life++;
        }

        public void IncWidth() {
            this.shape.ScaleX(2.0f);
        }

        public void DecWidth() {
            this.shape.ScaleX(0.5f);
        }
    }
}
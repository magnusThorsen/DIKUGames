using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout {

    public class Player : IGameEventProcessor {

        private Entity entity;
        private DynamicShape shape;
        private float moveLeft; 
        private float moveRight;
        private const float MOVEMENT_SPEED = 0.02f;

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            moveLeft = 0.0f;
            moveRight = 0.0f; 
        }

        /// <summary>
        /// renders the player
        /// </summary>
        public void Render() {
            entity.RenderEntity();
        }
        
        /// <summary>
        /// checks if the player can move.
        /// </summary>
        public void Move() {
            if (shape.Position.X + shape.Direction.X > 0.0f && shape.Position.X + 
            shape.Direction.X < 0.84499f) {
                shape.Move();
            } 
        }

        /// <summary>
        /// sets the left movement bool
        /// </summary>
        /// <param name="val">if the move right field should change or not</param>
        public void SetMoveLeft(bool val) {
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
        public void SetMoveRight(bool val) {
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
            shape.Direction.X = moveRight + moveLeft;
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
            if (gameEvent.EventType == GameEventType.PlayerEvent) { //Checks if it a InputEvent
                switch (gameEvent.Message) { //switches on message, only does something with 
                                             //KeyPress and KeyRelease
                    case "KeyPress":
                        KeyPress((KeyboardKey)gameEvent.IntArg1);
                        break;
                    case "KeyRelease":
                        KeyRelease((KeyboardKey)gameEvent.IntArg1);
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
    }
}
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;

namespace Breakout {
    
    public class PowerUpBlock : Block {

        private int health; 
        private int value;
        private PowerUpDrop powerUpDrop;

        public PowerUpBlock(Shape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            health = 100;
            value = 1;
        }

        /// <summary>
        /// Returns the health of the block.
        /// </summary>
        /// <returns> int health field</returns>
        public override int GetHealth() {
            return health;
        }

        /// <summary>
        /// Does nothing, because unbreakable blocks cannot loose health.
        /// </summary>
        public override void DecHealth () {
            health--;
            if (health <= 0){
                DeleteEntity();
                powerUpDrop = new PowerUpDrop( // powerUpDrop is instantiated with positions and image
                    new DynamicShape(shape.Position, new Vec2F(0.02f, 0.02f)),
                    new Image(Path.Combine("Assets", "Images", "RocketPickUp.png")));
                BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.GraphicsEvent, IntArg1 = 5, 
                    });
            }
        }

        /// <summary>
        /// Returns the value field.
        /// </summary>
        /// <returns></returns>
        public override int GetValue() {
            return value;
        }

        /// <summary>
        /// sets the value field to amount.
        /// </summary>
        /// <param name="amount">what to set value field to</param>
        public override void SetValue(int amount) {
            value = amount;
        }

        /// <summary>
        /// gets the position of the shape of the Block
        /// </summary>
        /// <returns></returns>
        public override Vec2F GetPosition() {
            return shape.Position;
        }

        /// <summary>
        /// processes a gameEvent
        /// </summary>
        /// <param name="gameEvent">the gameEvent to process</param>
        public override void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.InputEvent && gameEvent.IntArg1 == value) { //Checks if it a InputEvent
                DecHealth();
            } 
        }
    }
}
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;

namespace Breakout {

    /// <summary>
    ///  This class represents PowerUpBlocks Blocks. Only class where IsPowerUp returns true.
    /// </summary>  
    public class PowerUpBlock : Block {

        private int health; 
        private int value;
        public PowerUpBlock(Shape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            health = 1;
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

        public override bool IsPowerUp() {
            return true;
        }

        /// <summary>
        /// processes a gameEvent
        /// </summary>
        /// <param name="gameEvent">the gameEvent to process</param>
        public override void ProcessEvent(GameEvent gameEvent) { //Checks if it a InputEvent
            if (gameEvent.EventType == GameEventType.InputEvent && gameEvent.IntArg1 == value) { 
                DecHealth();
            } 
        }
    }
}
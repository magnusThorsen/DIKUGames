using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;

namespace Breakout {

    /// <summary>
    ///  This class represents normal Blocks.
    /// </summary>  
    public class NormalBlock : Block {
        private int health; 
        private int value;

        public NormalBlock(Shape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            health = 1;
            value = 1;
        }


        /// <summary>
        /// Returs the health of the Block.
        /// </summary>
        /// <returns>the health of the block in int</returns>
        public override int GetHealth() {
            return health;
        }

        /// <summary>
        /// Decreases the health field.
        /// </summary>
        public override void DecHealth(){
            health--;
            if (health <= 0){
                DeleteEntity();
                BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.GraphicsEvent, IntArg1 = 5, 
                    });
            }
        }

        /// <summary>
        /// Returns the value field. Only there not to get warning for now.
        /// </summary>
        /// <returns></returns>
        public override int GetValue(){
            return value;
        }

        /// <summary>
        /// Sets the vakue to amount.
        /// </summary>
        /// <param name="amount">what to set value to</param>
        public override void SetValue(int amount) {
            value = amount;
        }

        /// <summary>
        /// Returns the position of the shape of the block.
        /// </summary>
        /// <returns></returns>
        public override Vec2F GetPosition() {
            return shape.Position;
        }

        public override bool IsPowerUp() {
            return false;
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
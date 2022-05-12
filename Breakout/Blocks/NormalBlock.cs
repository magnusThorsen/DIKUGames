using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;

namespace Breakout {

    public class NormalBlock : Block, IGameEventProcessor {
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
            if (health == 0){
                DeleteEntity();
            }
        }

        /// <summary>
        /// Returns the value field. Only there not to get warning for now.
        /// </summary>
        /// <returns></returns>
        public override int GetValue(){
            return value;
        }

        public override void SetValue(int amount) {
            value = amount;
        }

        public override Vec2F GetPosition() {
            return shape.Position;
        }


        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.InputEvent && gameEvent.IntArg1 == value) { //Checks if it a InputEvent
                        DecHealth();
            } 
        }
    }
}
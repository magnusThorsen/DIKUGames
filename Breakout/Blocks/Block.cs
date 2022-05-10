using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

namespace Breakout {

    public class Block : Entity, IBlock, IGameEventProcessor {
        public Shape shape {get;}
        private int health; 
        private int value;

        public Block(Shape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            health = 1;
            value = 1;
        }


        /// <summary>
        /// Returs the health of the Block.
        /// </summary>
        /// <returns>the health of the block in int</returns>
        public int GetHealth() {
            return health;
        }

        /// <summary>
        /// Decreases the health field.
        /// </summary>
        public void DecHealth(){
            health--;
            if (health == 0){
                DeleteEntity();
            }
        }

        /// <summary>
        /// Returns the value field. Only there not to get warning for now.
        /// </summary>
        /// <returns></returns>
        public int GetValue(){
            return value;
        }

        public void SetValue(int amount) {
            value = amount;
        }


        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.InputEvent && gameEvent.IntArg1 == value) { //Checks if it a InputEvent
                        DecHealth();
            } 
        }
    }
}
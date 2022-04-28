using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

namespace Breakout {

    public class Block : Entity, IGameEventProcessor {
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
        private void DecHealth(){
            health--;
            if (health == 0){
                DeleteEntity();
            }
        }

        /// <summary>
        /// Returns the value field. Only there not to get warning for now.
        /// </summary>
        /// <returns></returns>
        private int GetValue(){
            return value;
        }

        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.InputEvent) { //Checks if it a InputEvent
                switch (gameEvent.Message) { //switches on message, only does something with 
                                             //KeyPress and KeyRelease
                    case "Hit":
                        DecHealth();
                        break;
                    default: break;
                }
            } 
        }





    }
}
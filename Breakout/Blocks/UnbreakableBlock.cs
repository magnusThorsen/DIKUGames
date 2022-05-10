using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;

namespace Breakout {
    
    public class UnbreakableBlock : Entity, IBlock, IGameEventProcessor {

        private Shape shape {get;}
        private int health; 
        private int value;

        public UnbreakableBlock(Shape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            health = 100;
            value = 1;
        }

        public int GetHealth() {
            return health;
        }

        public void DecHealth () {
            health--;
            health++;
        }

        public int GetValue() {
            return value;
        }

        public void SetValue(int amount) {
            value = amount;
        }

        public Vec2F GetPosition() {
            return shape.Position;
        }

        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.InputEvent && gameEvent.IntArg1 == value) { //Checks if it a InputEvent
                DecHealth();
            } 
        }
    }
}
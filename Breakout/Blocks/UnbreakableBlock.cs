using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;

namespace Breakout {
    
    public class UnbreakableBlock : Block {

        private int health; 
        private int value;

        public UnbreakableBlock(Shape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            health = 100;
            value = 1;
        }

        public override int GetHealth() {
            return health;
        }

        public override void DecHealth () {
        }

        public override int GetValue() {
            return value;
        }

        public override void SetValue(int amount) {
            value = amount;
        }

        public override Vec2F GetPosition() {
            return shape.Position;
        }

        public override void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.InputEvent && gameEvent.IntArg1 == value) { //Checks if it a InputEvent
                DecHealth();
            } 
        }
    }
}
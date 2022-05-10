using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;

namespace Breakout {
    
    public class HardenedBlock : Entity, IBlock, IGameEventProcessor {

        private Shape shape {get;}
        private int startHealth;
        private int health; 
        private int value;
        private string Color;

        public HardenedBlock(Shape shape, IBaseImage image, string color) : base(shape, image) {
            Color = color;
            this.shape = shape;
            health = 2;
            startHealth = 2;
            value = 1;
        }

        public int GetHealth() {
            return health;
        }

        public void DecHealth () {
            health--;
            if (health < startHealth/2) {
                ChangeImage();
            }
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

        public void ChangeImage() {
            Color = Color + "-damaged.png";
            Image = new Image(Path.Combine("Assets","Images", Color));
        }

        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.InputEvent && gameEvent.IntArg1 == value) { //Checks if it a InputEvent
                DecHealth();
            } 
        }
    }
}
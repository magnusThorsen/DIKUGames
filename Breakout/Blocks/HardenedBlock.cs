using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;

namespace Breakout {
    
    public class HardenedBlock : Block {

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

        public override int GetHealth() {
            return health;
        }

        public override void DecHealth () {
            health--;
            if (health < startHealth/2) {
                ChangeImage();
            }
            if (health == 0){
                DeleteEntity();
            }
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

        public void ChangeImage() {
            Color = Color + "-damaged.png";
            Image = new Image(Path.Combine("Assets","Images", Color));
        }

        public override void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.InputEvent && gameEvent.IntArg1 == value) { //Checks if it a InputEvent
                DecHealth();
            } 
        }
    }
}
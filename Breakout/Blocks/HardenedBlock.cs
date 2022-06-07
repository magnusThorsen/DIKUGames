using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;

namespace Breakout {
    /// <summary>
    ///  This class represents Hardened Blocks. Has two lives and changes picture.
    /// </summary>  
    public class HardenedBlock : Block {
        private int health; 
        private int value;
        private string Color;

        public HardenedBlock(Shape shape, IBaseImage image, string color) : base(shape, image) {
            Color = color;
            this.shape = shape;
            health = 2;
            value = 1;
        }

        /// <summary>
        /// Returns the health field of the block
        /// </summary>
        /// <returns>health</returns>
        public override int GetHealth() {
            return health;
        }

        /// <summary>
        /// Decreases the healthfield by one, and changes the image if needed, and 
        /// crates a GameEvent if needed
        /// </summary>
        public override void DecHealth () {
            health--;
            if (health == 1) {
                ChangeImage();
            }
            if (health == 0){
                DeleteEntity();
                BreakoutBus.GetBus().RegisterEvent (new GameEvent {
                        EventType = GameEventType.GraphicsEvent, IntArg1 = 10, 
                    });
            }
            
        }


        /// <summary>
        /// Returns the block value
        /// </summary>
        /// <returns></returns>
        public override int GetValue() {
            return value;
        }

        /// <summary>
        /// Sets the valu to the amount
        /// </summary>
        /// <param name="amount">what to set value to</param>
        public override void SetValue(int amount) {
            value = amount;
        }


        /// <summary>
        /// Returns the positon of the shape of the block
        /// </summary>
        /// <returns></returns>
        public override Vec2F GetPosition() {
            return shape.Position;
        }


        /// <summary>
        /// changes the image to the damaged version
        /// </summary>
        public void ChangeImage() {
            Color = Color + "-block-damaged.png";
            Image = new Image(Path.Combine("Assets","Images", Color));
        }

        
        public override bool IsPowerUp() {
            return false;
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
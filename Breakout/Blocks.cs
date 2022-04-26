using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Breakout {

    public class Blocks : Entity {
        public Shape shape {get;}
        private int health; 
        private int value;

        public Blocks(Shape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            health = 1;
            value = 1;
        }

        public int Health() {
            return health;
        }

        public void DecHealth(){
            health--;
            if (health == 0){
                DeleteEntity();
            }
        } 
    }
}
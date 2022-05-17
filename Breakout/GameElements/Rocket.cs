using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;

namespace Breakout {

    public class Rocket : Entity, IGameEventProcessor {
        //We make sure we can get the shape of the enemy whenever we want by using get;.
        private static Vec2F extend; 
        private static Vec2F Dir;
        public DynamicShape shape {get;}
        private Entity entity;
        private List<Image> explosionStride;
        private bool moving; 

        //We create a constructor for Enemy, so we can make instances when we want.
        public Rocket(DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            this.shape = shape;
            entity = new Entity(shape, image);
            extend = new Vec2F(0.01f, 0.025f);
            Dir = new Vec2F(0.0f, 0.03f);
            explosionStride = ImageStride.CreateStrides(4, Path.Combine("Assets",
            "Images", "Explosion.png"));
            moving = false;
        }
        
        public void Explode() {
            Image = new ImageStride(20, explosionStride);
            Shape.Scale(20.0f);
        }

        public void Move() {
            if (moving){
                shape.Move();
            }
            if (!moving){
            }
        }

        /// <summary>
        /// processes a gameEvent
        /// </summary>
        /// <param name="gameEvent">the gameEvent to process</param>
        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.InputEvent) { 
                switch (gameEvent.Message) {  
                    case "LAUNCH ROCKET":
                        moving = true;
                        break;
                    default:
                        break;
                }
            } 
        }
    }
}
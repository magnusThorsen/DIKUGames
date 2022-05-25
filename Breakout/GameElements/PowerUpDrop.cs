using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;

namespace Breakout {

    public class PowerUpDrop : Entity, IGameEventProcessor {
        private float Yvelocity;
        private static Vec2F extend; 
        public DynamicShape shape {get;}
        private Entity entity;

        public PowerUpDrop(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            entity = new Entity(shape, image);
            Yvelocity = 0.1f;
            shape.Direction = new Vec2F(0.0f,0.01f);
            extend = new Vec2F(0.04f, 0.04f);
        }

        public void Render() {
            this.RenderEntity();
        }

        public void Move() {
            shape.Position += new Vec2F(shape.Direction.X, shape.Direction.Y) * new Vec2F(0.0f, Yvelocity);
            shape.Move();
        }

        public void Consume(Player player) {
            if (CollisionDetection.Aabb(shape, player.Shape).Collision) {
                this.DeleteEntity();
            }
        }

        /// <summary>
        /// processes a gameEvent
        /// </summary>
        /// <param name="gameEvent">the gameEvent to process</param>
        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.InputEvent) { 
                switch (gameEvent.Message) {  
                    default:
                        break;
                }
            } 
        }
    }
}
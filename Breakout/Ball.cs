using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Physics;

namespace Breakout {

    public class Ball : Entity {
        private Shape shape;
        private float Xvelocity;
        private float Yvelocity;

        public Ball(Shape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            Shape = shape;
            Xvelocity = 0.05f;
            Yvelocity = 0.05f;
        }

        /// <summary>
        /// renders the ball
        /// </summary>
        public void Render() {
            this.RenderEntity();
        }

        public void Move(Player p, EntityContainer<Block> b) {
            if (shape.Position.X > 1.0f || shape.Position.X < 0.0f) {
                Xvelocity = -Xvelocity;
            }
            if (shape.Position.Y > 1.0f) {
                Yvelocity = -Yvelocity;
            }

            Bounce(p, b);

            shape.Move();

        }

        private void Bounce(Player p, EntityContainer<Block> b){
            BounceBlock(b);
            BouncePlayer(p);
        }


        public void BounceBlock(EntityContainer<Block> b) {          
            foreach(Block block in b){
                if (DIKUArcade.Physics.CollisionDetection.Aabb(
                this.shape.AsDynamicShape(), block.shape).Collision) {
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.InputEvent, 
                            IntArg1 = block.GetValue()
                        }
                    );
                }    

                switch ((DIKUArcade.Physics.CollisionDetection.Aabb(
                this.shape.AsDynamicShape(), block.shape).CollisionDir)) {
                    case CollisionDirection.CollisionDirUp:
                        Yvelocity = -Yvelocity;
                        break;
                    case CollisionDirection.CollisionDirDown:
                        Yvelocity = -Yvelocity;
                        break;
                    case CollisionDirection.CollisionDirLeft:
                        Xvelocity = -Xvelocity;
                        break;
                    case CollisionDirection.CollisionDirRight:
                        Xvelocity = -Xvelocity;
                        break;
                    case CollisionDirection.CollisionDirUnchecked:
                        break;
                }
            }
        }

        public void BouncePlayer(Player p) {          
            if (DIKUArcade.Physics.CollisionDetection.Aabb(
            this.shape.AsDynamicShape(), p.shape).Collision) {
                if (shape.Position == p.GetPosition()) {
                    Yvelocity = -Yvelocity;
                }
                else if (shape.Position.X == p.GetPosition().X + (0.5)) {
                    Xvelocity = Xvelocity + 0.2f;
                    Yvelocity = Yvelocity - 0.2f;
                }
                else if (shape.Position.X == p.GetPosition().X - (0.5)) {
                    Xvelocity = Xvelocity - 0.2f;
                    Yvelocity = Yvelocity - 0.2f;
                }
            }
        }



        
    }
}
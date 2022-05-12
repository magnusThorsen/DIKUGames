using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;

namespace Breakout {

    public class Ball : Entity, IGameEventProcessor {
        private float Xvelocity;
        private float Yvelocity;
        private static Vec2F extend; 
        public DynamicShape shape {get;}
        private bool moving;
        private Entity entity;

        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            entity = new Entity(shape, image);
            Xvelocity = 0.01f;
            Yvelocity = 0.01f;
            moving = false;
            shape.Direction = new Vec2F(0.0f,0.01f);
            extend = new Vec2F(0.04f, 0.04f);
        }

        /// <summary>
        /// renders the ball
        /// </summary>
        public void Render() {
            this.RenderEntity();
        }

        public void Move(Player p, EntityContainer<Block> b) {
            if (moving){
                shape.Position += new Vec2F(shape.Direction.X, shape.Direction.Y) * new Vec2F(Xvelocity, Yvelocity);
            }
            if (!moving){
                shape.Position = new Vec2F(p.shape.Position.X+0.055f, p.shape.Position.Y+0.01f);
            }

            Bounce(p, b);

            shape.Move();

        }

        private void Bounce(Player p, EntityContainer<Block> b){
            BounceBlock(b);
            BouncePlayer(p);
            BounceWall();
        }


        public void BounceBlock(EntityContainer<Block> b) {      
            foreach(Block block in b){
                if (CollisionDetection.Aabb(shape, block.Shape).Collision) {
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent{
                                EventType = GameEventType.InputEvent, 
                                IntArg1 = block.GetValue()
                            }
                        );
                  
                    switch ((CollisionDetection.Aabb(this.shape.AsDynamicShape(), 
                            block.Shape).CollisionDir)) {
                        case CollisionDirection.CollisionDirUp:
                            shape.Direction.Y = -shape.Direction.Y;
                            break;
                        case CollisionDirection.CollisionDirDown:
                            shape.Direction.Y = -shape.Direction.Y;
                            break;
                        case CollisionDirection.CollisionDirLeft:
                            shape.Direction.X = -shape.Direction.X;
                            break;
                        case CollisionDirection.CollisionDirRight:
                            shape.Direction.X = -shape.Direction.X;
                            break;
                        case CollisionDirection.CollisionDirUnchecked:
                            break;
                    }
                }
            }
        }


        public void BouncePlayer(Player p) {          
            if (DIKUArcade.Physics.CollisionDetection.Aabb(
                this.shape, p.shape).Collision) {

                if (shape.Position.X > p.GetPosition().X + (0.1)) {
                    shape.Direction.X = 0.01f;
                    shape.Direction.Y = 0.01f;
                }
                else if (shape.Position.X < p.GetPosition().X - (0.002)) {
                    shape.Direction.X = -0.01f;
                    shape.Direction.Y = 0.01f;

                }
                else {
                    shape.Direction.Y = -shape.Direction.Y;
                }
            }
        }


        private void BounceWall(){
            if (shape.Position.Y < -0.05f){
                DeleteEntity();
                moving = false;
                BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.StatusEvent, 
                            Message = "BallUnderPlayer",
                        }
                    );
            }
            if (shape.Position.X > 0.95f || shape.Position.X < 0.0f) {
                shape.Direction.X = -shape.Direction.X;
            }
            if (shape.Position.Y > 0.95f) {
                shape.Direction.Y = -shape.Direction.Y;
            }
        }



        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.InputEvent) { 
                switch (gameEvent.Message) {  
                    case "LAUNCH_BALL":
                        moving = true;
                        break;
                    default:
                        break;
                }
            } 
        }


        public void Reset(){
            Xvelocity = 0.0f;
            Yvelocity = 0.01f;
            moving = false;
            shape.Direction.X = 0.0f;
            shape.Direction.Y = 0.01f;
            shape.Position = new Vec2F(0.3f, 0.03f);
        }



        
    }
}
using System;
using System.IO;
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
        public int randNumber;
        private Random rand; 
        private PowerUps pwUp;

        public PowerUpDrop(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            entity = new Entity(shape, image);
            Yvelocity = 0.1f;
            shape.Direction = new Vec2F(0.0f,-0.01f);
            extend = new Vec2F(0.1f, 0.1f);
            rand = new Random(); 
            randNumber = rand.Next(5);
            pwUp = new PowerUps();
            this.PowerPicture(randNumber);
        }

        public void Render() {
            this.RenderEntity();
        }

        public void Move() {
            shape.Position += new Vec2F(shape.Direction.X, shape.Direction.Y) * new Vec2F(0.0f, Yvelocity);
            shape.Move();
        }

        public void PowerPicture(int number) {
            switch (number) {
                case 0: 
                    entity.Image = new Image(Path.Combine("Assets", "Images", "LifePickUp.png"));
                    break;
                case 1: 
                    entity.Image = new Image(Path.Combine("Assets", "Images", "DoubleSpeedPowerUp.png"));
                    break;
                case 2: 
                    entity.Image = new Image(Path.Combine("Assets", "Images", "SpeedPickUp.png"));
                    break;
                case 3: 
                    entity.Image = new Image(Path.Combine("Assets", "Images", "hourglass.png"));
                    break;
                case 4: 
                    entity.Image = new Image(Path.Combine("Assets", "Images", "WidePowerUp.png"));
                    break;
                default: break;
            }
        }

        public void Consume(Player player, int number) {
            if (CollisionDetection.Aabb(shape, player.Shape).Collision) {
                switch (number) {
                    case 0: 
                        pwUp.LifePowerUp();
                        break;
                    case 1: 
                        pwUp.DoubleSpeedPowerUp();
                        break;
                    case 2: 
                        pwUp.PlayerSpeedPowerUp();
                        break;
                    case 3: 
                        pwUp.MoreTimePowerUp();
                        break;
                    case 4: 
                        pwUp.WidePowerUp();
                        break;
                default: break;
            }
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
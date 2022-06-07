using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

namespace Breakout {
    /// <summary>
    ///  This class represents the powerupdrops, which are the powerups that go down the screen
    /// when a powerupblock is hit. What the powerup does is randomly decided.
    /// </summary>  
    public class PowerUpDrop : Entity {
        private float Yvelocity;
        private static Vec2F extend; 
        public DynamicShape shape {get;}
        private Entity entity;
        private int randNumber;
        private Random rand; 
        public int powerUpNumber;
        private PowerUps pwUp;

        public PowerUpDrop(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            entity = new Entity(shape, image);
            Yvelocity = 0.1f;
            shape.Direction = new Vec2F(0.0f,-0.01f);
            extend = new Vec2F(0.1f, 0.1f);
            rand = new Random(); 
            randNumber = rand.Next(5);
            powerUpNumber = randNumber;
            pwUp = new PowerUps();
            switch (powerUpNumber) {
                case 0: 
                    Image = new Image(Path.Combine("Assets", "Images", "LifePickUp.png"));
                    break;
                case 1: 
                    Image = new Image(Path.Combine("Assets", "Images", "DoubleSpeedPowerUp.png"));
                    break;
                case 2: 
                    Image = new Image(Path.Combine("Assets", "Images", "SpeedPickUp.png"));
                    break;
                case 3: 
                    Image = new Image(Path.Combine("Assets", "Images", "hourglass.png"));
                    break;
                case 4: 
                    Image = new Image(Path.Combine("Assets", "Images", "WidePowerUp.png"));
                    break;
                default: break;
                };
        }
        

        /// <summary>
        /// renders the powerupdrop.
        /// </summary>
        public void Render() {
            this.RenderEntity();
        }

        /// <summary>
        /// Moves the powerupdrop down.
        /// </summary>
        public void Move() {
            shape.Position += new Vec2F(shape.Direction.X, shape.Direction.Y) * new Vec2F(0.0f, Yvelocity);
            shape.Move();
        }


        /// <summary>
        /// If there is collision bewteen player and the powerupdrop the appropriate powerup is
        /// activated by calling Powerups functions that in turn creates an event.
        /// </summary>
        /// <param name="player">The player to check collision with</param>
        /// <param name="number">the number of the powerup, deciding what the powerup is</param>
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
    }
}
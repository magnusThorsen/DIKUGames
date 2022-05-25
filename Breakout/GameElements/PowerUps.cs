using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;

namespace Breakout {
    public class PowerUps {
        private Ball ball1;
        private Ball ball2;
        private DynamicShape shape;
        private IBaseImage image;


        public void LifePowerUp() {
            BreakoutBus.GetBus().RegisterEvent (
                new GameEvent {
                    EventType = GameEventType.PlayerEvent, 
                    Message = "IncLife"
            });
        }

        public void WidePowerUp() {
            BreakoutBus.GetBus().RegisterEvent (
                new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "IncWidth"
            });
        }

        public void SplitPowerUp() {
            ball1 = new Ball(shape, image);
            ball2 = new Ball(shape, image);
            ball1.shape.Position =  Ball.GetPosition();          
        }
    }
}





// SplitPowerUp

// InfinitePowerUp

// RocektPowerUp
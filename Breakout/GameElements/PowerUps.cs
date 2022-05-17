using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;

namespace Breakout {
    public class PowerUps {


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
    }
}





// WidePowerUp

// SplitPowerUp

// InfinitePowerUp

// RocektPowerUp
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

        public void PlayerSpeedPowerUp() {
            BreakoutBus.GetBus().RegisterEvent (
                new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "IncSpeed"
            });
        }

        public void DoubleSpeedPowerUp() {
            BreakoutBus.GetBus().RegisterEvent (
                new GameEvent {
                    EventType = GameEventType.InputEvent,
                    Message = "IncSpeed"
            });
        }

        public void MoreTimePowerUp() {
            BreakoutBus.GetBus().RegisterEvent (
                new GameEvent {
                    EventType = GameEventType.StatusEvent,
                    Message = "IncTime"
            });
        }
    }
}
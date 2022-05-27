using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;

namespace Breakout {
    public class PowerUps {
      

        /// <summary>
        /// Creates an event that is caught by player to increase life
        /// </summary>
        public void LifePowerUp() {
            BreakoutBus.GetBus().RegisterEvent (
                new GameEvent {
                    EventType = GameEventType.PlayerEvent, 
                    Message = "IncLife"
            });
        }

        /// <summary>
        /// Creates an event that is caught by player to increase width
        /// </summary>
        public void WidePowerUp() {
            BreakoutBus.GetBus().RegisterEvent (
                new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "IncWidth"
            });
        }

        /// <summary>
        /// Creates an event that is caught by player to increase speed
        /// </summary>
        public void PlayerSpeedPowerUp() {
            BreakoutBus.GetBus().RegisterEvent (
                new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "IncSpeed"
            });
        }

        /// <summary>
        /// Creates an event that is caught by ball to increase speed
        /// </summary>
        public void DoubleSpeedPowerUp() {
            BreakoutBus.GetBus().RegisterEvent (
                new GameEvent {
                    EventType = GameEventType.InputEvent,
                    Message = "IncSpeed"
            });
        }

        /// <summary>
        /// Creates an event that is caught by gamerunning to increase time by 10
        /// </summary>
        public void MoreTimePowerUp() {
            BreakoutBus.GetBus().RegisterEvent (
                new GameEvent {
                    EventType = GameEventType.StatusEvent,
                    Message = "IncTime"
            });
        }
    }
}
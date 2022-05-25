using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Timers;

namespace Breakout {
    
    public class Timer {

        private int seconds;
        private int current;

        public void GameLevelTimer() {
            current = StaticTimer.timer.ElapsedMilliseconds;
        }

        public void PowerUpTimer() {

        }
    }
}
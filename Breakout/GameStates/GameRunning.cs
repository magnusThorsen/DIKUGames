using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System.Collections.Generic;

namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState {

        private static GameRunning instance = null;

        /// <summary>
        /// GetInstance sets up the GameRunning
        /// </summary>
        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }

        /// <summary>
        /// Initializes all fields, and what else is needed for the methods
        /// </summary>
        public void InitializeGameState() {

        }

        public void ResetState() {

        }

        public void UpdateState() {

        }

        public void RenderState() {

        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action, key) {
                case (KeyboardAction.KeyPress, KeyboardKey.Escape):
                    KeyPress(key);
                    break;
                case (KeyboardAction.KeyPress, KeyboardKey.Space):
                    KeyPress(key);
                    break;
            }
        }

        public void KeyPress(KeyboardKey key) {
            switch (key) { //switches on message, only does something with 
                                         //KeyPress and KeyRelease
                case KeyboardKey.Escape: 
                    ResetState();
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE",
                            StringArg1 = "MAIN_MENU"
                        }
                    );
                    break;
                default:
                    break;
            }
        }  
    }

}
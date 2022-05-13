using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System.Collections.Generic;
using Breakout.BreakoutStates;


namespace Breakout { 


    public class Game : DIKUGame, IGameEventProcessor {

        private StateMachine state;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {
                 GameEventType.InputEvent, 
            GameEventType.PlayerEvent, GameEventType.GameStateEvent, GameEventType.GraphicsEvent, GameEventType.StatusEvent});
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            window.SetKeyEventHandler(KeyHandler);
            state = new StateMachine();
        }

        /// <summary>
        /// Handles KeyboardActions and KeyboardKeys
        /// </summary>
        /// <param name="action">A KeyBoardAction</param>
        /// <param name="key">A KeyBoardKey</param>
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
                default: break;
            }  
        }

        /// <summary>
        /// Renders everything
        /// </summary>
        public override void Render() {
            state.ActiveState.RenderState();
        }

        /// <summary>
        /// Updates everything
        /// </summary>
        public override void Update() {
            state.ActiveState.UpdateState();
            BreakoutBus.GetBus().ProcessEventsSequentially(); 
        }


        /// <summary>
        /// Sends out the appropriate EventType for the KeyBoardKey pressed.
        /// </summary>
        /// <param name="key">The pressed key</param>
        private void KeyPress(KeyboardKey key) { // Initiating keypresses on given keys with switch
            state.ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, key);
        }


        /// <summary>
        /// Sends out the appropriate EventType for the KeyBoardKey released.
        /// </summary>
        /// <param name="key">The released key</param>
        private void KeyRelease(KeyboardKey key) {
            state.ActiveState.HandleKeyEvent(KeyboardAction.KeyRelease, key);
        }


        /// <summary>
        /// Processes all events in the bus and responds accordingly.
        /// </summary>
        /// <param name="gameEvent">the gamEvent to process</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.InputEvent) { //Checks if it a InputEvent
                switch (gameEvent.Message) { //switches on message, only does something with 
                                             //KeyPress and KeyRelease
                    case "escape": 
                        window.CloseWindow();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
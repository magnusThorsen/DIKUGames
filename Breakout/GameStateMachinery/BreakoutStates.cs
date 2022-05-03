using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;

namespace Breakout.BreakoutStates {

    public class StateMachine : IGameEventProcessor {

        public IGameState ActiveState { get; private set; }
        
        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
            GameRunning.GetInstance();
            //GamePaused.GetInstance();
        } 

        /// <summary>
        /// switches the state
        /// </summary>
        /// <param name="stateType">the state to switch to</param>
        public void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning: 
                    ActiveState.ResetState();
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused: 
                    break;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    ActiveState.ResetState();
                    break;
                default: break;
            }
        }
        
        /// <summary>
        /// Processes an event
        /// </summary>
        /// <param name="gameEvent">the event to process</param>
        public void ProcessEvent (GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) { 
                    case "CHANGE_STATE":
                        SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                        break;
                    default:
                        break;
                }
            }
            if (gameEvent.EventType == GameEventType.InputEvent) {
                switch (gameEvent.Message) { 
                    case "KeyPress":
                        ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, 
                        (KeyboardKey)gameEvent.IntArg1);
                        break; 
                }
            }
        }
    }
}
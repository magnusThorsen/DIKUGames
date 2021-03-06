using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;
using System.Collections.Generic;

namespace Breakout.BreakoutStates {
    /// <summary>
    ///  This class handles swithcing bewteen states and containing the active state.
    /// </summary>  

    public class StateMachine : IGameEventProcessor {

        public IGameState ActiveState { get; private set; }
        private IDictionary<GameStateType, IGameState> stateDic;
        private List<IGameState> gameStates {get;}
        private List<GameStateType> stateTypes{get;}
        
        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
            GameRunning.GetInstance();
            GamePaused.GetInstance();
            GameLost.GetInstance();
            GameWon.GetInstance();

            //Initializing the lists and Adding the states and IGameStates to lists
            gameStates = new List<IGameState>();
            stateTypes = new List<GameStateType>();
            stateDic = new Dictionary<GameStateType, IGameState>();
            gameStates.Add(MainMenu.GetInstance());
            gameStates.Add(GameRunning.GetInstance());
            gameStates.Add(GamePaused.GetInstance());
            gameStates.Add(GameLost.GetInstance());
            gameStates.Add(GameWon.GetInstance());
            stateTypes.Add(GameStateType.MainMenu);
            stateTypes.Add(GameStateType.GameRunning);
            stateTypes.Add(GameStateType.GamePaused);
            stateTypes.Add(GameStateType.GameLost);
            stateTypes.Add(GameStateType.GameWon);

            //Initializing and Filling the dictionary
            stateDic = new Dictionary<GameStateType, IGameState>();
            FillStateTypeToBreakoutStatesDic();
        } 

        /// <summary>
        /// switches the state
        /// </summary>
        /// <param name="stateType">the state to switch to</param>
        public void SwitchState(GameStateType stateType) {
            foreach(var elm in stateDic){
                if (elm.Key == stateType){
                    ActiveState = elm.Value;
                }
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
        }

        /// <summary>
        /// Fils the dictionary
        /// </summary>
        private void FillStateTypeToBreakoutStatesDic(){
            int i = 0;
            foreach (IGameState state in gameStates){
                stateDic.Add(stateTypes[i], state);
                i++;
            }
        }
    }
}

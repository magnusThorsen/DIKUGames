using System;

public class StateTransformer {

    /// <summary>
    /// Transform a string to a state
    /// </summary>
    /// <param name="state">The string to transform</param>
    /// <returns></returns>
    public static GameStateType TransformStringToState(string state) {
        if (state == "GAME_RUNNING") {
            return GameStateType.GameRunning;
        }
        else if (state == "GAME_PAUSED") {
            return GameStateType.GamePaused;
        }
        else if (state == "MAIN_MENU") {
            return GameStateType.MainMenu;
        }
        else {
            throw new ArgumentException("Invalid input");
        }
    }
    
    /// <summary>
    /// Transforms a state to a string
    /// </summary>
    /// <param name="state">the state to transform</param>
    /// <returns></returns>
    public static string TransformStateToString (GameStateType state) {
        if (state == GameStateType.GameRunning) {
            return "GAME_RUNNING";
        }
        else if (state == GameStateType.GamePaused) {
            return "GAME_PAUSED";
        }
        else if (state == GameStateType.MainMenu) {
            return "MAIN_MENU";
        }
        else {
            throw new ArgumentException("Invalid state");
        }
    }
}
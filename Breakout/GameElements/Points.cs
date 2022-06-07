using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;

public class Points : IGameEventProcessor {

    private int points;
    private Text display;

    public Points (Vec2F position, Vec2F extent) {
        points = 0;
        display = new Text ("Points: " + points.ToString(), position, extent);
        display.SetColor(new Vec3I(255, 0, 0));
    }

        /// <summary> 
        /// increments the point field.
        /// </summary>
        public void AddPoints(int s) {
            points = points + s;
        }

        /// <summary>
        /// renders the score on the board.
        /// </summary>
        public void RenderPoints () {
            display.SetText("Points: " + points.ToString());
            display.RenderText();
        }
        
        /// <summary>
        /// Resets score to 0
        /// </summary>
        public void ResetPoints() {
            points = 0;
        } 

        /// <summary>
        /// Returns the score field
        /// </summary>
        /// <returns>the score field</returns>
        public int GetPoints() {
            return points;
        }

        /// <summary>
        /// processes a gameEvent
        /// </summary>
        /// <param name="gameEvent">the gameEvent to process</param>
        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.GraphicsEvent) { 
                AddPoints(gameEvent.IntArg1);
            } 
        }

}
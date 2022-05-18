using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using System.IO;

namespace Breakout.BreakoutStates {

    public class GameLost : IGameState {

        private static GameLost instance = null;
        private Text[] menuButtons;
        private Text NewGameText;
        private Text QuitText;
        private Text lostGameText;
        private string SelectedButton;
        private Entity BackgroundImage;
        private StationaryShape shape;
        private IBaseImage image;
        
        public static GameLost GetInstance() {
            if (GameLost.instance == null) {
                GameLost.instance = new GameLost();
                GameLost.instance.InitializeGameState();
            }
            return GameLost.instance;
        }

        /// <summary>
        /// Initializes the gamestate
        /// </summary>
        public void InitializeGameState() {
            lostGameText = new Text("YOU LOST LLLL", new Vec2F(0.05f, 0.2f), new Vec2F(0.5f, 0.5f));
            lostGameText.SetColor(new Vec3I(0,255,0));
            NewGameText = new Text("Main Menu", new Vec2F(0.05f, 0.05f), new Vec2F(0.5f, 0.5f));
            NewGameText.SetColor(new Vec3I(255,255,255));
            QuitText = new Text("Quit Game", new Vec2F(0.05f, -0.1f), new Vec2F(0.5f,  0.5f));
            QuitText.SetColor(new Vec3I(255, 0, 0));
            menuButtons = new Text[] {NewGameText, QuitText};
            SelectedButton = "Main Menu";
            shape = new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
            image = new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png"));
            BackgroundImage = new Entity(shape, image);
        }

        /// <summary>
        /// Resets the state
        /// </summary>
        public void ResetState() {
        }

        /// <summary>
        /// Updates the state
        /// </summary>
        public void UpdateState() {
        }

        /// <summary>
        /// Renders the state
        /// </summary>
        public void RenderState() {
            BackgroundImage.RenderEntity();
            foreach (Text text in menuButtons) {
                text.RenderText();
            }
            lostGameText.RenderText();
        }

        /// <summary>
        /// handles key events
        /// </summary>
        /// <param name="action">the actio nto handle</param>
        /// <param name="key">the key pressed or released</param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case (KeyboardAction.KeyPress):
                    KeyPress(key);
                    break;
                default:
                    break;
            }
        }
        
        /// <summary>
        /// calls SetMoveRight or SetMoveLeft accordinly to the pressed key
        /// </summary>
        /// <param name="key">pressed key</param>
        private void KeyPress(KeyboardKey key) { // Initiating keypresses on given keys with switch
            switch (key) {
                case KeyboardKey.Up:
                    SelectedButton = "Main Menu";
                    NewGameText.SetColor(new Vec3I(255,255,255));
                    QuitText.SetColor(new Vec3I(255, 0, 0));
                    break;
                case KeyboardKey.Down: 
                    SelectedButton = "Quit Game";
                    NewGameText.SetColor(new Vec3I(255, 0, 0));
                    QuitText.SetColor(new Vec3I(255, 255, 255)); // Starting movement on press
                    break; 
                case KeyboardKey.Enter: 
                    if (SelectedButton == "Main Menu") {
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent{
                                EventType = GameEventType.GameStateEvent,
                                Message = "CHANGE_STATE",
                                StringArg1 = "MAIN_MENU"
                            }
                        );
                    }
                    else if (SelectedButton == "Quit Game") {
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent{
                                EventType = GameEventType.InputEvent, 
                                Message = "escape"
                            }
                        );
                    }
                    break;
                default: break;
            }
        }
    }
}

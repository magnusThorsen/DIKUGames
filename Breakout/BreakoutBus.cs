using DIKUArcade.Events;

namespace Breakout {
    public static class BreakoutBus {
        private static GameEventBus eventBus;

        /// <summary>
        /// Either crates a eventbus or returns the already created one. Only ever creates one.
        /// </summary>
        /// <returns>The GameEventBus</returns>
        public static GameEventBus GetBus() {
            return BreakoutBus.eventBus ?? (BreakoutBus.eventBus = new GameEventBus());
        }
    }
}
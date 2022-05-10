using DIKUArcade.Entities;

namespace Breakout {
    
    public interface IBlock {

        int GetHealth();
        void DecHealth ();
        int GetValue();
        void SetValue(int amount);
    }
}
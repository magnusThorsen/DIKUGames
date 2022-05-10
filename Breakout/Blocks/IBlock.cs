using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Breakout {
    
    public interface IBlock {

        int GetHealth();
        void DecHealth ();
        int GetValue();
        Vec2F GetPosition();
        void SetValue(int amount);
    }
}
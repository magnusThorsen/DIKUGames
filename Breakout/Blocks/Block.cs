using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

namespace Breakout {
    
    public abstract class Block : Entity, IGameEventProcessor {

        public Shape shape;
        public Block(Shape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
        }

        public abstract int GetHealth();
        public abstract void DecHealth ();
        public abstract int GetValue();
        public abstract Vec2F GetPosition();
        public abstract void SetValue(int amount);
        public abstract bool IsPowerUp();
        public abstract void ProcessEvent(GameEvent gameEvent);
    }
}
using Microsoft.Xna.Framework;

namespace PangBang.Particle
{
    public interface IParticleManager
    {
        void Load();
        void Unload();
        void Update(GameTime gameTime);
        void Draw();
    }
}
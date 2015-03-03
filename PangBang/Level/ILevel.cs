using Microsoft.Xna.Framework;

namespace PangBang.Level
{
    public interface ILevel
    {
        string Name { get; }
        void Load();
        void Unload();
        void Update(GameTime gameTime);
    }
}
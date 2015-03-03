using Microsoft.Xna.Framework;

namespace PangBang.Level
{
    public interface ILevelManager
    {
        void Load();
        void Unload();
        void Update(GameTime gameTime);
        void Draw();
    }
}
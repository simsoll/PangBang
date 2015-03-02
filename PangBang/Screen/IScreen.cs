using Microsoft.Xna.Framework;

namespace PangBang.Screen
{
    public interface IScreen
    {
        void Load();
        void Unload();
        void Update(GameTime gameTime);
        void Draw();
    }
}
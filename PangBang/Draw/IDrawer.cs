using Microsoft.Xna.Framework;
using PangBang.Rectangle;

namespace PangBang.Draw
{
    public interface IDrawer
    {
        void Draw(Vector2 position, Color color);
        void Draw(IRectangle destinationRectangle, Color color, float rotation);
    }
}
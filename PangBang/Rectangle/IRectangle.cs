using Microsoft.Xna.Framework;

namespace PangBang.Rectangle
{
    public interface IRectangle
    {
        bool Intersects(IRectangle value);

        float X { get; set; }
        float Y { get; set; }
        float Width { get; set; }
        float Height { get; set; }
        float Top { get; }
        float Bottom { get; }
        float Left { get; }
        float Right { get; }
        Vector2 Position { get; set; }
        Vector2 Scale { get; set; }
    }
}

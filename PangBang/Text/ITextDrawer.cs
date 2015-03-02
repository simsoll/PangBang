using Microsoft.Xna.Framework;

namespace PangBang.Text
{
    public interface ITextDrawer
    {
        void DrawText(string text, Vector2 position, int size, Color color);

        void DrawTextCentered(string text, Vector2 position, int size, Color color);
    }
}
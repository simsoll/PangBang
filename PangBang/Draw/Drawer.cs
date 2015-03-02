using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PangBang.Rectangle;

namespace PangBang.Draw
{
    public class Drawer : IDrawer
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly Texture2D _texture;
        private readonly IRectangle _sourceRetangle;

        public Drawer(SpriteBatch spriteBatch, Texture2D texture)
        {
            _spriteBatch = spriteBatch;
            _texture = texture;

            _sourceRetangle = new Rectangle.Rectangle(0, 0, _texture.Width, _texture.Height);
        }

        public void Draw(Vector2 position, Color color)
        {
            _spriteBatch.Draw(_texture, position, color);
        }

        public void Draw(IRectangle destinationRectangle, Color color, float rotation)
        {
            var origin = new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f);

            _spriteBatch.Draw(_texture, destinationRectangle.Position + destinationRectangle.Scale * origin,
                new Microsoft.Xna.Framework.Rectangle((int) _sourceRetangle.X, (int) _sourceRetangle.Y,
                    (int)_sourceRetangle.Width, (int)_sourceRetangle.Height), color, rotation, origin, destinationRectangle.Scale,
                SpriteEffects.None, 0.0f);
        }
    }
}
using Microsoft.Xna.Framework;
using PangBang.Rectangle;

namespace PangBang.Entities
{
    public class Wall : IWall
    {
        private readonly float _height;
        private readonly float _width;
        public Vector2 Position { get; set; }
        public Color Color { get; set; }

        public IRectangle Boundings
        {
            get { return new Rectangle.Rectangle(Position.X, Position.Y, _width, _height); }
        }

        public Wall(Vector2 position, float height, float width, Color color)
        {
            _height = height;
            _width = width;
            Color = color;
            Position = position;
        }
    }
}
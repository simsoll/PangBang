using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PangBang.Rectangle;

namespace PangBang.Entities
{
    public interface ICircle
    {
        Color Color { get; }
        Vector2 Velocity { get; set; }
        float Radius { get; }
        IList<Circle.Part> Parts { get; }

        void Update(GameTime gameTime, Vector2 center);
    }
}
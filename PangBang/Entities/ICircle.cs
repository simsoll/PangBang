using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PangBang.Rectangle;

namespace PangBang.Entities
{
    public interface ICircle
    {
        Color Color { get; }
        Vector2 Velocity { get; set; }
        IList<IRectangle> Parts { get; }
    }
}
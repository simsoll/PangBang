using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PangBang.Entities
{
    public interface IBall
    {
        IList<ICircle> Circles { get; }
        Vector2 Center { get; set; }
        Vector2 Velocity { get; set; }
        float Radius { get; }

        void Update(GameTime gameTime);
    }
}
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PangBang.Entities
{
    public interface IBall
    {
        IList<ICircle> Circles { get; }
        Vector2 Center { get; }
        Vector2 Velocity { get; }
        float Radius { get; }

        void Update(GameTime gameTime);
    }
}
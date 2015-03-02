using Microsoft.Xna.Framework;
using PangBang.Rectangle;

namespace PangBang.Particle
{
    public interface IParticle
    {
        IRectangle Boundings { get; }
        float Rotation { get; set; }
        Color Color { get; set; }
    }
}
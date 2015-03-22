using Microsoft.Xna.Framework;

namespace PangBang.Configuration
{
    public interface ILevelConfiguration
    {
        float WallThickness { get; }
        Color WallColor { get; }

        float BallLineThickness { get; }
        float BallSpeed { get; }
        Color BallColor { get; }
    }
}
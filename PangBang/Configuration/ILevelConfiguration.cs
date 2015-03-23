using Microsoft.Xna.Framework;

namespace PangBang.Configuration
{
    public interface ILevelConfiguration
    {
        Vector2 Gravity { get; }

        float WallThickness { get; }
        Color WallColor { get; }

        float BallLineThickness { get; }
        float BallRadius { get; }
        float BallRotationSpeed { get; }
        Color BallColor { get; }
    }
}
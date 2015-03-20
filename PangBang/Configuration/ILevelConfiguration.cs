using Microsoft.Xna.Framework;

namespace PangBang.Configuration
{
    public interface ILevelConfiguration
    {
        float WallThickness { get; }
        Color WallColor { get; }

        float CircleLineThickness { get; }
        float CircleSpeed { get; }
        Color CircleColor { get; }
    }
}
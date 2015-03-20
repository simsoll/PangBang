using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PangBang.Configuration
{
    public class LevelConfiguration : ILevelConfiguration
    {
        public LevelConfiguration(float wallThickness, Color wallColor, float circleLineThickness, float circleSpeed, Color circleColor)
        {
            CircleColor = circleColor;
            WallColor = wallColor;
            WallThickness = wallThickness;
            CircleLineThickness = circleLineThickness;
            CircleSpeed = circleSpeed;
        }

        public float WallThickness { get; private set; }
        public Color WallColor { get; private set; }
        public float CircleLineThickness { get; private set; }
        public float CircleSpeed { get; private set; }
        public Color CircleColor { get; private set; }
    }
}

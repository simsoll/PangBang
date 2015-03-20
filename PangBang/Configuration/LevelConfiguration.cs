using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PangBang.Configuration
{
    public class LevelConfiguration : ILevelConfiguration
    {
        public LevelConfiguration(float wallThickness, float circleLineThickness, float circleSpeed)
        {
            WallThickness = wallThickness;
            CircleLineThickness = circleLineThickness;
            CircleSpeed = circleSpeed;
        }

        public float WallThickness { get; private set; }
        public float CircleLineThickness { get; private set; }
        public float CircleSpeed { get; private set; }
    }
}

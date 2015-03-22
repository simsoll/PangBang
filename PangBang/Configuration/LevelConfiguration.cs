using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PangBang.Configuration
{
    public class LevelConfiguration : ILevelConfiguration
    {
        public LevelConfiguration(float wallThickness, Color wallColor, float ballLineThickness, float ballSpeed, Color ballColor)
        {
            BallColor = ballColor;
            WallColor = wallColor;
            WallThickness = wallThickness;
            BallLineThickness = ballLineThickness;
            BallSpeed = ballSpeed;
        }

        public float WallThickness { get; private set; }
        public Color WallColor { get; private set; }
        public float BallLineThickness { get; private set; }
        public float BallSpeed { get; private set; }
        public Color BallColor { get; private set; }
    }
}

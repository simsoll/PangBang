using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PangBang.Configuration
{
    public class LevelConfiguration : ILevelConfiguration
    {
        public LevelConfiguration(Vector2 gravity, float wallThickness, Color wallColor, float ballRadius, float ballRotationSpeed, float ballLineThickness, Color ballColor)
        {
            Gravity = gravity;
            WallColor = wallColor;
            WallThickness = wallThickness;
            BallRadius = ballRadius;
            BallRotationSpeed = ballRotationSpeed;
            BallLineThickness = ballLineThickness;
            BallColor = ballColor;
        }

        public Vector2 Gravity { get; private set; }
        public float WallThickness { get; private set; }
        public Color WallColor { get; private set; }
        public float BallRadius { get; private set; }
        public float BallRotationSpeed { get; private set; }
        public float BallLineThickness { get; private set; }
        public Color BallColor { get; private set; }
    }
}

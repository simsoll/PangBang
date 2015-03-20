using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PangBang.Rectangle;

namespace PangBang.Entities
{
    public class Circle : ICircle
    {
        public Vector2 Center { get; set; }
        public Color Color { get; private set; }
        public Vector2 Velocity { get; set; }
        public float Radius { get; private set; }
        public float Density { get; private set; }
        public float RotationVelocity { get; set; }
        public IList<IRectangle> Parts { get; private set; }

        public Circle(Vector2 center, float radius, float density, float borderThickness, Color color)
        {
            Center = center;
            Radius = radius;
            Density = density;
            Color = color;

            InitializeCircleLine(borderThickness);
        }

        private void InitializeCircleLine(float borderThickness)
        {
            var angleInDegreesPerPart = 360 / Density;

            for (var angleInDegrees = angleInDegreesPerPart;
                angleInDegrees <= 360;
                angleInDegrees += angleInDegreesPerPart)
            {
                var x = (float)Math.Round(Math.Cos(MathHelper.ToRadians(angleInDegrees)), 4);
                var y = angleInDegrees > 180 ? -1 * (float)Math.Sqrt(1 - x * x) : (float)Math.Sqrt(1 - x * x);

                var position = Center + new Vector2(x, y) * Radius;

                Parts.Add(new Rectangle.Rectangle(position.X - borderThickness/2.0f, position.Y - borderThickness/2.0f,
                    borderThickness, borderThickness));
            }
        }
    }
}

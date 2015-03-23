using System;
using System.Collections.Generic;
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
        public float RotationSpeed { get; set; }
        public IList<Part> Parts { get; private set; }

        public Circle(Vector2 center, float radius, float density, float rotationSpeed, float borderThickness, Color color)
        {
            Center = center;
            Radius = radius;
            Density = density;
            RotationSpeed = rotationSpeed;
            Color = color;
            Parts = new List<Part>();

            InitializeCircleLine(borderThickness);
        }

        private void InitializeCircleLine(float borderThickness)
        {
            var angleInDegreesPerPart = 360/Density;

            for (var angleInDegrees = angleInDegreesPerPart;
                angleInDegrees <= 360;
                angleInDegrees += angleInDegreesPerPart)
            {
                var x = (float) Math.Cos(MathHelper.ToRadians(angleInDegrees));
                var y = (float) Math.Sin(MathHelper.ToRadians(angleInDegrees));

                var position = Center + new Vector2(x, y)*Radius;

                Parts.Add(
                    new Part(
                        new Rectangle.Rectangle(position.X - borderThickness/2.0f, position.Y - borderThickness/2.0f,
                            borderThickness, borderThickness), angleInDegrees));
            }
        }

        public void Update(GameTime gameTime, Vector2 center)
        {
            Center = center;
            var deltaAngleInDegress = (float) (RotationSpeed*gameTime.ElapsedGameTime.TotalSeconds);

            foreach (var part in Parts)
            {
                part.Update(Center, Radius, deltaAngleInDegress);
            }

        }

        public class Part
        {
            public IRectangle Rectangle { get; set; }
            public float AngleInDegrees { get; private set; }

            public Part(IRectangle rectangle, float angleInDegrees)
            {
                Rectangle = rectangle;
                AngleInDegrees = angleInDegrees;
            }

            public void Update(Vector2 center, float radius, float deltaAngleInDegrees)
            {
                AngleInDegrees += deltaAngleInDegrees;

                var x = (float)Math.Cos(MathHelper.ToRadians(AngleInDegrees));
                var y = (float)Math.Sin(MathHelper.ToRadians(AngleInDegrees));

                var position = center + new Vector2(x, y) * radius;
                var centeredPosition = new Vector2(position.X - Rectangle.Width/ 2.0f, position.Y - Rectangle.Height / 2.0f);
                Rectangle.Position = centeredPosition;
            }
        }
    }
}

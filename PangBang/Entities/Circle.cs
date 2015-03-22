using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PangBang.Rectangle;

namespace PangBang.Entities
{
    public class Ball : IBall
    {
        public Ball(Vector2 center, Vector2 velocity, float radius, float boarderThickness, Color color)
        {
            Velocity = velocity;
            Center = center;
            Circles = InitializeLayers(radius, boarderThickness, color);
        }

        private IList<ICircle> InitializeLayers(float radius, float boarderThickness, Color color)
        {
            var returnResult = new List<ICircle>
            {
                new Circle(Center, radius, radius / 2.0f, boarderThickness, color),
                new Circle(Center, radius/2.0f, radius / 2.0f / 2.0f, boarderThickness, color),
                new Circle(Center, 0, 1, boarderThickness, color)
            };

            return returnResult;
        }

        public IList<ICircle> Circles { get; private set; }
        public Vector2 Center { get; private set; }
        public Vector2 Velocity { get; private set; }

        public float Radius
        {
            get { return Circles.Max(x => x.Radius); }
        }

        public void Update(GameTime gameTime)
        {
            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Center += Velocity*elapsed;

        }
    }

    public interface IBall
    {
        IList<ICircle> Circles { get; }
        Vector2 Center { get; }
        Vector2 Velocity { get; }
        float Radius { get; }

        void Update(GameTime gameTime);
    }


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
            Parts = new List<IRectangle>();

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
        
        public void Update(Vector2 center)
        {
            throw new NotImplementedException();
        }
    }
}

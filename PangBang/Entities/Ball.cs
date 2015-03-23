using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace PangBang.Entities
{
    public class Ball : IBall
    {
        public Ball(Vector2 gravity, Vector2 center, Vector2 velocity, float radius, float rotationSpeed, float boarderThickness, Color color)
        {
            _gravity = gravity;
            Velocity = velocity;
            Center = center;
            Circles = InitializeLayers(radius, rotationSpeed, boarderThickness, color);
        }

        private IList<ICircle> InitializeLayers(float radius, float rotationSpeed, float boarderThickness, Color color)
        {
            var returnResult = new List<ICircle>
            {
                new Circle(Center, radius, radius / 2.0f, rotationSpeed, boarderThickness, color),
                new Circle(Center, radius/2.0f, radius / 2.0f / 2.0f, - rotationSpeed, boarderThickness, color),
                new Circle(Center, 0, 1, 0, boarderThickness, color)
            };

            return returnResult;
        }

        private Vector2 _gravity;
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

            Velocity += _gravity*elapsed;
            Center += Velocity*elapsed;
            foreach (var circle in Circles)
            {
                circle.Update(gameTime, Center);
            }
        }
    }
}
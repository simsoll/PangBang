using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PangBang.Entities.Messages;
using PangBang.Messaging.Caliburn.Micro;

namespace PangBang.Entities
{
    public class Ball : IBall
    {
        public Ball(IEventAggregator eventAggregator, Vector2 gravity, Vector2 center, Vector2 velocity, float radius, float rotationSpeed, float boarderThickness, Color color)
        {
            _eventAggregator = eventAggregator;
            _gravity = gravity;
            Velocity = velocity;
            Center = center;
            Circles = InitializeLayers(radius, rotationSpeed, boarderThickness, color);
        }

        private IList<ICircle> InitializeLayers(float radius, float rotationSpeed, float boarderThickness, Color color)
        {
            var returnResult = new List<ICircle>
            {
                new Circle(Center, radius, radius / 2.5f, rotationSpeed, boarderThickness, color),
                new Circle(Center, radius/1.5f, radius / 4.0f, - rotationSpeed, boarderThickness, color),
                new Circle(Center, radius/3.0f, radius / 8.0f, rotationSpeed, boarderThickness, color),
                new Circle(Center, 0, 1, 0, boarderThickness, color)
            };

            return returnResult;
        }

        private readonly IEventAggregator _eventAggregator;
        private Vector2 _gravity;
        public IList<ICircle> Circles { get; private set; }
        public Vector2 Center { get; set; }
        public Vector2 Velocity { get; set; }

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

            _eventAggregator.Publish(new BallMoved
            {
                Ball = this
            });
        }
    }
}
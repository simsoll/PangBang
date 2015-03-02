using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PangBang.Rectangle;

namespace PangBang.Particle
{
    public class Particle : IParticle
    {
        public IRectangle Boundings
        {
            get
            {
                return new Rectangle.Rectangle(_position.X, _position.Y, _width, _height);  
            }
        }

        public float Rotation { get; set; }
        public Color Color { get; set; }

        private Vector2 _position;
        private readonly float _height;
        private readonly float _width;
        private Vector2 _velocity;
        private float _rotationVelocity;
        private TimeSpan _timeToLive;

        private Vector2 _gravity;
        private TimeSpan _elapsedLifetime;

        public Particle(Vector2 position, float height, float width, Vector2 velocity,
            float rotation, float rotationVelocity, Color color, Vector2 gravity, TimeSpan timeToLive)
        {
            _position = position;
            _height = height;
            _width = width;
            _velocity = velocity;
            Rotation = rotation;
            _rotationVelocity = rotationVelocity;
            _gravity = gravity;
            Color = color;
            _timeToLive = timeToLive;

            _elapsedLifetime = TimeSpan.Zero;
        }

        public bool IsDead()
        {
            return _timeToLive < _elapsedLifetime;
        }

        public void Update(GameTime gameTime)
        {
            _elapsedLifetime += gameTime.ElapsedGameTime;

            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _velocity += _gravity * elapsed;
            _position += _velocity * elapsed;
            Rotation += _rotationVelocity * elapsed;
        }
    }
}

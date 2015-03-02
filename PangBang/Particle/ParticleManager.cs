using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PangBang.Draw;
using PangBang.Messaging.Caliburn.Micro;
using PangBang.Randomizer;

namespace PangBang.Particle
{
    public class ParticleManager : IParticleManager
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDrawer _drawer;
        private readonly IRandomizer _randomizer;

        private IList<Particle> _particles;

        public ParticleManager(IEventAggregator eventAggregator, IDrawer drawer, IRandomizer randomizer)
        {
            _eventAggregator = eventAggregator;
            _drawer = drawer;
            _randomizer = randomizer;
            _particles = new List<Particle>();
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void Update(GameTime gameTime)
        {
            for (var particle = 0; particle < _particles.Count; particle++)
            {
                _particles[particle].Update(gameTime);

                if (_particles[particle].IsDead())
                {
                    _particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw()
        {
            foreach (var particle in _particles)
            {
                _drawer.Draw(particle.Boundings, particle.Color, particle.Rotation);
            }
        }

        private Particle GenerateNewParticleWithRandomVelocity(Vector2 position, float height, float width,
            Vector2 gravity, Color[] colors, TimeSpan lifeTime)
        {
            var velocity = new Vector2(
                75f*(_randomizer.NextDouble()*2 - 1),
                75f*(_randomizer.NextDouble()*2 - 1));

            return GenerateNewParticle(position, height, width, velocity, gravity, colors, lifeTime);
        }

        private Particle GenerateNewParticle(Vector2 position, float height, float width, Vector2 velocity,
            Vector2 gravity, Color[] colors, TimeSpan lifeTime)
        {
            var angle = 0;
            var angularVelocity = 10f*(_randomizer.NextDouble()*2 - 1);
            var color = colors[_randomizer.Next(colors.Length)];

            return new Particle(position, height, width, velocity, angle, angularVelocity, color, gravity, lifeTime);
        }
    }
}
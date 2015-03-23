using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PangBang.Entities;
using PangBang.Messaging.Caliburn.Micro;

namespace PangBang.Level
{
    public class Level : ILevel
    {
        private readonly IEventAggregator _eventAggregator;

        public string Name { get { return "Level 1"; } }
        public IEnumerable<IWall> Walls { get; private set; } 
        public IEnumerable<IBall> Balls { get; private set; }


        public Level(IEventAggregator eventAggregator, IEnumerable<IWall> walls, IEnumerable<IBall> balls)
        {
            _eventAggregator = eventAggregator;
            Walls = walls;
            Balls = balls;
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
            foreach (var ball in Balls)
            {
                ball.Update(gameTime);
            }
        }
    }
}
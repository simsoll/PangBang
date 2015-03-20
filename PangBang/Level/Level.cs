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
        public IEnumerable<ICircle> Circles { get; private set; }


        public Level(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Circles = new List<ICircle>
            {
                new Circle(new Vector2(100.0f, 100.0f), 75.0f, 30.0f, 10.0f, Color.Black)
            };
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
        }
    }
}
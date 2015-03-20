using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PangBang.Draw;
using PangBang.Messaging.Caliburn.Micro;

namespace PangBang.Level
{
    public class LevelManager : ILevelManager
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILevelFactory _levelFactory;
        private readonly IDrawer _drawer;
        private readonly ILevel _level;

        public LevelManager(IEventAggregator eventAggregator, ILevelFactory levelFactory, IDrawer drawer)
        {
            _eventAggregator = eventAggregator;
            _levelFactory = levelFactory;
            _drawer = drawer;

            _level = _levelFactory.CreateLevel();
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
            _level.Load();
        }

        public void Unload()
        {
            _level.Unload();
            _eventAggregator.Unsubscribe(this);
        }

        public void Update(GameTime gameTime)
        {
            _level.Update(gameTime);
        }

        public void Draw()
        {
            foreach (var circle in _level.Circles)
            {
                foreach (var rectangle in circle.Parts)
                {
                    _drawer.Draw(rectangle, circle.Color, 0.0f);
                }
            }
        }
    }
}

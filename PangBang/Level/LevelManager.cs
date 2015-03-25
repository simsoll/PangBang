using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PangBang.Collision;
using PangBang.Draw;
using PangBang.Messaging.Caliburn.Micro;

namespace PangBang.Level
{
    public class LevelManager : ILevelManager
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILevelFactory _levelFactory;
        private readonly IDrawer _drawer;
        private readonly ICollisionManager _collisionManager;
        private readonly ILevel _level;

        public LevelManager(IEventAggregator eventAggregator, ILevelFactory levelFactory, IDrawer drawer, ICollisionManager collisionManager)
        {
            _eventAggregator = eventAggregator;
            _levelFactory = levelFactory;
            _drawer = drawer;
            _collisionManager = collisionManager;

            _level = _levelFactory.CreateLevel();
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
            _collisionManager.Load();
            _level.Load();
        }

        public void Unload()
        {
            _level.Unload();
            _collisionManager.Unload();
            _eventAggregator.Unsubscribe(this);
        }

        public void Update(GameTime gameTime)
        {
            _level.Update(gameTime);
        }

        public void Draw()
        {
            foreach (var ball in _level.Balls)
            {
                foreach (var circle in ball.Circles)
                {
                    foreach (var part in circle.Parts)
                    {
                        _drawer.Draw(part.Rectangle, circle.Color, 0.0f);
                    }    
                }
            }

            foreach (var wall in _level.Walls)
            {
                _drawer.Draw(wall.Boundings, wall.Color, 0.0f);
            }
        }
    }
}

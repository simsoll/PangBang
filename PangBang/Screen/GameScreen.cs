using Microsoft.Xna.Framework;
using PangBang.Level;
using PangBang.Messaging.Caliburn.Micro;

namespace PangBang.Screen
{
    public class GameScreen : IScreen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILevelManager _levelManager;

        public GameScreen(IEventAggregator eventAggregator, ILevelManager levelManager)
        {
            _eventAggregator = eventAggregator;
            _levelManager = levelManager;
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
            _levelManager.Load();
        }

        public void Unload()
        {
            _levelManager.Unload();
            _eventAggregator.Unsubscribe(this);
        }

        public void Update(GameTime gameTime)
        {
            _levelManager.Update(gameTime);
        }

        public void Draw()
        {
            _levelManager.Draw();
        }
    }
}
using Microsoft.Xna.Framework;
using PangBang.Messaging.Caliburn.Micro;

namespace PangBang.Screen
{
    public class GameScreen : IScreen
    {
        private readonly IEventAggregator _eventAggregator;

        public GameScreen(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
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

        public void Draw()
        {
        }
    }
}
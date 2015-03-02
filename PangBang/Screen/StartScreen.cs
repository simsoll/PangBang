using System;
using Microsoft.Xna.Framework;
using PangBang.Input.Keyboard.Messages;
using PangBang.Messaging.Caliburn.Micro;
using PangBang.Screen.Messages;
using PangBang.Text;

namespace PangBang.Screen
{
    public class StartScreen : IScreen, IHandle<KeyPressed>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ITextDrawer _textDrawer;

        public StartScreen(IEventAggregator eventAggregator, ITextDrawer textDrawer)
        {
            _eventAggregator = eventAggregator;
            _textDrawer = textDrawer;
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

        public void Handle(KeyPressed message)
        {
            _eventAggregator.Publish(new StartGame());
        }
    }
}
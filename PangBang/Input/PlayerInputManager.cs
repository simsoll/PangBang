using Microsoft.Xna.Framework.Input;
using PangBang.Input.Keyboard.Messages;
using PangBang.Messaging.Caliburn.Micro;

namespace PangBang.Input
{
    public class PlayerInputManager : IHandle<KeyPressed>, IHandle<KeyHeld>
    {
        private readonly IEventAggregator _eventAggregator;

        public PlayerInputManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Handle(KeyPressed message)
        {
            HandleKey(message.Key);
        }

        public void Handle(KeyHeld message)
        {
            HandleKey(message.Key);
        }

        public void HandleKey(Keys key)
        {
            //TODO
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}

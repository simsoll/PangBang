using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PangBang.Input.Keyboard.Messages;
using PangBang.Messaging.Caliburn.Micro;

namespace PangBang.Input.Keyboard
{
    public class KeyboardManager
    {
        private readonly TimeSpan _heldDurationThreshold;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDictionary<Keys, TimeSpan> _keyPressDurations;
        private IEnumerable<Keys> _previousPressedKeys;

        public KeyboardManager(IEventAggregator eventAggregator, TimeSpan heldDurationThreshold)
        {
            _eventAggregator = eventAggregator;
            _heldDurationThreshold = heldDurationThreshold;
            _keyPressDurations = new Dictionary<Keys, TimeSpan>();
            _previousPressedKeys = new List<Keys>();
        }

        public void Update(GameTime gameTime)
        {
            var pressedKeys = Microsoft.Xna.Framework.Input.Keyboard.GetState().GetPressedKeys();

            foreach (var key in pressedKeys)
            {
                if (!_keyPressDurations.ContainsKey(key))
                {
                    _keyPressDurations.Add(key, gameTime.ElapsedGameTime);

                    _eventAggregator.Publish(new KeyPressed
                    {
                        Key = key
                    });
                }
                else
                {
                    _keyPressDurations[key] += gameTime.ElapsedGameTime;
                    var held = _keyPressDurations[key];

                    if (_heldDurationThreshold < held)
                    {
                        _eventAggregator.Publish(new KeyHeld
                        {
                            Key = key
                        });
                    }

                }
            }

            foreach (var key in _previousPressedKeys.Except(pressedKeys))
            {
                _keyPressDurations.Remove(key);

                _eventAggregator.Publish(new KeyReleased
                {
                    Key = key
                }); 
            }

            _previousPressedKeys = pressedKeys;
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

using System;
using PangBang.Level;
using PangBang.Messaging.Caliburn.Micro;
using PangBang.Text;

namespace PangBang.Screen
{
    public class ScreenFactory : IScreenFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ITextDrawer _textDrawer;
        private readonly ILevelManager _levelManager;

        public ScreenFactory(IEventAggregator eventAggregator, ITextDrawer textDrawer, ILevelManager levelManager)
        {
            _eventAggregator = eventAggregator;
            _textDrawer = textDrawer;
            _levelManager = levelManager;
        }

        public IScreen CreateStartScreen()
        {
            return new StartScreen(_eventAggregator, _textDrawer);
        }

        public IScreen CreateGameScreen()
        {
            return new GameScreen(_eventAggregator, _levelManager);
        }
    }
}
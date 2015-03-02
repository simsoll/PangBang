using Microsoft.Xna.Framework;
using PangBang.Messaging.Caliburn.Micro;
using PangBang.Screen.Messages;

namespace PangBang.Screen
{
    public class ScreenManager: IHandle<StartGame>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IScreen _startScreen;
        private readonly IScreen _gameScreen;

        private IScreen _currentScreen;

        public ScreenManager(IEventAggregator eventAggregator, IScreen startScreen, IScreen gameScreen)
        {
            _eventAggregator = eventAggregator;
            _startScreen = startScreen;
            _gameScreen = gameScreen;

            _currentScreen = _startScreen;
            LoadScreen(_startScreen);
        }

        public void Update(GameTime gameTime)
        {
            _currentScreen.Update(gameTime);
        }

        public void Draw()
        {
            _currentScreen.Draw();
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void LoadScreen(IScreen screen)
        {
            _currentScreen.Unload();
            _currentScreen = screen;
            _currentScreen.Load();
        }

        public void Handle(StartGame message)
        {
            LoadScreen(_gameScreen);
        }
    }
}

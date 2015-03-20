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

        public ScreenManager(IEventAggregator eventAggregator, IScreenFactory screenFactory)
        {
            _eventAggregator = eventAggregator;
            _startScreen = screenFactory.CreateStartScreen();
            _gameScreen = screenFactory.CreateGameScreen();

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

        private void LoadScreen(IScreen screen)
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

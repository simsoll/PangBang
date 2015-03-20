using PangBang.Messaging.Caliburn.Micro;

namespace PangBang.Level
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IEventAggregator _eventAggregator;

        public LevelFactory(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public ILevel CreateLevel()
        {
            return new Level(_eventAggregator);
        }
    }
}
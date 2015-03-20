using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PangBang.Configuration;
using PangBang.Entities;
using PangBang.Messaging.Caliburn.Micro;

namespace PangBang.Level
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IScreenConfiguration _screenConfiguration;
        private readonly ILevelConfiguration _levelConfiguration;

        public LevelFactory(IEventAggregator eventAggregator, IScreenConfiguration screenConfiguration, ILevelConfiguration levelConfiguration)
        {
            _eventAggregator = eventAggregator;
            _screenConfiguration = screenConfiguration;
            _levelConfiguration = levelConfiguration;
        }

        public ILevel CreateLevel()
        {
            var walls = CreateWalls(_screenConfiguration.Width, _screenConfiguration.Height,
                _levelConfiguration.WallThickness, _levelConfiguration.WallColor);

            var circles = CreateCircles(_levelConfiguration.CircleLineThickness);

            return new Level(_eventAggregator, walls, circles);
        }

        private IEnumerable<IWall> CreateWalls(int screenWidth, int screenHeight, float thickness, Color color)
        {
            return new List<IWall>
            {
                new Wall(new Vector2(0, 0), thickness, screenWidth - thickness, color),
                new Wall(new Vector2(screenWidth - thickness, 0), screenHeight - thickness, thickness, color),
                new Wall(new Vector2(thickness, screenHeight - thickness), thickness, screenWidth - thickness, color),
                new Wall(new Vector2(0, thickness), screenHeight - thickness, thickness, color)
            };
        }

        private IEnumerable<ICircle> CreateCircles(float thickness)
        {
            return new List<ICircle>
            {
                new Circle(new Vector2(100.0f, 100.0f), 75.0f, 30.0f, 10.0f, Color.Black)
            };
        }
    }
}
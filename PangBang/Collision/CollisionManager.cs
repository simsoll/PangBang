using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PangBang.Entities;
using PangBang.Entities.Messages;
using PangBang.Level;
using PangBang.Level.Messages;
using PangBang.Messaging.Caliburn.Micro;
using PangBang.Rectangle;

namespace PangBang.Collision
{
    public class CollisionManager : ICollisionManager, IHandle<BallMoved>, IHandle<LevelLoaded>, IHandle<LevelUnloaded>
    {
        private readonly IEventAggregator _eventAggregator;
        private ILevel _level;

        public CollisionManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Handle(BallMoved message)
        {
            CheckForBallCollisionWithWalls(message.Ball, _level.Walls);
        }

        private void CheckForBallCollisionWithWalls(IBall ball, IEnumerable<IWall> walls)
        {
            var collistionSummury = GetCollisionSummary(ball, walls);

            if (collistionSummury == null)
            {
                return;
            }

            var rectangle = collistionSummury.Part.Rectangle;
            var rectangleLastFrame = new Rectangle.Rectangle(rectangle.X - ball.Velocity.X,
                rectangle.Y - ball.Velocity.Y, rectangle.Width,
                rectangle.Height);

            var collisionPenetration = CollisionPenetration(collistionSummury.Wall.Boundings, rectangle,
                rectangleLastFrame);

            ball.Center -= collisionPenetration.Depth;
            switch (collisionPenetration.From)
            {
                case Direction.Left:
                case Direction.Right:
                    ball.Velocity *= new Vector2(-1, 1);
                    break;
                case Direction.Top:
                case Direction.Bottom:
                    ball.Velocity *= new Vector2(1, -1);
                    break;
            }
        }

        private CollisionSummary GetCollisionSummary(IBall ball, IEnumerable<IWall> walls)
        {
            foreach (var wall in walls)
            {
                foreach (var part in ball.Circles.OrderByDescending(c => c.Radius).First().Parts)
                {
                    if (part.Rectangle.Intersects(wall.Boundings))
                    {
                        return new CollisionSummary
                        {
                            Part = part,
                            Wall = wall
                        };
                    }
                }
            }

            return null;
        }

        private CollisionPenetration CollisionPenetration(
            IRectangle struckObject,
            IRectangle movingObject,
            IRectangle movingObjectLastFrame)
        {
            var penetrations = new List<CollisionPenetration>();

            if (movingObject.Left <= struckObject.Right && struckObject.Right <= movingObjectLastFrame.Left)
            {
                penetrations.Add(new CollisionPenetration
                {
                    Depth = new Vector2(movingObject.Left - struckObject.Right, 0),
                    From = Direction.Left
                });
            }
            if (movingObjectLastFrame.Right <= struckObject.Left && struckObject.Left <= movingObject.Right)
            {
                penetrations.Add(new CollisionPenetration
                {
                    Depth = new Vector2(movingObject.Right - struckObject.Left, 0),
                    From = Direction.Right
                });
            }
            if (movingObjectLastFrame.Bottom <= struckObject.Top && struckObject.Top <= movingObject.Bottom)
            {
                penetrations.Add(new CollisionPenetration
                {
                    Depth = new Vector2(0, movingObject.Bottom - struckObject.Top),
                    From = Direction.Top
                });
            }
            if (movingObject.Top <= struckObject.Bottom && struckObject.Bottom <= movingObjectLastFrame.Top)
            {
                penetrations.Add(new CollisionPenetration
                {
                    Depth = new Vector2(0, movingObject.Top - struckObject.Bottom),
                    From = Direction.Bottom
                });
            }

            if (!penetrations.Any())
            {
                return new CollisionPenetration
                {
                    Depth = Vector2.Zero,
                    From = Direction.Unknown
                };
            }

            return penetrations.OrderBy(p => Math.Abs(p.Depth.X) + Math.Abs(p.Depth.Y)).FirstOrDefault();
        }

        public void Handle(LevelLoaded message)
        {
            _level = message.Level;
        }

        public void Handle(LevelUnloaded message)
        {
            if (_level.Name == message.Level.Name)
            {
                _level = null;
            }
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

    public class CollisionPenetration
    {
        public Vector2 Depth { get; set; }
        public Direction From { get; set; }
    }

    public class CollisionSummary
    {
        public IWall Wall { get; set; }
        public Circle.Part Part { get; set; }
    }

    public enum Direction
    {
        Unknown = 0,
        Left,
        Right,
        Top,
        Bottom
    }
}

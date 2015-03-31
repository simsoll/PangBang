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
        private readonly IList<IBall> _balls; //TODO: remove balls when they are destroyed

        public CollisionManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _balls = new List<IBall>();
        }

        public void Handle(BallMoved message)
        {
            if (!_balls.Contains(message.Ball))
            {
                _balls.Add(message.Ball);
            }

            CheckForBallCollisionWithWalls(message.Ball, _level.Walls);
            CheckForBallCollisionWithOtherBalls(message.Ball, _level.Balls.Except(new[] {message.Ball}));
        }

        private void CheckForBallCollisionWithOtherBalls(IBall ball, IEnumerable<IBall> otherBalls)
        {
            foreach (var otherball in otherBalls)
            {
                if (Colliding(ball, otherball))
                {
                    ResolveCollision(ball, otherball);
                }
            }
        }

        public bool Colliding(IBall ball, IBall otherBall)
        {
            var xd = ball.Center.X - otherBall.Center.X;
            var yd = ball.Center.Y - otherBall.Center.Y;

            var sumRadius = ball.Radius + otherBall.Radius;
            var sqrRadius = sumRadius * sumRadius;

            var distSqr = (xd * xd) + (yd * yd);

            return distSqr <= sqrRadius;
        }

        public void ResolveCollision(IBall ball, IBall otherBall)
        {
            // get the mtd
            var delta = ball.Center - otherBall.Center;
            var d = delta.Length();
            // minimum translation distance to push balls apart after intersecting
            var mtd = delta * (((ball.Radius + otherBall.Radius) - d) / d);

            // resolve intersection --
            // inverse mass quantities
            //float im1 = 1 / getMass();
            //float im2 = 1 / ball.getMass();
            var im1 = 1;
            var im2 = 1;

            // push-pull them apart based off their mass
            ball.Center += mtd * im1 / (im1 + im2);
            otherBall.Center -= mtd * im2 / (im1 + im2);

            // impact speed
            var v = ball.Velocity - otherBall.Velocity;
            var vn = Vector2.Dot(v, Vector2.Normalize(mtd));

            // sphere intersecting but moving away from each other already
            if (vn > 0.0f) return;

            // collision impulse
            var restitution = 1;
            var i = (-(1.0f + restitution) * vn) / (im1 + im2);
            var impulse = Vector2.Normalize(mtd) * i;

            // change in momentum
            ball.Velocity += impulse * im1;
            otherBall.Velocity -= impulse * im2;
        }

        private void CheckForBallCollisionWithWalls(IBall ball, IEnumerable<IWall> walls)
        {
            var collisionSummary = GetCollisionSummaryBetweenBallAndWall(ball, walls);

            if (!collisionSummary.Any())
            {
                return;
            }

            var collisionPenetrations = new List<CollisionPenetration>();

            foreach (var tuple in collisionSummary)
            {
                var rectangleLastFrame = RectangleLastFrame(tuple.Item1.Rectangle, ball.Velocity);

                collisionPenetrations.Add(CollisionPenetration(tuple.Item2.Boundings, tuple.Item1.Rectangle,
                    rectangleLastFrame));
            }

            var collisionPenetration = collisionPenetrations.OrderByDescending(x => x.Depth.LengthSquared()).First();

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

        private IEnumerable<Tuple<Circle.Part, IWall>> GetCollisionSummaryBetweenBallAndWall(IBall ball, IEnumerable<IWall> walls)
        {
            var result = new List<Tuple<Circle.Part, IWall>>();

            foreach (var wall in walls)
            {
                foreach (var part in ball.Circles.OrderByDescending(c => c.Radius).First().Parts)
                {
                    if (part.Rectangle.Intersects(wall.Boundings))
                    {
                        result.Add(new Tuple<Circle.Part, IWall>(part, wall));
                    }
                }
            }

            return result;
        }

        private IRectangle RectangleLastFrame(IRectangle rectangle, Vector2 velocity)
        {
            return new Rectangle.Rectangle(rectangle.X - velocity.X,
                rectangle.Y - velocity.Y, rectangle.Width,
                rectangle.Height);
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

    public enum Direction
    {
        Unknown = 0,
        Left,
        Right,
        Top,
        Bottom
    }
}

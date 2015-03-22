using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PangBang.Entities;

namespace PangBang.Level
{
    public interface ILevel
    {
        string Name { get; }
        void Load();
        void Unload();
        void Update(GameTime gameTime);

        IEnumerable<IWall> Walls { get; }
        IEnumerable<IBall> Balls { get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PangBang.Rectangle;

namespace PangBang.Entities
{
    public interface IWall
    {
        IRectangle Boundings { get; }
        Color Color { get; }
    }
}

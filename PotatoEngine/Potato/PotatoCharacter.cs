using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PotatoEngine.Graphics
{
    public abstract class PotatoCharacter : PotatoMovable
    {
        protected PotatoScene  Scene;
        protected PotatoSprite BaseSprite;

        public override bool TestCollision(PotatoMovable other)
        {
            return(BoundingBox.Intersects(other.BoundingBox));
        }

    }
}

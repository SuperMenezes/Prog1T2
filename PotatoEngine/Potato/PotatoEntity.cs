using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PotatoEngine.Graphics
{
    public abstract class PotatoEntity
    {
        public Vector2 Posicao;

        public abstract void Update(GameTime p_gametime);
        public abstract void Draw(SpriteBatch p_spritebatch, GameTime p_gametime);

        public virtual bool TestCollision(PotatoMovable other) { return false; }
    }
}

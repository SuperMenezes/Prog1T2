using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PotatoEngine.Graphics
{
    public abstract class PotatoTile : PotatoEntity
    {
        //private Texture2D m_texture;
        //private String m_tileID;

        public PotatoTile()
                    :base()
        {
        }

        public override void Update(GameTime p_gametime)
        {
        }

        public override void Draw(SpriteBatch p_spritebatch, GameTime p_gametime)
        {
            //p_spritebatch.Draw(m_texture, Posicao, Color.White);
        }
    }
}

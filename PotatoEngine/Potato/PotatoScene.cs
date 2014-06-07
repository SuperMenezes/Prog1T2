using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PotatoEngine.Graphics
{
    public class PotatoScene : PotatoEntity
    {
        protected List<PotatoLayer> Layers;

        public PotatoScene()
        {
            Layers = new List<PotatoLayer>();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime p_gametime)
        {
            foreach (PotatoLayer layer in Layers)
            {
                layer.Update(p_gametime);
            }
        }

        public override void Draw(SpriteBatch p_spritebatch, Microsoft.Xna.Framework.GameTime p_gametime)
        {
            foreach (PotatoLayer layer in Layers)
            {
                layer.Draw(p_spritebatch,p_gametime);
            }
        }
    }
}

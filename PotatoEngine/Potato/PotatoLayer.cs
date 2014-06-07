using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PotatoEngine.Graphics
{
    public class PotatoLayer : PotatoEntity
    {
        private PotatoScene m_scene;
        
        private Vector2 m_offset;
        private float m_scrollspeed;
        private bool m_visible;

        protected List<PotatoEntity> EntityList;
        protected List<PotatoEntity> TileList;

        public PotatoLayer()
        {
            EntityList = new List<PotatoEntity>();
        }

        public override void Update(GameTime p_gametime)
        {
            foreach (PotatoEntity entity in EntityList)
            {
                entity.Update(p_gametime);
            }
        }

        public override void Draw(SpriteBatch p_spritebatch, GameTime p_gametime)
        {
            foreach (PotatoEntity entity in EntityList)
            {
                entity.Draw(p_spritebatch, p_gametime);
            }
        }

        public void AddEntity(PotatoEntity p_sprite)
        {
            EntityList.Add(p_sprite);
        }


    }
}

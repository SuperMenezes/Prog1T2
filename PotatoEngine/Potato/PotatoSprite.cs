using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PotatoEngine.Graphics
{
    public  class PotatoSprite
    {
        private PotatoEntity m_entitypai;

        
        private Texture2D m_texture;
        public int Width;
        public int Height;

        protected float Scale { get; set; }

        private Point m_sheetsize;

        private Rectangle m_rectangle;

        private Dictionary<String, PotatoSpriteAnimation> m_animations;

        private PotatoSpriteAnimation m_currentanimation;

        private bool m_visible;

        public PotatoSprite(PotatoEntity p_entitypai, Texture2D p_texture, int p_spritewidth, int p_spriteheight)
        {
            m_entitypai = p_entitypai;


            m_texture = p_texture;
            m_animations = new Dictionary<string, PotatoSpriteAnimation>();

            Width = p_spritewidth;
            Height = p_spriteheight;

            m_rectangle = new Rectangle();
            m_rectangle.Width = p_spritewidth;
            m_rectangle.Height = p_spriteheight;

            m_sheetsize = new Point();
            m_sheetsize.X = p_texture.Width / p_spritewidth;
            m_sheetsize.Y = p_texture.Height / p_spriteheight;
        }

        public bool AddAnimation(String p_animationname, float p_frametime, bool p_loop, int p_framecount, int[] p_framepositions)
        {
            if (m_animations.ContainsKey(p_animationname))
            {
                return false;
            }

            PotatoSpriteAnimation newanimation = new PotatoSpriteAnimation(p_animationname, p_framecount, p_loop, p_frametime, p_framepositions);
            m_animations.Add(p_animationname, newanimation);

            return true;
        }

        public bool SetAnimation(String p_animationname)
        {
            if(!m_animations.ContainsKey(p_animationname))
            {
                return false;
            }

            if (p_animationname != m_animations[p_animationname].AnimationName)
            {
                m_currentanimation = m_animations[p_animationname];
                m_currentanimation.Reset();
            }
            else
            {
                m_currentanimation = m_animations[p_animationname];
            }
            return true;
        }
        
        public void Update(GameTime p_gametime)
        {
            if (m_currentanimation == null)
                return;

            m_currentanimation.Update(p_gametime);

            m_rectangle.X = (m_currentanimation.CurrentFrame % m_sheetsize.X )* Width;

            m_rectangle.Y = (m_currentanimation.CurrentFrame / m_sheetsize.X) * Height;
        }

        public void Draw(SpriteBatch p_spritebatch, GameTime p_gametime)
        {
            if (m_currentanimation == null)
                return;

            p_spritebatch.Draw(m_texture, m_entitypai.Posicao, m_rectangle, Color.White);
        }
    }
}

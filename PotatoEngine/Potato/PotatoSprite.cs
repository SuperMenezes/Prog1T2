using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PotatoEngine.Graphics
{
    public class PotatoSprite
    {
        private PotatoEntity m_entitypai;

        public Texture2D Texture;
        public int Width;
        public int Height;

        protected float Scale { get; set; }

        private Point m_sheetsize;

        public Rectangle FrameRectangle;

        private Dictionary<String, PotatoSpriteAnimation> m_animations;

        public PotatoSpriteAnimation CurrentAnimation;

        private bool m_visible;

        public PotatoSprite(PotatoEntity p_entitypai, Texture2D p_texture, int p_spritewidth, int p_spriteheight)
        {
            m_entitypai = p_entitypai;


            Texture = p_texture;
            m_animations = new Dictionary<string, PotatoSpriteAnimation>();

            Width = p_spritewidth;
            Height = p_spriteheight;

            FrameRectangle = new Rectangle();
            FrameRectangle.Width = p_spritewidth;
            FrameRectangle.Height = p_spriteheight;

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

        public bool SetAnimation(String p_animationname, bool restart)
        {
            if(!m_animations.ContainsKey(p_animationname))
            {
                return false;
            }

            if (p_animationname != m_animations[p_animationname].AnimationName)
            {
                CurrentAnimation = m_animations[p_animationname];
                CurrentAnimation.Reset();
            }
            else
            {
                CurrentAnimation = m_animations[p_animationname];
                if (restart)
                {
                    CurrentAnimation.Reset();
                }

            }
            return true;
        }
        
        public void Update(GameTime p_gametime)
        {
            if (CurrentAnimation == null)
                return;

            CurrentAnimation.Update(p_gametime);

            FrameRectangle.X = (CurrentAnimation.CurrentFrame % m_sheetsize.X )* Width;

            FrameRectangle.Y = (CurrentAnimation.CurrentFrame / m_sheetsize.X) * Height;
        }

        public void Draw(SpriteBatch p_spritebatch, GameTime p_gametime)
        {
            if (CurrentAnimation == null)
                return;

            p_spritebatch.Draw(Texture, m_entitypai.Posicao, FrameRectangle, Color.White);
        }
    }
}

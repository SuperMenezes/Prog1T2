using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PotatoEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotatoEngine.PotatoGraphics;

namespace Prog1T2
{
    public class PlayerShip : PotatoPlayer
    {

        public PlayerShip()
        {
            Velocity = Vector2.Zero ;
            MaxVelocity = 500;
            Acceleration = 1000;
            Posicao.X = 15;
            Posicao.Y = 15;
            Friction = 2.0f;
        }

        public override void Update(GameTime p_gametime)
        {
            base.Update(p_gametime);

            float dt = (float)p_gametime.ElapsedGameTime.TotalSeconds;
            Movement.Normalize();

            if (! ((Posicao + Velocity * dt).X < 10 || ((Posicao.X+Sprite.Width) + (Velocity * dt).X) > 728) )
            {
                Posicao.X = Posicao.X + Velocity.X * dt;
            }

            if (!((Posicao + Velocity * dt).Y < 10 || ((Posicao.Y + Sprite.Height) + (Velocity * dt).Y) > 624))
            {
                Posicao.Y = Posicao.Y + Velocity.Y * dt;
            }
            

            //Velocity = Velocity + Movement * Acceleration * dt;
            Velocity = new Vector2(200 * Movement.X, 200 * Movement.Y);
            float v = Velocity.Length();
            if (v > MaxVelocity)
            {
                Velocity = Velocity / v;
                Velocity = Velocity * MaxVelocity;

                v = MaxVelocity;
            }

            if (v > 0.0f)
            {
                float mul = (v - v * Friction * dt) / v;

                Velocity = Velocity * mul;
            }

            if (Movement.Y > 0)
            {
                SetAnimation("UP");
            }
            else if (Movement.Y < 0)
            {
                SetAnimation("DOWN");
            } else 
            {
                SetAnimation("IDLE");
            }

            Sprite.Update(p_gametime);
        }

        public override void Draw(SpriteBatch p_spritebatch, GameTime p_gametime)
        {
            Sprite.Draw(p_spritebatch, p_gametime);
        }

        internal void SetAnimation(string p_animationname)
        {
            Sprite.SetAnimation(p_animationname);
        }

        public void ClassicSetup(Texture2D p_texture, int p_spritewidth, int p_spriteheight)
        {
            Sprite = new PotatoSprite(this, p_texture, p_spritewidth, p_spriteheight);
            Sprite.AddAnimation("IDLE", 0.3f, false, 1, new int[] { 0 });
            Sprite.AddAnimation("UP", 0.3f, false, 1, new int[] { 1 });
            Sprite.AddAnimation("DOWN", 0.3f, false, 1, new int[] { 2 });

            SetAnimation("IDLE");
        }

        /*public void AlphaSetup(Texture2D p_texture, int p_spritewidth, int p_spriteheight)
        {
            Sprite = new PotatoSprite(this, p_texture, p_spritewidth, p_spriteheight);
            Sprite.AddAnimation("IDLE", 0.3f, true, 4, new int[] { 2, 3, 2, 1 });

            SetAnimation("IDLE");
        }*/
    }
}

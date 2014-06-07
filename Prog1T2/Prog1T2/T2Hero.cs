using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PotatoEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prog1T2
{
    public class T2Hero : PotatoCharacter
    {
        public T2Hero(Texture2D p_texture, int p_spritewidth, int p_spriteheight)
        {
            Velocity = new Vector2(200.0f, 0);

            Sprite = new PotatoSprite(this, p_texture, p_spritewidth, p_spriteheight);

            Sprite.AddAnimation("FRONT_IDLE", 0.3f, true, 1, new int[] { 0 });
            Sprite.AddAnimation("WALK_DOWN", 0.3f, true, 6, new int[] { 1, 2, 3, 4, 3, 2 });
            Sprite.AddAnimation("WALK_UP", 0.3f, true, 6, new int[] { 6, 7, 8, 9, 8, 7 });
            Sprite.AddAnimation("WALK_RIGHT", 0.3f, true, 6, new int[] { 15, 16, 17, 18, 17, 16 });
            Sprite.AddAnimation("WALK_LEFT", 0.3f, true, 4, new int[] { 10, 11, 12, 13 });
        }

        public override void Update(GameTime p_gametime)
        {
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

        internal void Move(Vector2 p_movement)
        {
            Posicao += p_movement;
        }
    }
}

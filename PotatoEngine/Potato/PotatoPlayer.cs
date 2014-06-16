using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PotatoEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PotatoEngine.PotatoGraphics
{
    public abstract class PotatoPlayer : PotatoCharacter
    {
        public override void Update(GameTime p_gametime)
        {
            Vector2 movement = Vector2.Zero;
            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                movement.Y -= 1;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                movement.Y += 1;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                movement.X += 1;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                movement.X -= 1;
            }
            Movement = movement;
        }
    }
}

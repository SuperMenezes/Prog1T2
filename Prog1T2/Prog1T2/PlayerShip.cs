using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PotatoEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotatoEngine.PotatoGraphics;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework.Input;

namespace Prog1T2
{
    public class PlayerShip : PotatoPlayer
    {

        public enum PlayerState { ALIVE, DIEING, DEAD };
        public PlayerState State;

        public PlayerShip(GameScene p_scene)
        {
            Scene = p_scene;
            State = PlayerState.ALIVE;

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

            switch (State)
            {
                case PlayerState.ALIVE:
                    float dt = (float)p_gametime.ElapsedGameTime.TotalSeconds;
                    Movement.Normalize();

                    if (!((Posicao + Velocity * dt).X < 10 || ((Posicao.X + BaseSprite.Width) + (Velocity * dt).X) > 728))
                    {
                        Posicao.X = Posicao.X + Velocity.X * dt;
                    }

                    if (!((Posicao + Velocity * dt).Y < 10 || ((Posicao.Y + BaseSprite.Height) + (Velocity * dt).Y) > 624))
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
                    }
                    else
                    {
                        SetAnimation("IDLE");
                    }

                    BoundingBox.X = (int)Posicao.X + (int)(BoundingBox.Width * 0.15f);
                    BoundingBox.Y = (int)Posicao.Y + (int)(BoundingBox.Height * 0.15f);

                    foreach (PotatoLayer layer in Scene.Layers)
                    {
                        foreach (PotatoMovable entity in layer.EntityList)
                        {
                            if (entity != this)
                            {
                                if (this.TestCollision(entity))
                                {
                                    Kill();
                                }
                            }
                        }
                    }

                    BoundingBox.Offset(((GameScene)Scene).mapView.X, 0);
                    foreach (ObjectLayer objLayer in ((GameScene)Scene).map.ObjectLayers)
                    {
                        foreach (MapObject mapObj in objLayer.MapObjects)
                        {
                             if (mapObj.Polygon != null)
                             {
                                if (mapObj.Polygon.Intersects(BoundingBox))
                                {
                                    Kill();
                                    break;
                                }
                             }
                             if (mapObj.Polyline != null)
                             {
                                 if (mapObj.Polyline.Intersects(BoundingBox))
                                 {
                                     Kill();
                                     break;
                                 }
                             }
                             if (mapObj.Polyline == null && mapObj.Polygon == null)
                             {
                                 if (mapObj.Bounds.Intersects(BoundingBox))
                                 {
                                     Kill();
                                     break;
                                 }
                             }
                        }
                    }
                    BoundingBox.Offset(((GameScene)Scene).mapView.X*-1, 0);
                    break;
                case PlayerState.DIEING:
                    if (BaseSprite.CurrentAnimation.IsFinished)
                    {
                        State = PlayerState.DEAD;
                    }

                    break;
            }



            BaseSprite.Update(p_gametime);
        }

        public void Kill()
        {
            State = PlayerState.DIEING;
            SetAnimation("EXPLOSION", true);
            GamePad.SetVibration(PlayerIndex.One, 0.2f, 0.2f);
            
        }

        public void Restore()
        {
            Posicao = new Vector2(100, 360 - 24);
            State = PlayerState.ALIVE;
            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            SetAnimation("IDLE", true);
        }

        public override void Draw(SpriteBatch p_spritebatch, GameTime p_gametime)
        {
            BaseSprite.Draw(p_spritebatch, p_gametime);

            p_spritebatch.Draw(BaseSprite.Texture, new Vector2(BoundingBox.Location.X, BoundingBox.Location.Y), BaseSprite.FrameRectangle, Color.Green, 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 1);

            //BoundingBox.Offset(((GameScene)Scene).mapView.X, 0);
            //p_spritebatch.Draw(BaseSprite.Texture, new Vector2(BoundingBox.Location.X, BoundingBox.Location.Y), BaseSprite.FrameRectangle, Color.Red, 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
            //BoundingBox.Offset(((GameScene)Scene).mapView.X*-1, 0);
        }

        internal void SetAnimation(string p_animationname, bool restart)
        {
            BaseSprite.SetAnimation(p_animationname, restart);
        }

        internal void SetAnimation(string p_animationname)
        {
            BaseSprite.SetAnimation(p_animationname, false);
        }

        public void ClassicSetup(Texture2D p_texture, int p_spritewidth, int p_spriteheight)
        {
            BoundingBox.Width = (int)(p_spritewidth * 0.7f);
            BoundingBox.Height = (int)(p_spriteheight * 0.7f);

            BaseSprite = new PotatoSprite(this, p_texture, p_spritewidth, p_spriteheight);
            BaseSprite.AddAnimation("IDLE", 0.3f, false, 1, new int[] { 0 });
            BaseSprite.AddAnimation("UP", 0.3f, false, 1, new int[] { 1 });
            BaseSprite.AddAnimation("DOWN", 0.3f, false, 1, new int[] { 2 });

            BaseSprite.AddAnimation("EXPLOSION", 0.2f, false, 8, new int[] { 3, 4, 5, 6, 7, 8, 9, 10 });

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

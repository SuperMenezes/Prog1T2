using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PotatoEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotatoEngine.PotatoGraphics;
using PotatoEngine;

namespace Prog1T2
{
    public class EnemyShip : PotatoEnemy
    {
        public enum EnemyState { HOLD, MOVING, KILLED, GONE };
        public EnemyState State = EnemyState.HOLD;

        List<Vector2> m_moveSequence = new List<Vector2>();
        int moveindex = 0;

        private PotatoTimer m_startTimer;

        public EnemyShip()
        {

        }

        public override void Update(GameTime p_gametime)
        {
            switch (State)
            {
                case EnemyState.HOLD:
                    if (!m_startTimer.Started)
                    {
                        m_startTimer.Start(p_gametime);
                    }
                    if(m_startTimer.Update(p_gametime))
                    {
                        State = EnemyState.MOVING;
                    }
                break;

                case EnemyState.MOVING:
                    if (Move(p_gametime))
                    {
                        if (moveindex < m_moveSequence.Count)
                        {
                            MoveTo(m_moveSequence[moveindex], 0.1f, 2);
                            moveindex++;
                        }
                        else
                        {
                            State = EnemyState.GONE;
                        }
                    }
                    
                break;
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

        public void ClassicSetup(Texture2D p_texture, int p_spritewidth, int p_spriteheight, Vector2 p_posicao, float p_starttimer)
        {
            Posicao = p_posicao;

            m_moveSequence.Add(new Vector2(290, 180));
            m_moveSequence.Add(new Vector2(576, 540));
            m_moveSequence.Add(new Vector2(1200, 540));


            Sprite = new PotatoSprite(this, p_texture, p_spritewidth, p_spriteheight);
            Sprite.AddAnimation("MOVE", 0.1f, true, 4, new int[] { 0,1,2,3 });

            m_startTimer = new PotatoTimer(p_starttimer);

            SetAnimation("MOVE");
            MoveTo(m_moveSequence[moveindex], 0f, 3);
            moveindex++;
        }

        /*public void AlphaSetup(Texture2D p_texture, int p_spritewidth, int p_spriteheight, Vector2 p_posicao, float p_starttimer)
        {
            Posicao = p_posicao;

            m_moveSequence.Add(new Vector2(290, 180));
            m_moveSequence.Add(new Vector2(576, 540));
            m_moveSequence.Add(new Vector2(1200, 540));


            Sprite = new PotatoSprite(this, p_texture, p_spritewidth, p_spriteheight);
            Sprite.AddAnimation("MOVE", 0.1f, true, 8, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
            Sprite.AddAnimation("DIE", 0.3f, false, 4, new int[] { 9, 10, 9, 10 });

            m_startTimer = new PotatoTimer(p_starttimer);

            SetAnimation("MOVE");
            MoveTo(m_moveSequence[moveindex], 0f, 3);
            moveindex++;
        }*/

    }
}

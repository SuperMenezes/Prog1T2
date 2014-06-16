using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PotatoEngine.Graphics
{
    public abstract class PotatoMovable : PotatoEntity
    {
        protected Vector2 Velocity { get; set; }
        protected float Acceleration { get; set; }
        protected float MaxVelocity { get; set; }
        public float Friction { get; set; }

        public Vector2 Movement { get; set; }

        public Rectangle BoundingBox;

        private Vector2 m_startPos;
        private Vector2 m_targetPos;
        private Vector2 m_offsetPos;

        private PotatoTimer m_moveTimer;
        private PotatoTimer m_delayTimer;

        public void MoveTo(Vector2 targetPos, float startDelay, float targetDelay)
        {
            m_startPos = Posicao;
            m_targetPos = targetPos;

            m_moveTimer = new PotatoTimer(targetDelay);
            m_delayTimer = new PotatoTimer(startDelay);

            if (targetDelay > 0.0f)
                m_offsetPos = (targetPos - m_startPos) / targetDelay;
            else 
                m_offsetPos = new Vector2(0f, 0f);
        }

        public bool Move(GameTime gameTime)
        {
            if (!m_delayTimer.Started)
            {
                m_delayTimer.Start(gameTime);
            }
            m_delayTimer.Update(gameTime);

            //Não atualiza depois de terminado
            if (m_delayTimer.Finished)
            {
                if (!m_moveTimer.Started)
                {
                    m_moveTimer.Start(gameTime);
                }
                m_moveTimer.Update(gameTime);
            }

            Posicao = m_startPos + (m_offsetPos * m_moveTimer.ElapsedTime);

            BoundingBox.X = (int)Posicao.X;
            BoundingBox.Y = (int)Posicao.Y;

            return m_moveTimer.Finished;
        }

    }
}

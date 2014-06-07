using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PotatoEngine.Graphics
{
    class PotatoSpriteAnimation
    {
        public String AnimationName { get; set; }

        private bool m_loop;

        private PotatoTimer m_frametimer;

        private int m_currentframe;
        private int m_framecount;

        private int[] m_framepositions;

        public int CurrentFrame
        {
            get
            {
                return m_framepositions[m_currentframe];
            }
        }

        public PotatoSpriteAnimation(String p_animationname, int p_framecount, bool p_loop, float p_frametime, int[] p_framepositions)
        {
            AnimationName = p_animationname;
            m_framecount = p_framecount;
            m_loop = p_loop;
            m_frametimer = new PotatoTimer(p_frametime);
            m_framepositions = p_framepositions;
        }

        public void Reset()
        {
            m_currentframe = 0;
        }

        public void Update(GameTime p_gametime)
        {
            if (!m_frametimer.Update(p_gametime))
            {
                return;
            }
            m_frametimer.Reset(p_gametime);


            /*if (((m_currentframe + 1) >= m_framecount) && !m_loop)
            {
                
            }*/
            if (((m_currentframe + 1) >= m_framecount) && m_loop)
            {
                m_currentframe = 0;
            }
            else if ((m_currentframe + 1) < m_framecount)
            {
                m_currentframe++;
            }

        }
    }
}

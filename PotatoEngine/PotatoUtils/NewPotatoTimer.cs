using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PotatoEngine
{
    public class NewPotatoTimer
    {
        private float m_start = 0.0f;
        private float m_count = 0.0f;
        private float m_timer = 0.0f;

        private bool m_started = false;

        public NewPotatoTimer(float timer)
        {
            m_timer = timer;
        }

        public NewPotatoTimer()
        {
            m_timer = 4.0f;
        }

        public bool Started
        {
            get { return m_started; }
        }

        public bool Finished
        {
            get { return m_count >= m_timer; }
        }

        public void Reset()
        {
            m_start = 0.0f;
            m_count = 0.0f;
            m_started = false;
        }

        public void Reset(GameTime gameTime)
        {
            m_count = 0.0f;
            m_start = (float)gameTime.TotalGameTime.TotalSeconds;
            m_started = true;
        }

        public void Start(GameTime gameTime)
        {
            m_start = (float)gameTime.TotalGameTime.TotalSeconds;
            m_started = true;
        }

        public bool Update(GameTime gameTime)
        {
            if(m_count < m_timer)
                m_count = (float)gameTime.TotalGameTime.TotalSeconds - m_start;
            if (m_count >= m_timer)
                return true;
            return false;
        }

        public string ElapsedTimeString
        {
            get { return m_count.ToString("00.00"); }
        }

        public string RemainingTimeString
        {
            get { return (m_timer - m_count).ToString("00.00"); }
        }

        public float ElapsedTime
        {
            get { return m_count; }
        }

        public float RemainingTime
        {
            get { return (m_timer - m_count); }
        }
    }
}

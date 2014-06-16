using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PotatoEngine;
using PotatoEngine.Graphics;

namespace Prog1T2
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState { GS_MENU, GS_GAME, GS_PAUSE };
        private GameState State;

        TimeSpan gamespan;
        private GameScene CurrentScene;

        private GamePadState m_gamePadState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 768;
            graphics.PreferredBackBufferHeight = 720;
            
            Content.RootDirectory = "Content";

            //this.IsMouseVisible = true;

            CurrentScene = new GameScene();
            State = GameState.GS_GAME;
        }

        protected override void Initialize()
        {
            base.Initialize();
            CurrentScene.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentScene.LoadContent(this.Content);
        }

        protected override void UnloadContent()
        {
            CurrentScene = null;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (State == GameState.GS_GAME)
            {
                if (m_gamePadState != GamePad.GetState(PlayerIndex.One))
                {
                    if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {

                        State = GameState.GS_PAUSE;
                    }
                }
            } else if (State == GameState.GS_PAUSE)
            {
                if (m_gamePadState != GamePad.GetState(PlayerIndex.One))
                {
                    if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {

                        State = GameState.GS_GAME;
                    }
                }
            }

            if (State == GameState.GS_GAME)
            {
                CurrentScene.Update(gameTime);
                base.Update(gameTime);
            }

            m_gamePadState = GamePad.GetState(PlayerIndex.One);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            CurrentScene.Draw(spriteBatch, gameTime);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

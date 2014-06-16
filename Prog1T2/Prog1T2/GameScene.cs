using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PotatoEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FuncWorks.XNA.XTiled;

namespace Prog1T2
{
    public class GameScene :  PotatoScene
    {
        public enum SceneStatus { MOVING, EVENT };
        public enum GameQuality { CLASSIC, ALPHA };

        public GameQuality GQuality = GameQuality.CLASSIC;
        public SceneStatus SceneStats = SceneStatus.MOVING; 

        public Map map;
        public Rectangle mapView;
        private PlayerShip ship;

        


        public GameStatus GameStatus;

        Texture2D enemyTexture;

        public GameScene()
            :base()
        {
            SceneStats = SceneStatus.MOVING;
        }

        public void LoadContent(ContentManager p_content)
        {
            Texture2D text;
            PotatoLayer layer = new PotatoLayer();
            

            if (GQuality == GameQuality.CLASSIC)
            {
                text = p_content.Load<Texture2D>(@"CLASSIC\vic2");
                ship = new PlayerShip(this);
                ship.ClassicSetup(text, 86, 48);
                ship.Posicao = new Vector2(100, 360 - 24);
                layer.AddEntity(ship);

                enemyTexture = p_content.Load<Texture2D>(@"CLASSIC\enemy1");

                map = p_content.Load<Map>(@"TMX\volcano2");
            }
            Layers.Add(layer);
            CriaInimigos();
        }

        public void Initialize()
        {
            mapView = new Rectangle(0,0,768,720);
            //PotatoLayer layer = new PotatoLayer();
            //layer.AddEntity(ship);
            

            //Layers.Add(layer);
            //CriaInimigos();
        }

        public override void Update(GameTime p_gametime)
        {
            switch (SceneStats)
            {
                case SceneStatus.MOVING:
                    if (mapView.X < map.Bounds.Width - mapView.Width)
                    {
                        mapView.X += 2;
                    }
                    if (mapView.X == 9000)
                    {
                        SceneStats = SceneStatus.EVENT;
                    }
                    break;

                case SceneStatus.EVENT:

                    break;
            }

            base.Update(p_gametime);

            if (ship.State == PlayerShip.PlayerState.DEAD)
            {
                //ship.ResetBuffs();
                ship.Restore();
            }
        }

        public override void Draw(SpriteBatch p_spritebatch, GameTime p_gametime)
        {
            //map.Draw(p_spritebatch, mapView);
            map.DrawLayer(p_spritebatch, 0, mapView, 0);
            base.Draw(p_spritebatch, p_gametime);
            map.DrawLayer(p_spritebatch, 1, mapView, 1);
            DrawHud(p_spritebatch);
            
        }

        private void CriaInimigos()
        {
            EnemyShip enemy = null;

            for (int i = 0; i < 20; i++)
            {
                enemy = new EnemyShip();
                enemy.ClassicSetup(enemyTexture, 45, 45, new Vector2(1200, 128), (5*i)+1.0f);
                Layers[0].AddEntity(enemy);

                enemy = new EnemyShip();
                enemy.ClassicSetup(enemyTexture, 45, 45, new Vector2(1200, 128), (5 * i) + 1.2f);
                Layers[0].AddEntity(enemy);

                enemy = new EnemyShip();
                enemy.ClassicSetup(enemyTexture, 45, 45, new Vector2(1200, 128), (5 * i) + 1.4f);
                Layers[0].AddEntity(enemy);

                enemy = new EnemyShip();
                enemy.ClassicSetup(enemyTexture, 45, 45, new Vector2(1200, 128), (5 * i) + 1.6f);
                Layers[0].AddEntity(enemy);
            }
        }

        private void DrawHud(SpriteBatch p_spritebatch)
        {

        }
    }
}

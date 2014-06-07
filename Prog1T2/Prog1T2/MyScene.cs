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
    public class MyScene :  PotatoScene
    {
        public enum GameQuality { CLASSIC, ALPHA };
        public GameQuality GQuality = GameQuality.CLASSIC;

        private Map map;
        private Rectangle mapView;


        private PlayerShip ship;

        Texture2D enemyTexture;

        public MyScene()
            :base()
        {

        }

        public void LoadContent(ContentManager p_content)
        {
            Texture2D text;

            if (GQuality == GameQuality.CLASSIC)
            {
                text = p_content.Load<Texture2D>(@"CLASSIC\vic");
                ship = new PlayerShip();
                ship.ClassicSetup(text, 99, 48);
                ship.Posicao = new Vector2(100, 360 - 24);
                enemyTexture = p_content.Load<Texture2D>(@"CLASSIC\enemy1");

                map = p_content.Load<Map>(@"TMX\gradius_volcano");
            }

            

        }

        public void Initialize()
        {
            mapView = new Rectangle(0,0,768,720);
            PotatoLayer layer = new PotatoLayer();
            layer.AddEntity(ship);
            

            Layers.Add(layer);
            CriaInimigos();
        }

        public override void Update(GameTime p_gametime)
        {
            
            if (mapView.X < map.Bounds.Width-mapView.Width)
            {
                mapView.X++;
            }
            base.Update(p_gametime);
        }

        public override void Draw(SpriteBatch p_spritebatch, GameTime p_gametime)
        {
            map.Draw(p_spritebatch, mapView);
            base.Draw(p_spritebatch, p_gametime);
        }

        private void CriaInimigos()
        {
            EnemyShip enemy = null;

            for (int i = 0; i < 20; i++)
            {
                enemy = new EnemyShip();
                enemy.ClassicSetup(enemyTexture, 45, 45, new Vector2(1200, 180), (3*i)+1.0f);
                Layers[0].AddEntity(enemy);

                enemy = new EnemyShip();
                enemy.ClassicSetup(enemyTexture, 45, 45, new Vector2(1200, 180), (3 * i) + 1.2f);
                Layers[0].AddEntity(enemy);

                enemy = new EnemyShip();
                enemy.ClassicSetup(enemyTexture, 45, 45, new Vector2(1200, 180), (3 * i) + 1.4f);
                Layers[0].AddEntity(enemy);
            }
        }
    }
}

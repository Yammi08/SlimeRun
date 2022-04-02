using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Remplaze.Content.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tutorial1;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;
using Tutorial1.Content.Entities;

namespace Remplaze.Content.Pages
{
    public class Page2 : PageManager
    {
        #region Entities
        //Enemie
        Dictionary<string, EntitiesGame> birds = new Dictionary<string, EntitiesGame>();
        Dictionary<string, EntitiesGame> spikeUpDown = new Dictionary<string, EntitiesGame>();
        Dictionary<string, EntitiesGame> spikes = new Dictionary<string, EntitiesGame>();
        //entities
        Player player;
        PortalNextLevel portalNext;
        //TileMap
        TileMapLvl2 tileMapLvl2;
        

        #endregion
        public Page2(Game1 game1) : base(game1)
        {
            
        }

        public override void Initialize()
        {

            drawOn = new Dictionary<Renderer, Transform>();
            drawMaps = new List<Map>();
            refreshSprite = new List<Animation>();
            DrawingParticles.RefreshParticles(new List<List<Code.Particle>>());
            portalNext = new PortalNextLevel(this, new Vector2(8 * 163 + 10, -20), "Page3");
            entities.Instance(portalNext);
            Camera.LimitRoom(new Vector2(110, -10), new Vector2(1230, 30));
        }
        public override void EnterTree()
        {
            
            //entities.Instance(portalNext);
            //player
            

            tileMapLvl2 = new TileMapLvl2(this);
            
            
            
            entities.Instance(tileMapLvl2);
            
            entities.EnterTree();
            DrawingSystem.UpdateDraw(drawOn);
            DrawingMap.UpdateMap(drawMaps);
            AnimationPlay.UpdateAnimation(refreshSprite);
#if DEBUG
            DrawingShape.refreshShape(rectangles, rectanglesMaps);
#endif

            Camera.EnterTree(player.transform);
        }
        public override void Start()
        {

        }
        public override void LoadContent(ContentManager content)
        {
            birds.Add(EnemieBird.CurrentName(), new EnemieBird(this, new Vector2((8 * 107), -8)));
            birds.Add(EnemieBird.CurrentName(), new EnemieBird(this, new Vector2((8 * 113), -8)));
            birds.Add(EnemieBird.CurrentName(), new EnemieBird(this, new Vector2(8 * 75 + 10, 8 * 6 - 61)));

            spikeUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2((8 * 13) + 10, (8 * 9) - 61)));
            spikeUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2((8 * 12) + 10, (8 * 9) - 61)));

            spikeUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2((8 * 61) + 10, (8 * 9) - 61)));
            spikeUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2((8 * 60) + 10, (8 * 9) - 61)));

            spikeUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2((8 * 139) + 10, (8 * 7) - 61)));
            spikeUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2((8 * 144) + 10, (8 * 6) - 61)));

            spikeUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 34 + 10, 8 * 9 - 61)));

            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 35 + 10, 8 * 9 - 61)));


            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 72 + 10, 8 * 9 - 61)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 73 + 10, 8 * 9 - 61)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 74 + 10, 8 * 9 - 61)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 75 + 10, 8 * 9 - 61)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 76 + 10, 8 * 9 - 61)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 77 + 10, 8 * 9 - 61)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 78 + 10, 8 * 9 - 61)));

            player = new Player(this, new Vector2(26, 7.5f));
            entities.Instance(player);

            foreach (var spike in spikes)
                entities.Instance(spike.Value);
            foreach (var spike in spikeUpDown)
                entities.Instance(spike.Value);
            foreach (var bird in birds)
                entities.Instance(bird.Value);

            tileMapLvl2 = new TileMapLvl2(this);

            tileMapLvl2.LoadContent(content);


            entities.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            entities.Update(gameTime);
            Camera.Follow(player.transform, gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //throw new NotImplementedException();
        }
    }
}

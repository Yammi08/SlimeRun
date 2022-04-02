using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    class Page5 : PageManager
    {
        Dictionary<string, EntitiesGame> spikes = new Dictionary<string, EntitiesGame>();
        Dictionary<string, EntitiesGame> spikesUpDown = new Dictionary<string, EntitiesGame>();
        Dictionary<string, EntitiesGame> birds = new Dictionary<string, EntitiesGame>();
        HitGround spikeRot;
        //entities
        Player player;
        PortalNextLevel portalNext;
        //TileMap
        TileMapLvl5 tileMapLvl5;

        public Page5(Game1 game1) : base(game1)
        {
        }

        public override void Initialize()
        {
            DrawingParticles.RefreshParticles(new List<List<Code.Particle>>());
            drawOn = new Dictionary<Renderer, Transform>();
            drawMaps = new List<Map>();
            refreshSprite = new List<Animation>();
            Camera.LimitRoom(new Vector2(110, -10), new Vector2(774, 30));
        }
        public override void EnterTree()
        {
            player = new Player(this, new Vector2(14,-7.5f));
            tileMapLvl5 = new TileMapLvl5(this);

            portalNext = new PortalNextLevel(this, new Vector2(8 * 120 +10, -108), "Page6");
            entities.Instance(tileMapLvl5);
            entities.Instance(player);
            entities.Instance(portalNext);

            entities.EnterTree();
            DrawingSystem.UpdateDraw(drawOn);
            DrawingMap.UpdateMap(drawMaps);
            AnimationPlay.UpdateAnimation(refreshSprite);
#if DEBUG
            DrawingShape.refreshShape(rectangles, rectanglesMaps);
#endif
            Camera.EnterTree(player.transform);
            Camera.LimitRoom(new Vector2(110, -64), new Vector2(894, 30));
        }

        public override void LoadContent(ContentManager Content)
        {
            tileMapLvl5 = new TileMapLvl5(this);
            birds.Add(EnemieBird.CurrentName(),new EnemieBird(this,new Vector2(8*12+10,8*6.5f-100)));
            birds.Add(EnemieBird.CurrentName(), new EnemieBird(this, new Vector2(8 * 30.5f + 10, 8 * 6.5f - 100)));
            birds.Add(EnemieBird.CurrentName(), new EnemieBird(this, new Vector2(8 * 37.5f + 10, 8 * 6.5f - 100)));
            birds.Add(EnemieBird.CurrentName(), new EnemieBird(this, new Vector2(8 * 44.5f + 10, 8 * 6.5f - 100)));

            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 65 + 10, 8 * 6 - 100)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 66 + 10, 8 * 6 - 100)));

            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 71 + 10, 8 * 5 - 100)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 72 + 10, 8 * 5 - 100)));

            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 77 + 10, 8 * 4 - 100)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 78 + 10, 8 * 4 - 100)));

            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 85 + 10, 8 * 3 - 100)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 86 + 10, 8 * 3 - 100)));

            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 99 + 10, 8*3 - 100), 0));

            spikeRot = (HitGround)spikes[HitGround.NameE() + HitGround.IdName()];
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 97 + 10, 8 * 3 - 100)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 96 + 10, 8 * 3 - 100)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 95 + 10, 8 * 3 - 100)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 94 + 10, 8 * 3 - 100)));
            spikes.Add(HitGround.CurrentName(),new HitGround(this,  new Vector2(8 * 93 + 10, 8 * 3 - 100)));
            
            
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 89 + 10, 8 * 3 - 100)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 90 + 10, 8 * 3 - 100)));

            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 91 + 10, 8 * 3 - 100)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 88 + 10, 8 * 3 - 100)));
            foreach (var bird in birds)
                entities.Instance(bird.Value);
            foreach (var spikeupdown in spikesUpDown)
                entities.Instance(spikeupdown.Value);
            foreach (var spike in spikes)
                entities.Instance(spike.Value);
            tileMapLvl5.LoadContent(Content);
            entities.LoadContent(Content);
        }
        
        
        public override void Start()
        {
            entities.Start();
        }
        public override void Update(GameTime gameTime)
        {
            entities.Update(gameTime);
            Camera.Follow(player.transform, gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            entities.Draw();
        }
    }
}

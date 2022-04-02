using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Remplaze.Content.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial1;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;
using Tutorial1.Content.Entities;

namespace Remplaze.Content.Pages
{
    class Page3 : PageManager
    {
        //Enemie

        Dictionary<string, EntitiesGame> spikesUpDown = new Dictionary<string, EntitiesGame>();
        Dictionary<string, EntitiesGame> spikes = new Dictionary<string, EntitiesGame>();
        Dictionary<string, EntitiesGame> birds = new Dictionary<string, EntitiesGame>();
        //entities
        Player player;
        PortalNextLevel portalNext;
        //TileMap
        TileMapLvl3 tileMapLvl3;
        public Page3(Game1 game1) : base(game1)
        {
        }
        public override void Initialize()
        {

            DrawingParticles.RefreshParticles(new List<List<Code.Particle>>());
            drawOn = new Dictionary<Renderer, Transform>();
            drawMaps = new List<Map>();
            refreshSprite = new List<Animation>();
            Camera.LimitRoom(new Vector2(110, -90), new Vector2(950, 30));

        }
        public override void EnterTree()
        {

            //player
            player = new Player(this, new Vector2(32, 12.5f));

            tileMapLvl3 = new TileMapLvl3(this);
            portalNext = new PortalNextLevel(this,new Vector2(1030,-95),"Page4");
            birds.Add("Bird1",new EnemieBird(this,new Vector2(8*67+10,-4)));

            
            foreach (var bird in birds.Values)
                entities.Instance(bird);

            entities.Instance(tileMapLvl3);
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
        }
        public override void Start()
        {

        }
        public override void LoadContent(ContentManager content)
        {
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 73 + 10, 8 * 10 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 79 + 10, 8 * 9 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 85 + 10, 8 * 10 - 80)));


            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 93 + 10, 8 * 11 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 94 + 10, 8 * 11 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 95 + 10, 8 * 11 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 96 + 10, 8 * 11 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 97 + 10, 8 * 11 - 80)));

            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 98 + 10, 8 * 10 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 99 + 10, 8 * 10 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 100 + 10, 8 * 10 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 101 + 10, 8 * 10 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 102 + 10, 8 * 10 - 80)));

            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 103 + 10, 8 * 9 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 104 + 10, 8 * 9 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 105 + 10, 8 * 9 - 80)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 106 + 10, 8 * 9 - 80)));

            
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 105 + 10, 8 * 5 - 80)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 113 + 10, 8 * 6 - 80)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 113 + 10, 8 * 4 - 80)));
            foreach (var spikeUp in spikesUpDown.Values)
                entities.Instance(spikeUp);
            foreach (var spike in spikes.Values)
                entities.Instance(spike);
            tileMapLvl3 = new TileMapLvl3(this);
            tileMapLvl3.LoadContent(content);

            entities.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            entities.Update(gameTime);
            Camera.Follow(player.transform, gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}

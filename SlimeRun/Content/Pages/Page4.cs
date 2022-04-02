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
    class Page4 : PageManager
    {
        //Enemie

        Dictionary<string, EntitiesGame> spikes = new Dictionary<string, EntitiesGame>();
        Dictionary<string, EntitiesGame> spikesUpDown = new Dictionary<string, EntitiesGame>();
        Dictionary<string, EntitiesGame> birds = new Dictionary<string, EntitiesGame>();

        //entities
        Player player;
        PortalNextLevel portalNext;
        //TileMap
        TileMapLvl4 tileMapLvl4;
        public Page4(Game1 game1) : base(game1)
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

            //entities.Instance(portalNext);
            //player
            player = new Player(this, new Vector2(/*8*139,6*5f-100));*/32, 16.5f));

            tileMapLvl4 = new TileMapLvl4(this);
            portalNext = new PortalNextLevel(this, new Vector2(8*137, 6*6.5f-100), "Page5");
            entities.Instance(tileMapLvl4);
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
            Camera.LimitRoom(new Vector2(110, -40), new Vector2(1024, 30));
        }
        public override void Start()
        {

        }
        public override void LoadContent(ContentManager content)
        {

            tileMapLvl4 = new TileMapLvl4(this);
            //TileMapLvl4
            tileMapLvl4.LoadContent(content);
            /*//Enemies
            foreach (var dog in dogs.Values)
            {
                entities.Instance(dog);
            }*/
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 32 + 10, 8 * 14 - 100)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 33 + 10, 8 * 14 - 100)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 34 + 10, 8 * 14 - 100)));
            /*spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 36 + 10, 8 * 13 - 100)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 37 + 10, 8 * 13 - 100)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 38 + 10, 8 * 13 - 100)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 39 + 10, 8 * 13 - 100)));*/

            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 70 + 10, 8 * 10 - 100)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 71 + 10, 8 * 10 - 100)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 72 + 10, 8 * 10 - 100)));
            //spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 74 + 10, 8 * 9 - 100)));

            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 90 + 10, 8 * 7 - 100)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 90 + 10, 8 * 9 - 100)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 94 + 10, 8 * 8 - 100)));

            birds.Add(EnemieBird.CurrentName(),new EnemieBird(this , new Vector2(8 * 116 + 10, 8 * 5 - 100)));
            birds.Add(EnemieBird.CurrentName(), new EnemieBird(this, new Vector2(8 * 123 + 10, 8 * 5 - 100)));
            birds.Add(EnemieBird.CurrentName(), new EnemieBird(this, new Vector2(8 * 130 + 10, 8 * 5 - 100)));
            foreach (var bird in birds.Values)
                entities.Instance(bird);
            foreach (var spike in spikes.Values)
                entities.Instance(spike);
            foreach (var spike in spikesUpDown.Values)
                entities.Instance(spike);

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

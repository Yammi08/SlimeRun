using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial1.Code;
using Tutorial1;
using Tutorial1.Code.EntityCode;
using Tutorial1.Content.Entities;
using Remplaze.Content.Entities;
using System.Diagnostics;

namespace Tutorial1.Content.Pages
{
    public class Page1 : PageManager
    {
        //Camera Dimension
        #region Entities
        //Enemie
        
        //Dictionary<string, EntitiesGame> dogs = new Dictionary<string, EntitiesGame>();
        Dictionary<string, EntitiesGame> spikes = new Dictionary<string, EntitiesGame>();
        Dictionary<string, EntitiesGame> spikesUpDown = new Dictionary<string, EntitiesGame>();
        //entities
        Player player;
        PortalNextLevel portalNext;
        //TileMap
        TileMapLvl1 tileMapLvl1;
        
        #endregion
        public Page1(Game1 game1) : base(game1)
        {

        }

        public override void Initialize()
        {

            drawOn = new Dictionary<Renderer, Transform>();
            drawMaps = new List<Map>();
            refreshSprite = new List<Animation>();
            portalNext = new PortalNextLevel(this, new Vector2(8 * 76+10, 5), "Page2");
            player = new Player(this, new Vector2(32, 23.5f));



            Camera.LimitRoom(new Vector2(110, -10), new Vector2(534, 30));
        }
        public override void EnterTree()
        {

            entities.Instance(portalNext);
            //Player
            entities.Instance(player);
            //dogs.Add(EnemieDog.CurrentName(), new EnemieDog(this, new Vector2(32, -15), 1));
            //dogs.Add(EnemieDog.CurrentName(), new EnemieDog(this, new Vector2(330, 18), 1));


            //spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 39 + 10, 80 - 61)));


            tileMapLvl1 = new TileMapLvl1(this);
            entities.Instance(tileMapLvl1);
            //Dog

            entities.EnterTree();


            AnimationPlay.UpdateAnimation(refreshSprite);
            DrawingSystem.UpdateDraw(drawOn);
            DrawingMap.UpdateMap(drawMaps);
#if DEBUG
            DrawingShape.refreshShape(rectangles, rectanglesMaps);
#endif
            Camera.EnterTree(player.transform);

        }
        public override void LoadContent(ContentManager content)
        {
            tileMapLvl1 = new TileMapLvl1(this);
            //GroupA
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 57 + 10, 80 - 53)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 56 + 10, 80 - 53)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 55 + 10, 80 - 53)));
            //spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 66 + 10, 80 - 61)));
            //spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 52 + 10, 80 - 61)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 52 + 10, 80 - 53)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 51 + 10, 80 - 53)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 50 + 10, 80 - 53)));
            //spikes.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 49 + 10, 80 - 61)));
            //GroupB
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 47 + 10, 80 - 53)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 46 + 10, 80 - 53)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 45 + 10, 80 - 53)));
            //spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 43 + 10, 80 - 61)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 42 + 10, 80 - 53)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 41 + 10, 80 - 53)));
            spikes.Add(HitGround.CurrentName(), new HitGround(this, new Vector2(8 * 40 + 10, 80 - 53)));
            

            //SpikeUpDown
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 28 + 10, 80 - 53)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 29 + 10, 80 - 53)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 30 + 10, 80 - 53)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 31 + 10, 80 - 53)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 32 + 10, 80 - 53)));
            spikesUpDown.Add(SpikeUpDown.CurrentName(), new SpikeUpDown(this, new Vector2(8 * 33 + 10, 80 - 53)));



            foreach (var spike in spikes.Values)
                entities.Instance(spike);
            foreach (var spikeUpDown in spikesUpDown.Values)
                entities.Instance(spikeUpDown);

            tileMapLvl1.LoadContent(content);

            entities.LoadContent(content);
            

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

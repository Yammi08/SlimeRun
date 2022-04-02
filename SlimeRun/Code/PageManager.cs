using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial1.Code.EntityCode;

namespace Tutorial1.Code
{
    public abstract class PageManager
    {
        private readonly int id;
        private static int nextId;
        public ScreenEntities entities;
        public Dictionary<string,List<Collision>> group = new Dictionary<string, List<Collision>>();
        public Dictionary<string, List<CollisionMap>> groupMap = new Dictionary<string, List<CollisionMap>>();
        public Dictionary<Renderer, Transform> drawOn;
        public List<Map> drawMaps;
        //Rectangles Shape
        public Dictionary<Transform, Collision> rectangles = new Dictionary<Transform, Collision>();
        public Dictionary<Map, List<Rectangle>> rectanglesMaps = new Dictionary<Map, List<Rectangle>>();
        public List<Animation> refreshSprite;
        public Game1 game1;

        private static int GenerateId()
        {
            nextId += 1;
            return nextId;
             
        }
        public PageManager(Game1 game1)
        {
            entities = new ScreenEntities();
            id = GenerateId();
            this.game1 = game1;
            group.Add("Ground", new List<Collision>());
            groupMap.Add("Ground", new List<CollisionMap>());

            group.Add("Void", new List<Collision>());
            groupMap.Add("Void", new List<CollisionMap>());
            group.Add("Interactue", new List<Collision>());
            groupMap.Add("Interactue", new List<CollisionMap>());
            group.Add("Disable", new List<Collision>());
            groupMap.Add("Disable", new List<CollisionMap>());
            group.Add("Enemies",new List<Collision>());
            groupMap.Add("Enemies", new List<CollisionMap>());
        }
        public abstract void LoadContent(ContentManager Content);
        public abstract void Initialize();
        public abstract void EnterTree();
        public abstract void Start();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace Remplaze.Content.Entities
{
    class HitGround : EntitiesGame
    {

        private static int Name = 0;
        private static string GenerateName()
        {
            Name += 1;
            string name = "HitGround" + Name;
            return name;
        }
        public static string CurrentName()
        {
            var currentNext = 1 + Name;
            return "HitGround" + currentNext;
        }
        public static string NameE()
        {
            return "HitGround";
        }
        public static int IdName()
        {
            return Name;
        }
        private Vector2 startPosition;
        private float rotation;
        Vector2 Position 
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        public Transform transform;
        public static Texture2D texture;

        public HitGround(PageManager pageManager, Vector2 position,float rotation = 0) : base(pageManager)
        {
            this.startPosition = position;
            this.rotation = rotation;
            name = GenerateName();
            AddComponent(new Transform(this, startPosition, new Vector2(1, 1), new Vector2(0, 0), rotation));

            AddComponent(new Renderer(this, texture, Color.White, new Vector2(2, 1), new Vector2(1, 1)));
            transform = GetComponent<Transform>();
            transform.pivot = new Vector2(0, GetComponent<Renderer>().tileCut.Y);
            Position = transform.pivot + Position;

            AddComponent(new Collision(this, new Rectangle((int)-transform.pivot.X, (int)-transform.pivot.Y + 6, 8, 2)));
            page.group["Enemies"].Add(GetComponent<Collision>());
            
        }
        public override void EnterTree()
        {
            GetComponent<Transform>().position = startPosition;
            page.drawOn.Add(GetComponent<Renderer>(), transform);
        }
        public override void LoadContent(ContentManager Content){}
        public override void Start(){}
        public override void Update(GameTime gameTime){}
    }
}

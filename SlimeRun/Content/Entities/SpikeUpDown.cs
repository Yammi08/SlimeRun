using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace Remplaze.Content.Entities
{
    class SpikeUpDown : EntitiesGame
    {
        private static int Name = 0;
        private static string GenerateName()
        {
            Name += 1;
            string name = "SpikeUpDown" + Name;
            return name;
        }
        public static string CurrentName()
        {
            var currentNext = 1 + Name;
            return "SpikeUpDown" + currentNext;
        }

        Transform transform;
        Vector2 startPosition;
        float startRotation;
        public static Texture2D texture;
        float timer0 = 0;
        const float timer0Start = 1.5f;

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

        public SpikeUpDown(PageManager pageManager, Vector2 position , float rotation = 0) : base(pageManager)
        {
            startPosition = position;
            startRotation = rotation;
            name = GenerateName();
        }

        public override void EnterTree()
        {
            AddComponent(new Transform(this,startPosition,new Vector2(1,1),new Vector2(0,0),startRotation));
            AddComponent(new Renderer(this,texture,Color.White,new Vector2(2,1),new Vector2(1,1)));
            AddComponent(new Animation(this,GetComponent<Renderer>()));

            GetComponent<Animation>().Add("Start", new Vector2[] { new Vector2(1, 1), new Vector2(2, 1) });
            GetComponent<Animation>().Play("Start");
            GetComponent<Animation>().Stopping();
            transform = GetComponent<Transform>();
            transform.pivot = new Vector2(0, GetComponent<Renderer>().tileCut.Y);
            startPosition = transform.pivot + startPosition;

            AddComponent(new Collision(this, new Rectangle((int)-transform.pivot.X, (int)-transform.pivot.Y + 6, 8, 2)));
            
            page.group["Interactue"].Add(GetComponent<Collision>());
            page.drawOn.Add(GetComponent<Renderer>(), transform);
            page.refreshSprite.Add(GetComponent<Animation>());
        }

        public override void LoadContent(ContentManager Content){}

        public override void Start(){}

        public override void Update(GameTime gameTime)
        {
            if(timer0 <= 0)
            {
                if(GetComponent<Animation>().currentFrame == 0)
                {
                    GetComponent<Animation>().currentFrame = 1;
                    page.group["Enemies"].Remove(GetComponent<Collision>());
                }
            }
            else
            {
                if(timer0 <= 1.3f)
                {
                    if(!page.group["Enemies"].Contains(GetComponent<Collision>()))
                    {
                        GetComponent<Animation>().currentFrame = 0;
                        page.group["Enemies"].Add(GetComponent<Collision>());
                    }
                        
                }
                timer0 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
        public void UpSpike()
        {
            if(timer0 <= 0)
            {
                timer0 = timer0Start;
            }
            
        }
    }
}

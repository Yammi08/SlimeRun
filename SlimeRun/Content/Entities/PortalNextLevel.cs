using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Remplaze.Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace Tutorial1.Content.Entities
{
    class PortalNextLevel : EntitiesGame
    {
        #region Transform
        Transform transform;
        Vector2 position;
        #endregion
        #region Variable
        public string room;
        #endregion
        public static Texture2D texture;
        
        private static int Name = 0;
        
        private static string GenerateName()
        {
            Name += 1;
            return "Portal" + Name;
        }

        public PortalNextLevel(PageManager pageManager, Vector2 position, string goToRoom) : base(pageManager)
        {
            name = GenerateName();
            this.position = position;
            room = goToRoom;
            
        }

        public override void LoadContent(ContentManager Content)
        {
            //
        }
        public override void EnterTree()
        {
            #region AddComponent
            AddComponent(new Renderer(this, texture, Color.White, new Vector2(4, 1), new Vector2(1, 1)));
            AddComponent(new Transform(this, position, new Vector2(1, 1), new Vector2((int)GetComponent<Renderer>().tileCut.X / 2, (int)GetComponent<Renderer>().tileCut.X / 2), 0));
            AddComponent(new Collision(this, new Rectangle(-(int)GetComponent<Transform>().pivot.X+4, -(int)GetComponent<Transform>().pivot.Y+3, (int)GetComponent<Renderer>().tileCut.X-8, (int)GetComponent<Renderer>().tileCut.Y-8)));
            AddComponent(new Animation(this, GetComponent<Renderer>()));
            AddComponent(new ParticleSystem(this, GetComponent<Transform>(),
                new Renderer(this, Game1.textureParticle, Color.Red, new Vector2(1, 1), new Vector2(1, 1)), new Rectangle(0, 0, 0, 0)
                , 20
                , new Color[] { new Color(0.3843f, 0.2549f, 0.4901f, 1f), new Color(0.3843f, 0.2549f, 0.4901f, 1f)*0.1f }
                , 600
                , true
                , false
                , 0
                , 0
                , 360
                , 10
                , 0f
                , 0)
                );
            #endregion
            #region GetComponent
            transform = GetComponent<Transform>();
            GetComponent<Renderer>().color.A = 200;

            GetComponent<Animation>().Add("Normal",new Vector2[] { new Vector2(1,1), new Vector2(2,1), new Vector2(3,1) , new Vector2(4,1)});
            GetComponent<Animation>().Play("Normal");
            GetComponent<Animation>().Playing();
            GetComponent<Animation>().loop = true;
            #endregion
            page.drawOn.Add(GetComponent<Renderer>(), GetComponent<Transform>());
            page.refreshSprite.Add(GetComponent<Animation>());
            page.group["Void"].Add(GetComponent<Collision>());

        }
        public override void Update(GameTime gameTime)
        {
            GetComponent<ParticleSystem>().UpdateParticle(gameTime);
            /*if(GetComponent<Animation>().currentFrame >= 4.5f )
            {
                GetComponent<Animation>().currentFrame = 0;
            }*/
            //Debug.WriteLine(GetComponent<Animation>().currentFrame);
        }

        public override void Start()
        {
            
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial1;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace SlimeRun.Content.Entities.Menu
{
    class SizeVolumen : EntitiesGame
    {
        
        Renderer[] renderer;
        List<Transform> transforms;
        protected Transform transform;
        protected Vector2 Position 
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

        public SizeVolumen(PageManager pageManager, Vector2 position) : base(pageManager)
        {
            AddComponent(new Transform(this,new Vector2(0,0),Vector2.One));
            transform = GetComponent<Transform>();
            renderer = new Renderer[2];
            renderer[0] = new Renderer(this,Game1.volumen,new Vector2(4,1),new Vector2(2,1));
            renderer[1] = new Renderer(this, Game1.volumen, new Vector2(4, 1), new Vector2(3,1));
            renderer[2] = new Renderer(this, Game1.volumen, new Vector2(4, 1), new Vector2(4,1));
            for(int i = 1; i < 4; i++)
            {
                transforms.Add(new Transform(this,new Vector2((i*renderer[0].tileCut.X) + Position.X,Position.Y), transform.scale*Vector2.One));
            }
            DrawingSystem.Add(renderer[0], transforms[0]);
            for (int i = 1; i < 3; i++)
            {
                DrawingSystem.Add(renderer[1],transforms[i]);
            }
            DrawingSystem.Add(renderer[2], transforms[4]);
        }

        public override void EnterTree()
        {
            
        }

        public override void LoadContent(ContentManager Content)
        {
        }

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}

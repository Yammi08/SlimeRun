using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace SlimeRun.Code
{
    public class Button : EntitiesGame
    {
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
        protected string texto;

        public Button(PageManager pageManager) : base(pageManager)
        {
            AddComponent(new Transform(this, Vector2.Zero));
            transform = GetComponent<Transform>();
        }

        public override void EnterTree()
        {
            
        }

        public override void LoadContent(ContentManager Content)
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

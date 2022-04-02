using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SlimeRun.Code;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial1;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace SlimeRun.Content.Entities.Menu
{
    public class ButtonResumen : Button
    {
        
        public ButtonResumen(PageManager pageManager) : base(pageManager)
        {
            AddComponent(new Renderer(this,Game1.button,new Vector2(3,3),new Vector2(1)));
            texto = "Resumen";

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

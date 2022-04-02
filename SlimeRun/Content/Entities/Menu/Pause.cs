using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SlimeRun.Content.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace SlimeRun.Content.Pages
{
    public class Pause : EntitiesGame
    {
        EntitiesGame buttonResumen;
        string Message;
        EntitiesGame settings;
        EntitiesGame volumenMs;
        EntitiesGame volumenSf;

        public Pause(PageManager pageManager) : base(pageManager)
        {
            Message = "Pausa";
            buttonResumen = new ButtonResumen(pageManager);
            volumenMs = new VolumenMS(pageManager,new Vector2(20));
            volumenSf = new VolumenSf(pageManager, new Vector2(20));
            
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

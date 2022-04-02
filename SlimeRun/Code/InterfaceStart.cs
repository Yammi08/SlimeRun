using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tutorial1;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace SlimeRun.Code
{
    public class InterfaceStart : EntitiesGame
    {
        public Transform transform;
        protected Renderer renderer;

        public Vector2 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }
        
        public InterfaceStart(PageManager pageManager) : base(pageManager)
        {
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
    public class ButtonStart : InterfaceStart
    {
        public static Texture2D texture;
        public static Animation animation;
        private Renderer text;
        private float timer0;
        private const float timerStart0 = 2;
        private bool play = false;
        private Color start = new Color(217, 230, 143, 255);
        private Color target = new Color(81, 136, 124, 255);

        public ButtonStart(PageManager pageManager) : base(pageManager)
        {
            transform = new Transform(this,new Vector2(0,0),new Vector2(1,1),new Vector2(0,0));
            renderer = new Renderer(this,texture,Color.White,new Vector2(2,2),new Vector2(1,1));
            text = new Renderer(this, texture,new Color(217, 230, 143,255), new Vector2(2, 2), new Vector2(2, 1));
            transform.pivot = new Vector2(renderer.tileCut.X/2,renderer.tileCut.Y/2);
            animation = new Animation(this,renderer);
            animation.Add("Active",new Vector2[] {new Vector2(1,1),new Vector2(1,2)});
            animation.Play("Active");
            animation.Stopping();
            animation.currentFrame = 0;
            timer0 = timerStart0;
            page.drawOn.Add(renderer, transform);
            //DrawingSystem.Add(GetComponent<Renderer>(), transform);
            page.refreshSprite.Add(animation);

        }
        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                animation.currentFrame = 1;
                play = true;
            }
            if(play)
            {
                if (timer0 < 0)
                    Game1.GoToPage("Page1");
                text.color = Color.Lerp(start,target, (float)gameTime.ElapsedGameTime.TotalSeconds);
                timer0 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                
            }
        }

    }
}

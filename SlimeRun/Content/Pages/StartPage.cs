using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Remplaze.Content.Entities;
using SlimeRun.Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tutorial1;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace SlimeRun.Content.Pages
{
    class StartPage : PageManager
    {
        static public Texture2D texture;
        String start = "Press 'Z' to start";
        Vector3 color = new Vector3(90, 102, 133);
        bool visible = true;
        bool pressed = false;
        Vector3[] colors = new Vector3[] { new Vector3(102, 169, 109),new Vector3(213, 98, 71), new Vector3(89, 22, 14) , new Vector3(98, 65, 125), new Vector3(69, 123, 180) };
        int i = 0;
        const float timerStart0 = 2f;
        float timer0 = timerStart0;
        public StartPage(Game1 game1) : base(game1)
        {

        }

        public override void EnterTree()
        {
            
            entities.EnterTree();
            
            Camera.LimitRoom(new Vector2(0, 0), new Vector2(0, 0));
            
        }

        public override void Initialize()
        {
            drawOn = new Dictionary<Renderer, Transform>();
            drawMaps = new List<Map>();
            refreshSprite = new List<Animation>();
        }

        public override void LoadContent(ContentManager content)
        {
            entities.LoadContent(content);
        }

        public override void Start()
        {
            DrawingSystem.UpdateDraw(drawOn);
            DrawingMap.UpdateMap(drawMaps);
#if DEBUG
            DrawingShape.refreshShape(rectangles, rectanglesMaps);
#endif
            AnimationPlay.UpdateAnimation(refreshSprite);
            Camera.Position = new Vector2(400,240);

            entities.Start();
        }

        public override void Update(GameTime gameTime)
        {
            entities.Update(gameTime);
            //Camera.Follow(buttonStart.transform,gameTime);

                color = MathForm.Interpolation(color, colors[i], (float)gameTime.ElapsedGameTime.TotalSeconds * 2);
                    
                Vector3 colorBool = -color + colors[i];
                
                //colorBool = new Vector3(Math.Abs(colorBool.X),Math.Abs(colorBool.Y),Math.Abs(colorBool.Z));
                float distance = Math.Abs(Math.Abs(colorBool.X)+ Math.Abs(colorBool.Y)+Math.Abs(colorBool.Z));
                if (distance < (float)Math.Pow(4f, 2))
                {
                    color = colors[i];
                    i = i < (colors.Length - 1) ? i + 1 : 0;

                }
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                pressed = true;
            if(pressed)
            {
                timer0 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if ((int)(timer0*10) % 2 == 0)
                {
                    visible = false;
                    if(timer0 < 0)
                        Game1.GoToPage("Page1");
                }
                else
                    visible = true;
            }

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(visible)
                spriteBatch.DrawString(Game1.font, start, new Vector2(game1.screenWidth / 2 - 299.6f, 80.4f), new Color((int)color.X, (int)color.Y, (int)color.Z), 0, Game1.font.MeasureString(start) / 2, new Vector2(0.098f, 0.098f), SpriteEffects.None, 0);
            spriteBatch.Draw(texture, position: new Vector2(game1.screenWidth / 2 - 345f, 10), Color.White);
            entities.Draw();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Remplaze.Content.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Tutorial1;
using Tutorial1.Code;

namespace SlimeRun.Code
{
    static class EndGame
    {
        static public bool stade = false;
        static int countDeath = 1;
        static private string veces = " vez";
        static public string death
        {
            get => "Has muerto " + countDeath + " "+ veces; private set { }
        }
        
        static public string[] Words;



        static private Vector3[] colors = new Vector3[] { new Vector3(102, 169, 109), new Vector3(213, 98, 71), new Vector3(89, 22, 14), new Vector3(98, 65, 125), new Vector3(69, 123, 180) };
        static private Vector3 color = new Vector3(90, 102, 133);
        static private int currentColor = 0;
        static public void EndDraw()
        {
            stade = false;
            countDeath += 1;
            veces = "veces";
        }
        static public void StartDraw()
        {
            stade = true;
            
        }

        static public void Update(GameTime gameTime)
        {

            color = MathForm.Interpolation(color, colors[currentColor], (float)gameTime.ElapsedGameTime.TotalSeconds * 2);

            Vector3 colorBool = -color + colors[currentColor];

            colorBool = new Vector3(Math.Abs(colorBool.X),Math.Abs(colorBool.Y),Math.Abs(colorBool.Z));
            float distance = Math.Abs(Math.Abs(colorBool.X) + Math.Abs(colorBool.Y) + Math.Abs(colorBool.Z));
            if (distance < (float)Math.Pow(4f, 2))
            {
                color = colors[currentColor];
                currentColor = currentColor < (colors.Length - 1) ? currentColor + 1 : 0;
            }
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            if (stade)
            {


                spriteBatch.DrawString(Game1.font, death,
                    new Vector2((-Convert.ToSingle(Keyboard.GetState().IsKeyDown(Keys.Right)) + Convert.ToSingle(Keyboard.GetState().IsKeyDown(Keys.Left))),0) + new Vector2(-Camera.screenWidth / Camera.zoom, -Camera.screenHeight / Camera.zoom) / 2 + new Vector2((-Camera.Transform.Translation.X + Camera.screenWidth) / Camera.zoom, (-Camera.Transform.Translation.Y + Camera.screenHeight) / Camera.zoom),
                    new Color((int)color.X,(int)color.Y,(int)color.Z), 0, Game1.font.MeasureString(death) / 2,
                    new Vector2(0.098f), SpriteEffects.None, 0);
                
            }
            
        }
    }
}

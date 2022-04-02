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
        public const string path = @"C:\Users\Rue\source\repos\SlimeRun\SlimeRun\Content\frases.txt";
        static public int currentIntWord = 0;
        static public string currentWord;

        static private Vector3[] colors = new Vector3[] { new Vector3(102, 169, 109), new Vector3(213, 98, 71), new Vector3(89, 22, 14), new Vector3(98, 65, 125), new Vector3(69, 123, 180) };
        static private Vector3 color = new Vector3(90, 102, 133);
        static private int currentColor = 0;
        static public void LoadContent()
        {
            Words = File.ReadAllLines(path);
        }
        static public void EndDraw()
        {
            stade = false;
            countDeath += 1;
            //currentIntWord += 1;
            veces = "veces";
        }
        static public void StartDraw()
        {
            stade = true;
            //currentWord = Words[currentIntWord];
        }

        static public void Update(GameTime gameTime)
        {

            color = MathForm.Interpolation(color, colors[currentColor], (float)gameTime.ElapsedGameTime.TotalSeconds * 2);

            Vector3 colorBool = -color + colors[currentColor];

            //colorBool = new Vector3(Math.Abs(colorBool.X),Math.Abs(colorBool.Y),Math.Abs(colorBool.Z));
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
                /*string[] currwords = currentWord.Split(" ");
                string[] parts = new string[0];
                int divide;
                
                if (currentWord.Length > 26)
                {
                    
                    divide = (int)(currentWord.Length/26);
                    
                    parts = new string[divide+1];
                    int partsdiv = (int)(currwords.Length / (divide+1));
                    int endvalue = 0;
                    
                    for (int i = 0; i <= divide; i++)
                    {
                        //Debug.WriteLine(i);
                        for (int curr = partsdiv *i;curr < partsdiv *(i+1); curr++)
                        {
                            
                            parts[i] += currwords[curr]+ " ";
                            endvalue = partsdiv * (i+1);
                        }
                    }
                    for (int curr = endvalue; curr < currwords.Length; curr++)
                    {

                        parts[parts.Length-1] += currwords[curr] + " ";

                    }
                }
                /*for(int i = 0; i<parts.Length;i++)
                    Debug.WriteLine(parts[i]);*/
                /*spriteBatch.DrawString(Game1.font, currentWord,
                    new Vector2(-Camera.screenWidth / Camera.zoom, -Camera.screenHeight / Camera.zoom)/2  +new Vector2((-Camera.Transform.Translation.X + Camera.screenWidth) / Camera.zoom, (-Camera.Transform.Translation.Y + Camera.screenHeight) / Camera.zoom),
                    Color.Black, 0, Game1.font.MeasureString(currentWord) / 2,
                    new Vector2(0.098f), SpriteEffects.None, 0);*/


                spriteBatch.DrawString(Game1.font, death,
                    /*new Vector2(0,2f+ Game1.font.MeasureString(currentWord).Y*0.098f) +*/new Vector2((-Convert.ToSingle(Keyboard.GetState().IsKeyDown(Keys.Right)) + Convert.ToSingle(Keyboard.GetState().IsKeyDown(Keys.Left))),0) + new Vector2(-Camera.screenWidth / Camera.zoom, -Camera.screenHeight / Camera.zoom) / 2 + new Vector2((-Camera.Transform.Translation.X + Camera.screenWidth) / Camera.zoom, (-Camera.Transform.Translation.Y + Camera.screenHeight) / Camera.zoom),
                    new Color((int)color.X,(int)color.Y,(int)color.Z), 0, Game1.font.MeasureString(death) / 2,
                    new Vector2(0.098f), SpriteEffects.None, 0);
                
            }
            
        }
    }
}

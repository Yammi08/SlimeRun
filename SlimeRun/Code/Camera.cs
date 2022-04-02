using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using Tutorial1.Code.EntityCode;
using Tutorial1.Content.Entities;
using Microsoft.Xna.Framework.Input;

namespace Tutorial1.Code
{

    static public class Camera
    {
        static public Matrix Transform { get; private set; }
        static public int screenHeight { get; private set; }
        static public int screenWidth { get; private set; }
        static public Vector2 Position 
        {
            get
            {
                return new Vector2(Transform.Translation.X,Transform.Translation.Y);
            }
            set
            {
                Matrix transform = Transform;
                transform.Translation = new Vector3(-value.X,-value.Y,0);
            }
        }
        static private float speedSmooth;
        static private Vector2 screen;
        static private Vector2 follow;
        static private float dir;
        static private float limitMinRoomX = 0;
        static private float limitMaxRoomX = 0;
        static private float limitMinRoomY = 0;
        static private float limitMaxRoomY = 0;
        const float speed = 10f;
        public const float zoom = 4f;
        
        static public void LimitRoom(Vector2 limitMin,Vector2 LimitMax)
        {
            limitMinRoomX = -limitMin.X;
            limitMaxRoomX = -LimitMax.X;
            limitMinRoomY = -limitMin.Y;
            limitMaxRoomY = -LimitMax.Y;
            
        }


        static public void SetScreen(int screenWidth, int screenHeight)
        {
            Camera.screenHeight = screenHeight;
            Camera.screenWidth = screenWidth;
            screen = new Vector2(((screenWidth) / (zoom)), ((screenHeight) / (zoom)));
            Camera.Transform = Matrix.CreateScale(new Vector3(zoom, zoom, 0f));
        }
        static public void EnterTree(Transform target)
        {
            follow = new Vector2(-(int)target.position.X,-(int)target.position.Y);
        }

        static public void Follow(Transform target, GameTime gameTime)
        {
            float move = (Convert.ToSingle(Keyboard.GetState().IsKeyDown(Keys.Right)) - Convert.ToSingle(Keyboard.GetState().IsKeyDown(Keys.Left)));
            follow = new Vector2(MathHelper.Lerp(follow.X, (int)-target.position.X , speedSmooth * speed * (float)gameTime.ElapsedGameTime.TotalSeconds), MathHelper.Lerp(follow.Y, -(int)target.position.Y, (speed * 0.1f) * (float)gameTime.ElapsedGameTime.TotalSeconds));
            dir = MathHelper.Lerp(dir,-(move * 7), speedSmooth * 0.4f*(float)gameTime.ElapsedGameTime.TotalSeconds);
            follow = new Vector2((float)Math.Round(follow.X+dir, 1), (float)Math.Round(follow.Y, 1));
            float disX = (follow.X + target.position.X);
            if (Math.Abs(disX) < 3f && Math.Abs(disX) != 0)
            {
                speedSmooth += 1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                speedSmooth = MathHelper.Clamp(speedSmooth,0,3);
            }
            else
                speedSmooth = 1;
            follow = new Vector2(Math.Clamp(follow.X,limitMaxRoomX, limitMinRoomX),Math.Clamp(follow.Y,limitMaxRoomY, limitMinRoomY));
            Camera.Transform = Matrix.CreateTranslation(follow.X, follow.Y, 0) * Matrix.CreateTranslation(screen.X/2f, screen.Y/2f, 0)
                * Matrix.CreateScale(new Vector3(zoom, zoom, 0f));
        }


        
    }
}

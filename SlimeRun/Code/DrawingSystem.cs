using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Tutorial1.Code.EntityCode;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Remplaze.Code;
using Remplaze.Content.Entities;

namespace Tutorial1.Code
{
    public static class DrawingSystem
    {
        private static Dictionary<Renderer, Transform> components = new Dictionary<Renderer, Transform>();

        public static void Add(Renderer renderer, Transform transform)
        {

            if (!components.ContainsKey(renderer))
            {

                components.Add(renderer, transform);
            }
        }
        public static void UpdateDraw(Dictionary<Renderer, Transform> component)
        {
            components = component;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {

            foreach (var item in components)
            {
                var spriteEffect = SpriteEffects.None;
                Vector2 scale = item.Value.scale;
                if (item.Value.scale.X < 0.0 && item.Value.scale.Y > 0.0)
                {
                    spriteEffect = SpriteEffects.FlipHorizontally;
                    scale = new Vector2(Math.Abs(item.Value.scale.X), Math.Abs(item.Value.scale.Y));
                }
                else if (item.Value.scale.X > 0.0 && item.Value.scale.Y < 0.0)
                {
                    spriteEffect = SpriteEffects.FlipVertically;
                    scale = new Vector2(Math.Abs(item.Value.scale.X), Math.Abs(item.Value.scale.Y));
                }

                spriteBatch.Draw(item.Key.texture, position: item.Value.position
                   , new Rectangle((int)item.Key.selectMin.X, (int)item.Key.selectMin.Y, (int)item.Key.selectMax.X, (int)item.Key.selectMax.Y)
                   , color: item.Key.color
                   , MathHelper.ToRadians(item.Value.angle)
                   , item.Value.pivot
                   , scale
                   , spriteEffect
                   , 1f);
            }
        }
    }
    public static class DrawingMap
    {
        private static List<Map> maps;
        public static void Add(Map mapAdd)
        {
            if (maps is null)
            {
                maps = new List<Map>();
            }
            maps.Add(mapAdd);
        }
        public static void UpdateMap(List<Map> maps)
        {
            DrawingMap.maps = maps;
        }
        public static void DrawMap(SpriteBatch _spriteBatch)
        {
            foreach (var item in maps)
            {

                foreach (var map in item.GetMap())
                        _spriteBatch.Draw(item.GetRenderer().texture, position: new Vector2(map.Key.X, map.Key.Y), map.Value, item.GetRenderer().color);
            }

        }
    }
    public static class BackGround
    {
        static public List<Vector2> start = new List<Vector2>();
        public static void background(SpriteBatch spriteBatch)
        {
            List<Vector2> positions = new List<Vector2>();
            for (int y = 0; y < GameLife.sizeY; y++)
            {
                for (int x = 0; x < GameLife.sizeX; x++)
                {
                    if (GameLife.scene[y, x] == 1)
                    {
                       
                        positions.Add(new Vector2(y, x));
                    }
                }
            }
            Vector2 positionScreen = new Vector2(Camera.Transform.Translation.X, Camera.Transform.Translation.Y); 
            for (int y = -1; y * (GameLife.sizeY) < Math.Abs(Camera.screenHeight / Camera.zoom)*1.1f; y++)
            {
                for (int x = -1; x * (GameLife.sizeX) < Math.Abs(Camera.screenWidth / Camera.zoom)*1.1f; x++)
                {
                   
                    for(int i = 0; i< positions.Count;i++)
                    {

                        spriteBatch.Draw(Game1.textureParticle, position: -((positionScreen) / Camera.zoom)+(positions[i]*2)+new Vector2(x*(GameLife.sizeX),y*(GameLife.sizeY)), new Color(151, 166, 183, 255)*0.4f);
                    }
                }
            }
        }
    }
    public static class AnimationPlay
    {
        private static List<Animation> refreshSprite = new List<Animation>();
        public static void UpdateAnimation(List<Animation> refreshSprite)
        {
            AnimationPlay.refreshSprite = refreshSprite;
        }
        public static void RefreshSprite(GameTime gametime)
        {

            foreach (Animation item in refreshSprite)
            {

                item.currentFrame += item.animationSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                if (item.currentFrame < item.ubication[item.GetName()].GetLength(0))
                {
                    item.renderer.selectMax = item.ubication[item.GetName()][(int)item.currentFrame, 0];
                    item.renderer.selectMin = item.ubication[item.GetName()][(int)item.currentFrame, 1];
                }
                if (item.loop && item.currentFrame > (item.ubication[item.GetName()].GetLength(1)))
                {
                    item.currentFrame = 0;
                }
            }
        }
        public static void Add(Animation animadd)
        {
            refreshSprite.Add(animadd);
        }
    }
#if DEBUG
    static class DrawingShape
    {
        static Dictionary<Transform, Collision> rectangles = new Dictionary<Transform, Collision>();
        static Dictionary<Map, List<Rectangle>> rectanglesMaps = new Dictionary<Map,List<Rectangle>>();

        static public void Add(Transform transform,Collision rectangle)
        {
            Debug.WriteLine("Add shape");
            rectangles.Add(transform, rectangle);
        }
        static public void AddMap(Map map,List<Rectangle> rectangles)
        {
            Debug.WriteLine("AddMap shape");
            rectanglesMaps.Add(map, rectangles);
        }
        static public void refreshShape(Dictionary<Transform,Collision> rectangles,Dictionary<Map,List<Rectangle>> rectanglesMaps)
        {
            Debug.WriteLine("Refresh shape");
            DrawingShape.rectangles = rectangles;
            DrawingShape.rectanglesMaps = rectanglesMaps;
        }
        static public void UpdateShape(Texture2D texture,SpriteBatch spriteBatch)
        {
            foreach (var rectangle in rectangles)
            {
                Color color = Color.Black*0.5f;
                spriteBatch.Draw(texture, position: rectangle.Key.position + rectangle.Value.offset +new Vector2(rectangle.Value.hitBox.X,rectangle.Value.hitBox.Y), new Rectangle(0,0,1,1), color, 0f,new Vector2(0,0), new Vector2(rectangle.Value.hitBox.Width,rectangle.Value.hitBox.Height) , SpriteEffects.None, 1f);
            }
            foreach (var MapList in rectanglesMaps)
            {
                Color color = Color.LightGreen;
                color = new Color( (int)(144 * 0.3921f),(int)(238 * 0.3921f),(int)(144 * 0.3921f),100);
                foreach (var rectangle in MapList.Value)
                    spriteBatch.Draw(texture, position:  new Vector2(rectangle.X, rectangle.Y), new Rectangle(0, 0, 1, 1), color, 0f, new Vector2(0, 0), new Vector2(rectangle.Width, rectangle.Height), SpriteEffects.None, 1f);
            }
        }
        
    }
#endif
    static class DrawingParticles
    {
        static List<List<Particle>> particles = new List<List<Particle>>();
        public static void AddList(List<Particle> particles)
        {
            DrawingParticles.particles.Add(particles);
        }
        static public void RefreshParticles(List<List<Particle>> particles)
        {
            DrawingParticles.particles = particles;
        }
        public static void UpdateDrawingParticles(SpriteBatch spriteBatch)
        {
            foreach(var systemParticle in particles)
            {
                foreach (Particle particle in systemParticle)
                {
                    Renderer renderer = particle.GetComponent<Renderer>();
                    Transform transform = particle.GetComponent<Transform>();
                    
                    spriteBatch.Draw(renderer.texture,position:transform.position
                        ,new Rectangle((int)renderer.selectMin.X
                        ,(int)renderer.selectMin.Y
                        ,(int)renderer.selectMax.X
                        ,(int)renderer.selectMax.Y)
                        ,renderer.color,transform.angle,transform.pivot,transform.scale,SpriteEffects.None,-1f);
                }
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace Tutorial1.Code.EntityCode
{
    public class Component
    {

        public EntitiesGame entitieGame;
        public Component(EntitiesGame other)
        {
            entitieGame = other;
        }

    }

    #region Transform
    public class Transform : Component
    {
        public Vector2 position = new Vector2(0, 0);
        public Vector2 scale = new Vector2(1, 1);
        public float angle = 0.0f;
        public Vector2 pivot = new Vector2();




        public Transform(EntitiesGame other, Vector2 position, Vector2 scale, Vector2 pivot, float angle = 0f) : base(other)
        {
            this.position = position;
            this.scale = scale;
            this.angle = angle;
            this.pivot = pivot;
        }
        public Transform(EntitiesGame other, Vector2 position, Vector2 scale, float angle = 0f) : base(other)
        {
            this.position = position;
            this.scale = scale;
            this.angle = angle;
            this.pivot = new Vector2(0);
        }
        public Transform(EntitiesGame other, Vector2 position) : base(other)
        {
            this.position = position;
            this.scale = new Vector2(1);
            this.angle = 0;
            this.pivot = new Vector2(0);
        }
    }
    #endregion
    #region Renderer
    public class Renderer : Component
    {
        public Vector2 tileCut = new Vector2(1, 1);
        public Texture2D texture;
        public Color color;
        public Vector2 selectMin;
        public Vector2 selectMax;

        public Renderer(EntitiesGame other, Texture2D texture, Color color, Vector2 tileCut, Vector2 ubication) : base(other)
        {
            this.texture = texture;
            this.color = color;
            this.tileCut = new Vector2((int)(texture.Width / tileCut.X), (int)(texture.Height / tileCut.Y));
            this.selectMin = (this.tileCut * ubication) - this.tileCut;
            this.selectMax = this.tileCut;
        }
        public Renderer(EntitiesGame other, Texture2D texture, Vector2 tileCut, Vector2 ubication) : base(other)
        {
            this.texture = texture;
            this.color = Color.White;
            this.tileCut = new Vector2((int)(texture.Width / tileCut.X), (int)(texture.Height / tileCut.Y));
            this.selectMin = (this.tileCut * ubication) - this.tileCut;
            this.selectMax = this.tileCut;
        }
    }
    #endregion
    #region Animation
    public class Animation : Component
    {
        public bool loop = true;
        public float animationSpeed = 5f;
        public bool isActive = true;
        private float saveSpeed = 5f;
        public float currentFrame;
        public Dictionary<string, Vector2[,]> ubication = new Dictionary<string, Vector2[,]>();
        public Renderer renderer;
        private string nameAnimation;
        private int i;
        public Animation(EntitiesGame other,Renderer renderer) : base(other)
        {
            saveSpeed = animationSpeed;
            
            this.renderer = renderer;
        }
        public void Play(string nameAnimation)
        {
            this.nameAnimation = nameAnimation;
        }
        public void Playing()
        {
            
            this.currentFrame = 0;
                if(animationSpeed == 0)
                    animationSpeed = saveSpeed;
                else
                    saveSpeed = animationSpeed;
            i += 1;
            
            isActive = true;
        }
        public void Stopping()
        {
            
            if (animationSpeed != 0)
                saveSpeed = animationSpeed;
            animationSpeed = 0f;
            isActive = false;
        }

        public string GetName()
        {
            return nameAnimation;
        }

        public void Add(string Name, Vector2[] ubication)
        {
            this.ubication.Add(Name, new Vector2[ubication.Length, 2]);

            for (int x = 0; x < ubication.Length; x++)
            {
                this.ubication[Name][x, 0] = new Vector2(renderer.tileCut.X, renderer.tileCut.Y);
                this.ubication[Name][x, 1] = new Vector2((renderer.tileCut.X * ubication[x].X) - renderer.tileCut.X, (renderer.tileCut.Y * ubication[x].Y) - renderer.tileCut.Y);
            }
        }
        
    }
    #endregion
    #region Collision
    public class Collision : Component 
    {
        public Rectangle hitBox;
        public readonly Vector2 offset;
        public readonly Vector2 entitie;
        private EntitiesGame entitiesGame;

        public Collision(EntitiesGame other,Rectangle hitBox,float offsetx = 0,float offsety = 0) : base(other)
        {
            entitiesGame = other;
            this.hitBox = hitBox;
            this.offset = new Vector2(offsetx, offsety);
            entitie = new Vector2(0, 0);
#if DEBUG
            entitieGame.page.rectangles.Add(entitieGame.GetComponent<Transform>(), this);
#endif
        }
        public bool IsColliding(Vector2 offset,string collisionString)
        {
            var collisionEnties =  entitiesGame.page.group[collisionString];
            var collisionTilemap = entitiesGame.page.groupMap[collisionString];
            foreach (var entiCol in collisionEnties)
            {
                Rectangle col = entiCol.hitBox;
                if (hitBox.X + this.offset.X + offset.X < col.X + col.Width &&
                        hitBox.X + this.offset.X + offset.X + hitBox.Width > col.X &&
                        hitBox.Y + this.offset.Y + offset.Y < col.Y + col.Height &&
                        hitBox.Y + this.offset.Y + offset.Y + hitBox.Height > col.Y)
                {
                    return true;
                }
            }

            foreach (var tile in collisionTilemap)
            {
                    

                foreach(var col in tile.collision)
                {
                    if (hitBox.X + this.offset.X + offset.X < col.X + col.Width &&
                        hitBox.X + this.offset.X + offset.X + hitBox.Width > col.X &&
                        hitBox.Y + this.offset.Y + offset.Y < col.Y + col.Height &&
                        hitBox.Y + this.offset.Y + offset.Y + hitBox.Height > col.Y)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public IEnumerable<EntitiesGame> PlaceWith(Vector2 offset, string collisionString)
        {
            var collisionEnties = entitiesGame.page.group[collisionString];
            var collisionTilemap = entitiesGame.page.groupMap[collisionString];
            for (int i = 0;i < collisionEnties.Count; i++)
            {
                var col = collisionEnties[i];
                    if (hitBox.X + this.offset.X + offset.X < col.hitBox.X + col.hitBox.Width + col.entitieGame.GetComponent<Transform>().position.X &&
                            hitBox.X + this.offset.X + offset.X + hitBox.Width > col.hitBox.X + col.entitieGame.GetComponent<Transform>().position.X &&
                            hitBox.Y + this.offset.Y + offset.Y < col.hitBox.Y + col.hitBox.Height + col.entitieGame.GetComponent<Transform>().position.Y &&
                            hitBox.Y + this.offset.Y + offset.Y + hitBox.Height > col.hitBox.Y + col.entitieGame.GetComponent<Transform>().position.Y)
                    {
                        yield return col.entitieGame;
                    }
            }
            foreach (var tile in collisionTilemap)
            {
                foreach (var col in tile.collision)
                {
                    if (hitBox.X + this.offset.X + offset.X < col.X + col.Width &&
                        hitBox.X + this.offset.X + offset.X + hitBox.Width > col.X &&
                        hitBox.Y + this.offset.Y + offset.Y < col.Y + col.Height &&
                        hitBox.Y + this.offset.Y + offset.Y + hitBox.Height > col.Y)
                    {
                        yield return tile.entitieGame;
                    }
                }

            }

        }
        public bool ContactWith(Vector2 offset, EntitiesGame entitie)
        {
            Collision col = entitie.GetComponent<Collision>();
            Transform transform = entitie.GetComponent<Transform>();
            if (hitBox.X + this.offset.X + offset.X < transform.position.X + col.hitBox.Width &&
                        hitBox.X + this.offset.X + offset.X + hitBox.Width > transform.position.X &&
                        hitBox.Y + this.offset.Y + offset.Y < transform.position.Y + col.hitBox.Height &&
                        hitBox.Y + this.offset.Y + offset.Y + hitBox.Height > transform.position.Y)
            {
                return true;
            }
            return false;
        }



    }
    #endregion
    #region RigidBody
    class RigidBody : Component
    {
        
        Vector2 velocity;
        Transform transform;
        Collision collision;
        public RigidBody(EntitiesGame other) : base(other)
        {
            this.transform = entitieGame.GetComponent<Transform>();
            collision = entitieGame.GetComponent<Collision>();
        }
        public Vector2 MoveAndSlide(Vector2 velocity, Vector2 pixelUp, float delta)
        {
            float velocityAftX = velocity.X * delta;
            this.velocity.X = velocity.X;
            if (collision.IsColliding(new Vector2(transform.position.X + velocityAftX + pixelUp.X, transform.position.Y + pixelUp.Y), "Ground"))
            {
                transform.position.X = (int)Math.Round(transform.position.X) + pixelUp.X;
                this.velocity.X = (int)Math.Ceiling(this.velocity.X);
                velocityAftX = (int)Math.Ceiling(velocityAftX);
                float vX = velocityAftX;
                for (int x = 0; x < Math.Abs(vX) + 1; x++)
                {

                    if (collision.IsColliding(new Vector2((transform.position.X + velocityAftX), transform.position.Y) + pixelUp, "Ground"))
                    {
                        this.velocity.X -= Math.Sign(vX);
                        velocityAftX -= Math.Sign(vX);
                    }
                    else
                        break;
                }
            }
            float velocityAftY = velocity.Y * delta;
            this.velocity.Y = velocity.Y;
            if (collision.IsColliding(new Vector2(transform.position.X + velocityAftX, transform.position.Y + velocityAftY) + pixelUp, "Ground"))
            {
                transform.position.Y = (int)Math.Round(transform.position.Y) + pixelUp.Y;
                velocityAftY = (int)Math.Ceiling(velocityAftY);
                this.velocity.Y = (int)Math.Ceiling(this.velocity.Y);
                float vY = velocityAftY;
                for (int y = 0; y < Math.Abs(vY) + 1; y++)
                {

                    if (collision.IsColliding(new Vector2((transform.position.X + velocityAftY), transform.position.Y + this.velocity.Y) + pixelUp, "Ground"))
                    {
                        velocityAftY -= Math.Sign(vY);
                        this.velocity.Y -= Math.Sign(vY);
                    }
                    else
                        break;
                }
            }


            return this.velocity;
        }

        public void SetVelocity(float x,float y)
        {
            velocity = new Vector2(x,y);
            
        }
        public void UpdateTransform(float delta)
        {
            transform.position += velocity * delta;
        }
    }
    #endregion


}


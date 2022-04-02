using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Remplaze.Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tutorial1;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace Remplaze.Content.Entities
{
    class EnemieBird : EntitiesGame
    {
        protected Vector2 _velocity;

        Transform transform;
        Vector2 position;
        #region RigidBody

        Vector2 Velocity 
        {
            get { return _velocity; }
            set 
            {
                _velocity = value;
                transform.position += _velocity;
            }
        } 
        #endregion
        private bool destroy = false;
        private float restart = 0f;
        private float timer0 = 0f;
        private const float startTimer0 = 6f;
        private float timer1 = 0f;
        private const float startTimer1 = 1f;

        public static Texture2D texture;
        private static int Name = 0;
        private static string GenerateName()
        {
            Name += 1;
            string name = "Bird" + Name;
            return name;
        }
        public static string CurrentName()
        {
            var currentNext = 1 + Name;
            return "Bird" + currentNext;
        }
        public EnemieBird(PageManager pageManager, Vector2 position) : base(pageManager)
        {
            name = GenerateName();
            this.position = position;
        }
        public override void EnterTree()
        {
            timer0 = startTimer0;
            AddComponent(new Transform(this,position, new Vector2(1,1), new Vector2(0,0),0));
            AddComponent(new Renderer(this,texture,Color.White,new Vector2(8,1),new Vector2(1,1))); 
            AddComponent(new Animation(this,GetComponent<Renderer>()));
            AddComponent(new Collision(this, new Rectangle(-(int)(GetComponent<Renderer>().tileCut.X / 2)+2, -(int)(GetComponent<Renderer>().tileCut.Y / 2)+2, (int)GetComponent<Renderer>().tileCut.X-5, (int)GetComponent<Renderer>().tileCut.Y-4)));
            AddComponent(new ParticleSystem(this, GetComponent<Transform>(),
                new Renderer(this, Game1.textureParticle, Color.Red, new Vector2(1, 1), new Vector2(1, 1)),
                new Rectangle(0, 0, 0, 0)
                , 10
                , new Color[] { new Color(164, 60, 60, 255), new Color(182, 205, 227, 255)*0.5f }
                , 600
                , true
                , false
                , 0
                , 0
                , 360
                , 10
                , 0f
                , 0)
                );

            GetComponent<Animation>().Add("Attack", new Vector2[] {new Vector2(1,1)});
            GetComponent<Animation>().Add("Fly",new Vector2[] { new Vector2(3,1), new Vector2(2,1)});
            GetComponent<Animation>().Add("Hit",new Vector2[] { new Vector2(4,1),new Vector2(5,1)});
            GetComponent<Animation>().Add("Death",new Vector2[] {new Vector2(6,1), new Vector2(7,1), new Vector2(8,1)});
            GetComponent<Animation>().Add("Respawn",new Vector2[] {new Vector2(8,1),new Vector2(7,1), new Vector2(6,1) });
            GetComponent<Animation>().Play("Fly");
            transform = GetComponent<Transform>();
            transform.pivot = GetComponent<Renderer>().tileCut / 2;

            page.drawOn.Add(GetComponent<Renderer>(), GetComponent<Transform>());
            page.refreshSprite.Add(GetComponent<Animation>());
            page.group["Enemies"].Add(GetComponent<Collision>());
        }

        public override void LoadContent(ContentManager Content)
        {

        }
        
        public override void Start()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
            if (destroy)
            {
                if(timer0 <= 5)
                {

                    GetComponent<ParticleSystem>().isActive = true;

                    var animation = GetComponent<Animation>();
                    if(animation.GetName() != "Death" && animation.GetName() != "Respawn")
                    {
                        GetComponent<Animation>().Play("Death");
                        GetComponent<Animation>().loop = false;
                    }

                        
                    if (timer0 <= 4)
                    {
                        Velocity = new Vector2(0, MathHelper.Lerp(Velocity.Y, -65* (float)gameTime.ElapsedGameTime.TotalSeconds, (float)gameTime.ElapsedGameTime.TotalSeconds));
                        if (timer0 <= 0.5f)
                        {
                            transform.position = position;
                            Velocity = new Vector2(0, 0);
                            Collision colision = GetComponent<Collision>();
                            if (!page.group["Enemies"].Contains(colision))
                            {
                                page.group["Disable"].Remove(colision);
                                page.group["Enemies"].Add(colision);
                            }
                            animation.Play("Respawn");
                            animation.loop = true;
                            if (timer0 <= 0)
                            {
                                restart = (float)gameTime.TotalGameTime.TotalSeconds-1.73f;
                                int ubscale = Math.Sign(globalEntities.PaperWatch(new Vector2(0,0), transform.position, -1).X);
                                transform.scale.X = ubscale;
                                destroy = false;
                                animation.Play("Fly");
                            }
                                
                        }

                    }

                }
                timer0 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                if(timer1 <= 0)
                {
                    if(GetComponent<Animation>().GetName()  != "Fly")
                        GetComponent<Animation>().Play("Fly");
                    GetComponent<ParticleSystem>().isActive = false;
                    transform.position = position + new Vector2(0, MathForm.SinEffect((float)Math.Round(gameTime.TotalGameTime.TotalSeconds - restart, 2) * 2, 1) + MathForm.SinEffect((float)Math.Round((gameTime.TotalGameTime.TotalSeconds + 3 - restart), 2), 3));
                    transform.scale = globalEntities.PaperWatch(transform.scale, transform.position, -(float)gameTime.ElapsedGameTime.TotalSeconds * 7);
                }
                else
                {
                    timer1 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    restart += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                
            }
            
            GetComponent<ParticleSystem>().UpdateParticle(gameTime);
        }
        public void Destroy()
        {
            transform.scale.X = (float)Math.Ceiling(transform.scale.X);
            if(transform.scale.X == 0)
            {
                transform.scale.X = -1;
            }
            GetComponent<Animation>().Play("Hit");
            destroy = true;
            timer0 = startTimer0;
            page.group["Disable"].Add(GetComponent<Collision>());
            page.group["Enemies"].Remove(GetComponent<Collision>());
        }
        public void Attack()
        {
            GetComponent<Animation>().Play("Attack");
            timer1 = startTimer1;
        }
    }
}

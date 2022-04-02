using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Remplaze.Content.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace Remplaze.Code
{
    class ParticleSystem : Component
    {
        //This
        public Transform transform;
        //Principal Stats
        public bool isActive;
        public bool isColliding;
        public int numberEntities;
        public int timeLife;
        private float timeCreate;
        //Mov
        public float force;
        public int directionMin;
        public int directionMax;
        public float speed;
        public float gravity;
        public Rectangle rectangle;
        public float angle;
        //Character
        public Renderer renderer;
        public Color[] colors;
        public float distanceColors;
        //Entities
        public List<Particle> particlesIn;
        public List<Particle> particlesOut;

        private Random random;

        public ParticleSystem(EntitiesGame entitieGame, Transform transform, Renderer renderer, Rectangle rectangle, int numberEntities, Color[] color, int timeLife = 5, bool isActive = true, bool isColliding = false, float force = 0, int directionMin = 0, int directionMax = 0, float speed = 1, float gravity = 0, float angle = 0) : base(entitieGame)
        {
            #region AddComponent
            this.transform = transform;

            #endregion
            #region GetComponent
            //renderer.texture.Reload();
            #endregion
            random = new Random();
            this.renderer = renderer;
            colors = color;
            angle = 0;
            this.rectangle = rectangle;
            gravity = 0f;
            this.speed = speed;
            this.directionMin = directionMin;
            this.directionMax = directionMax;
            this.numberEntities = numberEntities;
            this.isActive = isActive;
            this.isColliding = isColliding;
            this.timeLife = timeLife;
            distanceColors = timeLife / colors.Length;
            particlesIn = new List<Particle>();
            particlesOut = new List<Particle>();
            for (int part = 0; part < numberEntities; part++)
            {
                particlesOut.Add(new Particle(entitieGame.page, this));
            }
            DrawingParticles.AddList(particlesIn);
        }
        public void UpdateParticle(GameTime gameTime)
        {
                if (particlesIn.Count > 0)
                {
                    
                    if (isColliding)
                    {
                        /*for (int i = 0; i < particlesIn.Count; i++)
                        {
                          var particle = particlesIn[i];

                        particle.GetComponent<RigidBody>().MoveAndSlide(particle.direction.X * speed, particle.direction.Y * speed,(float)gameTime.ElapsedGameTime.TotalSeconds);

                        }*/
                    }
                    else
                    {
                        for (int i = 0; i < particlesIn.Count; i++)
                        {
                            var particle = particlesIn[i];
                            
                            particle.GetComponent<RigidBody>().SetVelocity(particle.direction.X * speed*60 * (float)gameTime.ElapsedGameTime.TotalSeconds, (gravity*(float)gameTime.ElapsedGameTime.TotalSeconds) + particle.direction.Y * speed *60* (float)gameTime.ElapsedGameTime.TotalSeconds);
                            
                        }
                    }
                    for(int i = 0; i< particlesIn.Count;i++ )
                    {
                        var particle = particlesIn[i];
                        particle.Update(gameTime);
                    }
                }
            if (isActive)
            {
                CreateParticle(gameTime);
            }
            
        }
        void CreateParticle(GameTime gameTime)
        {
            timeCreate -= 10 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(particlesOut.Count > 0 && timeCreate <= 0)
            {
                for(int i= 0; i < 1; i++)
                {
                    particlesOut[i].UpdateValue();
                    particlesIn.Add(particlesOut[i]);
                    particlesOut.Remove(particlesOut[i]);
                }
                timeCreate = random.Next(1, 5);
            }
        }
    }
    class Particle : EntitiesGame
    {
          ParticleSystem particleSystem;
          public Vector2 direction;
          private float gravity;
          private float timeLife;
          private float speed;
          private float angle;
          private float force;
          //Data Particle
          private float lifeSpent = 0;

          Random random;

        public Particle(PageManager page, ParticleSystem particleSystem) : base(page)
        {
            random = new Random();
            this.particleSystem = particleSystem;
            AddComponent(new Transform(this, particleSystem.transform.position, new Vector2(1, 1), new Vector2(0, 0)));
            AddComponent(new RigidBody(this));
            AddComponent(new Renderer(this,particleSystem.renderer.texture,particleSystem.colors[0], particleSystem.renderer.tileCut, new Vector2(1,1)));
            GetComponent<Renderer>().selectMin = particleSystem.renderer.selectMin;
            GetComponent<Renderer>().selectMin = particleSystem.renderer.selectMax;
            GetComponent<Transform>().pivot = GetComponent<Renderer>().tileCut / 2;
            UpdateValue();
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
            
            lifeSpent += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if ( lifeSpent >= timeLife )
            {
                particleSystem.particlesOut.Add(this);
                particleSystem.particlesIn.Remove(this);
            }
            UpdateProjectile(gameTime);
            
        }

        public void UpdateValue()
        {
            GetComponent<Transform>().position = particleSystem.transform.position + ( new Vector2((float)random.NextDouble() * particleSystem.rectangle.Right, (float)random.NextDouble()));
            int direction = random.Next(particleSystem.directionMin, particleSystem.directionMax);
            this.direction = new Vector2((float)Math.Cos(direction), (float)Math.Sin(direction));
            this.gravity = particleSystem.gravity;
            this.timeLife = random.Next(particleSystem.timeLife * 150, particleSystem.timeLife * 195);
            this.timeLife = (timeLife * 0.0001f)/particleSystem.speed;
            
            

            GetComponent<Renderer>().color = particleSystem.colors[0];
            this.speed = particleSystem.speed;
            this.angle = particleSystem.angle;
            this.force = particleSystem.force;

            //GetComponent<RigidBody>().SetVelocity(this.direction.X * speed, this.direction.Y * speed);

            lifeSpent = 0;
        }
        private void AddForce()
        {
            speed += force;
        }
        private void AddAngle()
        {
            GetComponent<Transform>().angle = MathHelper.Lerp(GetComponent<Transform>().angle,angle,0.1f);
        }
        private void UpdateProjectile(GameTime gameTime)
        {
            GetComponent<RigidBody>().UpdateTransform((float)gameTime.ElapsedGameTime.TotalSeconds);
            
            GetComponent<Renderer>().color = MathForm.mix(GetComponent<Renderer>().color, particleSystem.colors[1], (float)gameTime.ElapsedGameTime.TotalSeconds);

        }
        
    }
    
}

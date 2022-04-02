using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace Tutorial1.Content.Entities
{
    class EnemieDog : EntitiesGame
    {
        private static int Name = 0;
        private static string GenerateName()
        {
            Name += 1;
            string name = "Dog" + Name;
            return name;
        }
        public static string CurrentName()
        {
            var currentNext = 1 + Name;
            return "Dog" + currentNext;
        }
        #region Transform
        private Transform transform;
        private Vector2 position;
        private Vector2 scale = new Vector2(1,1);
        private float angle = 0f;
        #endregion
        #region Rigidbody
        RigidBody rigidBody;
        Vector2 velocity = new Vector2(0,0);
        float blocks = 0;
        int dir = 1; 
        #endregion
        #region Renderer
        public static Texture2D texture;
        #endregion
        public EnemieDog(PageManager pageManager,Vector2 position, int dir = -1) : base(pageManager)
        {
            this.position = position;
            this.dir = dir;
            scale.X = Math.Sign(dir)*-1;
            name = GenerateName();
        }

        public override void LoadContent(ContentManager content)
        {



        }
        public override void EnterTree()
        {
            #region AddComponent
            AddComponent(new Renderer(this, texture, Color.White, new Vector2(3, 1), position));
            AddComponent(new Transform(this, position, scale, new Vector2((int)GetComponent<Renderer>().tileCut.X / 2, 0), angle));

            AddComponent(new Animation(this, GetComponent<Renderer>()));
            AddComponent(new Collision(this, new Rectangle(-((int)GetComponent<Transform>().pivot.X) + 2, 3, (int)GetComponent<Renderer>().tileCut.X - 4, (int)GetComponent<Renderer>().tileCut.Y - 4)));
            AddComponent(new RigidBody(this));
            #endregion
            #region GetComponent
            GetComponent<Animation>().Add("Attack", new Vector2[] {new Vector2(3,1),
                                                                 new Vector2(1,1),
                                                                new Vector2(2,1)});
            GetComponent<Animation>().Play("Attack");
            transform = GetComponent<Transform>();
            rigidBody = GetComponent<RigidBody>();
            GetComponent<Animation>().Stopping();
            

            #endregion
            #region ActionComponent
            page.drawOn.Add(GetComponent<Renderer>(), GetComponent<Transform>());
            page.refreshSprite.Add(GetComponent<Animation>());
            #endregion
        }
        public override void Start()
        {
            while (!GetComponent<Collision>().IsColliding(new Vector2(0, 1) + transform.position, "Ground"))
                transform.position.Y += 1;

            page.group["Enemies"].Add(GetComponent<Collision>());
        }
        public override void Update(GameTime gameTime)
        {
            var nextPosX = transform.position.X - (transform.position.X % 8) + 10;
            float posX = (nextPosX  - (transform.position.X + 10 + (velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds)));
            velocity.X = (1 - posX) * 2 * dir;
            rigidBody.MoveAndSlide(velocity, new Vector2(0, 0f), (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (posX != blocks && !GetComponent<Collision>().IsColliding(transform.position + new Vector2((dir*8), 4), "Ground") || posX != blocks && GetComponent<Collision>().IsColliding(transform.position + new Vector2((1 * dir), 0), "Ground"))
                dir = -dir;

            Animation animation = GetComponent<Animation>();
            if(Math.Abs(velocity.X) > 4.0)
            {
                if (!animation.isActive)
                {
                    animation.currentFrame = 1;
                    animation.Playing();
                }
            }
            else
                animation.currentFrame = 0;

            transform.scale.X -= (7* dir * (float)gameTime.ElapsedGameTime.TotalSeconds);
            transform.scale.X = Math.Clamp(transform.scale.X, -1, 1);
            blocks = posX;
            GetComponent<RigidBody>().UpdateTransform((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;
using Microsoft.Xna.Framework.Input;
using Remplaze.Content.Entities;
using Remplaze.Code;
using SlimeRun.Code;

namespace Tutorial1.Content.Entities
{
    class Player : EntitiesGame
    {
        private static int Name = 0;

        #region Transform
        public Transform transform;
        public Vector2 Position
        { get
            {
                return transform.position;
            }

            set
            {
                transform.position = value;
            }


        }
        public Vector2 Scale
        {
            get
            {
                return transform.scale;
            }
            set
            {
                transform.scale = value;
            }
        }
        public float Rotation
        {
            get
            {
                return transform.angle;
            }
            set
            {
                transform.angle = value;
            }
        }
        #endregion
        #region RigidBody
        //private float prevX;
        public Vector2 velocity;
        const float gravity =200f;
        const float jumpSpeed = -70;
        bool pressedZ = false;
        bool ground = true;
        float friction = 1f;
        private Vector2 velocityHide;

        const float frictionGround = 5f;
        const float frictionAir = 1.5f;



        #endregion
        #region Renderer
        public static Texture2D texture;
        public static Texture2D textureDead;
        private Collision collision;
        #endregion
        #region Animation
        int beforeSpeedRight = 1;
        enum Animation {stop, walk, jump,intermediateJump, fall, death}
        public int currentAnimation;
        #endregion
        #region Timers
        float timer0 = 0;
        const float startTimer0 = 0.2f;
        #endregion

        private Vector2 startPosition;
        private Transform portalNextLevel;

        bool downKey;
        bool Col = false;
        bool downKeyR;
        private int tick = 1;
        private static string GenerateName()
        {
            Name += 1;
            return "Player" + Name;
        }
        
        public Player(PageManager pageManager,Vector2 position) : base(pageManager)
        {
            startPosition = position;
            name = GenerateName();
        }
        public override void LoadContent(ContentManager Content)
        {

        }
        public override void EnterTree()
        {

            #region AddComponent
            AddComponent(new Transform(this, startPosition, new Vector2(1,1), Vector2.Zero, 0));
            
            AddComponent(new Renderer(this, texture, Color.White, new Vector2(4, 2), new Vector2(1, 1)));
            AddComponent(new Code.EntityCode.Animation(this, GetComponent<Renderer>()));
            AddComponent(new Collision(this, new Rectangle((int)(-(int)GetComponent<Renderer>().tileCut.X / 2) + 2,
                                                    (int)(-(int)GetComponent<Renderer>().tileCut.Y / 2) + 1,
                                                    (int)GetComponent<Renderer>().tileCut.X - 4,
                                                    (int)GetComponent<Renderer>().tileCut.Y - 3), 0, 0.5f));


            GetComponent<Code.EntityCode.Animation>().Add("Run", new Vector2[] { new Vector2(1, 1), new Vector2(2, 1) });
            GetComponent<Code.EntityCode.Animation>().Add("Jump", new Vector2[] { new Vector2(1, 2), new Vector2(2, 2), new Vector2(3, 2) });
            GetComponent<Code.EntityCode.Animation>().Play("Run");
            GetComponent<Code.EntityCode.Animation>().Add("Death",new Vector2[] {new Vector2(3,1), new Vector2(4,1)});
            #endregion
            #region GetComponent

            GetComponent<Code.EntityCode.Animation>().Playing();
            GetComponent<Transform>().pivot = new Vector2(GetComponent<Renderer>().tileCut.X / 2, GetComponent<Renderer>().tileCut.Y / 2);
            #endregion
            #region ActionComponent
            transform = GetComponent<Transform>();
            Position = startPosition;
            page.drawOn.Add(GetComponent<Renderer>(), transform);
            //DrawingSystem.Add(GetComponent<Renderer>(), transform);
            page.refreshSprite.Add(GetComponent<Code.EntityCode.Animation>());
            //GetComponent<ParticleSystem>().isActive = false;

            collision = GetComponent<Collision>();

            globalEntities.target = this;
            velocity = new Vector2(0, 0);
            #endregion
            Scale = new Vector2(-1,1);
            
        }
        public override void Start()
        {
            velocity = new Vector2(0, 0);
            Scale = new Vector2(-1, 1);
        }
        public override void Update(GameTime gameTime)
        {

            foreach (var Enti in collision.PlaceWith(transform.position, "Void"))
            {
                PortalNextLevel enti = (PortalNextLevel)Enti;
                portalNextLevel = enti.GetComponent<Transform>();
                Col = true;
                if(((int)(gameTime.TotalGameTime.TotalSeconds*(60*(velocity.X + velocity.Y)* (float)gameTime.ElapsedGameTime.TotalSeconds)) % 2) == tick)
                {
                    Rotation += 90;
                    tick = tick == 1 ? 0 : 1;

                }
            }
            if(Col)
            {
                var moveTod = MathForm.MoveToward(Position, portalNextLevel.position, 60);
                velocity = new Vector2(MathHelper.Lerp(velocity.X, moveTod.X, (float)gameTime.ElapsedGameTime.TotalSeconds*3), MathHelper.Lerp(velocity.Y, moveTod.Y, (float)gameTime.ElapsedGameTime.TotalSeconds*3));
                Position += velocity *(float)gameTime.ElapsedGameTime.TotalSeconds;
                //Debug.WriteLine(velocity);
                
                if ((MathHelper.Distance(Position.X,portalNextLevel.position.X) < 0.2f && MathHelper.Distance(Position.Y, portalNextLevel.position.Y)<0.2f && Math.Abs(velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds) < 0.2f && Math.Abs(velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds) < 0.2f) || Keyboard.GetState().IsKeyDown(Keys.Z) &&  !downKey)
                {
                    
                    PortalNextLevel enti = (PortalNextLevel)portalNextLevel.entitieGame;
                    Game1.GoToPage(enti.room);
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Z))
                {
                    downKey = false;
                }
            }
            else if (currentAnimation == (int)Animation.death)
            {
                velocity.Y += (gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);

                Position += new Vector2(0,velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds); 
            }
            else
            {
                #region Move
                if (Keyboard.GetState().IsKeyDown(Keys.Z) && pressedZ)
                {
                    velocity.Y += (gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    friction = frictionAir;
                }
                #region CollisionEnemie
                IEnumerable<EntitiesGame> collisionInteractive = collision.PlaceWith(new Vector2(Position.X, Position.Y), "Interactue");
                foreach (var interactive in collisionInteractive)
                {
                    Transform transform;
                    if (interactive is SpikeUpDown && ground)
                    {
                        
                        SpikeUpDown spikeUpDown = (SpikeUpDown)interactive;
                        transform = spikeUpDown.GetComponent<Transform>();
                        spikeUpDown.UpSpike();

                    }
                }


                IEnumerable<EntitiesGame> collisionEnemie = collision.PlaceWith(new Vector2(Position.X, Position.Y), "Enemies");
                foreach (var enemie in collisionEnemie)
                {
                    Transform transform;
                    Rectangle hitBox;
                    if (enemie is EnemieBird)
                    {
                        transform = enemie.GetComponent<Transform>();
                        hitBox = enemie.GetComponent<Collision>().hitBox;
                        EnemieBird bird = (EnemieBird)enemie;
                        if (transform.position.Y >= Position.Y)
                        {
                            velocity.Y = 32 * -Convert.ToByte(Keyboard.GetState().IsKeyDown(Keys.Z)) + jumpSpeed * 0.58f;
                            bird.Destroy();
                            break;
                        }
                        else if (transform.position.Y + hitBox.Height <= Position.Y || Position.X <= transform.position.X || Position.X >= transform.position.X + hitBox.Width)
                        {
                            //velocity.X *= -1f;
                            //velocity.Y = jumpSpeed * 0.5f;
                            bird.Attack();
                        }
                    }
                    else if (enemie is SpikeUpDown)
                    {
                        SpikeUpDown spike = (SpikeUpDown)enemie;
                    }
                    Destroy(1);
                    break;
                }
                #endregion

                velocity.X = MathHelper.Lerp(velocity.X,
                (Convert.ToSingle(Keyboard.GetState().IsKeyDown(Keys.Right)) - Convert.ToSingle(Keyboard.GetState().IsKeyDown(Keys.Left))) * 70,
                friction * 0.04f);
                if (Math.Abs(velocity.X) < 0.4f)
                {
                    Position = new Vector2((float)Math.Round(Position.X), Position.Y);
                    velocity.X = 0;

                }
                float newVelocityX = velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;//velocity
                if (collision.IsColliding(Position + new Vector2(newVelocityX, 0), "Ground"))
                {
                    Position = new Vector2((float)Math.Round(Position.X), Position.Y);
                    newVelocityX = (float)Math.Round(newVelocityX);
                    for (int i = 0; i < (Math.Abs(velocity.X) + 2); i++)
                    {
                        if (collision.IsColliding(Position + new Vector2(newVelocityX, 0), "Ground"))
                            newVelocityX -= Math.Sign(velocity.X);
                        else
                        {
                            velocity.X = 0;
                            break;
                        }
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Z))
                {
                    if (pressedZ && timer0 > 0)
                    {
                        velocity.Y = jumpSpeed;
                        timer0 = 0;
                        downKey = true;
                    }
                    pressedZ = false;
                }
                velocity.Y += (gravity * (float)gameTime.ElapsedGameTime.TotalSeconds); //Gravity
                ground = false;
                float newVelocityY = velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
                friction = frictionAir;
                if (collision.IsColliding(Position + new Vector2(newVelocityX, newVelocityY - 1), "Ground") && collision.IsColliding(Position + new Vector2(newVelocityX, newVelocityY), "Ground") && newVelocityY < 0)
                {
                    for (var i = 1; i <= 6; i++)
                    {
                        if (!collision.IsColliding(Position + new Vector2(newVelocityX + i, newVelocityY - 1), "Ground"))
                        {
                            newVelocityX += i;
                            break;
                        }
                        else if (!collision.IsColliding(Position + new Vector2(newVelocityX - i, newVelocityY - 1), "Ground"))
                        {
                            newVelocityX -= i;
                            break;
                        }
                    }

                }
                if (collision.IsColliding(Position + new Vector2(newVelocityX, newVelocityY), "Ground"))
                {
                    Position = new Vector2(Position.X, (float)Math.Round(Position.Y) + 0.5f);
                    newVelocityY = (float)Math.Round(newVelocityY);
                    for (int i = 0; i < (Math.Abs(velocity.Y) + 2); i++)
                    {
                        if (collision.IsColliding(Position + new Vector2(newVelocityX, newVelocityY), "Ground"))
                            newVelocityY -= Math.Sign(velocity.Y);
                        else
                        {
                            if (velocity.Y > 0)
                            {
                                timer0 = startTimer0;
                                ground = true;
                                friction = frictionGround;
                            }

                            velocity.Y = 0;

                            break;
                        }
                    }
                }

                #endregion
                if (Position.Y > 100f)
                    Destroy(1.5f);
                #region Animation Paper

                if (velocity.X != 0) beforeSpeedRight = 7 * Math.Sign(velocity.X);
                Scale = new Vector2(Scale.X - (beforeSpeedRight * (float)gameTime.ElapsedGameTime.TotalSeconds), Scale.Y);
                Scale = new Vector2(Math.Clamp(Scale.X, -1f, 1f), Scale.Y);
                #endregion
                #region Animation
                if (currentAnimation != (int)Animation.death)
                {
                    if (ground)
                    {
                        if (velocity.X != 0)
                        {
                            currentAnimation = (int)Animation.walk;
                        }
                        else if (velocity.X == 0 && velocity.Y != 0)
                        {
                            currentAnimation = (int)Animation.intermediateJump;
                        }
                        else
                        {
                            currentAnimation = (int)Animation.stop;
                        }
                    }
                    else
                    {
                        if (Math.Abs(velocity.Y) <= 0.1f)
                        {
                            currentAnimation = (int)Animation.intermediateJump;
                        }
                        else if (velocity.Y > 0.1f)
                        {
                            currentAnimation = (int)Animation.fall;
                        }
                        else
                        {
                            currentAnimation = (int)Animation.jump;
                        }

                    }
                }

                switch (currentAnimation)
                {
                    case (int)Animation.walk:
                        if (!GetComponent<Code.EntityCode.Animation>().isActive)
                        {
                            GetComponent<Code.EntityCode.Animation>().Playing();
                            GetComponent<Code.EntityCode.Animation>().currentFrame = 1;

                        }
                        GetComponent<Code.EntityCode.Animation>().Play("Run");

                        break;
                    case (int)Animation.stop:

                        GetComponent<Code.EntityCode.Animation>().Play("Run");
                        GetComponent<Code.EntityCode.Animation>().currentFrame = 0;
                        GetComponent<Code.EntityCode.Animation>().Stopping();
                        break;
                    case (int)Animation.jump:
                        GetComponent<Code.EntityCode.Animation>().Play("Jump");
                        GetComponent<Code.EntityCode.Animation>().Stopping();
                        GetComponent<Code.EntityCode.Animation>().currentFrame = 0;
                        break;
                    case (int)Animation.intermediateJump:
                        GetComponent<Code.EntityCode.Animation>().Play("Jump");
                        GetComponent<Code.EntityCode.Animation>().Stopping();
                        GetComponent<Code.EntityCode.Animation>().currentFrame = 1;
                        break;
                    case (int)Animation.fall:
                        GetComponent<Code.EntityCode.Animation>().Play("Jump");
                        GetComponent<Code.EntityCode.Animation>().Stopping();
                        GetComponent<Code.EntityCode.Animation>().currentFrame = 2;
                        break;
                    case (int)Animation.death:
                        if (!GetComponent<Code.EntityCode.Animation>().isActive)
                        {
                            GetComponent<Code.EntityCode.Animation>().Playing();

                        }
                        GetComponent<Code.EntityCode.Animation>().Play("Death");
                        break;
                }

                #endregion
                if (Keyboard.GetState().IsKeyUp(Keys.Z))
                {
                    if (!pressedZ && downKey && !ground && velocity.Y < 0)
                    {
                        velocity.Y *= 0.5f;
                        downKey = false;
                    }
                    pressedZ = true;

                }


                Position += new Vector2(newVelocityX, newVelocityY);

                Position = new Vector2((float)Math.Round(Position.X, 2), (float)Math.Round(Position.Y, 2));
                //Debug.WriteLine("Position: " + Position);
                //prevX = velocity.X;
                
                
                timer0 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                timer0 = Math.Clamp(timer0, 0, startTimer0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R) && !downKeyR)
            {
                velocity = Vector2.Zero;
                Position = startPosition;
                Scale = new Vector2(-1, 1);
                Rotation = 0f;
                downKeyR = true;
                currentAnimation = (int)Animation.stop;
                Col = false;
                EndGame.EndDraw();
            }
            if (Keyboard.GetState().IsKeyUp(Keys.R))
                downKeyR = false;
            if (Position.Y > 102)
            {
                EndGame.Update(gameTime);
                EndGame.StartDraw();
            }
               
        }
        void Destroy(float salto)
        {
            currentAnimation = (int)Animation.death;
            
            velocity.Y = jumpSpeed * salto;
        }

    }
}

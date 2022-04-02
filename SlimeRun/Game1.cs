using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Tutorial1.Code;
using Tutorial1.Content.Entities;
using Tutorial1.Content.Pages;
using Remplaze.Content.Pages;

using SlimeRun.Code;
using SlimeRun.Content.Pages;
using Remplaze.Content.Entities;
using SlimeRun.Content.Entities.Menu;

namespace Tutorial1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //Camera Dimension
        public int screenHeight;
        public int screenWidth;
        static private Dictionary<string,PageManager> pages = new Dictionary<string, PageManager>();
        static private PageManager currentPage;
        private Texture2D textureSquare;
        //texture
        public static Texture2D textureParticle;
        public static Texture2D button;
        public static Texture2D volumen;
        //public Camera camera;
        private int tick = 1;
        private static bool gotoRoom;
        private static string nextPage;
        public  static SpriteFont font;
        private float speed = 0.5f;


        public Game1()
        {
            

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


            this.TargetElapsedTime = TimeSpan.FromSeconds(1f/ 60f);
            
            this.IsFixedTimeStep = true;
            this._graphics.ApplyChanges();

            
        }

        protected override void Initialize()
        {
            
            screenHeight = _graphics.PreferredBackBufferHeight;
            screenWidth = _graphics.PreferredBackBufferWidth;
            Camera.SetScreen(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("FontB");
            textureSquare = Content.Load<Texture2D>("Square");
            Player.texture = Content.Load<Texture2D>("CAT");
            Player.textureDead = Content.Load<Texture2D>("DeadCAT");

            StartPage.texture = Content.Load<Texture2D>("Logo");
            EnemieDog.texture = Content.Load<Texture2D>("boss");
            TileMapLvl1.texture = Content.Load<Texture2D>("Ground");
            PortalNextLevel.texture = Content.Load<Texture2D>("PortalPoint");
            EnemieBird.texture = Content.Load<Texture2D>("BirdEnemie");
            HitGround.texture = Content.Load<Texture2D>("HitScene");
            ButtonStart.texture = Content.Load<Texture2D>("Button");
            button = Content.Load<Texture2D>("Button");
            SpikeUpDown.texture = HitGround.texture;
            volumen = Content.Load<Texture2D>("Volumen");

            TileMapLvl2.texture = TileMapLvl1.texture;
            TileMapLvl3.texture = TileMapLvl1.texture;
            TileMapLvl4.texture = TileMapLvl1.texture;
            TileMapLvl5.texture = TileMapLvl1.texture;
            
            
            textureParticle = textureSquare;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pages.Add("StartPage", new StartPage(this));
            pages.Add("Page1", new Page1(this));

            pages.Add("Page2", new Page2(this));

            pages.Add("Page3", new Page3(this));

            pages.Add("Page4", new Page4(this));

            pages.Add("Page5", new Page5(this));
            currentPage = pages["StartPage"];
            currentPage.Initialize();



            
            currentPage.EnterTree();

            foreach (var value in pages.Values)
                value.LoadContent(Content);

            currentPage.Start();
            GameLife.start();
        }

        protected override void Update(GameTime gameTime)
        {
            if (gotoRoom)
            {
                try
                {
                    GameLife.start();
                    currentPage = pages[nextPage];
                    pages[nextPage].Initialize();
                    pages[nextPage].EnterTree();
                    pages[nextPage].Start();
                    gotoRoom = false;
                }
                catch(System.Collections.Generic.KeyNotFoundException)
                {
                    Exit();
                }
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            currentPage.Update(gameTime);
            
            if(((int)(gameTime.TotalGameTime.TotalSeconds*speed) % 2) == tick)
            {
                GameLife.Update();
                tick = tick == 0 ? 1 : 0;
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(182, 205, 227, 255));

            AnimationPlay.RefreshSprite(gameTime);
            _spriteBatch.Begin(transformMatrix: Camera.Transform,blendState: BlendState.AlphaBlend,samplerState:SamplerState.PointClamp);
            BackGround.background(_spriteBatch);
            DrawingSystem.Draw(_spriteBatch);
            DrawingParticles.UpdateDrawingParticles(_spriteBatch);
            DrawingMap.DrawMap(_spriteBatch);
#if DEBUG
            DrawingShape.UpdateShape(textureSquare, _spriteBatch);
#endif
            currentPage.Draw(gameTime,_spriteBatch);
            
            EndGame.Draw(_spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        static public void GoToPage(string namePage)
        {
            gotoRoom = true;
            nextPage = namePage;
        }
    }
    
}

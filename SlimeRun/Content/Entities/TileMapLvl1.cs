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
    class TileMapLvl1 : EntitiesGame
    {

        private static int Name = 0;
        private static int GenerateName()
        {
            Name += 1;
            return Name;
        }
        
        static public Texture2D texture;
        static public CollisionMap collisionMap;
        static public Map tileMap;
        static private int number = 0;
        static private Renderer renderer;
        public TileMapLvl1(PageManager pageManager): base(pageManager)
        {
            name = "TileMap" + GenerateName();
        }

         public override void LoadContent(ContentManager Content)
         {
            renderer = new Renderer(this, texture, Color.White, new Vector2(5, 5), new Vector2(2, 1));
            tileMap = new Map(this,new Vector2(10, -61), renderer,
            new string[]{
            "           ZCC3        ZCC3       ZCCCCCCC3       ZC3",
            "           ZCC3        ZCC3       ZCCCCCCC3       ZC3",
            "           ZCC3        ZCC3       ZCCCCCCC3       ZC3",
            "           ZCC3        ZCC3       ZCCCCCCC3       ZC3",
            "           ZCC3        ZCC3       ZCCCCCCC3       ZC3",
            "           ZCC3        ZCC3       ZCCCCCCC3       ZC3",
            "           ZCC3        ZCC3       ZCCCCCCC3       ZC3",
            "           ZCC3        ZCC3       DUUUUUUUB       DUB",
            "           DUUB        DUUB                        $",
            "",                                                    
            "                                       $   <>   <>   <>   $       <XXXXXXXXXX>",
            "<XXXXXXX>       <XXXX>      <XXXX>     <XXX!~XXX!~XXX!~XXX>       ZCCCCCCCCCC3",
            "ZCCCCCCC3       DUUUUB      DUUUUB     ZCCCCCCCCCCCCCCCCCC3       DUUUUUUUUUUB",
            "ZCCCCCCC3                              ZCCCCCCCCCCCCCCCCCC3",
            "ZCCCCCCC3                              ZCCCCCCCCCCCCCCCCCC3",
            "ZCCCCCCC3                              DUUUUUUUUUUUUUUUUUUB",
            "ZCCCCCCC3",
            "ZCCCCCCC3",
            "ZCCCCCCC3",
            "ZCCCCCCC3",
            "ZCCCCCCC3",
            "ZCCCCCCC3"
            }, new Dictionary<char, Vector2>() { { '<', new Vector2(1, 1) }, { 'X', new Vector2(2, 1) }, { '>', new Vector2(3, 1) }, {'~' ,new Vector2(4, 1) },
                                                {'Z',new Vector2(1,2) } , {'C', new Vector2(2,2) },{'3',new Vector2(3,2)}, {'!' ,new Vector2(4, 2) },
                                                {'D',new Vector2(1,3) },{'U',new Vector2(2,3) }, {'B', new Vector2(3,3) },{ '@', new Vector2(4, 3)}
                                                ,{'$',new Vector2(1,4) },{ '#',new Vector2(4, 4) },
                                                {'%',new Vector2(1,5) },{'R', new Vector2(2,5) }, {'^', new Vector2(3,5) }});

            collisionMap = new CollisionMap(this, tileMap);

         }

        public override void EnterTree()
        {
            AddComponent(renderer);
            AddComponent(tileMap);
            AddComponent(collisionMap);

            if (tileMap != null)
            {
                page.drawMaps.Add(tileMap);
                page.groupMap["Ground"].Add(collisionMap);
#if DEBUG
                page.rectanglesMaps.Add(tileMap, collisionMap.collision);
#endif
            }
        }
        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Start()
        {
            
        }
    }
}

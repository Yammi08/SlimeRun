using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;

namespace Remplaze.Content.Entities
{
    class TileMapLvl5 : EntitiesGame
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

        public TileMapLvl5(PageManager pageManager) : base(pageManager)
        {
            name = "TileMap" + GenerateName();
        }

        public override void LoadContent(ContentManager Content)
        {

            renderer = new Renderer(this, texture, Color.White, new Vector2(5, 5), new Vector2(2, 1));
            tileMap = new Map(this, new Vector2(10, -124), renderer,
            new string[]{
            "                         ZCCCCCCCCCCCCCCCCCCCCCCC3",
            "                         ZCCCCCCCCCCCCCCCCCCCCCCC3",
            "                         ZCCCCCCCCCCCCCCCCCCCCCCC3",
            "                         ZCCCCCCCCCCCCCCCCCCCCCCC3                                               %R^",
            "                         ZCCCCCCCCCCCCCCCCCCCCCCC3                                          $     $        %RRRRRRRRRRRRRR^",
            "                         ZCCCCCCCCCCCCCCCCCCCCCCC3                                     $    $     $",
            "                         ZCCCCCCCCCCCCCCCCCCCCCCC3                                   %RRRRRRRRRRRRR^",
            "                         DUUUUUUUUUUUUUUUUUUUUUUUB                           %^        ",
            "                                                                       %^              ",
            "                          <>     <>     <>     <>      <X>       %^                    ",
            "                %RRRRRRRR^Z3     Z3     Z3     Z3      ZC3",
            "       $                  Z3     Z3     Z3     Z3      ZC3",
            "             $            Z3     Z3     Z3     Z3      ZC3",
            "         $                DB     DB     DB     DB      DUB",
            "     $",
            "$  ",
            "   ",
            "",
            "",
            "",
            ""
            }, new Dictionary<char, Vector2>() { { '<', new Vector2(1, 1) }, { 'X', new Vector2(2, 1) }, { '>', new Vector2(3, 1) }, {'~' ,new Vector2(4, 1) },
                                                {'Z',new Vector2(1,2) } , {'C', new Vector2(2,2) },{'3',new Vector2(3,2)}, {'!' ,new Vector2(4, 2) },
                                                {'D',new Vector2(1,3) },{'U',new Vector2(2,3) }, {'B', new Vector2(3,3) },{ '@', new Vector2(4, 3)}
                                                ,{'$',new Vector2(1,4) },{ '#',new Vector2(4, 4) },
                                                {'%',new Vector2(1,5) },{'R', new Vector2(2,5) }, {'^', new Vector2(3,5) }});

            collisionMap = new CollisionMap(this, tileMap);

            /*if (number == 0)
            {
                
                AddComponent(renderer);
                AddComponent(tileMap);
                AddComponent(collisionMap);

                page.rectanglesMaps.Add(tileMap, collisionMap.collision);
                page.drawMaps.Add(tileMap);
                page.groupMap["Ground"].Add(collisionMap);
                number = 1;
            }*/


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

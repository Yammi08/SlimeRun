using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial1.Code.EntityCode;
using System.Diagnostics;
namespace Tutorial1.Code
{
    #region Map
    public class Map : Component
    {
        Vector2 size;
        Renderer obstacle;
        Dictionary<Vector2, Rectangle> map = new Dictionary<Vector2, Rectangle>();
        public readonly Vector2 offset;
        public Vector2 GetSize()
        {
            return size;
        }
        public Renderer GetRenderer()
        {
            return obstacle;
        }
        //get map
        public Dictionary<Vector2, Rectangle> GetMap()
        {
            return map;
        }
        public Map(EntitiesGame other,Vector2 position, Renderer obstacle, string[] map,Dictionary<char,Vector2> Convert) : base(other)
        {
            int NumberX = 0;
            offset = position;

            List<Rectangle> rectangles = new List<Rectangle>();
            for (int length = 0;length < map.Length;length++)
            {
                if (NumberX < map[length].Length)
                {
                    NumberX = map[length].Length;
                }
            }
            size = new Vector2((NumberX+1)*8, (map.Length+1)*8);
            this.obstacle = obstacle;
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    foreach (var item in Convert)
                    {
                        if (map[y][x] == item.Key)
                        {
                            this.map.Add(new Vector2((x * obstacle.tileCut.X), (y * obstacle.tileCut.Y))+position,
                                new Rectangle(((int)obstacle.tileCut.X * (int)item.Value.X) - (int)obstacle.tileCut.X,
                                ((int)obstacle.tileCut.Y * (int)item.Value.Y) - (int)obstacle.tileCut.Y,
                                ((int)obstacle.tileCut.X),
                                ((int)obstacle.tileCut.Y)));
                            break;
                        }
                    }
                }
            }
        }
    }
    #endregion
    #region CollisionMap

    public class CollisionMap : Component
    {
        public Map map;
        public List<Rectangle> collision = new List<Rectangle>();
        public CollisionMap(EntitiesGame other, Map map) : base(other)
        {
            this.map = map;
            int collisionBit = 0;
            Vector2 prevItem = new Vector2(0, 0);
            Rectangle preview = new Rectangle(0, 0, 0, 0);
            bool findData = false;
            bool calculate = false;
            foreach (Vector2 item in map.GetMap().Keys)
            {

                if (item.X - prevItem.X != map.GetRenderer().tileCut.X || item.Y != prevItem.Y && Math.Abs(item.Y - prevItem.Y) != map.GetRenderer().tileCut.Y && Math.Abs(item.Y - prevItem.Y) != 0)
                {
                    #region Comend
                    findData = false;
                    for (int x = 0; x < collision.Count; x++)
                    {
                        if (collision[x].X == item.X && Math.Abs(item.Y - prevItem.Y) == map.GetRenderer().tileCut.Y)
                        {
                            collisionBit = x;
                            findData = true;
                            break;
                        }
                    }

                    if (findData)
                    {
                        collision.Add(preview);
                        if (collision[collisionBit].X == preview.X && collision[collisionBit].Y == Math.Abs(item.Y - prevItem.Y))
                        {

                            
                            collision[collisionBit] = new Rectangle(collision[collisionBit].X, collision[collisionBit].Y, preview.Width, collision[collisionBit].Height + preview.Height);

                        }
                        
                        
                        preview = new Rectangle((int)item.X, (int)item.Y, (int)map.GetRenderer().tileCut.X, (int)map.GetRenderer().tileCut.Y);
                    }
                    else
                    {
                        #endregion
                        if (!calculate)
                        {
                            preview = new Rectangle((int)item.X, (int)item.Y, (int)map.GetRenderer().tileCut.X, (int)map.GetRenderer().tileCut.Y);
                            calculate = true;
                        }
                        else
                        {
                            //preview = new Rectangle(preview.X,preview.Y,preview.Width- (int)map.GetRenderer().tileCut.X,preview.Height);
                            collision.Add(preview);
                            
                            preview = new Rectangle((int)item.X, (int)item.Y, (int)map.GetRenderer().tileCut.X, (int)map.GetRenderer().tileCut.Y);
                        }
                    }

                }
                else
                {
                    if (!findData || collision[collisionBit].Width >= preview.Width)
                    {
                        preview = new Rectangle(preview.X, preview.Y, preview.Width + (int)map.GetRenderer().tileCut.X, preview.Height);//(int)(item.X + map.GetRenderer().tileCut.X) - preview.X, (int)(item.Y + map.GetRenderer().tileCut.Y) - preview.Y);
                    }

                }
                prevItem = item;

            }
            collision.Add(preview);
            for (int i = 0; i < collision.Count; i++)
            {
                collision[i] = new Rectangle(collision[i].X /*+ (int)map.GetRenderer().tileCut.X + 2*/, collision[i].Y/*+(int)map.GetRenderer().tileCut.Y+2*/, collision[i].Width, collision[i].Height);


            }
            
        }
        
    }
    #endregion
}

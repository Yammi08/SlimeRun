using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Remplaze.Content.Entities
{
    static class GameLife
    {
        public const int sizeX = 10;
        public const int sizeY = 10;
        public static byte[,] scene;
        public static List<byte[,]> maps = new List<byte[,]>();
        private static Random random;
        public static void start()
        {
            random = new Random();
            //Background.start.Add(new Vector2(Camera.Transform.Translation.X, Camera.Transform.Translation.Y));
            maps.Add(
            new byte[sizeY, sizeX]
            { 
            {1,1,1,0,0,1,0,0,1,0},
            {1,0,0,0,0,1,0,0,0,1},
            {0,0,0,1,0,1,0,1,0,0},
            {0,0,0,1,0,1,0,1,0,0},
            {0,1,0,1,0,1,0,1,0,1},
            {0,1,0,1,0,1,0,1,0,1},
            {0,0,0,0,0,0,1,0,1,0},
            {0,1,0,1,0,1,0,1,0,1},
            {0,1,0,1,0,1,0,1,0,1},
            {0,1,0,1,0,1,0,1,0,1}
            });
            maps.Add(
            new byte[sizeY, sizeX]
            {
            {1,1,1,1,1,1,1,1,1,0},
            {0,1,0,0,1,0,1,0,0,0},
            {0,1,0,0,1,0,1,1,1,0},
            {1,1,1,1,1,1,0,1,1,0},
            {1,0,0,0,1,0,0,0,0,0},
            {1,1,1,0,1,0,0,0,0,0},
            {1,1,1,1,1,1,0,1,1,0},
            {1,0,0,0,1,0,0,0,0,1},
            {1,1,1,0,1,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0}
            });
            maps.Add(
            new byte[sizeY, sizeX]
            {
            {1,0,0,0,0,0,0,0,0,1},
            {0,0,1,1,0,1,1,0,0,0},
            {0,1,0,0,1,0,0,1,0,1},
            {0,1,0,0,1,0,0,1,0,1},
            {0,1,0,1,0,1,0,1,0,0},
            {1,0,1,0,0,0,1,0,0,0},
            {0,0,0,1,0,1,0,0,0,1},
            {0,0,0,0,1,0,0,0,0,0},
            {1,0,0,1,0,1,0,0,1,0},
            {1,1,0,0,1,0,0,1,1,0}
            });
            scene = maps[random.Next(0, maps.Count - 1)];

        }
        public static void Update()
        {
            byte[,] copyScene = (byte[,])scene.Clone();
            for(int y =0; y< sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    //parents
                    int xLeft = ((x - 1));
                    int xRigth = ((x + 1) % sizeX);
                    int xCenter = ((x) % sizeX);
                    int yDown = ((y + 1) % sizeY);
                    int yCenter = ((y) % sizeY);
                    int yUp = ((y - 1) % sizeY);
                    if (xLeft == -1)
                        xLeft = sizeX-1;
                    if (yUp == -1)
                        yUp = sizeY-1;

                    int lifes = scene[yUp, xLeft] + scene[yUp, xCenter] + scene[yUp, xRigth] +
                                scene[yCenter, xLeft] + scene[yCenter, xRigth] +
                                scene[yDown, xLeft] + scene[yDown, xCenter] + scene[yDown, xRigth];
                    if (scene[y, x] == 1 && (lifes < 2 || lifes > 3))
                    {
                        copyScene[y,x] = 0;
                    }
                    else if(scene[y,x] == 0 && lifes == 3)
                    {
                        copyScene[y, x] = 1;
                    }
                }
            }
            scene = copyScene;
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial1.Code;
using Tutorial1.Code.EntityCode;
using Tutorial1.Content.Entities;

namespace Remplaze.Code
{
    static class globalEntities
    {
        public static Player target;

        public static Vector2 PaperWatch(Vector2 scale, Vector2 position,float speed)
        {
            Vector2 result = scale + (new Vector2(speed, 0)* Math.Sign(position.X - target.Position.X));
            result.X = Math.Clamp(result.X,-1,1);
            return result;
        }
        public static Vector2 Raycast(Transform start,float dir,float length,string name)
        {
            //Vector2 end = start + (new Vector2((float)Math.Cos(dir), (float)Math.Sin(dir))*length);
            for(int i = 0; i< start.entitieGame.page.group[name].Count; i++)
            {
                Transform transform = start.entitieGame.page.group[name][i].entitieGame.GetComponent<Transform>();
                Collision collision = start.entitieGame.page.group[name][i].entitieGame.GetComponent<Collision>();
                Vector2 localPosition = start.position - (transform.position + collision.hitBox.Center.ToVector2());
                float distance = (float)Math.Sqrt(transform.position.X* transform.position.X + transform.position.Y* transform.position.Y);
                Vector2 pos = localPosition * distance;
                return pos;
                /*Vector2 pointA1 = transform.position;
                Vector2 pointA2 = transform.position + new Vector2(collision.hitBox.Width, collision.hitBox.Height);

                Vector2 pointB1 = start.position;
                Vector2 pointB2 = start.position + (new Vector2((float)Math.Cos(dir), (float)Math.Sin(dir)) * length);

                float den = (pointA1.X-pointA2.X)*(pointB1.Y-pointB2.Y)- (pointA1.Y-pointA2.Y)*(pointB1.X-pointB2.X);
                if(den == 0)
                {
                    continue;
                }
                float t = ((pointA1.X - pointB1.X) * (pointB1.X - pointB2.X) - (pointA1.Y - pointB1.Y) * (pointB1.X - pointB2.X))/den;
                float u = -((pointA1.X - pointA2.X) * (pointA1.Y - pointB1.Y) - (pointA1.Y - pointA2.Y) * (pointA1.X - pointB1.X))/den;*/
            }
            return new Vector2();
        }
    }
    
}

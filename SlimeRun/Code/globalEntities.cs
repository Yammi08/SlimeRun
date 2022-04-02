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
            for(int i = 0; i< start.entitieGame.page.group[name].Count; i++)
            {
                Transform transform = start.entitieGame.page.group[name][i].entitieGame.GetComponent<Transform>();
                Collision collision = start.entitieGame.page.group[name][i].entitieGame.GetComponent<Collision>();
                Vector2 localPosition = start.position - (transform.position + collision.hitBox.Center.ToVector2());
                float distance = (float)Math.Sqrt(transform.position.X* transform.position.X + transform.position.Y* transform.position.Y);
                Vector2 pos = localPosition * distance;
                return pos;
            }
            return new Vector2();
        }
    }
    
}

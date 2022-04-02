using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Remplaze.Content.Entities
{
    class MathForm
    {
        static public float SinEffect(float x, float amplitude)
        {
            float y = (amplitude * (float)Math.Sin(Math.PI * x / amplitude)) + amplitude;
            return y;
        }
        static public Vector2 Interpolation(Vector2 A, Vector2 B, float Speed)
        {
            var result = A + (B - A) * Speed;
            return result;
        }
        static public Vector3 Interpolation(Vector3 first, Vector3 next, float timeLaps)
        {
            return new Vector3(
                    MathHelper.Lerp(first.X, next.X, timeLaps)
                   , MathHelper.Lerp(first.Y, next.Y, timeLaps)
                   , MathHelper.Lerp(first.Z, next.Z, timeLaps));
        }
        static public Vector2 MoveToward(Vector2 A, Vector2 B, float Speed)
        {
            var positionLocal = (B - A);
            var result = new Vector2(1 / Math.Abs(positionLocal.X), 1 / Math.Abs(positionLocal.Y)) * positionLocal * Speed;
            if (float.IsInfinity(Math.Abs(result.X)) || float.IsNaN(result.X))
                result.X = 0;
            if (float.IsInfinity(Math.Abs(result.Y)) || float.IsNaN(result.Y))
                result.Y =  0;            
            return result;
        }
        static public Color mix(Color first, Color next, float timeLaps)
        {
            return new Color(
                    (int)MathHelper.Lerp(first.R, next.R, timeLaps)
                   , (int)MathHelper.Lerp(first.G, next.G, timeLaps)
                   , (int)MathHelper.Lerp(first.B, next.B, timeLaps)
                   , (int)MathHelper.Lerp(first.A, next.A, timeLaps));
        }
    }
    
}

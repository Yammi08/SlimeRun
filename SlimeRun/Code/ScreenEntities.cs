using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tutorial1.Code.EntityCode;

namespace Tutorial1.Code
{
     public class ScreenEntities
    {
        private Dictionary<Type, List<EntitiesGame>> screenEntities = new Dictionary<Type,List<EntitiesGame>>();
        private Dictionary<Type, List<EntitiesGame>> outEntities = new Dictionary<Type, List<EntitiesGame>>();
        public void LoadContent(ContentManager Content)
        {
            foreach (List<EntitiesGame> Entities in screenEntities.Values)
            {
                foreach(EntitiesGame entity in Entities)
                    entity.LoadContent(Content);
            }
        }
        public void EnterTree()
        {
            foreach (List<EntitiesGame> Entities in screenEntities.Values)
            {
                foreach (EntitiesGame entity in Entities)
                    entity.EnterTree();
            }
        }
        public void Start()
        {
            foreach (List<EntitiesGame> Entities in screenEntities.Values)
            {
                foreach (EntitiesGame entity in Entities)
                    entity.Start();
            }
        }
        public void Update(GameTime gameTime)
        {
            foreach (List<EntitiesGame> Entities in screenEntities.Values)
            {
                foreach (EntitiesGame entity in Entities)
                    entity.Update(gameTime);
            }
        }
        public void Draw()
        {

        }
        public void Instance(EntitiesGame entitie)
        {
            try
            {
                for (int enti = 0; enti < outEntities[entitie.GetType()].Count; enti++)
                {
                    if (outEntities[entitie.GetType()][enti].GetId() == entitie.GetId())
                    {
                        outEntities[entitie.GetType()].Remove(entitie);
                        screenEntities[entitie.GetType()].Add(entitie);
                        break;
                    }
                }
            }
            catch (KeyNotFoundException)
            {
                try
                {
                    screenEntities.Add(entitie.GetType(), new List<EntitiesGame>());
                    screenEntities[entitie.GetType()].Add(entitie);
                }
                catch(ArgumentException)
                {
                    screenEntities[entitie.GetType()].Add(entitie);
                }
            }
            catch(ArgumentNullException)
            {
                screenEntities[entitie.GetType()].Add(entitie);
            }
        }
        public void Remove(EntitiesGame entitie)
        {
            try
            {
                for (int enti = 0; enti < screenEntities[entitie.GetType()].Count; enti++)
                {
                    if (screenEntities[entitie.GetType()][enti].GetId() == entitie.GetId())
                    {
                        screenEntities[entitie.GetType()].Remove(entitie);
                        outEntities[entitie.GetType()].Add(entitie);
                        break;
                    }
                }
            }
            catch (KeyNotFoundException)
            {
                outEntities.Add(entitie.GetType(), new List<EntitiesGame>());
                outEntities[entitie.GetType()].Add(entitie);
            }
            catch (ArgumentNullException)
            {
                outEntities[entitie.GetType()].Add(entitie);
            }

        }
    }
}

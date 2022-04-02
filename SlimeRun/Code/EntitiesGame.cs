using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;

namespace Tutorial1.Code.EntityCode
{
    public abstract class EntitiesGame
    {
        //groupCol
        public static Dictionary<string, List<Rectangle>> group = new Dictionary<string, List<Rectangle>>();

        public bool active;
        public PageManager page;
        public List<Component> components = new List<Component>();

        int id;
        public string name;
       public EntitiesGame(PageManager pageManager)
       {
            page = pageManager;
            id = GenerateId();
            active = true;
       }

        public abstract void LoadContent(ContentManager Content);
        public abstract void EnterTree();
        public abstract void Start();
        public abstract void Update(GameTime gameTime);
        public int GetId()
        {
            return id;
        }


        static int NextiD = 0;
        private static int GenerateId()
        {
            NextiD += 1;
            return NextiD;
        }

        public void RemoveComponent(Component component)
        {
            this.components.Remove(component);
        }
        public void AddComponent(Component component)
        {
            this.components.Add(component);
        }
        public T GetComponent<T>() where T : Component
        {
            var component = components.FirstOrDefault(c => c is T);
            if(component != null)
            {
                return (T)component;
            }
            return null;
        }

        public List<Component> GetComponents()
        {
            return components;
        }
        

    }
}

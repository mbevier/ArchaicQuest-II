using Artemis;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace WhoPK.GameLogic.Core
{
    public class World
    {
        EntityWorld world;

        public void Start()
        {
            // create ecs environment.
            var world = new EntityWorld();
        }

        public void Update()
        {
            // process all dependent systems.
            world.Update();
        }
    }

}

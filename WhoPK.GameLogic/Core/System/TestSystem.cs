using Artemis;
using Artemis.Manager;
using Artemis.System;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Core.Component;

namespace WhoPK.GameLogic.Core.System
{
    namespace WhoPK.GameLogic.Core.System
    {
        [Artemis.Attributes.ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Update, Layer = 1)]
        public class TestSystem : EntityProcessingSystem
        {
            public TestSystem() : base(Aspect.All(typeof(PlayerInputComponent)))
            {
            }

            public override void Process(Entity entity)
            {
                var commandStack = entity.GetComponent<PlayerInputComponent>().commands;
            }
        }
    }
}

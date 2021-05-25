using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.HierarchalTaskNetworkPlanner;
using WhoPK.HierarchalTaskNetworkPlanner.Contexts;
using WhoPK.HierarchalTaskNetworkPlanner.Debug;
using WhoPK.HierarchalTaskNetworkPlanner.Factory;

namespace WhoPK.GameLogic.Generator
{

    public class AreaContext : BaseContext
    {
        public override List<string> MTRDebug { get; set; } = null;
        public override List<string> LastMTRDebug { get; set; } = null;
        public override bool DebugMTR { get; } = false;
        public override Queue<IBaseDecompositionLogEntry> DecompositionLog { get; set; } = null;
        public override bool LogDecomposition { get; } = false;

        public override IFactory Factory { get; set; } = new DefaultFactory();
        private byte[] _worldState = new byte[Enum.GetValues(typeof(AreaState)).Length];
        public override byte[] WorldState => _worldState;

        // Custom state
        public bool Done { get; set; } = false;

        public override void Init()
        {
            base.Init();

            //TODO: Custom init of state
        }

        public bool HasState(AreaState state, bool value)
        {
            return HasState((int)state, (byte)(value ? 1 : 0));
        }

        public bool HasState(AreaState state)
        {
            return HasState((int)state, 1);
        }

        public void SetState(AreaState state, bool value, EffectType type)
        {
            SetState((int)state, (byte)(value ? 1 : 0), true, type);
        }
    }
}

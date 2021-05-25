using System.Collections.Generic;
using WhoPK.HierarchalTaskNetworkPlanner.Compounds;

namespace WhoPK.HierarchalTaskNetworkPlanner
{
    public interface IDomain
    {
        TaskRoot Root { get; }
        void Add(ICompoundTask parent, ITask subtask);
        void Add(ICompoundTask parent, Slot slot);
    }
}
